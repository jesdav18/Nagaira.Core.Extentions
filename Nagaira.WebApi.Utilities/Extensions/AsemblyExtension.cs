using System.Reflection;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public static class AsemblyExtension
    {
        public static AssemblyInfo GetAssemblyInfo(Assembly assembly)
        {
            var assemblyName = assembly?.GetName().Name;
            var version = assembly?.GetName().Version?.ToString();

            return new AssemblyInfo
            {
                Name = assemblyName,
                Version = version
            };
        }

        public class AssemblyInfo
        {
            public string? Name { get; set; }
            public string? Version { get; set; }
        }
    }
}
