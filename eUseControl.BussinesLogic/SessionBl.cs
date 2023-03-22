using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;


namespace eUseControl.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {

        private readonly UserApi userapi = new UserApi();

        public ULoginResp UserLogin(ULoginData data)
        {
            return userapi.UserLoginAction(data);
        }

        public ULoginResp UserRegister(URegisterData data)
        {
            return userapi.UserRegisterAction(data);
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public ULoginResp ProdRegister(UProductData data)
        {
            return userapi.ProductRegisterAction(data);
        }
    }
}