using Medicare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class ArticlesController : Controller
    {
        readonly ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Articles
        public ActionResult Index()
        {
            List<Article> articles = database.Articles.ToList();
            return View(articles);
        }
        public ActionResult Create()
        {
            if (!SessionHandler.IsUserDoctor(Session)) return RedirectToAction("index","articles");
            return View("ArticleForm");
        }


        public ActionResult Edit(int? id)
        {
            if (!SessionHandler.IsUserDoctor(Session)) return RedirectToAction("index", "articles");

            if (id.HasValue)
            {
                Article article = database.Articles.Include(a => a.User).SingleOrDefault(a => a.Id == id.Value);
                if (article != null)
                {
                    return View("ArticleForm", article);
                }
            }

            return Content("No article found");
        }
        public ActionResult Delete(int? id)
        {
            if (!SessionHandler.IsUserDoctor(Session)) return RedirectToAction("index", "articles");

            if (id.HasValue)
            {
                Article article = database.Articles.Include(a => a.User).SingleOrDefault(a => a.Id == id.Value);
                if (article != null)
                {
                    database.Articles.Remove(article);
                    database.SaveChanges();
                    return RedirectToAction("");
                }
            }

            return Content("No article found");
        }


        public ActionResult Save(Article article)
        {
            article.Time = DateTime.Now;
            if (article.Id == 0)
            {
                article.UserId = SessionHandler.GetUser(Session).Id;
                database.Articles.Add(article);
            }
            else
            {
                Article dbArticle = database.Articles.Single(a => a.Id == article.Id);
                dbArticle.Title = article.Title;
                dbArticle.Description = article.Description;
            }
            database.SaveChanges();
            return RedirectToAction("index","articles");
        }

        public ActionResult View(int? id)
        {
            if (id.HasValue)
            {
                Article article = database.Articles.Include(a=>a.User ).SingleOrDefault(a => a.Id == id.Value);
                if (article != null)
                {
                    return View(article);
                }
            }

            return Content("No article found");
            
        }
        public ActionResult Mine()
        {
            if (!SessionHandler.IsUserSignedIn(Session)) { return RedirectToAction("index", "signIn"); }

            int userId = SessionHandler.GetUser(Session).Id;

            List<Article> articles = database.Articles.Where(a => a.UserId == userId).ToList();

            return View(articles);

        }


    }






}