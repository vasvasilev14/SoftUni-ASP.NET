using Git.Data;
using Git.ViewModels.Commits;
using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
  public  class CommitsService:ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string description, string repoId,string userId)
        {
            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                Description = description,
                RepositoryId = repoId,
                CreatorId=userId,
                
            };
            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }
        public void Delete(string id)
        {
            var commit = this.db.Commits.Find(id);
            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }
        public IEnumerable<CommitViewModel>  GetAll(string userId)
        {
            var commits = this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                    CreatedOn = x.CreatedOn.ToString(),
                    Description = x.Description,
                    RepositoryName = x.Repository.Name,
                    Id = x.Id
                }).ToList();
            return commits;
        }

    }
}

