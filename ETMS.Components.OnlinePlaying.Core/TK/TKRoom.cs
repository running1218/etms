using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using ETMS.Utility;

namespace ETMS.Components.OnlinePlaying
{
    public class TKRoom
    {
        private string domain = ConfigurationManager.AppSettings["TKLiveDomain"];
        private string restUrl = ConfigurationManager.AppSettings["TKLiveRoot"];
        private string key = ConfigurationManager.AppSettings["TKKey"];
        private string videotype = ConfigurationManager.AppSettings["VideoType"];
        private string videoframerate = ConfigurationManager.AppSettings["VideoFramerate"];
        private string autoopenav = ConfigurationManager.AppSettings["AutoOpenav"];
        public string chairmanpwd = ConfigurationManager.AppSettings["ChairmanPwd"];
        public string assistantpwd = ConfigurationManager.AppSettings["SssistantPwd"];
        public string patrolpwd = ConfigurationManager.AppSettings["PatrolPwd"];
        private int passwordrequired = 0; //选填 房间是否需要学生密码 0:否、1:是 
        private string confuserpwd = string.Empty; //passwordrequired = 1 时必填(4位以上) 学生密码
        private int allowsidelineuser = 0; //选填 是否允许旁听用户参加 0:否、1:是
        private string sidelineuserpwd = string.Empty;

        /// <summary>
        /// 创建小（中）班直播
        /// </summary>
        /// <param name="roomname"></param>
        /// <param name="roomtype">0:1 对 1  3：1 对多</param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public string CreateRoom(string roomname, int roomtype, DateTime starttime, DateTime endtime)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomname", System.Web.HttpUtility.UrlEncode(roomname, System.Text.Encoding.UTF8));
            mParams.Add("roomtype", roomtype);
            mParams.Add("starttime", getSecondEnd(starttime));
            mParams.Add("endtime", getSecondEnd(endtime));
            mParams.Add("videotype", this.videotype);
            mParams.Add("chairmanpwd", this.chairmanpwd);
            mParams.Add("assistantpwd", this.assistantpwd);
            mParams.Add("patrolpwd", this.patrolpwd);
            mParams.Add("videoframerate", this.videoframerate);
            mParams.Add("autoopenav", this.autoopenav);
            mParams.Add("passwordrequired", this.passwordrequired);
            mParams.Add("allowsidelineuser", this.allowsidelineuser);

            return call("roomcreate", mParams, "GET", null);
        }

