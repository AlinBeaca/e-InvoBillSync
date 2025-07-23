using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace e_InvoBillSync
{
    public partial class ForgetPassword_page : System.Web.UI.Page
    {
        private readonly string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnTrimiteLink_Click(object sender, EventArgs e)
        {
            string userEmail = txtemail.Text.Trim();

            if (!string.IsNullOrEmpty(userEmail))
            {
                string resetToken = Guid.NewGuid().ToString(); // Token de resetare unic
                string resetLink = $"{Request.Url.Scheme}://{Request.Url.Authority}/ResetPassword_page.aspx?token={resetToken}";

                // Salvarea tokenului în baza de date
                if (SavePasswordResetToken(userEmail, resetToken))
                {
                    // Trimiterea emailului de resetare
                    if (SendResetEmail(userEmail, resetLink))
                    {
                        lblMessage.Text = "Un link de resetare a fost trimis la adresa de email specificată.";
                    }
                    else
                    {
                        lblMessage.Text = "Eroare la trimiterea emailului.";
                    }
                }
                else
                {
                    lblMessage.Text = "Eroare la salvarea tokenului de resetare.";
                }
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Vă rugăm să introduceți o adresă de email validă.";
                lblMessage.Visible = true;
            }
        }

        private bool SavePasswordResetToken(string email, string token)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                    INSERT INTO [dbo].[Token_resetare_parola] 
                    (Email, Token, ExpiryDate) 
                    VALUES 
                    (@Email, @Token, @ExpiryDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Token", token);
                        command.Parameters.AddWithValue("@ExpiryDate", DateTime.Now.AddHours(24)); // Token-ul expiră în 24 de ore

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();

                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            lblMessage.Text = "Insert command did not affect any rows.";
                            lblMessage.Visible = true;
                            return false;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                lblMessage.Text = $"SQL Exception: {sqlEx.Message}";
                lblMessage.Visible = true;
                return false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"General Exception: {ex.Message}";
                lblMessage.Visible = true;
                return false;
            }
        }

        private bool SendResetEmail(string email, string resetLink)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("alin.beaca99@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "Resetare parolă - e_InvoBillSync";
                mailMessage.Body = $"Click pe link-ul următor pentru a reseta parola: <a href='{resetLink}'>Resetează parola</a>";
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("alin.beaca99@gmail.com", "gvqv khsb ikqn amyq"); 
                smtpClient.EnableSsl = true;

                // Adaugă logare pentru fiecare pas
                lblMessage.Text = "Se trimite emailul...";
                lblMessage.Visible = true;

                smtpClient.Send(mailMessage);

                lblMessage.Text = "Email trimis cu succes!";
                lblMessage.Visible = true;

                return true;
            }
            catch (SmtpFailedRecipientException smtpEx)
            {
                lblMessage.Text = $"SmtpFailedRecipientException: {smtpEx.Message}";
                lblMessage.Visible = true;
                return false;
            }
            catch (SmtpException smtpEx)
            {
                lblMessage.Text = $"SmtpException: {smtpEx.Message}";
                lblMessage.Visible = true;
                return false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"General Exception: {ex.Message}";
                lblMessage.Visible = true;
                return false;
            }
        }
    }
}
