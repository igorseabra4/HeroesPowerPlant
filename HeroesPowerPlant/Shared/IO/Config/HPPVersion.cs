using System.Reflection;

namespace HeroesPowerPlant.Shared.IO.Config
{
    public class HPPVersion
    {
        public static readonly string Current = typeof(HPPVersion).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion.Split('+')[0];

        public string version = Current;
        public string versionName = Current;
    }
}
