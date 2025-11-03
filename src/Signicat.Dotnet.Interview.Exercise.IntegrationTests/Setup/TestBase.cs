using Xunit.Abstractions;

namespace Signicat.Dotnet.Interview.Exercise.IntegrationTests.Setup;

public class TestBase
{
    private readonly ITestOutputHelper _output;
    
    protected TestBase(ITestOutputHelper output)
    {
        _output = output;
    }

    protected ApiFactory CreateApiFactory()
    {
        return new ApiFactory(_output);
    }
}