        /// <summary>
        /// 修改（小班）直播
        /// </summary>
        /// <param name="serial">直播序列号</param>
        /// <param name="roomname"></param>
        /// <param name="roomtype">0:1 对 1  3：1 对多</param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public string ModifyRoom(string serial, string roomname, int roomtype, DateTime starttime, DateTime endtime)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("serial", serial);
            mParams.Add("roomname", roomname);
            mParams.Add("roomtype", roomtype);
            mParams.Add("starttime", getSecondEnd(starttime));
            mParams.Add("endtime", getSecondEnd(endtime));
            mParams.Add("videotype", this.videotype);
            mParams.Add("videoframerate", this.videoframerate);
            mParams.Add("autoopenav", this.autoopenav);
            mParams.Add("passwordrequired", this.passwordrequired);
            mParams.Add("allowsidelineuser", this.allowsidelineuser);
            mParams.Add("chairmanpwd", this.chairmanpwd);
            mParams.Add("assistantpwd", this.assistantpwd);
            mParams.Add("patrolpwd", this.patrolpwd);
            return call("roommodify", mParams, "GET", null);
        }
        /// <summary>
        /// 删除直播（小班）
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public string DeleteRoom(string serial)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("serial", serial);
            return call("roomdelete", mParams, "GET", null);
        }

        /// <summary>
        /// 获取回放列表
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public string GetRecordList(string serial)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("serial", serial);
            return call("getrecordlist", mParams, "GET", null);
        }

        /// <summary>
        /// 进入直播间
        /// </summary>
        /// <param name="serial">直播ID</param>
        /// <param name="username">用户名（业务用户帐号）</param>
        /// <param name="usertype">用户类型</param>
        /// <returns></returns>
        public string EntryRoom(string serial, string username, string passoword, int usertype)
        {
            int ts = GetTimeStamp();
            Hashtable mParams = new Hashtable();
            mParams.Add("domain", this.domain);
            mParams.Add("serial", serial);
            mParams.Add("username", username);
            mParams.Add("usertype", usertype);
            mParams.Add("ts", ts);
            mParams.Add("auth", CrypProvider.MD5(string.Format("{0}{1}{2}{3}", this.key, ts, serial, usertype)));

            if (usertype != 2)
            {
                mParams.Add("userpassword", CrypProvider.AesEncrypt(GetUserTypePassword(usertype), this.key));
            }
            return string.Format("{0}{1}", this.restUrl, "entry") + this.hashtableToQueryString(mParams);
        }

        public string CreateLoginKey(string serial, string username, int usertype)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("key", this.key);
            mParams.Add("serial", serial);
            mParams.Add("nickname", username);
            mParams.Add("usertype", usertype);
            mParams.Add("password", chairmanpwd);
            mParams.Add("invisibleuser", 0);
            return string.Format("{0}{1}", this.restUrl, "createloginkey") + this.hashtableToQueryString(mParams);
        }

        public string call(string cmd, Hashtable mParams, string httpMethod, ArrayList files)
        {
            mParams.Add("key", this.key);            

            if ("POST" == httpMethod)
            {
                return doPost(string.Format("{0}{1}",this.restUrl, cmd), mParams, files);
            }
            else {
                // 构造请求URL
                string resurl = string.Format("{0}{1}", this.restUrl, cmd) + this.hashtableToQueryString(mParams);
                return this.doGet(resurl);
            }
        }

        public string doGet(string url)
        {
            string retString = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                //接受返回来的数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                retString = streamReader.ReadToEnd();

                streamReader.Close();
                stream.Close();
                response.Close();
            }
            catch (Exception)
            {
            }

            return retString;
        }

        public string doPost(string url, Hashtable data, ArrayList files)
        {
            Encoding encoding = Encoding.UTF8;

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                if (data != null)
                {
                    foreach (string key in data.Keys)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, data[key]);
                        byte[] formitembytes = encoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }

                if (files != null)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    for (int i = 0; i < files.Count; ++i)
                    {
                        Hashtable file = (Hashtable)files[i];
                        string filePath = (string)file["file"];

                        string contentType = getContentType(filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1)));
                        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: " + contentType + "\r\n\r\n";

                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, file["field"], Path.GetFileName(filePath));
                        byte[] headerbytes = encoding.GetBytes(header);
                        stream.Write(headerbytes, 0, headerbytes.Length);
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }

                stream.Write(endbytes, 0, endbytes.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string getContentType(string fileType)
        {
            string retval = "application/octet-stream";

            if ("jpeg" == fileType)
            {
                retval = "image/jpeg";
            }
            else if ("jpg" == fileType)
            {
                retval = "image/jpg";
            }
            else if ("png" == fileType)
            {
                retval = "image/png";
            }
            else if ("gif" == fileType)
            {
                retval = "image/gif";
            }

            return retval;
        }
        /// <summary>
        /// 参数转换
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string hashtableToQueryString(Hashtable param)
        {
            string retval = "";

            foreach (string key in param.Keys)
            {
                retval += string.Format("/{0}/{1}", key, param[key]);
            }

            return retval;
        }
        /// <summary>
        /// datetime to int
        /// </summary>
        /// <param name="livingTime"></param>
        /// <returns></returns>
        private int getSecondEnd(DateTime livingTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            int unixTime = (int)Math.Round((livingTime - startTime).TotalSeconds, MidpointRounding.AwayFromZero);
            return unixTime;
        }

        public static int GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        private string GetUserTypePassword(int userType)
        {
            switch (userType)
            {
                case 0:
                    return chairmanpwd;
                case 1:
                    return assistantpwd;
                case 4:
                    return patrolpwd;
                default:
                    return string.Empty;
            }
        }
    }
}
