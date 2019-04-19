using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class ListOfObject {

		/// <summary>
		/// Gets a property or field from all the objects in the list
		/// </summary>
		/// <typeparam name="T">The type of the list object</typeparam>
		/// <typeparam name="T2">The type of the property you want to retrieve</typeparam>
		public static List<T2> GetProps<T, T2>(this IEnumerable<T> list, string propOrPath) {
			List<T2> values = new List<T2>();

			foreach (T obj in list) {
				if (obj != null) {
					T2 value = obj.GetPropValue<T2>(propOrPath);
					values.Add(value);
				}
			}

			return values;
		}

	}
}
