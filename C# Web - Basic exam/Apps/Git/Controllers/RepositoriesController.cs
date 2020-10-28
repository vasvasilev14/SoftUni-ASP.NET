using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
   public class RepositoriesController:Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }


        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var repositoriesViewModel = this.repositoriesService.GetAll();

            return this.View(repositoriesViewModel);
        }
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
           
            return this.View();
        }
        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 10)
            {
                return this.Error("Invalid Name!");
            }

            var userId = this.GetUserId();
            this.repositoriesService.Create(name, repositoryType, userId);
            return this.Redirect("/");
        }



    }
}
