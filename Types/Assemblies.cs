using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Assemblies {

		/// <summary>
		/// Dynamically load a DLL file as a .NET assembly and return the object.
		/// If the assembly has any dependencies, store them in the same folder as the assembly.
		/// </summary>
		public static Assembly LoadAssembly(this string path) {
			Assembly assembly = Assembly.LoadFrom(path);
			return assembly;
		}

		/// <summary>
		/// Dynamically load a DLL file as a .NET assembly and return all the types contained within.
		/// If the assembly has any dependencies, store them in the same folder as the assembly.
		/// </summary>
		public static List<Type> LoadAssemblyTypes(this string path) {
			Assembly assembly = Assembly.LoadFrom(path);
			var types = assembly.GetTypes().ToList();
			return types;
		}

	}
}
