using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Model
{
    public class Project
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectCategory Category { get; set; }
        public decimal Goal { get; set; }
        public decimal GoalGained { get; set; }
        [ForeignKey("ProjectCreatorId")]
        public ProjectCreator ProjectCreator { get; set; }
        public List<RewardPackage> RewardPackages { get; set; } = new();
        public List<Backer> Backers { get; set; } = new();
    }
}
