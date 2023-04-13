namespace Shared.RequestFeatures.Registro
{
    public class DepartamentoParameters : RequestParameters
    {
        public DepartamentoParameters() => OrderBy = "Nombre";
        public int? Except { get; set; }
        public int? TipoDepartamentoId { get; set; }
    }
}
