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
        [HttpPost]
        public ActionResult Create(JournalGetViewModel journalView)
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

        [HttpPost]
        public ActionResult Update(JournalGetViewModel journalView)
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