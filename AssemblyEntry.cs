using System.Reflection;
using System.Collections.Generic;

namespace LunraGames.Reflection
{
	public class AssemblyEntry : INarcissusEntry
	{
		public Assembly Assembly { get; private set; }

		List<TypeEntry> _Types;

		public AssemblyEntry(Assembly assembly)
		{
			Assembly = assembly;
		}

		public TypeEntry[] Types
		{
			get 
			{
				if (_Types == null) 
				{
					_Types = new List<TypeEntry>();
					foreach (var type in Assembly.GetTypes()) _Types.Add(new TypeEntry(type));
				}
				return _Types.ToArray();
			}
		}

		#region IEntry implementation
		public string Name { get { return Assembly.GetName().Name; } }
		public string FullName { get { return Assembly.GetName().FullName; } }
		#endregion
	}
}