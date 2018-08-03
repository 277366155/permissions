using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using P.PContext.Interface;
using P.Service.Interface;
using P.Model;
using P.Common.Tools;
using P.Model.ViewModel;
using P.Repository.Interface;

namespace P.Service.CoreService
{
    /// <summary>
    /// 账户业务类
    /// </summary>
    public class AccountService : CoreServiceBase, IAccountService
    {

        private readonly IUserRepository _UserRepository;
        private readonly IRoleService _RoleService;

        public AccountService(IUserRepository userRepository, IRoleService roleService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._UserRepository = userRepository;
            this._RoleService = roleService;
        }
        public IQueryable<User> Users
        {
            get
            {
                return _UserRepository.DbSet;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginVM">登录模型信息</param>
        /// <returns>登录操作结果</returns>
        public Result Login(LoginVM loginVM)
        {
            PublicHelper.CheckArgument(loginVM, "loginVM");
            User user = _UserRepository.GetEntitiesByEager(new List<string> { "Roles", "UserGroups" }).SingleOrDefault(m => m.UserName == loginVM.LoginName.Trim());
            Result result;
            if (user == null)
            {
                result = new Result(ResultType.QueryNull, "指定账号的用户不存在");
            }
            else if (user.Password != EncryptionHelper.GetMd5Hash(loginVM.Password.Trim()))
            {
                result = new Result(ResultType.Warning, "登录密码不正确。");
            }
            else
            {
                result = new Result(ResultType.Success, "登录成功。", user);
                #region 设置用户权限缓存
                var roleIdsByUser = user.Roles.Select(r => r.Id).ToList();
                var roleIdsByUserGroup = user.UserGroups.SelectMany(g => g.Roles).Select(r => r.Id).ToList();
                roleIdsByUser.AddRange(roleIdsByUserGroup);
                var roleIds = roleIdsByUser.Distinct().ToList();
                List<Permission> permissions = _RoleService.Roles.Where(t => roleIds.Contains(t.Id) && t.Enabled == true).SelectMany(c => c.Permissions).Distinct().ToList();
                var strKey = CacheKey.StrPermissionsByUid + "_" + user.Id;
                //设置Cache滑动过期时间为1天
                CacheHelper.SetCache(strKey, permissions, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0, 0));
                #endregion
            }
            if (result.ResultType != ResultType.Success) return result;
            User userTemp = (User)result.AppendData;
            DateTime expiration = loginVM.IsRememberLogin
                ? DateTime.Now.AddDays(14)
                : DateTime.Now.Add(FormsAuthentication.Timeout);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, //指定版本号：可随意指定
                userTemp.UserName,//登录用户名：对应 Web.config 中 <allow users="Admin" … /> 的 users 属性
                DateTime.Now,  //发布时间
                expiration, //失效时间
                true,//是否为持久 Cookie
                userTemp.Id.ToString(), //用户数据：可用 ((System.Web.Security.FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData 获取
                FormsAuthentication.FormsCookiePath); //指定 Cookie 为 Web.config 中 <forms path="/" … /> path 属性，不指定则默认为“/”
            string str = FormsAuthentication.Encrypt(ticket); //加密身份验票
            //声明一个 Cookie，名称为 Web.config 中 <forms name=".APSX" … /> 的 name 属性，对应的值为身份验票加密后的字串
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str);
            if (loginVM.IsRememberLogin)
            {
                cookie.Expires = DateTime.Now.AddDays(14);//此句非常重要，少了的话，就算此 Cookie 在身份验票中指定为持久性 Cookie ，也只是即时型的 Cookie 关闭浏览器后就失效；
            }
            HttpContext.Current.Response.Cookies.Set(cookie); //或Response.Cookies.Add(ck);添加至客户端
            result.AppendData = null;
            return result;
        }

        /// <summary>
        ///  用户退出
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return;
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

    }
}
