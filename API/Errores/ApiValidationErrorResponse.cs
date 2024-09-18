namespace API.Errores
{
    public class ApiValidationErrorResponse: ApiErrorResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
                 
        }
        public IEnumerable<string> Errores { get; set; }
    }
}
