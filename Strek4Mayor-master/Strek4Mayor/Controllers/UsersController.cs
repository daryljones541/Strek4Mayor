using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.Text;
using Microsoft.Owin.Security;

namespace Strek4Mayor.Controllers
{
    public class UsersController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();
        private UserManager<User> userManager = new UserManager<User>(
                new UserStore<User>(new Strek4MayorContext()));

        // GET: Users
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [AllowAnonymous]
        public ActionResult Login()
        {
            var viewModel = new LoginVM();
            var getURI = Request.QueryString;
            if (getURI["ReturnUrl"] != null) viewModel.ReturnUrl = HttpUtility.UrlDecode(getURI["ReturnUrl"]);
            HttpCookie myCookie = Request.Cookies["strek4mayor"];
            if (myCookie != null)
            {
                if (!string.IsNullOrEmpty(myCookie.Values["userid"]))
                {
                    string encryptedLogin = myCookie.Values["userid"].ToString();
                    byte[] stream = HttpServerUtility.UrlTokenDecode(encryptedLogin);
                    byte[] decodedValue = MachineKey.Unprotect(stream);
                    viewModel.Username = Encoding.UTF8.GetString(decodedValue);
                    viewModel.Remember = true;
                }
            }
            return View(viewModel);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "Username,Password,Remember,ReturnUrl")]LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = userManager.Find(model.Username, model.Password);

            if (user != null)
            {
                var identity = userManager.CreateIdentity(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                if (model.Remember)
                {
                    byte[] stream = Encoding.UTF8.GetBytes(user.UserName);
                    byte[] encodedValue = MachineKey.Protect(stream);
                    string encryptedLogin = HttpServerUtility.UrlTokenEncode(encodedValue);
                    HttpCookie myCookie = new HttpCookie("strek4mayor");
                    myCookie.Values.Add("userid", encryptedLogin);
                    myCookie.Expires = System.DateTime.Now.AddHours(336);
                    Response.Cookies.Add(myCookie);
                }
                if (string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl))
                {
                    if (userManager.IsInRole(user.Id, "Admin")) return RedirectToAction("Index", "Admin");
                    else return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }

            // user authentication failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                UserName = model.Username,
                Name = model.Name,
                Email = model.Email,
                Contact = model.Contact
            };

            var result = userManager.Create(user, model.Password);

            if (result.Succeeded)
            {
                SignIn(user);
                Session["user"] = user.Name;
                string controller = (string)Session["controller"];
                string action = (string)Session["action"];
                if (controller == null || action == null) return RedirectToAction("Index", "Home");
                else
                {
                    if (Session["parameter"] == null) return RedirectToAction(action, controller);
                    else return RedirectToAction(action + "/" + (string)Session["parameter"], controller);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private void SignIn(User user)
        {
            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            GetAuthenticationManager().SignIn(identity);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        } 

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.GetUserId() == id || User.IsInRole("Admin"))
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,Name,Contact")] User user, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.GetUserId() == id || User.IsInRole("Admin"))
            {
                User oldUser = db.Users.Find(id);
                if (oldUser == null)
                {
                    return HttpNotFound();
                }
                oldUser.Email=user.Email;
                oldUser.Contact=user.Contact;
                oldUser.UserName=user.UserName;
                oldUser.Name=user.Name;
                db.Entry(oldUser).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Changes saved.";
            }
            else return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            string controller = (string)Session["controller"];
            string action = (string)Session["action"];
            if (controller == null || action == null) return RedirectToAction("index", "home");
            else
            {
                if (Session["parameter"] == null) return RedirectToAction(action, controller);
                else return RedirectToAction(action + "/" + (string)Session["parameter"], controller);
            }
        }

        public ActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.GetUserId() == id || User.IsInRole("Admin"))
            {
                ChangePasswordVM viewModel = new ChangePasswordVM();
                return View(viewModel);
            }
            else return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "NewPassword,OldPassword,ConfirmPassword")] ChangePasswordVM viewModel, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.GetUserId() == id || User.IsInRole("Admin"))
            {
                User dbUser = db.Users.Find(id);
                var user = userManager.Find(dbUser.UserName, viewModel.OldPassword);
                if (user != null)
                {
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, viewModel.NewPassword);
                    ViewBag.Message = "Password successfully update.";
                }
                else ViewBag.Message = "Incorrect old password.";
                return View(viewModel);
            }
            else return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
