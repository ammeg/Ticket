using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Testes
{
    public class ExemplosController : Controller
    {
        
            /*<div style="background-color: black; margin: 0px; padding: 10px 10px 10px 10px;">
            <input type="text" />
        </div>

        <div style=" background-color: red; margin: 10px 0 0 0" class="">
            <input type="text" />
        </div>*/

        //
        // GET: /Exemplos/

        public ActionResult Index()
        {
            return View();
        }

    }
}
