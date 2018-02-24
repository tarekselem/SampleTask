using Futura.BusinessOperations.Configurations;
using NUnit.Framework;

namespace Futura.Tests.BusinessOperations
{
    [SetUpFixture]
    public class TestsSetUp
    {

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            MappingConfigurations.Configure();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
