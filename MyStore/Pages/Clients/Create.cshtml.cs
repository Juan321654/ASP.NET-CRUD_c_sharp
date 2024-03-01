using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string message = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            string connectionSettings = "Server=localhost;User=juan;Password=password;Port=3306;database=mystore;";
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO `clients` (`name`, `email`, `phone`, `address`) VALUES (@name, @email, @phone, @address);";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@name", clientInfo.name);
                        mySqlCommand.Parameters.AddWithValue("@email", clientInfo.email);
                        mySqlCommand.Parameters.AddWithValue("@phone", clientInfo.phone);
                        mySqlCommand.Parameters.AddWithValue("@address", clientInfo.address);
                        mySqlCommand.ExecuteNonQuery();
                        EmptyFields();
                        message = "Created";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error Creating";
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void EmptyFields()
        {
            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
        }
    }
}