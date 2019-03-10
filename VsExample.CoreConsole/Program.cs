using System;
using System.Diagnostics;
namespace VsExample.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Joined Value: {string.Join(", ", args)}");

            Console.WriteLine($"Environment Variable 'DotNetCoreEnv': {Environment.GetEnvironmentVariable("DotNetCoreEnv")}");

            Console.ReadKey();
        }
    }
}
