using System;
using System.Reflection;
using System.Security.Policy;

namespace Runner.Loading
{
    [Serializable]
    public class AssemblyLoader : IDisposable
    {
        private AssemblyWrapper wrapper = null;
        private AppDomain domain = null;

        public AssemblyLoader(byte[] bytes)
        {
            Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            
            AppDomain childDomain = AppDomain.CreateDomain("DiscoveryRegion", evidence, setup);

            childDomain.AssemblyResolve += ChildDomain_AssemblyResolve;

            Type proxyType = typeof(AssemblyWrapper);
            var proxy = (AssemblyWrapper)childDomain.CreateInstanceAndUnwrap(
               proxyType.Assembly.FullName,
               proxyType.FullName);

            proxy.ReadAssembly(bytes);
            wrapper = proxy;
            domain = childDomain;
        }

        public void RunWrappedAssembly()
        {
            if (wrapper != null)
                wrapper.RunAssembly();
        }

        private Assembly ChildDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        public void Dispose()
        {
            if (domain != null)
                AppDomain.Unload(domain);
        }

        public class AssemblyWrapper : MarshalByRefObject
        {
            private Assembly internalAssembly { get; set; }

            public void ReadAssembly(byte[] bytes)
            {
                try
                {
                    internalAssembly = Assembly.Load(bytes);
                }
                catch (Exception)
                {
                }
            }

            public void RunAssembly()
            {
                internalAssembly.EntryPoint.Invoke(null, new string[] { });
            }
        }
    }
}
