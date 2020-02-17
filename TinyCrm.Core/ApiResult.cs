namespace TinyCrm.Core
{
    public class ApiResult<T> where T : class
    {
        public StatusCode ErrorCode { get; set; }
        
        public string ErrorText { get; set; }

        public T Data { get; set; }

        public ApiResult()
        {

        }        
        
        public ApiResult(StatusCode errorCode, string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }

    }
}
