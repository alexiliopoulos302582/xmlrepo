using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class ChaniaInvoice
    {
        [Key]
        public string ChaniaDocID { get; set; }
        public DateTime ChaniaDocDate { get; set; }
        public string ChaniaDocType { get; set; }
        public int ChaniaDocNumber { get; set; }
        public bool ChaniaIsReversal { get; set; }
        [Precision(18, 2)]
        public decimal ChaniaDocAmount { get; set; }
        public string ChaniaDocCurrency { get; set; }


        public List<ChaniaTransaction> ChaniaTransactions { get; set; } = new List<ChaniaTransaction>();




    }
}
