using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace SigiFluent.Helpers
{
    public static class ApplicationPath
    {

        static ApplicationPath()
        {
            GetResourcePaths(Assembly.GetExecutingAssembly());

        }

        public static string GetFullPath(string partial_path)
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            var exeAssembly = Path.GetDirectoryName(assembly.Location);

            return Path.Combine(exeAssembly, partial_path);
        }

        private static void GetResourcePaths(Assembly assembly)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var resourceName = assembly.GetName().Name + ".g";
            var resourceManager = new ResourceManager(resourceName, assembly);

            try
            {
                var resourceSet = resourceManager.GetResourceSet(culture, true, true);

                foreach (System.Collections.DictionaryEntry resource in resourceSet)
                {

                    if (resource.Value != null && !ResourceFiles.ContainsKey(Path.GetFileName(resource.Key.ToString())) )
                        ResourceFiles.Add(Path.GetFileName(resource.Key.ToString()), resource.Key.ToString());
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                resourceManager.ReleaseAllResources();
            }
        }

        public static string GetPathByFileNameFromResources(string fileName, bool contains = false)
        {
            
            var value = ResourceFiles.FirstOrDefault(f => f.Key.Contains(fileName));

            return value.Key!=null  ? value.Value : string.Empty;

        }

        private static Dictionary<string,string> ResourceFiles = new Dictionary<string, string>();
    }
}
