using Runner.Loading;
using System.IO;
using System.Windows.Forms;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application.EnableVisualStyles();
            // MessageBox.Show("1");
            // IntSecurity.UnmanagedCode.Demand();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            byte[] bytes = File.ReadAllBytes(@"C:\AsmLoading\1.dat");

            using (AssemblyLoader loader = new AssemblyLoader(bytes))
            {
                loader.RunWrappedAssembly();
            }
        }
    }
}
