using System;
using System.Collections.Generic;

namespace ETMS.Components.Reporting.API.Entity
{
    public class ItemCollection : List<OnLineStudyTitleItem>
    {
        public new ItemCollection FindAll(Predicate<OnLineStudyTitleItem> match)
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
