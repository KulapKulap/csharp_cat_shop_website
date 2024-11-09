using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using cat_shop_aspnet_csharp.Models; // Import the Models namespace

namespace cat_shop_aspnet_csharp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IConfiguration _configuration;

        public PrivacyModel(ILogger<PrivacyModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Logic for GET requests can go here
        }

        public IActionResult OnPost()
{
    ReviewModel review = new ReviewModel
    {
        Name = Request.Form["name"],
        Email = Request.Form["email"],
        Review = Request.Form["review"]
    };

    string connectionString = _configuration.GetConnectionString("MySqlConnection");

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();
        string query = "INSERT INTO reviews (name, email, review) VALUES (@name, @email, @review)";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", review.Name);
        command.Parameters.AddWithValue("@email", review.Email);
        command.Parameters.AddWithValue("@review", review.Review);

        command.ExecuteNonQuery();
    }

    return RedirectToPage("ThankYou");  // Ensure the path is correct
}

    }
}
