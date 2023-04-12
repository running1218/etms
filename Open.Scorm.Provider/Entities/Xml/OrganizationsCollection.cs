using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class OrganizationsCollection : List<Organizations>
    {
        public new OrganizationsCollection FindAll(Predicate<Organizations> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            OrganizationsCollection list = new OrganizationsCollection();

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
