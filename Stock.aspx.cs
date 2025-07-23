using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace e_InvoBillSync
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeCulture();
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


        protected void btnsave_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");

            conn.Open();

            string insertStocProduseQuery = @"
                INSERT INTO dbo.Stoc_produse (Denumire, Depozit, Categorie, UM, Oras, Cantitate, Pret_unitar_fara_TVA, TVA, Subtotal_fara_TVA)
                VALUES (@Denumire, @Depozit, @Categorie, @UM, @Oras, @Cantitate, @PretUnitarFaraTVA, @TVA, @SubtotalFaraTVA);";

            using (SqlCommand cmd = new SqlCommand(insertStocProduseQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Denumire", txtDenumire.Text);
                cmd.Parameters.AddWithValue("@Depozit", txtDepozit.Text);
                cmd.Parameters.AddWithValue("@Categorie", txtCategorie.Text);
                cmd.Parameters.AddWithValue("@UM", txtUnitateMasura.Text);
                cmd.Parameters.AddWithValue("@Oras", txtOras.Text);
                cmd.Parameters.AddWithValue("@Cantitate", txtCanitate.Text);
                cmd.Parameters.AddWithValue("@PretUnitarFaraTVA", Decimal.Parse(txtPret.Text));
                cmd.Parameters.AddWithValue("@TVA", int.Parse(txtTVA.Text));
                cmd.Parameters.AddWithValue("@SubtotalFaraTVA", Decimal.Parse(txtSubtotal.Text));

                cmd.ExecuteNonQuery();
            }
            ClearAllTextBoxes(this);
           

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");

            conn.Open();

            string updateStocProduseQuery = @"
                UPDATE dbo.Stoc_produse
                SET
                    Denumire = @Denumire,
                    Depozit = @Depozit,
                    Categorie = @Categorie,
                    UM = @UM,
                    Oras = @Oras,
                    Cantitate = @Cantitate,
                    Pret_unitar_fara_TVA = @PretUnitarFaraTVA,
                    TVA = @TVA,
                    Subtotal_fara_TVA = @SubtotalFaraTVA
                WHERE
                    ID_Stoc = @ID_Stoc;";

            using (SqlCommand cmd = new SqlCommand(updateStocProduseQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ID_Stoc", int.Parse(txtIDStoc.Text));
                cmd.Parameters.AddWithValue("@Denumire", txtDenumire.Text);
                cmd.Parameters.AddWithValue("@Depozit", txtDepozit.Text);
                cmd.Parameters.AddWithValue("@Categorie", txtCategorie.Text);
                cmd.Parameters.AddWithValue("@UM", txtUnitateMasura.Text);
                cmd.Parameters.AddWithValue("@Oras", txtOras.Text);
                cmd.Parameters.AddWithValue("@Cantitate", txtCanitate.Text);
                cmd.Parameters.AddWithValue("@PretUnitarFaraTVA", Decimal.Parse(txtPret.Text));
                cmd.Parameters.AddWithValue("@TVA", int.Parse(txtTVA.Text));
                cmd.Parameters.AddWithValue("@SubtotalFaraTVA", Decimal.Parse(txtSubtotal.Text));

                cmd.ExecuteNonQuery();
            }
            ClearAllTextBoxes(this);
            
        }



        protected void btnshow_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");
            SqlDataAdapter sda = new SqlDataAdapter(" SELECT Id_Stoc AS 'ID', Denumire AS 'DENUMIRE', Depozit AS 'DEPOZIT', Categorie AS 'CATEGORIE', UM AS 'UNITATE MASURA', Oras AS 'ORAS', Cantitate AS 'CANTITATE', Pret_unitar_fara_TVA AS 'PRET UNITAR  ', TVA, Subtotal_fara_TVA AS 'SUBTOTAL' from Stoc_produse" , conn);
            DataSet dts1 = new DataSet();
            sda.Fill(dts1);
            grdShow2.DataSource = dts1.Tables[0];
            grdShow2.DataBind();


        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;"))
            {
                conn.Open();

                string deleteStocProduseQuery = @"
                DELETE FROM dbo.Stoc_produse
                WHERE ID_Stoc = @ID_Stoc;";

                using (SqlCommand cmd = new SqlCommand(deleteStocProduseQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Stoc", int.Parse(txtIDStoc.Text));
                    cmd.ExecuteNonQuery();
                }
                
            }
            ClearAllTextBoxes(this);
           
        }
    
    }    
}