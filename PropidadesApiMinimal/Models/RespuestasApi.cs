using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace PropidadesApiMinimal.Models
{
    public class RespuestasApi
    {

        public RespuestasApi()
        {
            Errores = new List<string>();
        }
        public bool Success { get; set; }

        public Object Resultado { get; set; }

        public HttpStatusCode  statusCode { get; set; }

        public List<string> Errores { get; set; }
    }
}
