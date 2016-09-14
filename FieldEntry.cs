using System;
using System.Linq;
using System.Reflection;

namespace LunraGames.Reflection
{
	public class FieldEntry : Attributable, INarcissusEntry
	{
		public FieldInfo Field { get; private set; }

		public FieldEntry(FieldInfo field)
		{
			Field = field;
		}

		protected override Attribute[] AllAttributes { get { return Field.GetCustomAttributes(true).Cast<Attribute>().ToArray(); } }

		#region IEntry implementation
		public string Name { get { return Field.Name; } }
		public string FullName { get { return Field.DeclaringType.FullName + "." + Name; } }
		public string FriendlyName { get { return Field.DeclaringType.Name + "." + Name; } }
		#endregion
	}
}