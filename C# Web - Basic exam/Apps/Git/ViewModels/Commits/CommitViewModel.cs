using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Commits
{
   public class CommitViewModel
    {
        public string Description { get; set; }
        public string RepositoryName { get; set; }
        public string Id { get; set; }
        public string CreatedOn { get; set; }
    }
}
