using System;

namespace Open.Scorm.Provider
{
    public class BusinessException : Exception
    {
        public BusinessException(string message):base(message)
        {
        }
    }
}
