using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class ResourceCollection : List<Resource>
    {
        public new ResourceCollection FindAll(Predicate<Resource> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            ResourceCollection list = new ResourceCollection();

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
