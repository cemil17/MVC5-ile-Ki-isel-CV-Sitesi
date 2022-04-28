using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CV_Udemy.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace CV_Udemy.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default

        CV_SiteEntities entities = new CV_SiteEntities();
        public ActionResult Index()
        {
            var list = entities.TBL_About.ToList();
            return View(list);
        }
        public PartialViewResult SocialMedia()
        {
            var media = entities.TBL_SocialMedia.Where(x => x.State == true).ToList();
            return PartialView(media);
        }
        public PartialViewResult Experience()
        {
            var experience = entities.TBL_Experience.OrderByDescending(x => x.ID).ToList();
            return PartialView(experience);
        }

        public PartialViewResult Education()
        {
            var edu = entities.TBL_Education.ToList();
            return PartialView(edu);
        }
        public PartialViewResult Talents()
        {
            var talent = entities.TBL_Skills.ToList();
            return PartialView(talent);
        }
        public PartialViewResult Hobby(int number = 1)
        {
            var hobby = entities.TBL_Hobby.OrderByDescending(x => x.ID).ToList().ToPagedList(number, 20);
            var max1 = entities.TBL_Hobby.Count().ToString();
            ViewBag.d = max1;
            return PartialView(hobby);

        }
        public PartialViewResult Certicate()
        {
            var certificate = entities.TBL_Certificate.OrderByDescending(x => x.ID).ToList();
            return PartialView(certificate);
        }
        [HttpGet]
        public PartialViewResult Contact()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Contact(TBL_Contact c)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Contact");
            }
            c.DateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            entities.TBL_Contact.Add(c);
            entities.SaveChanges();
            return PartialView();
        }
    }
}