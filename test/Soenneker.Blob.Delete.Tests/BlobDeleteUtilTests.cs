using Soenneker.Blob.Delete.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blob.Delete.Tests;

[Collection("Collection")]
public class BlobDeleteUtilTests : FixturedUnitTest
{
    private readonly IBlobDeleteUtil _util;

    public BlobDeleteUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IBlobDeleteUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
