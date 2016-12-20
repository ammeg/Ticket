using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class EmailAutoCompleteController : Controller
    {



        public ActionResult Autocomp()
        {

            return View();

        }

        public ActionResult GetEmailIds(string query)
        {

            query = query.Replace(" ", "");

            if (query.Length > 1)
            {

                int op = query.LastIndexOf(",");

                query = query.Substring(op + 1);

            }

            List<nameemail> obj = new List<nameemail>();

            obj.Add(new nameemail { name = "siba", email = "siba@sdf" });

            obj.Add(new nameemail { name = "ssiba", email = "sissssba@sdf" });

            obj.Add(new nameemail { name = "ram", email = "ram@sdf" });



            var users = (from u in obj

                         where u.name.Contains(query)

                         select u.email).Distinct().ToArray();

            return Json(users, JsonRequestBehavior.AllowGet);

        }

    }
    public class nameemail
    {

        public string name { get; set; }

        public string email { get; set; }
        
    }

}
