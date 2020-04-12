using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenMoneyPlus.Models
{
    public class Tag : EntityBase
    {
        public string Icon { get; set; }
        public bool BudgetIncome { get; set; }
        public bool BudgetOutcome { get; set; }
        public bool? Required { get; set; }
        public string Color { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public bool ShowIncome { get; set; }
        public bool ShowOutcome { get; set; }
        public string Parent { get; set; }

        public Tag ParentTag { get; set; }
        public List<Tag> ChildrenTags { get; set; }
    }
}