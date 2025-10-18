using System.Net;
using System.Text.Json.Serialization;

namespace LawyerBasket.ProfileService.Application
{
  public class ApiResult<T>
  {
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count() == 0;
    public bool IsFail => !IsSuccess;
    public HttpStatusCode Status { get; set; }

    public static ApiResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
      return new ApiResult<T> { Data = data, Status = status };
    }
    public static ApiResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
      return new ApiResult<T>
      {
        ErrorMessage = errorMessage,
        Status = status
      };
    }
    public static ApiResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
      return new ApiResult<T>
      {
        ErrorMessage = new List<string> { errorMessage },
        Status = status
      };
    }
  }
  public class ApiResult
  {
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count() == 0;
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }


    public static ApiResult Success(HttpStatusCode status = HttpStatusCode.OK)
    {
      return new ApiResult { Status = status };
    }

    public static ApiResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
      return new ApiResult()
      {
        ErrorMessage = errorMessage,
        Status = status
      };
    }

    public static ApiResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
      return new ApiResult()
      {
        ErrorMessage = new List<string> { errorMessage },
        Status = status
      };
    }
  }
}
