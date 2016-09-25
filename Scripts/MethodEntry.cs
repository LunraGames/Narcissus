using System;
using System.Linq;
using System.Reflection;

namespace LunraGames.Reflection
{
	public class MethodEntry : Attributable, INarcissusEntry
	{
		public MethodInfo Method { get; private set; }

		public MethodEntry(MethodInfo method)
		{
			Method = method;
		}

		protected override Attribute[] AllAttributes { get { return Method.GetCustomAttributes(true).Cast<Attribute>().ToArray(); } }

		#region IEntry implementation
		public string Name { get { return Method.Name; } }
		public string FullName { get { return Method.DeclaringType.FullName + "." + Name; } }
		public string FriendlyName { get { return Method.DeclaringType.Name + "." + Name; } }
		#endregion
	}
}