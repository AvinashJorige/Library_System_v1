using Domain.Entities;
using Library_System_v1.Models;
using RepositoryDB;
using RepositoryDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utilities;

namespace Library_System_v1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();
            return View();
        }

        #region == Login Function ==

        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        //POST: Login Function 
        [HttpPost]
        public async Task<ActionResult> Index(LoginModel _logModel)
        {
            #region -- Variables Declaractions -- 
            string OldHASHValue = string.Empty;
            var context = new MongoDataContext();
            #endregion -- END --

            try
            {
                //_adminMaster.adEmail = _logModel.Email;
                //_adminMaster.adPassword = _logModel.Password;

                if (_logModel != null && string.IsNullOrEmpty(_logModel.Email) && string.IsNullOrEmpty(_logModel.Password))
                {
                    AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByIdAsync(_logModel.Email);
                    if (logDetails != null)
                    {
                        OldHASHValue = logDetails.PasswordHashKey == null ? new Cipher().Encrypt(logDetails.adEmail + logDetails.adPassword) : logDetails.PasswordHashKey;
                        if(OldHASHValue != null)
                        {
                            bool isLogin = CompareHashValue(_logModel.Email.ToString(), _logModel.Password, OldHASHValue);
                            if (isLogin)
                            {
                                //Login Success
                                //For Set Authentication in Cookie (Remeber ME Option)
                                SignInRemember(logDetails.adEmail.ToString(), false);

                                Session["User_Email"] = logDetails.adEmail;
                                Session["User_Name"] = logDetails.adName;
                                Session["UserID"] = logDetails.Id;

                                //Set A Unique ID in session
                                if (logDetails.UserRole == "Admin")
                                {
                                    return Json(new { redirectionURL = "/Admin/Dashboard/Index" }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { redirectionURL = "/Customer/Admin_Category"}, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }

        public bool CompareHashValue(string pharse1, string pharse2, string OldHASHValue)
        {
            try
            {
                string expectedHashString = new Cipher().Encrypt((pharse1 + pharse2));

                return (OldHASHValue == expectedHashString);
            }
            catch
            {
                return false;
            }
        }

        //GET: SignInAsync
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            try
            {
                // Clear any lingering authencation data
                FormsAuthentication.SignOut();

                // Write the authentication cookie
                FormsAuthentication.SetAuthCookie(userName, isPersistent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("loginAccess", "loginAccess");
            }
            catch
            {
                throw;
            }
        }


        //POST: Logout
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToAction("loginAccess");
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region == Forget Password == 
        // GET: Forgot Password
        public ActionResult forgetPassword()
        {
            return View();
        }

        // POST: Forgot Password
        [HttpPost]
        public ActionResult forgetPassword(ForgetPasswordModel _forgetPasswordModel)
        {
            return View();
        }
        #endregion
    }
}