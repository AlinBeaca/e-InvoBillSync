using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_InvoBillSync
{
    public partial class Sablon_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_LOGIN"] == null)
            {
                Response.Redirect("Signin_page.aspx"); // Redirectioneaza loginul la pagina de conectare daca nu este conectat
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

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sablon (ID_LOGIN, CUI, Furnizor, Nr_Registrul_Comertului, Oras, Adresa, Judet, Date_firma, Capital_Social, Nume, Prenume, Telefon, Cod_IBAN, Banca) " +
                               "VALUES (@ID_LOGIN, @CUI, @Furnizor, @Nr_Registrul_Comertului, @Oras, @Adresa, @Judet, @Date_firma, @Capital_Social, @Nume, @Prenume, @Telefon, @Cod_IBAN, @Banca)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_LOGIN", Session["ID_LOGIN"]); // Va utiliza login id-ul sesiunii
                    command.Parameters.AddWithValue("@CUI", int.Parse(txtCUI.Text));
                    command.Parameters.AddWithValue("@Furnizor", txtFurnizor.Text);
                    command.Parameters.AddWithValue("@Nr_Registrul_Comertului", txtNrRegistrulComertului.Text);
                    command.Parameters.AddWithValue("@Oras", txtOras.Text);
                    command.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
                    command.Parameters.AddWithValue("@Judet", txtJudet.Text);
                    command.Parameters.AddWithValue("@Date_firma", txtDateFirma.Text);
                    command.Parameters.AddWithValue("@Capital_Social", Convert.ToDecimal(txtCapitalSocial.Text));
                    command.Parameters.AddWithValue("@Nume", txtNume.Text);
                    command.Parameters.AddWithValue("@Prenume", txtPrenume.Text);
                    command.Parameters.AddWithValue("@Telefon", Convert.ToInt32(txtTelefon.Text));
                    command.Parameters.AddWithValue("@Cod_IBAN", txtCodIBAN.Text);
                    command.Parameters.AddWithValue("@Banca", txtBanca.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                ClearAllTextBoxes(this);
            }
        }
        protected void BtnPreiaDate_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT TOP 1 *
            FROM Firme
            WHERE Tip_Client = 'Firma' AND CUI = @CUI";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CUI", txtCUI.Text);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtFurnizor.Text = reader["Nume_client"].ToString();
                        txtNrRegistrulComertului.Text = reader["Nr_Reg_Com"]?.ToString() ?? "";
                        txtOras.Text = reader["Oras"]?.ToString() ?? "";
                        txtAdresa.Text = reader["Sediu"]?.ToString() ?? "";
                        txtJudet.Text = reader["Judet"]?.ToString() ?? "";
                        txtDateFirma.Text = ""; // completabil dacă ai o sursă
                        txtCapitalSocial.Text = reader["Capital_social"] != DBNull.Value
                     ? Convert.ToDecimal(reader["Capital_social"]).ToString("0.##")
                     : "0";
                        txtNume.Text = "";
                        txtPrenume.Text = "";
                        txtTelefon.Text = "";
                        txtCodIBAN.Text = reader["Cont"]?.ToString() ?? "";
                        txtBanca.Text = reader["Banca"]?.ToString() ?? "";
                    }
                    else
                    {
                        // poți afișa un mesaj că firma nu a fost găsită
                    }
                }
            }
        }


    }
}
