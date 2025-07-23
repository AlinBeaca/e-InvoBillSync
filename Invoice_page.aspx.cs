using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_InvoBillSync
{

    public partial class Invoice_page : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateID();
            }
        }

        private void ClearAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else if (control.HasControls())
                {
                    ClearAllTextBoxes(control);
                }
            }
        }

        private void GenerateID()
        {
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("Select count(ID_Factura) from Facturi", conn);
            int i = Convert.ToInt32(cmd1.ExecuteScalar());
            SqlCommand cmd2 = new SqlCommand("Select count(ID_Transport) from Transport", conn);
            int j = Convert.ToInt32(cmd2.ExecuteScalar());
            SqlCommand cmd3 = new SqlCommand("Select count(ID_Produs) from Produse_facturi", conn);
            int l = Convert.ToInt32(cmd3.ExecuteScalar());
            conn.Close();
            i++;
            j++;
            l++;

            hfFacturaID.Value = i.ToString();
            hfTransportID.Value = j.ToString();
            hfProdusID.Value = l.ToString();
        }



        protected void BtnSalveaza_Click(object sender, EventArgs e)
        {
            if (Session["ID_LOGIN"] == null)
            {
                Response.Redirect("Signin_page.aspx");
                return;
            }

            string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Facturi (ID_factura, Nume_client, Tip_factura, Valuta_Factura, Data_Facturare, Termen_Plata, ID_LOGIN) " +
                               "VALUES (@ID_factura, @Nume_client, @Tip_factura, @Valuta_Factura, @Data_Facturare, @Termen_Plata, @ID_LOGIN)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_factura", int.Parse(hfFacturaID.Value));
                    command.Parameters.AddWithValue("@Nume_client", txtClient.Text);
                    command.Parameters.AddWithValue("@Tip_factura", txtTipFactura.Text);
                    command.Parameters.AddWithValue("@Valuta_Factura", ddlValuta.SelectedValue);
                    command.Parameters.AddWithValue("@Data_Facturare", DateTime.Parse(txtDataFactura.Text));
                    command.Parameters.AddWithValue("@Termen_Plata", DateTime.Parse(txtTermenPlata.Text));
                    command.Parameters.AddWithValue("@ID_LOGIN", Session["ID_LOGIN"]);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                

            }

           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Transport (ID_Transport, ID_Factura, Delegat, Seria_CI, Numar_inmatriculare) " +
                               "VALUES (@ID_Transport, @ID_Factura, @Delegat, @Seria_CI, @Numar_inmatriculare)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Transport", int.Parse(hfTransportID.Value));
                    command.Parameters.AddWithValue("@ID_factura", int.Parse(hfFacturaID.Value));
                    command.Parameters.AddWithValue("@Delegat", txtDelegat.Text);
                    command.Parameters.AddWithValue("@Seria_CI", txtSerieCI.Text);
                    command.Parameters.AddWithValue("@Numar_inmatriculare", txtTransport.Text);
                    

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Produse_facturi (ID_Produs, Denumire, ID_Factura, Cantitate) " +
                               "VALUES (@ID_Produs, @Denumire, @ID_Factura, @Cantitate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Produs", int.Parse(hfProdusID.Value));
                    command.Parameters.AddWithValue("@Denumire", txtDenumire.Text);
                    command.Parameters.AddWithValue("@ID_factura", int.Parse(hfFacturaID.Value));
                    command.Parameters.AddWithValue("@Cantitate", txtCantitate.Text);
                  


                    connection.Open();
                    command.ExecuteNonQuery();
                }
                ClearAllTextBoxes(this);
            }

            GenerateID();
        }


        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear(); 
            Response.Redirect("Signin_page.aspx");
        }



        protected void BtnPreiaDate_Click(object sender, EventArgs e)
        {
            
             string query = "SELECT Nume_client, Tip_Client, CUI, CNP, Nr_Reg_Com, Sediu, Oras, Judet, Tara, Cont, Banca FROM Firme WHERE Nume_client = @Nume_client";

            using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nume_client", txtNume.Text);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            txtTipclient.Text = reader["Tip_Client"].ToString();
                            txtCUI.Text = reader["CUI"].ToString();
                            txtCNP.Text = reader["CNP"].ToString();
                            txtNrRegistru.Text = reader["Nr_Reg_Com"].ToString();
                            txtSediu.Text = reader["Sediu"].ToString();
                            txtOras.Text = reader["Oras"].ToString();
                            txtJudet.Text = reader["Judet"].ToString();
                            txtTara.Text = reader["Tara"].ToString();
                            txtCont.Text = reader["Cont"].ToString();
                            txtBanca.Text = reader["Banca"].ToString();
                        }
                    }
                }
            }
        }
    }

    class Gridshow
    {
    }
}
