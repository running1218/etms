namespace ETMS.Editor.Common
{
    /// <summary>
    /// 等比压缩的基准，确定maxImageSideLength参数的参照对象。0为按照最长边，1为按照宽度，2为按照高度
    /// </summary>
    public enum CompressSide
    {
        选择基准, 按照最长边, 按照宽度, 按照高度
    }
    /// <summary>
    /// 编辑器回车标签。p或br
    /// </summary>
    public enum EnterTag
    {
        p, br
    }
    /// <summary>
    /// 源码的查看方式，codemirror 是代码高亮，textarea是文本框
    /// </summary>
    public enum SourceEditor
    {
        codemirror, textarea
    }
}
