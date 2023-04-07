using System;
using ETMS.Components.Reporting.API;
using ETMS.AppContext.Component;

namespace ETMS.Components.Reporting.Implement
{
    public class DefaultExampleFacade : DefaultComponent, IExampleFacade
    {
        #region IComponent 成员

        event EventHandler System.ComponentModel.IComponent.Disposed
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        System.ComponentModel.ISite System.ComponentModel.IComponent.Site
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
