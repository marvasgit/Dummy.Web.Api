namespace Dummy.Web.Api.Models
{
    public class ErrorDetail
    {
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }
        public ErrorDetail(string message)
        {
            Message = message;
        }
        public ErrorDetail(string message, string additionalInfo)
        {
            Message = message;
            AdditionalInfo = additionalInfo;
        }
    }
}