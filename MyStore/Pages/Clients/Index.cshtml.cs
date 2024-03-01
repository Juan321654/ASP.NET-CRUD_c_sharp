using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            string connectionSettings = "Server=localhost;User=juan;Password=password;Port=3306;database=mystore;";
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM `clients`;";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listClients.Add(new ClientInfo
                                {
                                    id = reader.GetInt32("id"),
                                    name = reader.GetString("name"),
                                    email = reader.GetString("email"),
                                    phone = reader.GetString("phone"),
                                    address = reader.GetString("address")
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    public class ClientInfo
    {
        public int id;
        public string name;
        public string email;
        public string phone;
        public string address;
    }

}

