using System.Net;

namespace Application.ModelCaptureException
{
    public class CaptureExceptions:Exception
    {
        public  HttpStatusCode statusCodeCapture {  get;}
        public Object errorsCapure { get; }
        public CaptureExceptions(HttpStatusCode httpStatusCode, 
              Object errors)
        {
            statusCodeCapture = httpStatusCode;
            errorsCapure = errors;
        }
    }
}
