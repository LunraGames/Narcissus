using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace LunraGames.Reflection
{
	public static class Narcissus
	{
		static List<AssemblyEntry> Assemblies = new List<AssemblyEntry>();

		static IEnumerable<AssemblyEntry> GetAssemblies(params string[] assemblies)
		{
			Assembly[] allAssemblies = null;
			var result = new List<AssemblyEntry>();
			foreach (var assemblyName in assemblies) 
			{
				var assembly = Assemblies.FirstOrDefault(a => a.Name == assemblyName);
				if (assembly == null)
				{
					var assemblyInstance = (allAssemblies ?? (allAssemblies = AppDomain.CurrentDomain.GetAssemblies())).FirstOrDefault(a => a.GetName().Name == assemblyName);
					if (assemblyInstance == null) throw new NullReferenceException("Assembly "+assemblyName+" doesn't exist");

					assembly = GetAssemblies(assemblyInstance).First();
				}
				result.Add(assembly);
			}
			return result;
		}

		static IEnumerable<AssemblyEntry> GetAssemblies(params Assembly[] assemblies)
		{
			var result = new List<AssemblyEntry>();
			foreach (var assemblyInstance in assemblies) 
			{
				var assembly = Assemblies.FirstOrDefault(a => a.Assembly == assemblyInstance);
				if (assembly == null)
				{
					assembly = new AssemblyEntry(assemblyInstance);
					Assemblies.Add(assembly);
				}
				result.Add(assembly);
			}
			return result;
		}

		public static IEnumerable<MethodEntry> GetMethods(params string[] assemblies)
		{
			var result = new List<MethodEntry>();
			foreach (var assembly in GetAssemblies(assemblies))
			{
				foreach (var type in assembly.Types)
				{
					foreach (var method in type.Methods)
					{
						result.Add(method);
					}
				}
			}
			return result;
		}

		public static IEnumerable<MethodEntry> GetMethods<A>(params string[] assemblies)
			where A : Attribute
		{
			var result = new List<MethodEntry>();
			foreach (var method in GetMethods(assemblies))
			{
				if (method.Attributes.Any(a => a is A)) result.Add(method);
			}
			return result;
		}

		public static IEnumerable<FieldEntry> GetFields(params string[] assemblies)
		{
			var result = new List<FieldEntry>();
			foreach (var assembly in GetAssemblies(assemblies))
			{
				foreach (var type in assembly.Types)
				{
					foreach (var field in type.Fields)
					{
						result.Add(field);
					}
				}
			}
			return result;
		}

		public static IEnumerable<FieldEntry> GetFields<A>(params string[] assemblies)
			where A : Attribute
		{
			var result = new List<FieldEntry>();
			foreach (var field in GetFields(assemblies))
			{
				if (field.Attributes.Any(a => a is A)) result.Add(field);
			}
			return result;
		}

		public static IEnumerable<INarcissusEntry> Get(string search, string[] assemblies)
		{
			var result = new List<INarcissusEntry>();
			result.AddRange(GetMethods(assemblies).FriendlyMatch(search, m => m.FullName).Cast<INarcissusEntry>());
			result.AddRange(GetFields(assemblies).FriendlyMatch(search, f => f.FullName).Cast<INarcissusEntry>());
			return result;
		}

		public static IEnumerable<INarcissusEntry> Get<A>(string search, string[] assemblies)
			where A : Attribute
		{
			var result = new List<INarcissusEntry>();
			result.AddRange(GetMethods<A>(assemblies).FriendlyMatch(search, m => m.FullName).Cast<INarcissusEntry>());
			result.AddRange(GetFields<A>(assemblies).FriendlyMatch(search, f => f.FullName).Cast<INarcissusEntry>());
			return result;
		}
	}
}
