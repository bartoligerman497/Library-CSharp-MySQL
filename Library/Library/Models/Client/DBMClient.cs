using System.Data;
using Library.Models.Client.MartialStatus;
using Library.Models.DTO;
using MySqlConnector;

namespace Library.Models.Client.DBMClient;
public class DBMClient
{
    public List<ClientModel> LoadClientList()
    {
        List<ClientModel> clientList = new List<ClientModel>();

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        string consult =
                @"SELECT
                        clients.client_id AS client_id,
                        martialstatus.martial_status_id AS martial_status_id,
                        martialstatus.description AS martial_status_description,
                        clients.name AS name,
                        clients.email AS email,
                        clients.birthdate AS birthdate,
                        clients.gender AS gender,
                        clients.created_at AS created_at,
                        clients.updated_at AS updated_at,
                        clients.active AS active
                      FROM clients
                        INNER JOIN martialstatus
                          ON clients.martial_status_id = martialstatus.martial_status_id
                      WHERE clients.active = 1";

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(reader.GetInt32(0));

            ClientModel client = new();
            client.Client_Id = Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id");

            MartialStatusModel martialStatus = new();
            martialStatus.Martial_Status_Id = Convert.IsDBNull(reader.GetValue("martial_status_id")) ? 0 : reader.GetInt32("martial_status_id");
            martialStatus.Description = Convert.IsDBNull(reader.GetValue("martial_status_description")) ? string.Empty : reader.GetString("martial_status_description");
            client.Martial_Status = martialStatus;

            client.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            client.Email = Convert.IsDBNull(reader.GetValue("email")) ? string.Empty : reader.GetString("email");
            client.Birthdate = Convert.IsDBNull(reader.GetValue("birthdate")) ? DateTime.Now : reader.GetDateTime("birthdate");

            client.Gender = Convert.IsDBNull(reader.GetValue("gender")) ? string.Empty : reader.GetString("gender");

            client.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            client.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            client.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            clientList.Add(client);
        }
        reader.Close();
        connection.Close();

        return clientList;
    }

    public DTOModel GetClientByCustomerID(int? Client_Id)
    {
        DTOModel dto = new();

        String consult =
                  @"SELECT
                        clients.client_id AS client_id,
                        martialstatus.martial_status_id AS martial_status_id,
                        martialstatus.description AS martial_status_description,
                        clients.name AS name,
                        clients.email AS email,
                        clients.birthdate AS birthdate,
                        clients.gender AS gender,
                        clients.created_at AS created_at,
                        clients.updated_at AS updated_at,
                        clients.active AS active
                      FROM clients
                        INNER JOIN martialstatus
                          ON clients.martial_status_id = martialstatus.martial_status_id
                      WHERE clients.active = 1 and
                      clients.client_id = @client_ID;";

        MySqlConnection connection = new MySqlConnection(DBMConnection.ConnectionString);

        MySqlCommand command = new MySqlCommand(consult, connection);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@client_ID", Client_Id);

        connection.Open();

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            //Console.WriteLine(Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id"));

            ClientModel client = new();
            client.Client_Id = Convert.IsDBNull(reader.GetValue("client_id")) ? 0 : reader.GetInt32("client_id");

            MartialStatusModel martialStatus = new();
            martialStatus.Martial_Status_Id = Convert.IsDBNull(reader.GetValue("martial_status_id")) ? 0 : reader.GetInt32("martial_status_id");
            martialStatus.Description = Convert.IsDBNull(reader.GetValue("martial_status_description")) ? string.Empty : reader.GetString("martial_status_description");
            client.Martial_Status = martialStatus;

            client.Name = Convert.IsDBNull(reader.GetValue("name")) ? string.Empty : reader.GetString("name");
            client.Email = Convert.IsDBNull(reader.GetValue("email")) ? string.Empty : reader.GetString("email");
            client.Birthdate = Convert.IsDBNull(reader.GetValue("birthdate")) ? DateTime.Now : reader.GetDateTime("birthdate");

            client.Gender = Convert.IsDBNull(reader.GetValue("gender")) ? string.Empty : reader.GetString("gender");

            client.Created_at = Convert.IsDBNull(reader.GetValue("created_at")) ? DateTime.Now : reader.GetDateTime("created_at");
            client.Updated_at = Convert.IsDBNull(reader.GetValue("updated_at")) ? DateTime.Now : reader.GetDateTime("updated_at");

            client.Active = Convert.IsDBNull(reader.GetValue("active")) ? false : reader.GetBoolean("active");

            dto.Client = client;
        }
        reader.Close();
        connection.Close();

        return dto;
    }
}
