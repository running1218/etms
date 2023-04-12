using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Security;
using System.Security.Cryptography;
using System.Text;

public partial class LoginToLearn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["key"] != null)
            {
                string key = Request.QueryString["key"];
                login(key);
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请求缺少参数！");
            }
        }
    }

    private void login(string key)
    {
        UrlKey urlKey = new UrlKey();
        User u= urlKey.CheckKey(key);

        string username = u.Uname;
        string password = u.Password;
        bool isSaveUserName = false;
        bool isAutoSignIn = false;

        //调用登录
        try
        {
            //1、验证url参数是否有效
            //DefaultAuthenticator.ValidSignInParams(Request);
            //2、验证用户输出参数：用户名、密码未空时，提示
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ETMS.Utility.JsUtility.AlertMessageBox("用户名或密码为空！");
            }
            //剔除前后空白字符
            username = username.Trim();
            password = password.Trim();
            //登录验证
            string appsignInUrl = DefaultAuthenticator.SignIn(username, password, isSaveUserName, isAutoSignIn);
            //保存用户默认设置
            SignInPageData pd = new SignInPageData();
            if (isSaveUserName)
            {
                pd.UserName = username;
            }
            pd.IsSaveUserName = isSaveUserName;
            pd.IsAutoSignIn = isAutoSignIn;
            pd.SaveToCookie();
            //跳转到相应的应用Url
            Response.Redirect(appsignInUrl);
            return;
        }
        //登录出错时处理
        catch (ETMS.AppContext.BusinessException ex)
        {
            SignInPageData pd = new SignInPageData();
            pd.LoadFromCookie();
            pd.UserName = username;
            //载入登录页(将用户的默认设置传入） 
            string message = ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex);
            ETMS.Utility.JsUtility.AlertMessageBox(message);
            return;
        }
    }
}


/// <summary>
/// 设置或获取URL key
/// </summary>
public class UrlKey
{
    public UrlKey()
    {

    }
    /// <summary>
    /// 计算key
    /// </summary>
    /// <param name="uname"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public string GenerateKey(string uname, string password)
    {
        string key = DesArithmetic.Encrypt(uname, password);
        return key;
    }
    /// <summary>
    /// 检查key,获取账户信息
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public User CheckKey(string key)
    {
        User user = DesArithmetic.Decrypt(key);
        return user;
    }

}
///DES算法描述简介：
/// DES是Data Encryption Standard（数据加密标准）的缩写。它是由IBM公司研制的一种加密算法，
/// 美国国家标准局于1977年公布把它作为非机要部门使用的数据加密标准；
/// 它是一个分组加密算法，他以64位为分组对数据加密。
/// 同时DES也是一个对称算法：加密和解密用的是同一个算法。
/// 它的密匙长度是56位（因为每个第8 位都用作奇偶校验），
/// 密匙可以是任意的56位的数，而且可以任意时候改变．
public class DesArithmetic
{
    public DesArithmetic()
    {

    }

    /// <summary>
    /// 进行DES加密。
    /// </summary>
    /// <param name="pToEncrypt">要加密的字符串。</param>
    /// <param name="sKey">密钥，且必须为8位。</param>
    /// <returns>以Base64格式返回的加密字符串。</returns>
    public static string Encrypt(string userName, string password)
    {
        try
        {
            long timestamp = GetTimestamp();
            int un_length = userName.Length;
            int pwd_length = password.Length;
            int tt_length = timestamp.ToString().Length;

            string plainText = un_length.ToString() + "_" + pwd_length.ToString() + "_" + tt_length.ToString() + "_" + userName + password + timestamp;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                des.Key = key;
                des.IV = iv;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        catch (ArgumentNullException ex)
        {
            return ex.Message;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return ex.Message;
        }
        catch (ArgumentException ex)
        {
            return ex.Message;
        }
        catch (CryptographicException ex)
        {
            return ex.Message;
        }
        catch (NotSupportedException ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// 进行DES解密。
    /// </summary>
    /// <param name="cipherText">要解密的以Base64</param>
    /// <returns>用户信息(uname 登录名 password 密码 Flag[1 访问成功|0 访问失败] Message 详细信息)</returns>
    public static User Decrypt(string cipherText)
    {

        User user = new User();
        user.Uname = "";
        user.Password = "";
        user.Flag = 0;

        try
        {
            byte[] inputByteArray = Convert.FromBase64String(cipherText);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = key;
                des.IV = iv;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string plainText = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();

                string[] pts = plainText.Split(new char[] { '_' }, 4);
                string uname = pts[3].Substring(0, Parser(pts[0]));
                string pwd = pts[3].Substring(Parser(pts[0]), Parser(pts[1]));
                string timestamp = pts[3].Substring(Parser(pts[0]) + Parser(pts[1]), Parser(pts[2]));

                long currentTimestamp = GetTimestamp();

                //if (currentTimestamp - long.Parse(timestamp) > 30 * 1000)
                //{
                //    user.Message = "链接失效";
                //    return user;
                //}

                user.Uname = uname;
                user.Password = pwd;
                user.Flag = 1;
                user.Message = "有效链接";
                return user;
            }
        }
        catch (ArgumentNullException ex)
        {
            user.Message = ex.Message;
            return user;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            user.Message = ex.Message;
            return user;
        }
        catch (ArgumentException ex)
        {
            user.Message = ex.Message;
            return user;
        }
        catch (FormatException ex)
        {
            user.Message = ex.Message;
            return user;
        }
        catch (CryptographicException ex)
        {
            user.Message = ex.Message;
            return user;
        }
        catch (NotSupportedException ex)
        {
            user.Message = ex.Message;
            return user;
        }
    }

    /// <summary>
    /// 封装TryParse
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Parser(string value)
    {
        int res = 0;
        int.TryParse(value, out res);
        return res;
    }
    /// <summary>
    /// 时间戳 即UTC时间
    /// </summary>
    /// <returns></returns>
    public static long GetTimestamp()
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        DateTime nowTime = DateTime.Now;
        long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        return unixTime;
    }


    /// <summary>
    /// 默认密钥向量
    /// </summary>
    private static byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// 密钥
    /// </summary>
    private static byte[] key = { 0xEF, 0xAB, 0x78, 0x90, 0x56, 0x34, 0xCD, 0x12 };
}


/// <summary>
/// 乐学账户
/// </summary>
public class User
{
    private string _uname = string.Empty;
    private string _password = string.Empty;
    private int _flag = 0;
    private string _message = string.Empty;
    /// <summary>
    /// 构造方法
    /// </summary>
    public User()
    {

    }
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="uname">登录名</param>
    /// <param name="password">登录密码</param>
    /// <param name="flag">访问标识</param>
    /// <param name="message">详细信息</param>
    public User(string uname, string password, int flag, string message)
    {
        this.Uname = uname;
        this.Password = password;
        this.Flag = flag;
        this.Message = message;
    }
    /// <summary>
    /// 登录名
    /// </summary>
    public string Uname
    {
        get { return _uname; }
        set { _uname = value; }
    }
    /// <summary>
    /// 登录密码
    /// </summary>
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }
    /// <summary>
    /// 访问成功与否标识(0访问失败,出现错误 1.访问成功 2.无权访问 )
    /// </summary>
    public int Flag
    {
        get { return _flag; }
        set { _flag = value; }
    }
    /// <summary>
    /// 详细信息
    /// </summary>
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }
}