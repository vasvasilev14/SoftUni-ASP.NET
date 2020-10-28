using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
   public interface ICommitsService
    {
        void Create(string description, string repoId, string userId);
        void Delete(string id);
        IEnumerable<CommitViewModel> GetAll(string userId);
    }
}
