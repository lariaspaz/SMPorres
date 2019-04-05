using Consultas.CustomAuthentication;
using Consultas.Lib.Security;
using Consultas.Models;
using Consultas.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Consultas.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser((object)loginView.UserName, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Rol.Nombre
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginView.UserName,
                            DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
            return View(loginView);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult ChangePwd()
        {
            return View(new ChangePwd());
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult ChangePwd(ChangePwd model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("", "Los datos ingresados son incorrectos.");
            else
            {
                var repo = new Repositories.AlumnosRepository();
                if (!repo.EsContraseña(Lib.Session.CurrentUserId, model.ContraseñaActual))
                    ModelState.AddModelError("", "La contraseña anterior es incorrecta.");
                if (model.ContraseñaActual == model.ContraseñaNueva)
                    ModelState.AddModelError("", "La nueva contraseña no puede ser igual a la actual.");
                if (model.ContraseñaNueva != model.ContraseñaNuevaRepetida)
                    ModelState.AddModelError("", "Debe reingresar la nueva contraseña.");
                if (model.ContraseñaNueva.Length < Membership.MinRequiredPasswordLength)
                    ModelState.AddModelError("", String.Format("La nueva contraseña debe tener {0} caracteres como mínimo.",
                        Membership.MinRequiredPasswordLength));

                bool min = false, may = false, dig = false;
                foreach (var c in model.ContraseñaNueva)
                {
                    if (Char.IsUpper(c)) may = true;
                    else if (Char.IsLower(c)) min = true;
                    else if (Char.IsDigit(c)) dig = true;
                }
                if (!may || !min || !dig)
                    ModelState.AddModelError("", "La nueva contraseña debe tener al menos una " +
                        "letra minúscula, una letra mayúscula y un número.");

                if (ModelState.IsValid)
                {
                    try
                    {
                        repo.ActualizarYEncriptarContraseña(Lib.Session.CurrentUserId, model.ContraseñaNueva);
                        ModelState.Clear();
                        ViewBag.Message = "La contraseña se actualizó correctamente.";
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                    }
                }
            }
            return View();
        }
    }
}
