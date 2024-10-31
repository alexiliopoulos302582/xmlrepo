using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace InvoiceApp.Controllers
{
    public class HomeController : Controller
    {



        private readonly ILogger<HomeController> _logger;


        private readonly ChaniaContext _context;




       // public HomeController(ILogger<HomeController> logger)
       // {
      //      _logger = logger;
       // }


        public HomeController(ChaniaContext context)
        {
            _context = context;
        }





        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        // Action to read and import XML data
        public async Task<IActionResult> ImportInvoices()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data.xml");
            if (!System.IO.File.Exists(filePath))
            {
                return Content("XML file not found.");
            }

            var xml = XDocument.Load(filePath);
            var docs = xml.Descendants("Doc");

            foreach (var doc in docs)
            {
                // Parse invoice details
                string docID = doc.Element("DocID")?.Value;
                DateTime docDate = DateTime.Parse(doc.Element("DocDate")?.Value);
                string docType = doc.Element("DocType")?.Value;
                int docNumber = int.Parse(doc.Element("DocNumber")?.Value);
                bool isReversal = doc.Element("IsReversal")?.Value == "1";
                string docCurrency = doc.Element("DocCurrency")?.Value;

                // Parse customer details
                string customerID = doc.Element("CustomerID")?.Value;
                string fullName = doc.Element("FullName")?.Value;
                string taxID = doc.Element("TaxID")?.Value;
                string countryCode = doc.Element("CountryCode")?.Value;
                string postCode = doc.Element("PostCode")?.Value;
                string cityName = doc.Element("CityName")?.Value;
                string address = doc.Element("Address")?.Value;
                string email = doc.Element("Email")?.Value;

                // Find or add the customer
                var customer = _context.ChaniaCustomers.FirstOrDefault(c => c.ChaniaTaxID == taxID);
                if (customer == null)
                {
                    customer = new ChaniaCustomer
                    {
                        ChaniaCustomerID = int.Parse(customerID),
                        ChaniaFullName = fullName,
                        ChaniaTaxID = taxID,
                        ChaniaCountryCode = countryCode,
                        ChaniaPostCode = postCode,
                        ChaniaCityName = cityName,
                        ChaniaAddress = address,
                        ChaniaEmail = email
                    };
                    _context.ChaniaCustomers.Add(customer);
                    await _context.SaveChangesAsync();
                }

                // Parse and add transactions and calculate the total DocAmount
                decimal totalDocAmount = 0;
                var transactions = doc.Descendants("Tran");

                foreach (var tran in transactions)
                {
                    decimal amount = decimal.Parse(tran.Element("Amount")?.Value ?? "0");
                    string currency = tran.Element("Currency")?.Value;
                    string description = tran.Element("Description")?.Value;
                    string glAccount = tran.Element("GLAccount")?.Value;
                    int tranType = int.Parse(tran.Element("TranType")?.Value);

                    // Add the transaction and accumulate the amount
                    var transaction = new ChaniaTransaction
                    {
                        ChaniaDocID = docID,
                        ChaniaAmount = amount,
                        ChaniaCurrency = currency,
                        ChaniaDescription = description,
                        ChaniaGLAccount = glAccount,
                        ChaniaTranType = tranType
                    };
                    _context.ChaniaTransactions.Add(transaction);

                    // Accumulate the transaction amount to the total DocAmount
                    totalDocAmount += amount;
                }

                // Add the invoice, setting the calculated DocAmount
                var invoice = new ChaniaInvoice
                {
                    ChaniaDocID = docID,
                    ChaniaDocDate = docDate,
                    ChaniaDocType = docType,
                    ChaniaDocNumber = docNumber,
                    ChaniaIsReversal = isReversal,
                    ChaniaDocAmount = totalDocAmount,  // Set the calculated DocAmount here
                    ChaniaDocCurrency = docCurrency
                };
                _context.ChaniaInvoices.Add(invoice);
                await _context.SaveChangesAsync();
            }

            return Content("Data imported and DocAmount calculated successfully.");
        }


























    }
}
