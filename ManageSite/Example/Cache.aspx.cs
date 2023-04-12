using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Example_Cache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int i = ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<int>("test_i", () =>
         {
             return 1;
         });

        Response.Write("<br/>值类型(int)缓存，期望值=1,实际值=" + i.ToString());

        DateTime date = ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<DateTime>("test_date", () =>
        {
            return DateTime.Parse("2012-04-05");
        });
        Response.Write("<br/>Struct(DateTime)类型缓存，期望值=2012-4-5,实际值=" + date.ToString("yyyy-MM-dd"));


        Guid guid = ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<Guid>("test_guid", () =>
        {
            return Guid.Parse("8A7454B7-A982-4D57-B1C7-BB074B9174C4");
        });
        Response.Write("<br/>Struct(Guid)类型缓存，期望值=8A7454B7-A982-4D57-B1C7-BB074B9174C4,实际值=" + guid.ToString());



        List<string> list = ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<List<string>>("test_str", () =>
        {
            List<string> result = new List<string>();
            result.Add("a");
            result.Add("b");
            return result;
        });
        Response.Write("<br/>引用类型(List&lt;string&gt;)缓存，集合数期望值=2,实际值=" + list.Count.ToString());


    }
}