using Revit.MediatR.Models;
using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// Чистая логика <see cref="Response{TResult}"/>, не затрагивающая Revit API — можно тестировать без установленного Revit.
/// </summary>
public class ResponseTests
{
    [Fact]
    public void Default_HasNoErrorAndEmptyMessage()
    {
        var response = new Response<bool>();

        Assert.False(response.HasError);
        Assert.Equal(string.Empty, response.Message);
        Assert.Null(response.Exception);
        Assert.False(response.Result);
    }

    [Fact]
    public void Result_CanBeSetAndRead()
    {
        var response = new Response<int> { Result = 42 };

        Assert.Equal(42, response.Result);
    }

    [Fact]
    public void HasError_CanCarryExceptionAndMessage()
    {
        var exception = new InvalidOperationException("boom");

        var response = new Response<bool>
        {
            HasError = true,
            Message = "boom",
            Exception = exception,
        };

        Assert.True(response.HasError);
        Assert.Equal("boom", response.Message);
        Assert.Same(exception, response.Exception);
    }
}
