using System;
using System.Collections;
using System.Web.Script.Serialization;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Configuration;


namespace ETMS.Components.OnlinePlaying
{

    /**
     *   欢拓语音视频服务开放接口SDK
     */
    public class MTCloud
    {
        //!!!!注意：$openID 以及 $openToken 请咨询欢拓技术人员进行获取。

        /*
        *  合作方ID：欢拓平台的唯一ID
        */
        private string openID = ConfigurationManager.AppSettings["LiveOpenID"];

        /*
        *  合作方秘钥：欢拓平台唯一ID对应的加密秘钥
        */
        private string openToken = ConfigurationManager.AppSettings["LiveOpenToken"];

        /*
        *   欢拓API接口地址
        */
        private string restUrl = ConfigurationManager.AppSettings["LiveRoot"];

        /**
         * 返回的数据格式
         */
        public string format = "json";

        /**
         * SDK版本号(请勿修改)
         */
        private string version = ConfigurationManager.AppSettings["LiveVersion"];

        /**
         * 回调handler
         */
        private Hashtable callbackHandler = null;

        /**
         * 状态码
         */
        public const int CODE_FAIL = -1;            // 失败
        public const int CODE_SUCCESS = 0;          // 成功的状态码，返回其他code均为失败
        public const int CODE_PARAM_ERROR = 10;     // 参数错误
        public const int CODE_SIGN_EXPIRE = 10002;  // 签名过期
        public const int CODE_SIGN_ERROR = 10003;   // 签名验证错误

        /**
         * 用户支持的角色
         */
        public const string ROLE_USER = "user";         // 普通用户
        public const string ROLE_ADMIN = "admin";       // 管理员，助教
        public const string ROLE_SPADMIN = "spadmin";   // 超级管理员，直播器用
        public const string ROLE_GUEST = "guest";       // 游客

        /**
         * 用户定义
         */
        public const int USER_GENDER_UNKNOW = 0;        // 未知
        public const int USER_GENDER_MALE = 1;          // 男性
        public const int USER_GENDER_FEMALE = 2;        // 女性

        /**
         * 主播账户类型
         */
        public const int ACCOUNT_TYPE_MT = 1;           // 欢拓账号类型
        public const int ACCOUNT_TYPE_THIRD = 2;        // 合作方账号类型

        /**
         * 直播记录常量
         */
        public const int LIVE_NO_PLAYBACK = 0;          // 没有直播回放的记录
        public const int LIVE_HAS_PLAYBACK = 1;         // 有直播回放的记录

        /**
         * 语音常量
         */
        public const int VOICE_FLOW_CLOUD = 1;          // 语音云模式
        public const int VOICE_FLOW_LISTEN_ONLY = 2;    // 只听模式

        // 房间模式常量
        public const int ROOM_MODE_VOICE_CLOUD = 1;     // 语音云模式
        public const int ROOM_MODE_BIG = 3;             // 大班模式
        public const int ROOM_MODE_SMALL = 5;           // 小班模式

        public MTCloud(string openID, string openToken)
        {
            if (openID.Length > 0)
            {
                this.openID = openID.Trim();
            }

            if (openToken.Length > 0)
            {
                this.openToken = openToken.Trim();
            }
        }

        public MTCloud()
        {

        }

        /**
         * 获取用户access_token,access_key及房间地址(替代roomGetUrl方法)
         * @param String uid		合作方系统内的用户的唯一ID
         * @param String nickname	用户的昵称
         * @param String role		用户的角色
         * @param String roomid		房间ID
         * @param int expire		返回的地址的有效时间
         * @return String 
         */
        public string userAccess(string uid, string nickname, string role, string roomid, int expire)
        {
            Hashtable options = new Hashtable();
            return userAccess(uid, nickname, role, roomid, expire, options);
        }

        /**
         * 用户进入直播间
         * @param String uid
         * @param String nickname
         * @param String role
         * @param String roomid
         * @param int expire
         * @param Hashtable options 		
         */
        public string userAccess(string uid, string nickname, string role, string roomid, int expire, Hashtable options)
        {
            if (!options.Contains("gender"))
            {
                // 用户性别
                options.Add("gender", MTCloud.USER_GENDER_UNKNOW);
            }
            if (!options.Contains("avatar"))
            {
                // 用户头像
                options.Add("avatar", "");
            }
            Hashtable mParams = new Hashtable();
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("roomid", roomid);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("user.access", mParams);
        }

        /**
         * 用户进入点播
         * @param String uid
         * @param String nickname
         * @param String role
         * @param String liveid
         * @param int expire
         */
        public string userAccessPlayback(string uid, string nickname, string role, string liveid, int expire)
        {
            Hashtable options = new Hashtable();
            return userAccessPlayback(uid, nickname, role, liveid, expire, options);
        }

        /**
         * 用户进入点播
         * @param String uid
         * @param String nickname
         * @param String role
         * @param String liveid
         * @param int expire
         * @param Hashtable options
         */
        public string userAccessPlayback(string uid, string nickname, string role, string liveid, int expire, Hashtable options)
        {
            if (!options.Contains("gender"))
            {
                // 用户性别
                options.Add("gender", MTCloud.USER_GENDER_UNKNOW);
            }
            if (!options.Contains("avatar"))
            {
                // 用户头像地址
                options.Add("avatar", "");
            }

            Hashtable mParams = new Hashtable();
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("liveid", liveid);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("user.access.playback", mParams);
        }

