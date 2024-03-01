using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string message = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            string connectionSettings = "Server=localhost;User=juan;Password=password;Port=3306;database=mystore;";
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM `clients` WHERE `id` = @id;";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = reader.GetInt32("id");
                                clientInfo.name = reader.GetString("name");
                                clientInfo.email = reader.GetString("email");
                                clientInfo.phone = reader.GetString("phone");
                                clientInfo.address = reader.GetString("address");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = "Error Reading";
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void OnPost()
        {
            clientInfo.id = Convert.ToInt32(Request.Form["id"]);
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
                    string query = "UPDATE `clients` SET `name` = @name, `email` = @email, `phone` = @phone, `address` = @address WHERE `id` = @id;";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@id", clientInfo.id);
                        mySqlCommand.Parameters.AddWithValue("@name", clientInfo.name);
                        mySqlCommand.Parameters.AddWithValue("@email", clientInfo.email);
                        mySqlCommand.Parameters.AddWithValue("@phone", clientInfo.phone);
                        mySqlCommand.Parameters.AddWithValue("@address", clientInfo.address);
                        mySqlCommand.ExecuteNonQuery();
                        message = "Updated";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error Updating";
                    Console.WriteLine(ex.Message);
                }

                Response.Redirect("/Clients/Index");
            }
        }
    }
}
