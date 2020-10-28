using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
   public class CommitsController:Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse All()

        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();
            var viewModel = this.commitsService.GetAll(userId);
            return this.View(viewModel);
        }
        public HttpResponse Create(HomeCommitsViewModel model)
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
           

            return this.View(model);
        }
      
        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (description.Length<5)
            {
                return this.Error("a string with min length 5");
            }
            var userId = this.GetUserId();
            
            this.commitsService.Create(description,  id, userId);
            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this.commitsService.Delete(id);
            return this.Redirect("/Commits/All");
        }

    }
}
