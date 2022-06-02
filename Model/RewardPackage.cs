using FundProjectAPI.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundProjectAPI.Model
{
    public class RewardPackage
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal FundAmount { get; set; }
        public string Reward { get; set; }
        public List<Project> Projects { get; set; } = new();
    }
}
