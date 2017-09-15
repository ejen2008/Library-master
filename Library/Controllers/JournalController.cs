using Library.BLL.Services;
using Library.ViewModels.JournalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class JournalController : Controller
    {
        private JournalService _journalService;

        public JournalController()
        {
            _journalService = new JournalService();
        }
        public ActionResult Index()
        {
            return View(_journalService.GetJournals());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(JournalUpdateViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _journalService.Create(journalView);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            JournalUpdateViewModel journalView = new JournalUpdateViewModel();
            if (id !=0)
            {
                journalView=  _journalService.GetJournal(id.Value);
            }
            if (journalView == null)
            {
                return HttpNotFound();
            }
            return View(journalView);
        }


        [HttpPost]
        public ActionResult Edit(JournalUpdateViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _journalService.Update(journalView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(JournalGetViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (journalView.Juornal.Id != 0)
            {
                _journalService.Delete(journalView);
            }
            return RedirectToAction("Index");
        }
    }
}