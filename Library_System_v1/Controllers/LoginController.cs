using Domain.Entities;
using Library_System_v1.Models;
using MongoDB.Bson;
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
            #endregion -- END --

            try
            {
                //_adminMaster.adEmail = _logModel.Email;
                //_adminMaster.adPassword = _logModel.Password;

                if (_logModel != null && !string.IsNullOrEmpty(_logModel.Email) && !string.IsNullOrEmpty(_logModel.Password))
                {
                    var context = new MongoDataContext();
                    //AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByCustomAsync(new AdminMaster(), _logModel.Email);
                    AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByCustomAsync(x => x.adEmail != null, "adEmail", _logModel.Email);
                    if (logDetails != null)
                    {
                        ObjectId _useId = new ObjectId();
                        _useId = logDetails.Id;

                        OldHASHValue = logDetails.PasswordHashKey; //_logModel.PasswordHashKey == null ? new Cipher().Encrypt(_logModel.Email + _logModel.Password) : logDetails.PasswordHashKey;
                        if (OldHASHValue != null)
                        {
                            bool isLogin = CompareHashValue(_logModel.Email.ToString(), _logModel.Password, OldHASHValue);
                            if (isLogin)
                            {
                                //Login Success
                                //For Set Authentication in Cookie (Remeber ME Option)
                                SignInRemember(logDetails.adEmail.ToString(), false);

                                Session["User_Email"] = logDetails.adEmail;
                                Session["User_Name"] = logDetails.adName;
                                Session["UserID"] = _useId.ToString();

                                //Set A Unique ID in session
                                if (logDetails.adCode == "Admin")
                                {
                                    return Json(new { redirectionURL = "/Admin/adDashboard/Index", logDetails = logDetails }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { redirectionURL = "/Customer/Admin_Category", logDetails = logDetails }, JsonRequestBehavior.AllowGet);
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

            return Json(new { error = "Invalid Login Credentails. Please check your account or try forget password if your existing user. Else Contact to your Admin." }, JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Index");
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
        public async Task<ActionResult> forgetPassword(ForgetPasswordModel _forgetPasswordModel)
        {
            var context = new MongoDataContext();
            AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByCustomAsync(x => x.adEmail != null, "adEmail", _forgetPasswordModel.Email);
            if (logDetails != null)
            {
                Session["fpEmail"] = _forgetPasswordModel.Email;
                var OTP = new GenerateOTP().OTPGenerate(true, 6);
                Session["OtpCode"] = new Cipher().Encrypt(OTP + "&" + logDetails.adEmail);
               // new SMSsend().sendSMS(logDetails.adPhone, "Hi " + logDetails.adName + ", please find the OTP code : " + new GenerateOTP().OTPGenerate(true, 6));
                return Json(new { obj = logDetails, otpCode = OTP }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "Email Doesn't match." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyOtp(ForgetPasswordModel _fPass)
        {
            try
            {
                if (!string.IsNullOrEmpty(_fPass.Otp) && !string.IsNullOrEmpty(Session["OtpCode"].ToString()))
                {
                    if (Session["OtpCode"].ToString() == new Cipher().Encrypt(_fPass.Otp + "&" + Session["fpEmail"].ToString()))
                    {
                        var context = new MongoDataContext();
                        AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByCustomAsync(x => x.adEmail != null, "adEmail", Session["fpEmail"].ToString());
                        if (logDetails != null)
                        {
                            return Json(new { obj = logDetails }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { error = "Invalid OTP." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> PasswordReset(LoginModel _lgModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(_lgModel.Password))
                {
                    var context = new MongoDataContext();
                    AdminMaster logDetails = await new GenericRepository<AdminMaster>(context).GetByCustomAsync(x => x.adEmail != null, "adEmail", Session["fpEmail"].ToString());
                    if(logDetails != null)
                    {
                        ObjectId _id = logDetails.Id;
                        logDetails.adPassword = new Cipher().Encrypt(_lgModel.Password);
                        logDetails.PasswordHashKey = new Cipher().Encrypt(Session["fpEmail"].ToString() + _lgModel.Password);
                        await new GenericRepository<AdminMaster>(context).SaveAsync(logDetails);

                        return Json(new { obj = string.Empty }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(new { error = "Unable to change the password. Server ERROR." }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}