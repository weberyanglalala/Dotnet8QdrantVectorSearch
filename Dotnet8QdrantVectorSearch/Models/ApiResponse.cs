namespace Dotnet8QdrantVectorSearch.Models;

using System.Collections.Generic;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }

    // Private constructor to force usage of static factory methods
    private ApiResponse()
    {
        Errors = new List<string>();
    }

    // Static method for creating a successful response
    public static ApiResponse<T> Success(T data, string message = "Operation successful", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }

    // Static method for creating a failure response
    public static ApiResponse<T> Failure(string message, int statusCode = 400, List<string> errors = null)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
            Data = default,
            Errors = errors ?? new List<string>()
        };
    }
}