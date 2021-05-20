using Blauhaus.TestHelpers.BaseTests;
using NUnit.Framework;

namespace Blauhaus.Graphics3D.Tests.Tests.Base
{
    public abstract class BaseGraphics3DTest<TSut> : BaseServiceTest<TSut> where TSut : class
    {
        [SetUp]
        public virtual void Setup()
        {
            Cleanup();
        }
    }
}