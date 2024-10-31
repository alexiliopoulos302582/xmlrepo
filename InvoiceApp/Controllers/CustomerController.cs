using Microsoft.AspNetCore.Mvc;
 using MySql.Data.MySqlClient;
using System.Collections.Generic;
using InvoiceApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Data;

namespace InvoiceApp.Controllers
{



    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public CustomerController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;


        }






        public IActionResult Index()
        {
            string connectionString = "Server=localhost;Database=destination;User=root;Password=Aa113718417YYZ!@;";
            List<Customer> customers = new List<Customer>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetCustomerData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                DestinationId = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourceid, 'id')")) ? 0 : reader.GetInt32("COALESCE(sourceid, 'id')"),
                                DestinationName = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourcename, 'name')")) ? string.Empty : reader.GetString("COALESCE(sourcename, 'name')"),
                                DestinationPhone = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourcephone, 'phone')")) ? string.Empty : reader.GetString("COALESCE(sourcephone, 'phone')"),
                                DestinationEmail = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourceemail, 'email')")) ? string.Empty : reader.GetString("COALESCE(sourceemail, 'email')"),
                                DestinationAddress = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourceaddress, 'address')")) ? string.Empty : reader.GetString("COALESCE(sourceaddress, 'address')"),
                                DestinationCity = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourcecity, 'city')")) ? string.Empty : reader.GetString("COALESCE(sourcecity, 'city')"),
                                DestinationPostalCode = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourcepostalcode, 'post code')")) ? string.Empty : reader.GetString("COALESCE(sourcepostalcode, 'post code')"),
                                DestinationCountry = reader.IsDBNull(reader.GetOrdinal("COALESCE(sourcecountry, 'country')")) ? string.Empty : reader.GetString("COALESCE(sourcecountry, 'country')")
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return View(customers);


        }
    }
}


/*
public IActionResult Index()
{
    var customers = new List<Customer>();

    string connectionString = "Server=localhost;Database=destination;User=root;Password=Aa113718417YYZ!@;";
    string tableName = "customer";
    string jsonMapping = "";

    using (var connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        // Step 1: Fetch the JSON mapping from `field_mappings` table.
        string fetchJsonQuery = "SELECT field_mapping FROM field_mappings WHERE table_name = @tableName;";

        using (var command = new MySqlCommand(fetchJsonQuery, connection))
        {
            command.Parameters.AddWithValue("@tableName", tableName);
            var result = command.ExecuteScalar();

            if (result != null)
            {
                jsonMapping = result.ToString();
            }
            else
            {
                return NotFound("No JSON mapping found for the specified table.");
            }
        }

        // Step 2: Construct and execute the dynamic query using the JSON mapping.
        StringBuilder queryBuilder = new StringBuilder();
        queryBuilder.Append("SELECT ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationid')) AS destinationid, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationname')) AS destinationname, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationphone')) AS destinationphone, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationemail')) AS destinationemail, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationaddress')) AS destinationaddress, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationcity')) AS destinationcity, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationpostalcode')) AS destinationpostalcode, ");
        queryBuilder.Append($"JSON_UNQUOTE(JSON_EXTRACT('{jsonMapping}', '$.destinationcountry')) AS destinationcountry ");
        queryBuilder.Append("FROM source.customer;");

        Console.WriteLine("Executing Query: " + queryBuilder.ToString());

        using (var command = new MySqlCommand(queryBuilder.ToString(), connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        // Adjusting the reading to get actual data instead of mapping strings
                        //  DestinationId = reader.IsDBNull(reader.GetOrdinal("destinationid")) ? 0 : reader.GetInt32("destinationid"),
                        DestinationName = reader.IsDBNull(reader.GetOrdinal("destinationname")) ? string.Empty : reader.GetString("destinationname"),
                        DestinationPhone = reader.IsDBNull(reader.GetOrdinal("destinationphone")) ? string.Empty : reader.GetString("destinationphone"),
                        DestinationEmail = reader.IsDBNull(reader.GetOrdinal("destinationemail")) ? string.Empty : reader.GetString("destinationemail"),
                        DestinationAddress = reader.IsDBNull(reader.GetOrdinal("destinationaddress")) ? string.Empty : reader.GetString("destinationaddress"),
                        DestinationCity = reader.IsDBNull(reader.GetOrdinal("destinationcity")) ? string.Empty : reader.GetString("destinationcity"),
                        DestinationPostalCode = reader.IsDBNull(reader.GetOrdinal("destinationpostalcode")) ? string.Empty : reader.GetString("destinationpostalcode"),
                        DestinationCountry = reader.IsDBNull(reader.GetOrdinal("destinationcountry")) ? string.Empty : reader.GetString("destinationcountry")
                    };
                    customers.Add(customer);
                }
            }
        }
    }

    return View(customers);*/



