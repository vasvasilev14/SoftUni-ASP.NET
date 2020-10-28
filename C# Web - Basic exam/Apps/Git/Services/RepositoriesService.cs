using Git.Data;
using System;
using System.Collections.Generic;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;
using Git.ViewModels.Repositories;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, string repositoryType,string userId)
        {
            var repository = new Repository
            {
                Name = name,
                IsPublic = repositoryType.ToLower() == "public" ? true : false,
                CreatedOn = DateTime.UtcNow,
                OwnerId=userId,
            };
            this.db.Repositories.Add(repository);
            this.db.SaveChanges();

        }

        public IEnumerable<RepositoriesViewModel> GetAll()
        {
            return this.db.Repositories.Where(x=>x.IsPublic==true).Select(x => new RepositoriesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreatedOn = x.CreatedOn,
                Owner = x.Owner.Username,
                CommitsCount = x.Commits.Count,
            }).ToList();
        }
    }
}
