namespace LawyerBasket.Gateway.Api.Dtos
{
    public class ApiResult<T>
    {
        public T? Data { get; set; }
        public string[]? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFail { get; set; }
        public int Status { get; set; }
    }
}

