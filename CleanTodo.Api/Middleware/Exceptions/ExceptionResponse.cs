﻿namespace CleanTodo.Api.Middleware.Exceptions
{
    public sealed class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
