using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace LunraGames.Reflection
{
	public abstract class Attributable
	{
		Attribute[] _Attributes;

		protected abstract Attribute[] AllAttributes { get; }

		public Attribute[] Attributes { get { return _Attributes ?? (_Attributes = AllAttributes); } }

		public T[] GetAttributes<T> (params Type[] types)
			where T : class
		{
			var result = new List<T>();
			foreach (var attribute in Attributes) if (attribute is T) result.Add (attribute as T);
			return result.ToArray();
		}

		public Attribute[] GetAttributes (params Type[] types)
		{
			return GetAttributes<Attribute>(types);
		}
	}
}