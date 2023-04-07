using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ETMS.Utility
{
    public class WebRequestHelper
    {
        private string url;

        public WebRequestHelper(){}
        public WebRequestHelper(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// 参数信息
        /// </summary>
        public List<ResourceParameter> Parameters { get; set; }

        public string Get(Uri url)
        {
            string result = string.Empty;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse httpWebRespones = (HttpWebResponse)request.GetResponse();
            Stream stream = httpWebRespones.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            result = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            return result;
        }

        #region RestClient Post请求
        public string PostRestClient(Uri uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            //Parameter参数数据
            if (Parameters != null)
            {
                Parameters.ForEach(p =>
                {
                    AddParameter(request, p);
                });
            }
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Post(string resource)
        {
            var client = new RestClient(new Uri(url));
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            //Parameter参数数据
            if (Parameters != null)
            {
                Parameters.ForEach(p =>
                {
                    AddParameter(request, p);
                });
            }
            var response = client.Execute(request);
            return response.Content;
        }


        #endregion

        #region RestClient Get请求
        public string Get(string resource,bool isFollowRedirects = false)
        {
            var client = new RestClient(new Uri(url));
            
            var request = new RestRequest(resource,Method.GET)
            {
                UseDefaultCredentials = true
            };

            //Parameter参数数据
            if (Parameters != null)
            {
                Parameters.ForEach(p =>
                {
                    AddParameter(request,p);
                });
            }

            if (isFollowRedirects)
            {
                // This property internally sets the AllowAutoRedirect of Http webrequest
                client.FollowRedirects = isFollowRedirects;
                // Optionally you can also add the max redirects 
                client.MaxRedirects = 2;
            }
            IRestResponse response = client.Execute(request);
            if (isFollowRedirects)
            {
                return response.ResponseUri.ToString();
            }
            else
            {
                return response.Content;
            }
        }
        public string GetRestClient(Uri uri, bool isFollowRedirects = false)
        {
            var client = new RestClient(uri);

            var request = new RestRequest(Method.GET)
            {
                UseDefaultCredentials = true
            };
            //Parameter参数数据
            if (Parameters != null)
            {
                Parameters.ForEach(p =>
                {
                    AddParameter(request, p);
                });
            }
            if (isFollowRedirects)
            {
                // This property internally sets the AllowAutoRedirect of Http webrequest
                client.FollowRedirects = isFollowRedirects;
                // Optionally you can also add the max redirects 
                client.MaxRedirects = 2;
            }
            IRestResponse response = client.Execute(request);
            if (isFollowRedirects)
            {
                return response.ResponseUri.ToString();
            }
            else
            {
                return response.Content;
            }
        }
        #endregion
        private void AddParameter(IRestRequest request,ResourceParameter param)
        {
            param.Value = param.Value ?? string.Empty;
            switch ((int)param.Type)
            {
                case (int)ParameterType.Cookie:
                    request.AddCookie(param.Key, param.Value.ToString());
                    break;
                case (int)ParameterType.GetOrPost:
                    request.AddParameter(param.Key, param.Value.ToString());
                    break;
                case (int)ParameterType.UrlSegment:
                    request.AddUrlSegment(param.Key, param.Value.ToString());
                    break;
                case (int)ParameterType.HttpHeader:
                    request.AddHeader(param.Key, param.Value.ToString());
                    break;
                case (int)ParameterType.RequestBody:
                    request.AddBody(param.Value);
                    break;
                case (int)ParameterType.QueryString:
                    request.AddQueryParameter(param.Key, param.Value.ToString());
                    break;
                default:
                    break;

            }
        }
    }
    public class ResourceParameter
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public ParameterType Type { get; set; }

        public string ContentType { get; set; }

        public bool isUncode { get; set; }

        public ResourceParameter() { }

        public ResourceParameter(string key, string val, ParameterType type)
        {
            Key = key;
            Value = val;
            Type = type;
        }
        /// <summary>
        /// 可设置是否对val转码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <param name="isuncode">是否转码，true转码，false不转码</param>
        public ResourceParameter(string key, string val, ParameterType type, bool isuncode)
        {
            Key = key;
            if (isuncode)
            {
                Value = System.Web.HttpUtility.UrlEncode(val, System.Text.Encoding.UTF8);
            }
            else
            {
                Value = val;
            }
            Type = type;
        }
    }
}
