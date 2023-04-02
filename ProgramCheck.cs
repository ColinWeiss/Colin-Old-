using System.Reflection;

namespace Colin
{
    internal static class ProgramCheck
    {
        public static void DoCheck()
        {
            IProgramChecker checker;
            foreach (Type item in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!item.IsAbstract && item.GetInterfaces().Contains(typeof(IProgramChecker)))
                {
                    checker = (IProgramChecker)Activator.CreateInstance(item);
                    checker.Check();
                }
            }
        }
    }
}