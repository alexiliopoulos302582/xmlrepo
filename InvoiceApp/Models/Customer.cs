namespace InvoiceApp.Models
{
    public class Customer
    {
        public string CustomerId { get; set; } = "";
        public string IrsNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Phone { get; set; } = "";
       
        public int Id { get; set; }
        
        public string City { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

         public int DestinationId { get; set; } // This should correspond to your destinationid column
    public string DestinationName { get; set; } // Corresponds to destinationname
    public string DestinationPhone { get; set; } // Corresponds to destinationphone
    public string DestinationEmail { get; set; } // Corresponds to destinationemail
    public string DestinationAddress { get; set; } // Corresponds to destinationaddress
    public string DestinationCity { get; set; } // Corresponds to destinationcity
    public string DestinationPostalCode { get; set; } // Corresponds to destinationpostalcode
    public string DestinationCountry { get; set; } // Corresponds to destinationcountry

    }
}
