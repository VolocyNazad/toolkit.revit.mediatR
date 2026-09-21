using Revit.MediatR.Models;
using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// Pure logic of <see cref="Response{TResult}"/> that does not touch the Revit API — can be tested without Revit installed.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names use underscores to separate scenario and expected result.")]
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
