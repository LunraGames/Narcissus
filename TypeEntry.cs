using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace LunraGames.Reflection
{
	public class TypeEntry : Attributable, INarcissusEntry
	{
		const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		public Type Type { get; private set; }

		MethodEntry[] _Methods;
		FieldEntry[] _Fields;

		public TypeEntry(Type type)
		{
			Type = type;
		}

		public MethodEntry[] Methods 
		{
			get 
			{
				if (_Methods == null)
				{
					var methods = new List<MethodEntry>();
					foreach (var method in Type.GetMethods(Flags)) methods.Add(new MethodEntry(method));
					_Methods = methods.ToArray();
				}
				return _Methods;
			}	
		}

		public FieldEntry[] Fields 
		{
			get 
			{
				if (_Fields == null)
				{
					var fields = new List<FieldEntry>();
					foreach (var field in Type.GetFields(Flags)) fields.Add(new FieldEntry(field));
					_Fields = fields.ToArray();
				}
				return _Fields;
			}	
		}

		protected override Attribute[] AllAttributes { get { return Type.GetCustomAttributes(true).Cast<Attribute>().ToArray(); } }

		#region IEntry implementation
		public string Name { get { return Type.Name; } }
		public string FullName { get { return Type.FullName; } }
		#endregion
	}
}