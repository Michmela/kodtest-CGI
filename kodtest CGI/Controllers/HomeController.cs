using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kodtest_CGI.Models;
using System.Net.Http;

namespace kodtest_CGI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<BusinesscardViewModel> businesscardlist = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:53342/api/");

            var apicontroller = hc.GetAsync("businesscard");
            apicontroller.Wait();
            var result = apicontroller.Result;
            if(result.IsSuccessStatusCode)
            {
                var readbusinesscard = result.Content.ReadAsAsync<IList<BusinesscardViewModel>>();
                readbusinesscard.Wait();
                businesscardlist = readbusinesscard.Result;
            }
            else
            {
                businesscardlist = Enumerable.Empty<BusinesscardViewModel>();
                ModelState.AddModelError(string.Empty,"Inga visitkort hittade");
            }
            return View(businesscardlist);
        }
    }
}
