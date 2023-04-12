using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class ResourcesCollection : List<Resources>
    {
        public new ResourcesCollection FindAll(Predicate<Resources> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            ResourcesCollection list = new ResourcesCollection();

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
