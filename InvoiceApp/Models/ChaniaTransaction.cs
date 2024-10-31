using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace InvoiceApp.Models
{
    public class ChaniaTransaction
    {

        [Key]
        public int ChaniaTransactionId { get; set; } // Primary key for each transaction line item

        public string ChaniaDocID { get; set; } // Foreign key to ChaniaInvoice
        [Precision(18, 2)]
        public decimal ChaniaAmount { get; set; }
        public string ChaniaCurrency { get; set; }
        public string ChaniaDescription { get; set; }
        public string ChaniaGLAccount { get; set; }
        public int ChaniaTranType { get; set; }

        // Navigation property to link back to ChaniaInvoice
        public ChaniaInvoice ChaniaInvoice { get; set; }
    }
}
