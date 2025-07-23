using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace e_InvoBillSync
{
    public partial class ResetPassword_page : System.Web.UI.Page
    {
        private readonly string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token) || !IsTokenValid(token))
                {
                    lblMessage.Text = "Token invalid sau expirat.";
                    lblMessage.Visible = true;
                    form1.Visible = false; // Ascunde formularul dacă tokenul este invalid sau expirat
                }
            }
        }

        protected void btnSalveazaParola_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = txtParolaNoua.Text.Trim();

            if (string.IsNullOrEmpty(newPassword))
            {
                lblMessage.Text = "Vă rugăm să introduceți o parolă nouă.";
                lblMessage.Visible = true;
                return;
            }

            if (UpdatePassword(token, newPassword))
            {
                lblMessage.Text = "Parola a fost resetată cu succes.";
            }
            else
            {
                lblMessage.Text = "Eroare la resetarea parolei.";
            }
            lblMessage.Visible = true;
        }

        private bool IsTokenValid(string token)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM [dbo].[Token_resetare_parola] WHERE Token = @Token AND ExpiryDate > GETDATE()";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Token", token);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return false;
            }
        }

        private bool UpdatePassword(string token, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    string getEmailQuery = "SELECT Email FROM [dbo].[Token_resetare_parola] WHERE Token = @Token";
                    string updatePasswordQuery = "UPDATE [dbo].[Utilizatori] SET Parola = @Parola WHERE Email = @Email";
                    string deleteTokenQuery = "DELETE FROM [dbo].[Token_resetare_parola] WHERE Token = @Token";

                    using (SqlCommand getEmailCommand = new SqlCommand(getEmailQuery, connection, transaction))
                    {
                        getEmailCommand.Parameters.AddWithValue("@Token", token);
                        string email = (string)getEmailCommand.ExecuteScalar();

                        if (email == null)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        using (SqlCommand updatePasswordCommand = new SqlCommand(updatePasswordQuery, connection, transaction))
                        {
                            updatePasswordCommand.Parameters.AddWithValue("@Parola", Encrypt(newPassword));
                            updatePasswordCommand.Parameters.AddWithValue("@Email", email);
                            updatePasswordCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand deleteTokenCommand = new SqlCommand(deleteTokenQuery, connection, transaction))
                        {
                            deleteTokenCommand.Parameters.AddWithValue("@Token", token);
                            deleteTokenCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Response.Redirect("Signin_page.aspx");
                    }

                    return true;
                    
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return false;
            }
        }

        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}
