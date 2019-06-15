using System;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssemblies(new Assembly[]
            {
                typeof(Base58Task).Assembly,
            }).Run(args);
        }
    }
}
