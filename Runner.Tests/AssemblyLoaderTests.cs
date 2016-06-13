using NUnit.Framework;
using Runner.Loading;
using System.IO;
using System.Windows.Forms;

namespace Runner.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests
    {
        [Test]
        public void AssemblyLoadTest()
        {
            // MessageBox.Show("123");

            byte[] bytes = File.ReadAllBytes(@"C:\AsmLoading\1.dat");

            using (AssemblyLoader loader = new AssemblyLoader(bytes))
            {
                loader.RunWrappedAssembly();
            }
        }
    }
}
