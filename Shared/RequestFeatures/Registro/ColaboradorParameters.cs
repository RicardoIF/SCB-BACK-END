namespace Shared.RequestFeatures.Registro
{
    public class ColaboradorParameters : RequestParameters
    {
        public ColaboradorParameters() => OrderBy = "Nombre";
        public int? Except { get; set; }
    }
}
