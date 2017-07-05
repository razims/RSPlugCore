using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using RSPlugCore.Library.Services;

namespace RSPlugCore.Presentation.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            Dependency.Initialize();

            foreach (var assembly in GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
            }

            var messageService = Dependency.Container.Resolve<IMessageService>();
            Console.WriteLine(messageService.Greeting);

            Console.ReadLine();
        }

        static List<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Name == ("RSPlugCore")
                   || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("RSPlugCore"));
        }
    }
}