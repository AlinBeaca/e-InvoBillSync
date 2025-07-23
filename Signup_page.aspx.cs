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
    public partial class Signup_page : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");


        static string Encrypt(string value)
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
            if (!IsPostBack)
            {
                GenerateID();
            }
        }

        private void GenerateID()
        {
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("Select count(ID_LOGIN) from Utilizatori", conn);
            int i = Convert.ToInt32(cmd1.ExecuteScalar());
            conn.Close();
            i++;
            lblid.Text = i.ToString();
        }

        protected void Password_TextChanged(object sender, EventArgs e)
        {
            txtpass.Text = Encrypt(txtpass.Text);
        }

        protected void Btnregister1_Click(object sender, EventArgs e)
        {
            string encryptedPassword = Encrypt(txtpass.Text);
            SqlDataAdapter sda = new SqlDataAdapter("select * from Utilizatori where Email='" + txtemail.Text + "' and Username='" + txtusername.Text + "' and Parola='" + encryptedPassword + "'", conn);
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);

            if (dt1.Rows.Count == 0)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Utilizatori (ID_LOGIN, Email, Username, Parola) values('" + int.Parse(lblid.Text) + "','" + txtemail.Text + "','" + txtusername.Text + "','" + encryptedPassword + "')", conn);
                cmd.ExecuteNonQuery();
                Response.Redirect("Signin_page.aspx");
                conn.Close();
            }
            else
            {
                Response.Write("Verifica-ti e-mail-ul!");
            }
        }
    }
}