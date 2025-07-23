using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace e_InvoBillSync
{
    public partial class Due_invoices_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");
            string query = "SELECT f.ID_factura, f.Nume_client, f.Termen_Plata, c.CUI AS ClientCUI, c.Nr_Reg_Com AS ClientNrRegCom, t.Delegat, t.Numar_inmatriculare, p.Denumire, p.Cantitate " +
                           "FROM Facturi f " +
                           "LEFT JOIN Clienti c ON f.Nume_client = c.Nume_client " +
                           "LEFT JOIN Transport t ON f.ID_factura = t.ID_Factura " +
                           "LEFT JOIN Produse_Facturi p ON f.ID_factura = p.ID_Factura " +
                           "WHERE f.ID_LOGIN = @ID_LOGIN " +
                           "AND DATEDIFF(day, GETDATE(), f.Termen_Plata) <= 5 " +
                           "ORDER BY f.ID_factura";

            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            sda.SelectCommand.Parameters.AddWithValue("@ID_LOGIN", Session["ID_LOGIN"]);

            DataSet dts2 = new DataSet();
            sda.Fill(dts2);

            // Afișează datele într-un control GridView
            grdShow1.DataSource = dts2.Tables[0];
            grdShow1.DataBind();
        }
    }
}