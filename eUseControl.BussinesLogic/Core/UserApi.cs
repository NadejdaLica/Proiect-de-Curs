using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web;
using AutoMapper;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;



namespace eUseControl.BusinessLogic.Core
{
    public class UserApi
    {
        internal ULoginResp CheckUserIfExist(ULoginData data)
        {
            var uC = data.Credential;
            //SELECT FROM USER WHERE uC == >>>

            return new ULoginResp { Status = true };
        }

        internal ULoginResp UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();

            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
        }

        internal ULoginResp ProductRegisterAction(UProductData data)
        {
            DbProduct prod_name_result;
            var prod = new DbProduct();
            prod.Prod_Name = data.Prod_Name;
            prod.Prod_Desc = data.Prod_Desc;
            prod.Prod_Price = data.Prod_Price;
            prod.Prod_Id = data.Prod_Id;
            prod.LastEditTime = data.LastEditTime;

            try
            {
                using (var db = new ProductContext())
                {
                    prod_name_result = db.Products.FirstOrDefault(u => u.Prod_Name == data.Prod_Name);
                    if (prod_name_result == null)
                    {
                        db.Products.Add(prod);
                        db.SaveChanges();
                        return new ULoginResp() { Status = true };
                    }
                    else
                    {
                        return new ULoginResp()
                        {
                            Status = false,
                            StatusMsg = "This product already exists!"
                        };
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        internal ULoginResp UserRegisterAction(URegisterData data)
        {
            UDbTable name_result;
            UDbTable email_result;
            var user = new UDbTable();
            user.Email = data.Email;
            user.Username = data.Username;
            user.Password = data.Password;
            user.Id = data.Id;
            user.LastLogin = data.RegisterDateTime;
            user.Level = URole.User;
            try
            {
                using (var db = new UserContext())
                {
                    name_result = db.Users.FirstOrDefault(u => u.Username == data.Username);
                    email_result = db.Users.FirstOrDefault(e => e.Email == data.Email);
                    if (name_result == null && email_result == null)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        return new ULoginResp { Status = true };
                    }
                    else
                    {
                        if (name_result != null)
                        {
                            return new ULoginResp
                            {
                                Status = false,
                                StatusMsg = "Invalid Username"
                            };
                        }
                        else
                        {
                            return new ULoginResp
                            {
                                Status = false,
                                StatusMsg = "Invalid Email"
                            };
                        }

                    }

                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            UDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);
            return userminimal;
        }




        internal List<ProductData> MyOrders(int UserId)
        {
            //SELECT IF USER EXIST

            //SELECT * FROM PRODUCT WHERE x.UserID = UserID


            return new List<ProductData>();
        }
    }
}