        public string userAccessPlaybackAlbum(string uid, string nickname, string role, string album_id, int expire, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("album_id", album_id);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("user.access.playbackAlbum", mParams);
        }

        /**
         * 获取在线用户列表
         * @param roomid		房间ID
         * @param start_time	查询开始时间，格式: 2015-01-01 12:00:00
         * @param end_time		查询结束时间，格式: 2015-01-01 13:00:00
         */
        public string userOnlineList(string roomid, string start_time, string end_time)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            return call("user.online.list", mParams);
        }

        /**
         * 获取主播管理登录地址
         * @param liveid
         * @param expire
         */
        public string getManagerUrl(string roomid, int expire, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("openID", openID);
            mParams.Add("roomid", roomid);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero) / 1000;

            mParams.Add("timestamp", unixTime);
            mParams.Add("expire", expire);

            if (options.Contains("nickname"))
            {
                mParams.Add("nickname", options["nickname"]);
            }

            if (options.Contains("role"))
            {
                mParams.Add("role", options["role"]);
            }

            mParams.Add("sign", generateSign(mParams));

            JavaScriptSerializer ser = new JavaScriptSerializer();
            String jsonStr = HttpUtility.UrlEncode(ser.Serialize(mParams));

            byte[] b = System.Text.Encoding.Default.GetBytes(jsonStr);
            string code = Convert.ToBase64String(b);
            return "http://open.talk-fun.com/live/manager.php?code=" + code;
        }

        /**
         * 获取直播房间地址(使用userAccess方法替代)
         * @param String uid		合作方系统内的用户的唯一ID
         * @param String nickname	用户的昵称
         * @param String role		用户的角色
         * @param String roomid		房间ID
         * @param int expire		返回的地址的有效时间
         */
        public string roomGetUrl(string uid, string nickname, string role, string roomid, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("roomid", roomid);
            mParams.Add("expire", expire);
            return call("user.register", mParams);
        }

        /**
         * 查询房间信息
         * @param String roomid	房间ID
         */
        public string roomGetInfo(string roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            return call("room.getInfo", mParams);
        }

        /**
         * 创建房间
         * @param String roomName 房间名称
         */
        public string roomCreate(string roomName)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomName", roomName);
            return call("room.create", mParams);
        }

        /**
         * 创建房间，新增管理员密码设置
         * @param roomName
         * @param authKey
         */
        public string roomCreate(string roomName, string authKey)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomName", roomName);
            mParams.Add("authKey", authKey);
            return call("room.create", mParams);
        }

        /**
         * 创建房间
         * @param roomName
         * @param voiceFlow
         * @param authKey
         */
        public string roomCreate(string roomName, int voiceFlow, string authKey)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomName", roomName);
            mParams.Add("voiceFlow", voiceFlow);
            mParams.Add("authKey", authKey);
            return call("room.create", mParams);
        }

        /**
         * 更新房间信息
         * @param roomid
         * @param params			包含字段：roomName，voiceFlow,authKey
         */
        public string roomUpdate(string roomid, Hashtable mParams)
        {
            mParams.Add("roomid", roomid);
            return call("room.update", mParams);
        }

        /**
         * 删除房间
         * @param String roomid 房间ID
         */
        public string roomDrop(string roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            return call("room.drop", mParams);
        }

        /**
         * 获取房间列表
         * @param int page
         * @param int size
         */
        public string roomList(int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("room.list", mParams);
        }

        /**
         * 房间绑定主播
         * @param String roomid	房间ID
         * @param String account 主播账号
         * @param int accountType 主播账号类型
         */
        public string roomBindAccount(string roomid, string account, int accountType)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            return call("room.live.bindAccount", mParams);
        }

        /**
         * 房间取消绑定主播
         * @param String roomid 房间ID
         * @param String account 主播账号
         * @param int accountType 主播账号类型
         */
        public string roomUnbindAccount(string roomid, string account, int accountType)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            return call("room.live.unbindAccount", mParams);
        }

        /**
         * 发送广播
         * @param String roomid  房间ID
         * @param String cms
         * @param HashMap options
         * @return
         * @throws Exception
         */
        public string roomBroadcastSend(string roomid, string cmd, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("cmd", cmd);
            mParams.Add("options", options);
            return call("room.broadcast.send", mParams);
        }

        /**
         *  虚拟用户导入
         *  @param  String roomid  房间ID
         *  @param  ArrayList  userList  [['nickname'=>'xxx', 'avatar'=>'xxx'], ['nickname'=>'xxxx', 'avatar'=>'xxx'], ......]
         */
        public string roomAddRobot(int roomid, ArrayList userList)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("userList", userList);

            return call("room.robot.add", mParams, "POST", null);
        }

        public string roomNoticeRoll(string roomid, string content, string link, int duration)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("content", content);
            mParams.Add("link", link);
            mParams.Add("duration", duration);
            return call("room.notice.roll", mParams);
        }

        /**
         * 获取主播信息
         * @param String account 主播账号
         * @param int accountType 主播账号类型
         */
        public string zhuboGet(string account, int accountType)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            return call("zhubo.get", mParams);
        }

        /**
         * 创建主播
         * @param String account 主播账号			(合作方主播账号的唯一ID)
         * @param String nickname 主播昵称
         * @param int accountType 账号类型		(如果是欢拓的账号类型，account可以为空)
         * @param String password 主播账号密码 
         * @param String description 用户简介
         */
        public string zhuboCreate(string account, string nickname, int accountType, string password, string introduce)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("nickname", nickname);
            mParams.Add("accountType", accountType);
            mParams.Add("password", password);
            mParams.Add("intro", introduce);
            return call("zhubo.create", mParams);
        }

        /**
         * 更新主播信息
         * @param account
         * @param accountType
         * @param nickname
         * @param introduce
         */
        public string zhuboUpdateInfo(string account, int accountType, string nickname, string introduce)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            mParams.Add("nickname", nickname);
            mParams.Add("intro", introduce);
            return call("zhubo.update.info", mParams);
        }

        /**
         * 更新主播密码
         * @param account
         * @param accountType
         * @param password
         */
        public string zhuboUpdatePassword(string account, int accountType, string password)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            mParams.Add("password", password);
            return call("zhubo.update.password", mParams);
        }

        /**
         * 更新主播头像
         * @param account			主播账号
         * @param accountType		账号类型
         * @param filename			图片路径(支持jpeg、jpg，图片大小不超过1M)
         */
        public string zhuboUpdatePortrait(string account, int accountType, string filename)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            string res = call("zhubo.portrait.uploadurl", mParams);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            Hashtable result = ser.Deserialize<Hashtable>(res);
            if ("0" == result["code"].ToString())
            {
                Dictionary<string, object> data = (Dictionary<string, object>)result["data"];

                string url = data["api"].ToString();

                Hashtable file = new Hashtable();
                file.Add("field", data["field"]);
                file.Add("file", filename);
                ArrayList files = new ArrayList();
                files.Add(file);

                return doPost(url, null, files);
            }

            return res;
        }

        /**
         * 删除主播
         * @param String account 主播账号
         * @param int accountType 账号类型
         */
        public string zhuboDel(string account, int accountType)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            mParams.Add("accountType", accountType);
            return call("zhubo.del", mParams);
        }

        /**
         * 主播列表
         * @param int page
         * @param int size
         */
        public string zhuboList(int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("zhubo.list", mParams);
        }

        public string zhuboLogin(string account)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("account", account);
            return call("zhubo.login", mParams);
        }

        /**
         *  根据房间ID获取主播登录地址
         *
         */
        public string zhuboRoomLogin(string roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            return call("zhubo.room.login", mParams);
        }

        /**
         * 获取直播回放记录(需要携带用户信息的，使用userAccessPlayback方法)
         * @param String liveid 直播ID
         * @param int expire 地址有效时间
         */
        public string liveGet(string liveid, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("expire", expire);
            return call("live.get", mParams);
        }

        /**
         * 批量获取直播回放记录
         * @param String[] liveids 直播ID列表 
         * @param int expire 地址有效期
         */
        public string liveGetBatch(string[] liveids, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveids", liveids);
            mParams.Add("expire", expire);
            return call("live.getBatch", mParams);
        }

        /**
         * 获取最新的几个直播记录
         * @param  int   size    每页个数
         */
        public string liveGetLast(int size, int roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("size", size);
            mParams.Add("roomid", roomid);
            return call("live.getlast", mParams);
        }

        /**
         * 获取直播回放记录列表
         * @param String startDate 开始日期
         * @param String endDate 结束日期
         * @param int page	页码
         * @param int size 每页条数
         * @param string playback 是否有回放
         */
        public string liveList(string startDate, string endDate, int page, int size, string playback)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("startDate", startDate);
            mParams.Add("endDate", endDate);
            mParams.Add("page", page);
            mParams.Add("size", size);
            mParams.Add("playback", playback);
            return call("live.list", mParams);
        }

        /**
         * 获取全部直播记录列表
         * @param   int  page   页码(默认:1)
         * @param   int  size   每页个数(默认:10)
         */
        public string liveListAll(int page, int size, string order, string roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("page", page);
            mParams.Add("size", size);
            mParams.Add("order", order);
            mParams.Add("roomid", roomid);
            return call("live.listall", mParams);
        }

        /**
         * 获取直播聊天列表
         * @param liveid
         * @param page
         */
        public string liveMessageList(string liveid, int page)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            return call("live.message", mParams);
        }

        /**
         *  获取直播鲜花记录
         *  @param  String  liveid     直播ID
         *  @param  int     page       页码(默认:1)
         *  @param  int     size       每页个数(默认:10)
         */
        public string liveFlowerList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.flower.list", mParams);
        }

        /**
         * 创建直播回放专辑
         * @param String album_name 专辑名称
         * @param String[] liveids 直播ID列表
         */
        public string liveAlbumCreate(string album_name, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_name", album_name);
            mParams.Add("liveids", liveids);
            return call("live.album.create", mParams);
        }

        /**
         * 获取专辑信息
         * @param String album_id 专辑ID
         * @param int expire 专辑地址有效期
         */
        public string liveAlbumGet(string album_id, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("expire", expire);
            return call("live.album.get", mParams);
        }

        /**
         * 删除专辑
         * @param String album_id 专辑ID
         */
        public string liveAlbumDelete(string album_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            return call("live.album.delete", mParams);
        }

        /**
         * 往专辑里面增加回放记录
         * @param String album_id 专辑ID
         * @param String[] liveids 回放记录列表
         */
        public string liveAlbumAdd(string album_id, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("liveids", liveids);
            return call("live.album.add", mParams);
        }

        /**
         * 从专辑中移除回放记录
         * @param String album_id 专辑ID
         * @param String[] liveids 回放记录列表
         */
        public string liveAlbumRemove(string album_id, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("liveids", liveids);
            return call("live.album.remove", mParams);
        }

        /**
         * 创建一个专辑
         * @param  String  album_name     专辑名称
         */
        public string albumCreate(string album_name, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_name", album_name);
            mParams.Add("liveids", liveids);
            return call("album.create", mParams);
        }

        /**
         * 获取一个直播专辑
         * @param  String album_id        专辑ID
         * @param  int expire          地址有效时间     
         */
        public string albumGet(string album_id, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("expire", expire);
            return call("album.get", mParams);
        }

        /**
         * 删除一个专辑
         * @param  String album_id   专辑ID
         */
        public string albumDelete(string album_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            return call("album.delete", mParams);
        }

        /**
         * 往专辑增加一个回放记录
         * @param  String album_id   专辑ID
         * @param  array liveids  回放记录的课程id
         */
        public string albumAdd(string album_id, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("liveids", liveids);
            return call("album.add", mParams);
        }

        /**
         * 从专辑里面清除某个回放
         * @param String  album_id   专辑ID
         * @param array liveids   回放记录的课程id
         */
        public string albumRemove(string album_id, string[] liveids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("liveids", liveids);
            return call("album.remove", mParams);
        }

        /**
         * 创建一个课程专辑
         * @param  String  album_name     专辑名称
         * @param  array course_ids      课程id
         */
        public string albumCreateCourse(string album_name, string[] course_ids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_name", album_name);
            mParams.Add("course_ids", course_ids);
            return call("album.course.create", mParams);
        }

        /**
         * 往课程专辑增加一个课程回放记录
         * @param  String album_id   专辑ID
         * @param  array course_ids  课程回放记录ID列表
         */
        public string albumAddCourse(string album_id, string[] course_ids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("course_ids", course_ids);
            return call("album.course.add", mParams);
        }

        /**
         * 从课程专辑里面清除某个课程回放
         * @param String  album_id   专辑ID
         * @param array course_ids   回放记录的课程id
         */
        public string albumRemoveCourse(string album_id, string[] course_ids)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("album_id", album_id);
            mParams.Add("course_ids", course_ids);
            return call("album.course.remove", mParams);
        }

        /**
         * 根据房间及时间获取回放记录
         * @param String roomid 房间ID
         * @param String start_time 开始时间 格式:2014-12-26 12:00:00
         * @param int expire 地址有效期
         */
        public string liveRoomGet(string roomid, string start_time, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("start_time", start_time);
            mParams.Add("expire", expire);
            return call("live.room.get", mParams);
        }

        /**
         * 根据房间及时间区间获取回放记录
         * @param String roomid 房间ID
         * @param String start_time 起始区间时间戳  格式：2014-12-26 00:00:00
         * @param String end_time 结束区间时间戳  格式: 2014-12-26 12:00:00
         * @param int expire 有效期
         */
        public string liveRoomList(string roomid, string start_time, string end_time, int expire)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            mParams.Add("expire", expire);
            return call("live.room.list", mParams);
        }

        /**
         * 根据直播ID获取访客列表
         * @param  String liveid      直播ID
         * @param  int page           页码
         * @param  int size           每页个数 
         */
        public string liveVisitorList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.visitor.list", mParams);
        }

        /**
         * 根据直播ID，用户ID获取访客列表
         * @param  String liveid      直播ID
         * @param  String uid         用户ID
         */
        public string liveVisitorGet(string liveid, string uid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("uid", uid);
            return call("live.visitor.get", mParams);
        }

        /**
         * 根据直播ID获取提问列表
         * @param  String  liveid      直播ID
         * @param  int     page        页码
         * @param  int     size        每页个数
         */
        public string liveQuestionList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.question.list", mParams);
        }

        /**
         * 获取音频下载地址
         * @param String  liveid    直播ID
         */
        public string liveAudioDownloadUrl(string liveid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            return call("live.audio.download.url", mParams);
        }

        /**
         * 根据直播ID获取回放访客列表
         * @param  String   liveid      直播ID
         * @param  int      page        页码
         * @param  int      size        每页个数
         */
        public string livePlaybackVisitorList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.playback.visitor.list", mParams);
        }

        /**
         * 按照时间区间获取回放访客列表    (时间区间不能大于7天)
         * @param  String  start_time     开始时间    格式：2016-01-01 00:00:00
         * @param  String  end_time       结束时间    格式：2016-01-02 00:00:00
         * @param  int     page           页码
         * @param  int     size           每页个数
         */
        public string livePlaybackVisitorTimeList(string start_time, string end_time, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.playback.visitor.timelist", mParams);
        }

        /**
         * 根据直播id获取回放视频
         * @param int  liveid 	直播id
         */
        public string livePlaybackVideo(string liveid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            return call("live.playback.video", mParams);
        }

        /**
         * 按照直播ID获取投票列表
         * @param  String   liveid      直播ID
         * @param  int      page        页码
         * @param  int      size        每页个数
         */
        public string liveVoteList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.vote.list", mParams);
        }

        /**
         * 按照投票ID和直播ID获取投票详情
         * @param  int      vid        投票ID
         * @param  int      liveid     直播ID
         * @param  int      page       页码
         * @param  int      size       每页个数
         */
        public string liveVoteDetail(int vid, int liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("vid", vid);
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.vote.detail", mParams);
        }

        /**
          * 按照直播ID获取抽奖列表
          * @param  String   liveid      直播ID
          * @param  int      page        页码
          * @param  int      size        每页个数
          */
        public string liveLotteryList(string liveid, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("live.lottery.list", mParams);
        }

        /**
         * 增加一个直播课程
         * @param String course_name 课程名称
         * @param String account 发起直播课程的主播账号
         * @param String start_time 课程开始时间,格式: 2015-01-10 12:00:00
         * @param String end_time 课程结束时间,格式: 2015-01-10 13:00:00
         */
        public string liveCourseAdd(string course_name, string account, string start_time, string end_time)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_name", course_name);
            mParams.Add("account", account);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            return call("live.course.add", mParams);
        }

        /**
         *  进入一个课程
         *  @param  String  course_id      课程ID 
         *  @param  String  uid            用户唯一ID
         *  @param  String  nickname       用户昵称 
         *  @param  String  role           用户角色，枚举见:ROLE 定义
         *  @param  Int     expire         有效期,默认:3600(单位:秒)
         *  @param  Array   options        可选项，包括:gender:枚举见上面GENDER定义,avatar:头像地址
         */
        public string liveCourseAccess(string course_id, string uid, string nickname, string role, int expire, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("live.course.access", mParams);
        }

        /**
         * 查询课程信息
         * @param String course_id 课程ID
         */
        public string liveCourseGet(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("live.course.get", mParams);
        }

        /**
         *   删除课程
         *   @param String course_id 课程ID
         */
        public string liveCourseDelete(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("live.course.delete", mParams);
        }

        /**
         * 课程列表(将返回开始时间在区间内的课程)
         * @param String start_time 开始时间区间,格式: 2015-01-01 12:00:00
         * @param String end_time 结束时间区间,格式: 2015-01-02 12:00:00
         */
        public string liveCourseList(string start_time, string end_time)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            return call("live.course.list", mParams);
        }

        /**
          *   更新课程信息
          *   @param String course_id 课程ID
          *   @param String account 发起直播课程的主播账号
          *   @param String course_name 课程名称
          *   @param String start_time 课程开始时间,格式:2015-01-01 12:00:00
          *   @param String end_time 课程结束时间,格式:2015-01-01 13:00:00
          */
        public string liveCourseUpdate(string course_id, string account, string course_name, string start_time, string end_time)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("course_name", course_name);
            mParams.Add("account", account);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            return call("live.course.update", mParams);
        }

        /**
         *    发起投票
         *    @param int        roomid      房间ID
         *    @param string     uid         投票发布者，合作方用户ID
         *    @param string     nickname    投票发布者，合作方用户昵称
         *    @param string     title       投票主题
         *    @param string     label       投票标签
         *    @param ArrayList  op          选项，json格式，比如 ["aaa","bbb"]，aaa为第一个选项，bbb为第二个选项
         *    @param int        type        类型，0为单选，1为多选
         *    @param int        optional    若为单选则传1，多选则传的值为多少表示可以选几项
         *    @param string     answer      答案，设置第几项为答案，传入 "0" 表示第一个选项为正确答案，传入 "0,2" 表示第一和第三项为正确答案，不设置答案则传空字符串
         *    @param string     image       图片路径，若要上传图片作为题目，则传入图片
         */
        public string liveVoteAdd(int roomid, string uid, string nickname, string title, string label, ArrayList op, int type, int optional, string answer, string image)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("title", title);
            mParams.Add("label", label);
            mParams.Add("op", op);
            mParams.Add("type", type);
            mParams.Add("optional", optional);
            mParams.Add("answer", answer);

            if ("" != image)
            {
                Hashtable img = new Hashtable();
                img.Add("file", image);
                img.Add("field", "image");

                ArrayList files = new ArrayList();
                files.Add(img);

                return call("live.vote.add", mParams, "POST", files);
            }

            return call("live.vote.add", mParams);
        }

        /**
         * 结束投票
         * @param int       vid         投票ID
         * @param int       showResult  是否显示投票结果，0为不显示，1为显示
         * @param string    uid         投票结束者，合作方用户ID
         * @param string    nickname    投票结束者，合作方用户昵称
         */
        public string liveVoteEnd(int vid, int showResult, string uid, string nickname)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("vid", vid);
            mParams.Add("showResult", showResult);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);

            return call("live.vote.end", mParams);
        }

        /**
         * 获取流地址
         * @param string        liveid      直播ID
         */
        public string liveStreamAddress(string liveid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);

            return call("live.streamAddress", mParams);
        }

        /**
        *   增加一个直播课程
        *   @param String course_name 课程名称
        *   @param String account 发起直播课程的主播账号
        *   @param String start_time 课程开始时间,格式: 2015-01-10 12:00:00
        *   @param String end_time 课程结束时间,格式: 2015-01-10 13:00:00
        */
        public string courseAdd(string course_name, string account, string start_time, string end_time, string nickname, string accountIntro, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_name", course_name);
            mParams.Add("account", account);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            mParams.Add("nickname", nickname);
            mParams.Add("accountIntro", accountIntro);
            mParams.Add("options", options);
            return call("course.add", mParams);
        }

        /**
         *  进入一个课程
         *  @param  String  course_id      课程ID 
         *  @param  String  uid            用户唯一ID
         *  @param  String  nickname       用户昵称 
         *  @param  String  role           用户角色，枚举见:ROLE 定义
         *  @param  Int     expire         有效期,默认:3600(单位:秒)
         *  @param  Array   options        可选项，包括:gender:枚举见上面GENDER定义,avatar:头像地址
         */
        public string courseAccess(string course_id, string uid, string nickname, string role, int expire, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("course.access", mParams);
        }

        /**
         *  进入一个课程回放
         *  @param  String  course_id      课程ID 
         *  @param  String  uid            用户唯一ID
         *  @param  String  nickname       用户昵称 
         *  @param  String  role           用户角色，枚举见:ROLE 定义
         *  @param  Int     expire         有效期,默认:3600(单位:秒)
         *  @param  Array   options        可选项，包括:gender:枚举见上面GENDER定义,avatar:头像地址
         */
        public string courseAccessPlayback(string course_id, string uid, string nickname, string role, int expire, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("role", role);
            mParams.Add("expire", expire);
            mParams.Add("options", options);
            return call("course.access.playback", mParams);
        }

        /**
        *   查询课程信息
        *   @param String course_id 课程ID
        */
        public string courseGet(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.get", mParams);
        }

        /**
        *   删除课程
        *   @param String course_id 课程ID
        */
        public string courseDelete(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.delete", mParams);
        }

        /**
        *   课程列表(将返回开始时间在区间内的课程)
        *   @param String start_time 开始时间区间,格式: 2015-01-01 12:00:00
        *   @param String end_time 结束时间区间,格式: 2015-01-02 12:00:00
        */
        public string courseList(string start_time, string end_time)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            return call("course.list", mParams);
        }

        /**
        *   更新课程信息
        *   @param String course_id 课程ID
        *   @param String account 发起直播课程的主播账号
        *   @param String course_name 课程名称
        *   @param String start_time 课程开始时间,格式:2015-01-01 12:00:00
        *   @param String end_time 课程结束时间,格式:2015-01-01 13:00:00
        */
        public string courseUpdate(string course_id, string account, string course_name, string start_time, string end_time, string nike_name)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("course_name", course_name);
            mParams.Add("account", account);
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            mParams.Add("nickname", nike_name);
            return call("course.update", mParams);
        }

        /**
         *  按照投票ID和课程ID获取投票详情
         *  @param  int      vid        投票ID
         *  @param  int      course_id   课程ID
         *  @param  int      page       页码
         *  @param  int      size       每页个数
         */
        public string courseVoteDetail(int vid, int course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("vid", vid);
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.votes.detail", mParams);
        }

        /**
         *  按照课程ID获取投票列表
         *  @param  String   course_id      课程ID
         *  @param  int      page        页码
         *  @param  int      size        每页个数
         */
        public string courseVoteList(string course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.votes", mParams);
        }

        /**
         *  按照课程ID获取抽奖列表
         *  @param  String   course_id      课程ID
         *  @param  int      page        页码
         *  @param  int      size        每页个数
         */
        public string courseLotteryList(string course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.lottery.list", mParams);
        }

        /**
         *  按照课程ID获取音频下载地址
         *  @param  String   course_id      课程ID
         */
        public string courseAudioDownloadUrl(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.audio.download.url", mParams);
        }

        /**
         *  根据课程ID获取访客列表
         *  @param  String course_id      课程ID
         *  @param  int page           页码
         *  @param  int size           每页个数 
         *  @options Hashtable options 可选参数
         */
        public string courseVisitorList(string course_id, int page, int size, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            mParams.Add("options", options);
            return call("course.visitor", mParams);
        }

        /**
         *  根据课程ID获取回放访客列表
         *  @param  String course_id      课程ID
         *  @param  int page           页码
         *  @param  int size           每页个数 
         *  @options Hashtable options 可选参数
         */
        public string coursePlaybackVisitorList(string course_id, int page, int size, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            mParams.Add("options", options);
            return call("course.visitor.playback", mParams);
        }

        public string courseVisitorListAll(string start_time, string end_time, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("start_time", start_time);
            mParams.Add("end_time", end_time);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.visitor.listall", mParams);
        }

        /**
         *  根据课程ID获取提问列表
         *  @param  String course_id      课程ID
         *  @param  int     page        页码
         *  @param  int     size        每页个数
         */
        public string courseQuestionList(string course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.question.list", mParams);
        }

        /**
         *  获取课程鲜花记录
         *  @param  string  course_id     课程ID
         *  @param  int     page       页码(默认:1)
         *  @param  int     size       每页个数(默认:10)
         */
        public string courseFlowerList(string course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("course.flower.list", mParams);
        }

        /**
         *  获取课程聊天列表
         *  @param  string      course_id         课程id
         *  @param  int         page           页码
         */
        public string courseMessageList(string course_id, int page)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            return call("course.message", mParams);
        }

        /**
         *  获取课件列表
         *  @param  string      course_id         课程id
         *  @param  int         page           页码
         */
        public string courseDocumentList(string course_id, int page)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            return call("course.document", mParams);
        }

        /**
         * 根据课程id获取回放视频
         * @param string 	 course_id 		课程id
         */
        public string courseVideo(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.video", mParams);
        }

        /**
         * 根据课程id获取课程配置
         * @param string 	course_id 		课程id
         */
        public string courseConfig(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.getConfig", mParams);
        }

        public string courseUpdateConfig(string course_id, Hashtable options)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("options", options);
            return call("course.updateConfig", mParams);
        }

        /**
         * 虚拟用户导入
         * @param Int 	course_id 	课程ID
         * @param Array userList 	虚拟用户列表
         */
        public string courseRobotSet(int course_id, ArrayList userList)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("userList", userList);

            Hashtable files = new Hashtable();

            return call("course.robot.set", mParams, "POST", null);
        }

        /**
         * 发布滚动公告
         * @param int       course_id   课程ID
         * @param string    content     滚动通知内容
         * @param string    link        滚动通知链接
         * @param int       duration    滚动通知显示时长(单位：秒)
         */
        public string courseNoticeRoll(int course_id, string content, string link, int duration)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("content", content);
            mParams.Add("link", link);
            mParams.Add("duration", duration);
            return call("course.notice.roll", mParams);
        }

        /**
         * 发布投票
         * @param int       course_id       课程ID
         * @param string    uid             投票发布者，合作方用户ID
         * @param string    nickname        投票发布者，合作方用户昵称
         * @param string    title           投票主题
         * @param string    label           投票标签
         * @param ArrayList op              选项，json格式，比如 ["aaa","bbb"]，aaa为第一个选项，bbb为第二个选项
         * @param int       type            类型，0为单选，1为多选
         * @param int       optional        若为单选则传1，多选则传的值为多少表示可以选几项
         * @param string    answer          答案，设置第几项为答案，传入 "0" 表示第一个选项为正确答案，传入 "0,2" 表示第一和第三项为正确答案，不设置答案则传空字符串
         * @param string    image           图片路径，若要上传图片作为题目，则传入图片
         */
        public string courseVoteAdd(int course_id, string uid, string nickname, string title, string label, ArrayList op, int type, int optional, string answer, string image)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);
            mParams.Add("title", title);
            mParams.Add("label", label);
            mParams.Add("op", op);
            mParams.Add("type", type);
            mParams.Add("optional", optional);
            mParams.Add("answer", answer);

            if ("" != image)
            {
                Hashtable img = new Hashtable();
                img.Add("file", image);
                img.Add("field", "image");

                ArrayList files = new ArrayList();
                files.Add(img);

                return call("course.votes.add", mParams, "POST", files);
            }

            return call("course.votes.add", mParams);
        }

        /**
         * 结束投票
         * @param int       vid         投票ID
         * @param int       showResult  是否显示投票结果，0为不显示，1为显示
         * @param string    uid         投票结束者，合作方用户ID
         * @param string    nickname    投票结束者，合作方用户昵称
         */
        public string courseVoteEnd(int vid, int showResult, string uid, string nickname)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("vid", vid);
            mParams.Add("showResult", showResult);
            mParams.Add("uid", uid);
            mParams.Add("nickname", nickname);

            return call("course.votes.end", mParams);
        }

        /**
         *  添加剪辑
         *  @param Int     liveid      直播ID
         *  @param String  name        剪辑名称
         *  @param ArrayList    time        剪辑时间，array(array('start'=>60,'end'=>180))
         *  @param Int     isRelated   是否关联源直播，默认不关联
         */
        public string clipAdd(int liveid, string name, ArrayList time, int isRelated)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("liveid", liveid);
            mParams.Add("name", name);
            mParams.Add("time", time);
            mParams.Add("isRelated", isRelated);
            return call("clip.add", mParams);
        }

        /**
         *  修改剪辑
         *  @param Int     clipid      剪辑ID
         *  @param String  name        剪辑名称
         *  @param Array   time        剪辑时间，array(array('start'=>60,'end'=>180))
         *  @param Int     isRelated   是否关联源直播，默认不关联
         */
        public string clipUpdate(int clipid, string name, ArrayList time, int isRelated)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("clipid", clipid);
            mParams.Add("name", name);
            mParams.Add("time", time);
            mParams.Add("isRelated", isRelated);
            return call("clip.update", mParams);
        }

        /**
         *  删除剪辑
         *  @param Int     clipid      剪辑ID
         */
        public string clipDelete(int clipid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("clipid", clipid);
            return call("clip.delete", mParams);
        }

        /**
         *  获取剪辑信息
         *  @param Int     clipid      剪辑ID
         */
        public string clipGet(int clipid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("clipid", clipid);
            return call("clip.get", mParams);
        }

        /**
         *  获取剪辑列表
         *  @param Int     page      页码
         *  @param Int     size      条数
         */
        public string clipList(int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("clip.list", mParams);
        }

        /**
         *  根据课程id获取剪辑列表
         *  @param Int     course_id 课程id
         *  @param Int     page      页码
         *  @param Int     size      条数
         */
        public string clipListByCid(int course_id, int page, int size)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("page", page);
            mParams.Add("size", size);
            return call("clip.course.list", mParams);
        }

        /**
         *  添加剪辑
         *  @param Int     course_id    课程ID
         *  @param String  name        剪辑名称
         *  @param Json    time        剪辑时间，array(array('start'=>60,'end'=>180))
         *  @param Int     isRelated   是否关联源直播，默认不关联
         */
        public string clipAddByCid(int course_id, string name, ArrayList time, int isRelated)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            mParams.Add("name", name);
            mParams.Add("time", time);
            mParams.Add("isRelated", isRelated);
            return call("clip.course.add", mParams);
        }

        /**
         * 上传文档
         * @param roomid		房间ID
         * @param files			文件 {"file":"文件路径","name":"文件名"}
         */
        public string documentUpload(string roomid, Hashtable file)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            mParams.Add("name", file["name"]);

            string res = call("document.uploadurl.get", mParams);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            Hashtable result = ser.Deserialize<Hashtable>(res);
            if ("0" == result["code"].ToString())
            {
                Dictionary<string, object> data = (Dictionary<string, object>)result["data"];

                string url = data["api"].ToString();

                Hashtable _file = new Hashtable();
                _file.Add("field", data["field"]);
                _file.Add("file", file["file"]);
                ArrayList files = new ArrayList();
                files.Add(_file);

                return doPost(url, null, files);
            }

            return res;
        }

        /**
         * 课件下载地址
         * @param  intval id 开放平台的文档ID
         */
        public string documentDownload(int id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("id", id);
            return call("document.downloadurl.get", mParams);
        }

        /**
         * 课件列表
         * @param  intval roomid 根据房间id获取课件列表
         */
        public string documentList(int roomid)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("roomid", roomid);
            return call("document.list", mParams);
        }

        /**
         * 课件详细信息
         * @param  intval id 根据课件id获取课件详细信息
         */
        public string documentGet(int id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("id", id);
            return call("document.get", mParams);
        }

        /**
         * 创建部门
         * @param String  departmentName 	部门名称
         */
        public string departmentCreate(string departmentName)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("departmentName", departmentName);
            return call("department.create", mParams);
        }

        /**
         * 更新部门信息
         * @param  int 		departmentId 	部门id
         * @param  String 	departmentName	部门名称
         */
        public string departmentUpdate(int departmentId, string departmentName)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("departmentId", departmentId);
            mParams.Add("departmentName", departmentName);
            return call("department.update", mParams);
        }

        /**
         * 删除部门 
         * @param  int 	departmentId 	部门id
         */
        public string departmentDelete(int departmentId)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("departmentId", departmentId);
            return call("department.delete", mParams);
        }

        /**
         * 获取部门信息 
         * @param 	int 	departmentId 	部门id
         */
        public string departmentGet(int departmentId)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("departmentId", departmentId);
            return call("department.get", mParams);
        }

        /**
         * 批量获取部门信息 
         * @param 	String[] 	departmentIds 	部门id数组
         */
        public string departmentGetBatch(string[] departmentIds)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("departmentIds", departmentIds);
            return call("department.getBatch", mParams);
        }

        public void registerCallbackHandler(Hashtable callbackHandler)
        {
            this.callbackHandler = callbackHandler;
        }

        public string call(string cmd, Hashtable mParams)
        {
            return this.call(cmd, mParams, "GET", null);
        }

        // 构造请求串
        public string call(string cmd, Hashtable mParams, string httpMethod, ArrayList files)
        {
            // 构造系统参数
            Hashtable sysParams = new Hashtable();

            sysParams.Add("openID", this.openID);
            // 获取时间戳
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero) / 1000;
            sysParams.Add("timestamp", unixTime);
            sysParams.Add("ver", this.version);
            sysParams.Add("format", this.format);
            sysParams.Add("cmd", cmd);

            JavaScriptSerializer ser = new JavaScriptSerializer();

            String jsonStr;

            if ("POST" == httpMethod)
            {
                jsonStr = HttpUtility.UrlEncode(ser.Serialize(mParams));
            }
            else {
                jsonStr = ser.Serialize(mParams);
            }

            sysParams.Add("params", jsonStr);
            // 签名
            sysParams.Add("sign", this.generateSign(sysParams));

            if ("POST" == httpMethod)
            {
                return doPost(this.restUrl, sysParams, files);
            }
            else {
                // 构造请求URL
                string resurl = "";
                resurl = this.restUrl + "?" + this.hashtableToQueryString(sysParams);
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

        /** 
         * 构造欢拓云签名
         * @param params 业务参数
         * @return string
         */
        public string generateSign(Hashtable mParams)
        {
            mParams.Remove("sign");

            string keyStr = "";

            List<string> lst = new List<string>();
            foreach (var _key in mParams.Keys)
            {
                lst.Add(_key.ToString());
            }
            lst.Sort();
            foreach (var item in lst)
            {
                keyStr += item + mParams[item.ToString()].ToString();
            }
            keyStr += this.openToken;
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(keyStr, "MD5").ToLower();
        }

        // 将Hashtable中的参数及对应值转换为查询字符串
        private string hashtableToQueryString(Hashtable param)
        {
            string retval = "";

            foreach (string key in param.Keys)
            {
                if ("" == retval)
                {
                    retval = key + "=" + param[key];
                }
                else {
                    retval += "&" + key + "=" + param[key];
                }
            }

            return retval;
        }

        /// <summary>
        /// 启动直播器
        /// </summary>
        /// <param name="course_id"></param>
        /// <returns></returns>
        public string courseLaunch(string course_id)
        {
            Hashtable mParams = new Hashtable();
            mParams.Add("course_id", course_id);
            return call("course.launch", mParams);
        }
    }
}




