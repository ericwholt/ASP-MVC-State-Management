using StateManagement.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StateManagement.Controllers
{
    public class HomeController : Controller
    {
        public void LoadUsers()
        {
            List<User> users = new List<User>();
            User u1 = new User();
            u1.UserName = "Johnny";
            u1.Password = "Abcd1234";
            u1.Email = "fake@email.com";
            u1.Age = 23;

            User u2 = new User();
            u2.UserName = "David";
            u2.Password = "Abcd1234";
            u1.Email = "fake@email.com";
            u1.Age = 12;

            User u3 = new User();
            u3.UserName = "Leon";
            u3.Password = "Abcd1234";
            u1.Email = "fake@email.com";
            u1.Age = 19;

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            Session["Users"] = users;
        }
        public ActionResult Index()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            return View();
        }

        public ActionResult Registration()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            ViewBag.Message = "Your registration page.";

            return View();
        }

        public ActionResult RegisterUser(User u)
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            if (Session["User"] != null)
            {
                Session["Error"] = "user wasn't null don't know why you are here";
                return RedirectToAction("Profile");
            }
            if (u.UserName == null || u.UserName == "")
            {
                Session["Error"] = "A User Name must be entered, field cannot be empty";
                return RedirectToAction("Registration");
            }
            else if (u.Password == null || u.Password == "")
            {
                Session["Error"] = "A password must be 8 characters, Have a Upper and Lower, and Have a digit";
                return RedirectToAction("Registration");
            }
            else if (u.Email == null || u.Email == "")
            {
                Session["Error"] = "A User Name must be entered, field cannot be empty";
                return RedirectToAction("Registration");
            }
            else
            {
                List<User> users = (List<User>)Session["Users"];
                users.Add(u);
                Session["User"] = u;
                return RedirectToAction("Profile");
            }
        }

        public ActionResult Login()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            if (Session["User"] != null)
            {
                return RedirectToAction("Profile");
            }
            Session["Error"] = null;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(User u)
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            if (Session["User"] == null)
            {
                if (LogUserIn(u))
                {
                    return RedirectToAction("Profile");

                }
                else
                {

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Profile");
            }
        }

        public bool LogUserIn(User u)
        {
            List<User> users = (List<User>)Session["Users"];

            foreach (User user in users)
            {

                if (u.UserName == user.UserName && u.Password == user.Password)
                {
                    Session["Error"] = null;
                    Session["User"] = user;
                    return true;
                }
                else
                {
                    Session["Error"] = "Incorrect Username or Password";
                    return false;
                }
            }
            return false;
        }
        public ActionResult Profile(User u)
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            if(Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            Session["User"] = null;
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Users"] == null)
            {
                LoadUsers();
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}