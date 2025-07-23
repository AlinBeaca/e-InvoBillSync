using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace e_InvoBillSync
{
    public partial class Clients : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClients();
            }
        }

        private void LoadClients()
        {
            var clienti = new List<string>();
            int userId = Convert.ToInt32(Session["ID_LOGIN"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT DISTINCT f.Nume_client
                    FROM dbo.Firme f
                    INNER JOIN dbo.Facturi s ON f.Nume_client = s.Nume_client
                    WHERE s.ID_LOGIN = @ID_LOGIN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_LOGIN", userId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clienti.Add(reader["Nume_client"].ToString());
                        }
                    }
                }
            }

            rptClients.DataSource = clienti;
            rptClients.DataBind();
        }
    }
}
