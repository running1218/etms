using System;
using System.Collections.Generic;

namespace Open.Scorm.Entities
{
    public class FileCollection : List<File>
    {
        public new FileCollection FindAll(Predicate<File> match)
        {
            if(match == null)
            {
                throw new ArgumentNullException("match");
            }

            FileCollection list= new FileCollection();

            foreach(var entity in this)
            {
                if(match(entity))
                {
                    list.Add(entity);
                }
            }
            return list;
        }
    }
}
