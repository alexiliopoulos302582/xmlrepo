namespace InvoiceApp.Services
{


    using System.IO;
using System.Xml.Linq;
using InvoiceApp.Models;



    public class InvoiceService
    {

     public Invoice GetInvoiceFromXml(string filePath)
        {
           var xDoc = XDocument.Load(filePath);

            var customer = new Customer
            {
                CustomerId = xDoc.Root.Element("Customer")?.Element("CustomerId")?.Value,
                IrsNumber = xDoc.Root.Element("Customer")?.Element("IrsNumber")?.Value,
                Name = xDoc.Root.Element("Customer")?.Element("Name")?.Value,
                Address = xDoc.Root.Element("Customer")?.Element("Address")?.Value,
                ZipCode = xDoc.Root.Element("Customer")?.Element("ZipCode")?.Value,
                Phone = xDoc.Root.Element("Customer")?.Element("Phone")?.Value
            };

            var product = new Product
            {
                Name = xDoc.Root.Element("Product")?.Element("Name")?.Value,
                Quantity = int.Parse(xDoc.Root.Element("Product")?.Element("Quantity")?.Value ?? "0"),
                Price = decimal.Parse(xDoc.Root.Element("Product")?.Element("Price")?.Value ?? "0")
            };

            return new Invoice
            {
                Customer = customer,
                Product = product
            };
        }



            
        




    }
}
