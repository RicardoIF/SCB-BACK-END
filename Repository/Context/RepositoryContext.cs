using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Repository.Configuration;
using Entities.Models;
using Repository.Helper.AuditTrail;
using Contracts.Interfaces;
using Entities.Models.Base;

namespace Repository.Context
{
    public class RepositoryContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region SCB

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<RoomStatus> RoomStatus { get; set; }





        

        #endregion

        public RepositoryContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected RepositoryContext() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuditConfig());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new RoomStatusConfiguration());

            

        }
        public override int SaveChanges()
        {
            AddAuditLogs("1059560");
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<BaseModel> entry in ChangeTracker.Entries<BaseModel>())
            {
                if (entry.State == EntityState.Deleted) // If the entity was updated
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Estatus = false;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.Estatus = true;
                }
            }

            var request = _httpContextAccessor.HttpContext.Request;
            request.Headers.TryGetValue("claims_nameId", out var userId);
            request.Headers.TryGetValue("claims_name", out var name);

            var user = userId != Microsoft.Extensions.Primitives.StringValues.Empty && name != Microsoft.Extensions.Primitives.StringValues.Empty
                ? new { id = Int32.Parse(userId.ToString()), userName = name.ToString() }
                : userId != Microsoft.Extensions.Primitives.StringValues.Empty && name == Microsoft.Extensions.Primitives.StringValues.Empty
                ? new { id = Int32.Parse(userId.ToString()), userName = "Usuario" }
                : new { id = 0, userName = "Sin Usuario" };

            AddAuditLogs(JsonConvert.SerializeObject(user));
            return await base.SaveChangesAsync();
        }
        public void AddAuditLogs(string user)
        {
            ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = new List<AuditEntry>();
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var auditEntry = new AuditEntry(entry, user);
                auditEntries.Add(auditEntry);
            }

            if (auditEntries.Any())
            {
                var logs = auditEntries.Select(x => x.ToAudit());
               // Audit.AddRange(logs);
            }
        }
        public async Task<string> GetUserBySSO()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjMiLCJnaXZlbl9uYW1lIjoiQWRtaW4gQWRtaW4iLCJ1bmlxdWVfbmFtZSI6IkFkbWluaXN0cmFkb3IiLCJuYW1laWQiOiIzIiwiQXBwbGljYXRpb25JZCI6IjEwMTkiLCJzdWIiOiJhZG1pbkBlY29ub21pYS5nb2IuZG8iLCJqdGkiOiIwOTY1OGJjYi04ZDIyLTRjYjctOWQ5MS00OGJiZjM5MzhhZTgiLCJleHAiOjE2NjAzMTYxNjYsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAvIn0.aAW-EKCS53-QOq9vVFbdkWg1GY6eSBEVxMjyEdXVgKc");
            client.BaseAddress = new Uri(" https://localhost:5001/Api/");
            var response = await client.PostAsync("ApplicationAuthorize", content);
            var answer = await response.Content.ReadAsStringAsync();
            return answer;
        }
    }
}
