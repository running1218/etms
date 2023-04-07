using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ETMS.Editor.Common
{
    public static class EditorPublic
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <returns></returns>
        public static String GetConfigString(Dictionary<String, Object> d)
        {
            StringBuilder r = new StringBuilder();
            foreach (String s in d.Keys)
            {
                r.Append(s);
                r.Append(":");
                if ((Regex.IsMatch(Convert.ToString(d[s]), @"^[0-9]+$|^true$|^false$", RegexOptions.IgnoreCase) && !Regex.IsMatch(s, @"^initialContent$", RegexOptions.IgnoreCase)) || Regex.IsMatch(s, @"^toolbars$", RegexOptions.IgnoreCase) || Regex.IsMatch(s, @"^toolbar$", RegexOptions.IgnoreCase))
                {
                    r.Append(d[s].ToString().ToLower());
                }
                else
                {
                    r.Append("'");
                    r.Append(d[s].ToString().Replace("\r", "").Replace("\n", ""));
                    r.Append("'");
                }
                r.Append(",");
            }
            String temp = r.ToString();
            if (!String.IsNullOrEmpty(temp) && temp.LastIndexOf(',') > 0)
            {
                temp = temp.Remove(temp.Length - 1);
            }
            return temp;
        }
    }
}
