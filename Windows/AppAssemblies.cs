using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class AppAssemblies {

		private static List<string> AssemblyPaths;
		private static List<string> AssemblyExtensions;

		/// <summary>
		/// Register a folder that contains .NET assemblies.
		/// This also automatically registers the assembly load handler that
		/// takes care of loading assemblies from multiple paths and extensions.
		/// </summary>
		public static bool RegisterFolder(string folderPath) {
			InitAssemblyHandling();
			return AssemblyPaths.AddOnce(folderPath);
		}

		/// <summary>
		/// Register a file extension that contains .NET assemblies.
		/// This also automatically registers the assembly load handler that
		/// takes care of loading assemblies from multiple paths and extensions.
		/// </summary>
		public static bool RegisterExtension(string extension) {
			InitAssemblyHandling();
			return AssemblyExtensions.AddOnce(extension.RemovePrefix(".").ToLower());
		}

		/// <summary>
		/// Register the assembly load handler that takes care of loading
		/// assemblies from multiple paths and extensions. This is called internally
		/// if required otherwise the default .NET handling is used.
		/// </summary>
		private static void InitAssemblyHandling() {
			if (AssemblyPaths == null) {
				AssemblyPaths = new List<string>();
				AssemblyExtensions = new List<string> { "dll" };
				AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
			}
		}

		/// <summary>
		/// The handler that loads assemblies from multiple directories, and
		/// prevents the same assembly from loading multiple times.
		/// 
		/// @url		https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders
		/// @author		Rick Strahl
		/// </summary>
		private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args) {

			// ignore missing resources
			if (args.Name.Contains(".resources")) {
				return null;
			}

			// check for assemblies already loaded
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
			if (assembly != null) {
				return assembly;
			}

			// split out the filename of the full assembly name
			string filename = args.Name.Split(',')[0];

			// search all the registered folders for the assembly
			foreach (var ext in AssemblyExtensions) {
				foreach (var folder in AssemblyPaths) {
					var finalPath = folder.AddPath(filename + "." + ext);
					if (finalPath.FileExists()) {
						return finalPath.LoadAssembly(true, true);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Dynamically load a DLL file as a .NET assembly and return the object.
		/// The assembly is loaded into the current AppDomain, optionally sharing context with it.
		/// If the assembly has any dependencies, you need to store them in the same folder as the assembly.
		/// 
		/// @docurl		https://docs.microsoft.com/en-us/dotnet/framework/deployment/best-practices-for-assembly-loading
		/// </summary>
		/// <param name="giveContext">If true then LoadFrom is used so the context is provided, else LoadFile is used and no context is provided. Read the given link to understand the difference.</param>
		/// <param name="absorbErrors">If true then assembly loading errors are caught and null is returned.</param>
		public static Assembly LoadAssembly(this string path, bool giveContext = true, bool absorbErrors = false) {
			if (absorbErrors) {
				try {
					return LoadAssembly(path, giveContext, false);
				}
				catch (Exception) {
					return null;
				}
			}
			else {
				if (giveContext) {
					return Assembly.LoadFrom(path);
				}
				else {
					return Assembly.LoadFile(path);
				}
			}
		}

		/// <summary>
		/// Dynamically load a DLL file as a .NET assembly and return all the types contained within.
		/// The assembly is loaded into the current AppDomain, optionally sharing context with it.
		/// If the assembly has any dependencies, syou need to tore them in the same folder as the assembly.
		/// 
		/// @docurl		https://docs.microsoft.com/en-us/dotnet/framework/deployment/best-practices-for-assembly-loading
		/// </summary>
		/// <param name="giveContext">If true then LoadFrom is used so the context is provided, else LoadFile is used and no context is provided. Read the given link to understand the difference.</param>
		/// <param name="absorbErrors">If true then assembly loading errors are caught and null is returned.</param>
		public static List<Type> LoadAssemblyTypes(this string path, bool giveContext = true, bool absorbErrors = false) {
			if (absorbErrors) {
				try {
					return LoadAssemblyTypes(path, giveContext, false);
				}
				catch (Exception) {
					return null;
				}
			}
			else {
				if (giveContext) {
					return Assembly.LoadFrom(path).GetTypes().ToList();
				}
				else {
					return Assembly.LoadFile(path).GetTypes().ToList();
				}
			}
		}

	}
}
