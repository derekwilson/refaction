using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
	public class ItemCollectionDto<T>
	{
		public IEnumerable<T> Items { get; private set; }

		public ItemCollectionDto(IEnumerable<T> items)
		{
			// TODO - maybe the items themselves should be mapped
			Items = items;
		}
	}
}