using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Services;
namespace InvoiceApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            var invoice = _invoiceService.GetInvoiceFromXml("wwwroot/InvoiceData.xml");
            return View(invoice);
        }
    }
}
