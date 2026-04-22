using Soenneker.Blob.Delete.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blob.Delete.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BlobDeleteUtilTests : HostedUnitTest
{
    private readonly IBlobDeleteUtil _util;

    public BlobDeleteUtilTests(Host host) : base(host)
    {
        _util = Resolve<IBlobDeleteUtil>(true);
    }

    [Test]
    public void Default()
    {
    }
}
