using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.AppointmentsDto;

namespace SCB.Mappers
{
    public class AppointmentMapper : Profile
    {
        public AppointmentMapper() 
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateUpdateAppointment>().ReverseMap();
        }
    }
}
