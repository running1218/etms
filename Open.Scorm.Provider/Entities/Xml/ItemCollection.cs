using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class ItemCollection : List<Item>
    {
        public new ItemCollection FindAll(Predicate<Item> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            ItemCollection list = new ItemCollection();

            foreach (var entity in this)
            {
                if (match(entity))
                {
                    list.Add(entity);
                }
            }
            return list;
        }
    }
}
