﻿namespace ECommerceRestApi.Core.Utilities.Result
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }

    }
}
