using ETMS.AppContext.Component;
namespace ETMS.Components.Mentor.API
{
    /// <summary>
    /// Example模块，门面接口定义，继承IComponent
    /// </summary>
    public interface IExampleFacade : IComponent
    {
        void Hello1();
        void Hello2();
        void Hello3();
        void Hello4();
    }
}

