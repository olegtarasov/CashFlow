using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenMoneyPlus.Models
{
    public class Transaction : EntityBase
    {
        public decimal Income { get; set; }
        public decimal Outcome { get; set; }
        public long IncomeInstrument { get; set; }
        public long OutcomeInstrument { get; set; }
        public long Created { get; set; }
        public string OriginalPayee { get; set; }
        public bool Deleted { get; set; }
        public bool Viewed { get; set; }
        public string Hold { get; set; }
        public string QrCode { get; set; }
        public string IncomeAccount { get; set; }
        public string OutcomeAccount { get; set; }
        public string Comment { get; set; }
        public string Payee { get; set; }
        public string OpIncome { get; set; }
        public string OpOutcome { get; set; }
        public string OpIncomeInstrument { get; set; }
        public string OpOutcomeInstrument { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Merchant { get; set; }
        public string IncomeBankId { get; set; }
        public string OutcomeBankId { get; set; }
        public string ReminderMarker { get; set; }
        
        [NotMapped]
        public string[] Tag { get; set; }

        public List<Tag> Tags { get; set; }
    }
}