using Colin.Common.IO;
using System.Reflection;

namespace Colin
{
    internal sealed class FileChecker : IProgramChecker
    {
        public void Check()
        {
            PropertyInfo[] properties = typeof(DirPhonebook).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                CheckDir((string)property.GetValue(null));
            }
        }
        public static void CheckDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
