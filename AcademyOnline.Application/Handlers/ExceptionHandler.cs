using System;
using System.Net;

namespace AcademyOnline.Application.Handlers
{
    public class ExceptionHandler : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errores { get; }

        public ExceptionHandler(HttpStatusCode code, object errores = null)
        {
            Code = code;
            Errores = errores;
        }
    }
}
