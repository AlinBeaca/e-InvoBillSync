using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_InvoBillSync
{
    public partial class Signin_page : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");


        private static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = Encrypt(txtpass.Text);

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;"))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ID_LOGIN FROM Utilizatori WHERE Username=@Username AND Parola=@Parola", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Parola", password);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int idLogin = Convert.ToInt32(result);
                            HttpCookie cookie = new HttpCookie("User");
                            cookie["Username"] = username;
                            cookie["Parola"] = password;
                            Response.Cookies.Add(cookie);
                            Session["ID_LOGIN"] = idLogin;
                            Response.Redirect("Sablon_page.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showErrorPopup('Username sau parola incorectă!');", true);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after adding proper logging)
                // Log.Error(ex, "Login error");
                Response.Write($"A apărut o eroare: {ex.Message}");
            }
        }

        protected void txtpass_TextChanged(object sender, EventArgs e)
        {
            txtpass.Text = Encrypt(txtpass.Text);
        }

        protected void Inregistrare(object sender, EventArgs e)
        {
            Response.Redirect("Signup_page.aspx");
        }
    }
}
