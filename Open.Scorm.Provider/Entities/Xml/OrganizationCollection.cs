using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class OrganizationCollection : List<Organization>
    {
        public new OrganizationCollection FindAll(Predicate<Organization> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            OrganizationCollection list = new OrganizationCollection();
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
