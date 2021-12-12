using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Security.Cryptography;
using CharmingNew.Models;
using System.Diagnostics.CodeAnalysis;

namespace CharmingNew.Controllers
{
    public class HomeController : Controller
    {
        CharmingNew.Models.charmingEntities1 db = new Models.charmingEntities1();
        SHA256 sha256 = new SHA256CryptoServiceProvider();


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Portfolio()
        {
            return View();
        }
        
        public ActionResult PortfolioZUO()
        {

            return View();
        }
       

        public ActionResult Member()
        {

            return View();
        }
        


        public ActionResult Order()
        {
            
            if (Session["userName"] == null || Session["userName"].ToString() == "")
            {
                return Redirect("Login");
            }
            else
            {
               

                return View();
            }
        }

        [HttpPost]
        public ActionResult Order(Models.order orderClient)
        {
           
            Models.order orderServer  = new Models.order()                              
            {
                Email = orderClient.Email,
                OrderBy = orderClient.OrderBy,
                OrderDate = orderClient.OrderDate,
                PackDate = orderClient.PackDate,
                Number = orderClient.Number,
                HeadTable = orderClient.HeadTable,
                EndTable = orderClient.EndTable,
            };
            db.orders.Add(orderServer);          /*在db裡面新增 剛剛 new出來的 物件*/
            db.SaveChanges();

            
            return Redirect("http://localhost:57260/Home/OrderList");

        }
  
        
        public ActionResult OrderList()
        {

            var query = from o in db.orders
                        where o.OrderId > 0
                        orderby o.OrderId descending
                        select o;

            var dataList = query.FirstOrDefault();

            //var dataList = query.FirstOrDefault();   SQL LINQ不支援
            ViewBag.p = dataList;

          
            return View();
            
        }


        public ActionResult Login()
        {
            if (Session["userName"] != null)
            {
                Session.Clear();
                return Redirect("Login");
            }
            else
            {
                return View();
            }


        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "login")]
        public ActionResult Login(Models.userN accountClient)
        {
            Session["userName"] = accountClient.Email;


            byte[] source = System.Text.Encoding.Default.GetBytes(accountClient.Password);  //把使用者輸入的password轉成byte
            byte[] crypto = sha256.ComputeHash(source);                              //進行SHA256加密
            string password_sha256 = Convert.ToBase64String(crypto);                 //把加密後的字串從Byte[]轉為字串
            CharmingNew.Models.userN login = new Models.userN();

           
            var query = (from o in db.userNs where o.Email == accountClient.Email && o.Password == password_sha256 select o).Count();            /*Count把找出來的結果顯示成比數 代表找出7筆資料*/
            if (query == 0)                                                                             /*去找資料庫 如果有找到Email一樣的會有1筆資料*/
            {                                                                                                        /* 判斷如果有1筆資料 就...*/

                TempData["message"] = "帳號密碼錯誤, 請您再輸入一次!";
                return View();

            }

            else
            {
                TempData["message"] = "登入成功";
                return RedirectToAction("Order", "Home");
            }








        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "signup")]
        public ActionResult signup(Models.userN accountClient)
        {
            var query = (from a in db.userNs
                         where a.Email == accountClient.Email
                         select a).Count();
            if (query < 1 )
            {

                byte[] source = System.Text.Encoding.Default.GetBytes(accountClient.Password);  //把使用者輸入的password轉成byte
                byte[] crypto = sha256.ComputeHash(source);                              //進行SHA256加密
                string password_sha256 = Convert.ToBase64String(crypto);                 //把加密後的字串從Byte[]轉為字串
                Models.userN accServer = new Models.userN()                                //新增一個會員的 物件
                {
                    UserName = accountClient.UserName,
                    Email = accountClient.Email,
                    Phone = accountClient.Phone,
                    Password = password_sha256

                };
                db.userNs.Add(accServer);                                                  /*在db裡面的會員表 新增 剛剛 new出來的 物件*/
                db.SaveChanges();                                                        //SaveChanges 存回server



                TempData["message"] = "註冊成功,請重新登入";
                return RedirectToAction("Order", "Home");

                /*結束回去那頁*/
            }

            else {


                TempData["message"] = "此信箱已註冊會員!";
                return Redirect("Login");



            }

        }


    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var isValidName = false;
            var keyValue = string.Format("{0}:{1}", Name, Argument);
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value != null)
            {
                controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                isValidName = true;
            }

            return isValidName;
        }
















    }
}