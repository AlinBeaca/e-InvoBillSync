using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace e_InvoBillSync
{
    public partial class Invoice_report_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            int invoiceID;
            if (int.TryParse(txtInvoiceID.Text, out invoiceID))
            {
                GenerateInvoicePDF(invoiceID);
            }

        }

        private void GenerateInvoicePDF(int invoiceID)
        {
            
            string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";

            
            DataTable dtFacturi = new DataTable();
            DataTable dtProduseFacturi = new DataTable();
            DataTable dtClienti = new DataTable();
            DataTable dtTransport = new DataTable();
            DataTable dtUtilizatori = new DataTable();
            DataTable dtSablon = new DataTable();
            DataTable dtStocProduse = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Preluați datele pentru factura specificată
                SqlDataAdapter daFacturi = new SqlDataAdapter("SELECT * FROM Facturi WHERE ID_factura = @InvoiceID", con);
                daFacturi.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daFacturi.Fill(dtFacturi);

                SqlDataAdapter daProduseFacturi = new SqlDataAdapter("SELECT * FROM Produse_facturi WHERE ID_Factura = @InvoiceID", con);
                daProduseFacturi.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daProduseFacturi.Fill(dtProduseFacturi);

                SqlDataAdapter daClienti = new SqlDataAdapter("SELECT * FROM Firme WHERE Nume_client = (SELECT Nume_client FROM Facturi WHERE ID_factura = @InvoiceID)", con);
                daClienti.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daClienti.Fill(dtClienti);

                SqlDataAdapter daTransport = new SqlDataAdapter("SELECT * FROM Transport WHERE ID_Factura = @InvoiceID", con);
                daTransport.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daTransport.Fill(dtTransport);

                SqlDataAdapter daUtilizatori = new SqlDataAdapter("SELECT * FROM Utilizatori WHERE ID_LOGIN = (SELECT ID_LOGIN FROM Facturi WHERE ID_factura = @InvoiceID)", con);
                daUtilizatori.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daUtilizatori.Fill(dtUtilizatori);

                // Actualizați interogarea pentru a include toate coloanele necesare
                SqlDataAdapter daSablon = new SqlDataAdapter("SELECT ID_LOGIN, CUI, Furnizor, Nr_Registrul_Comertului, Oras, Adresa, Judet, Date_firma, Capital_Social, Nume, Prenume, Telefon, Cod_IBAN, Banca FROM Sablon WHERE ID_LOGIN = (SELECT ID_LOGIN FROM Facturi WHERE ID_factura = @InvoiceID)", con);
                daSablon.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daSablon.Fill(dtSablon);

                SqlDataAdapter daStocProduse = new SqlDataAdapter("SELECT * FROM Stoc_produse WHERE ID_Stoc IN (SELECT ID_Produs FROM Produse_facturi WHERE ID_Factura = @InvoiceID)", con);
                daStocProduse.SelectCommand.Parameters.AddWithValue("@InvoiceID", invoiceID);
                daStocProduse.Fill(dtStocProduse);
            }

            if (dtFacturi.Rows.Count > 0)
            {
                // Configurați raportul RDLC
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/InvoiceReport.rdlc");

                // Adăugați sursele de date la raport
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Facturi", dtFacturi));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Produse_facturi", dtProduseFacturi));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Clienti", dtClienti));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Transport", dtTransport));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Utilizatori", dtUtilizatori));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Sablon", dtSablon));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet_Stoc_produse", dtStocProduse));

                // Verificați dacă toate dataset-urile au date înainte de a seta parametrii
                if (dtSablon.Rows.Count > 0 && dtFacturi.Rows.Count > 0 && dtClienti.Rows.Count > 0 && dtTransport.Rows.Count > 0 && dtUtilizatori.Rows.Count > 0)
                {
                    // Configurați parametrii raportului
                    ReportParameter[] parameters = new ReportParameter[]
                    {
                        new ReportParameter("Furnizor", dtSablon.Rows[0]["Furnizor"].ToString()),
                        new ReportParameter("NumarRegistru", dtSablon.Rows[0]["Nr_Registrul_Comertului"].ToString()),
                        new ReportParameter("CUI", dtSablon.Rows[0]["CUI"].ToString()),
                        new ReportParameter("Sediu", dtSablon.Rows[0]["Adresa"].ToString()),
                        new ReportParameter("Judet", dtSablon.Rows[0]["Judet"].ToString()),
                        new ReportParameter("Cont", dtSablon.Rows[0]["Cod_IBAN"].ToString()),
                        new ReportParameter("Banca", dtSablon.Rows[0]["Banca"].ToString()),
                        new ReportParameter("DataFacturii", DateTime.Parse(dtFacturi.Rows[0]["Data_Facturare"].ToString()).ToString("d")),
                        new ReportParameter("IdFactura", dtFacturi.Rows[0]["ID_factura"].ToString()),
                        new ReportParameter("Termen", dtFacturi.Rows[0]["Termen_Plata"].ToString()),
                        new ReportParameter("Delegat", dtTransport.Rows[0]["Delegat"].ToString()),
                        new ReportParameter("Seria", dtTransport.Rows[0]["Seria_CI"].ToString()),
                        new ReportParameter("NumarInmatriculare", dtTransport.Rows[0]["Numar_inmatriculare"].ToString()),
                        new ReportParameter("Client", dtClienti.Rows[0]["Nume_client"].ToString()),
                        new ReportParameter("NumarRegistru_Client", dtClienti.Rows[0]["Nr_Reg_Com"].ToString()),
                        new ReportParameter("CUI_Cumparator", dtClienti.Rows[0]["CUI"].ToString()),
                        new ReportParameter("Sediu_client", dtClienti.Rows[0]["Sediu"].ToString()),
                        new ReportParameter("Judet_client", dtClienti.Rows[0]["Judet"].ToString()),
                        new ReportParameter("Cont_client", dtClienti.Rows[0]["Cont"].ToString()),
                        new ReportParameter("Banca_client", dtClienti.Rows[0]["Banca"].ToString()),

                    };
                    ReportViewer1.LocalReport.SetParameters(parameters);
                }
                // Exportați raportul în format PDF
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = ReportViewer1.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension,
                    out streamIds, out warnings);

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=Invoice_" + invoiceID + ".pdf");

                Response.BinaryWrite(bytes); // create the file
                Response.Flush(); // send it to the client to download

            }
        }

        protected void Btnshow_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT f.ID_factura, f.Nume_client, c.CUI AS ClientCUI, c.Nr_Reg_Com AS ClientNrRegCom, t.Delegat, t.Numar_inmatriculare, p.Denumire, p.Cantitate FROM Facturi f LEFT JOIN Firme c ON f.Nume_client = c.Nume_client LEFT JOIN Transport t ON f.ID_factura = t.ID_Factura LEFT JOIN Produse_Facturi p ON f.ID_factura = p.ID_Factura WHERE f.ID_LOGIN ='" + Session["ID_LOGIN"] + "'", conn);

              DataSet dts2 = new DataSet();
              sda.Fill(dts2);

              grdShow1.DataSource = dts2.Tables[0];
              grdShow1.DataBind();
            


           



        }
    }
}

    


