using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;

namespace SCB_API.Formatters
{
    //public class CsvOutputFormatter : TextOutputFormatter
    //{
    //    public CsvOutputFormatter()
    //    {
    //        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
    //        SupportedEncodings.Add(Encoding.UTF8);
    //        SupportedEncodings.Add(Encoding.Unicode);
    //    }

    //    protected override bool CanWriteType(Type? type)
    //    {
    //        if (typeof(DependenciaDto).IsAssignableFrom(type) ||
    //       typeof(IEnumerable<DependenciaDto>).IsAssignableFrom(type))
    //        {
    //            return base.CanWriteType(type);
    //        }
    //        return false;
    //    }

    //    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    //    {
    //        var response = context.HttpContext.Response;
    //        var buffer = new StringBuilder();
    //        if (context.Object is IEnumerable<DependenciaDto>)
    //        {
    //            foreach (var dependencia in (IEnumerable<DependenciaDto>)context.Object)
    //            {
    //                FormatCsv(buffer, dependencia);
    //            }
    //        }
    //        else
    //        {
    //            FormatCsv(buffer, (DependenciaDto)context.Object);
    //        }
    //         await response.WriteAsync(buffer.ToString());
    //    }
    //    private static void FormatCsv(StringBuilder buffer, DependenciaDto dependencia)
    //    {
    //        buffer.AppendLine($"{dependencia.PadreId},\"{dependencia.HijoId},\"{dependencia.FechaRegistro},\"{dependencia.FechaModificacion}\"");
    //    }
    //}

}
