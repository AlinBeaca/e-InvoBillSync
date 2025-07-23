using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace e_InvoBillSync
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-V9L4110,3710;Initial Catalog=e_InvoBillSync;Integrated Security=True;Encrypt=True;";
        public string chartLabelsRON;
        public string chartValuesRON;
        public string chartValuesEUR;
        public string chartValuesUSD;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDash();
                LoadChartData();
            }
        }

        protected void LoadDash()
        {
            int userId = Convert.ToInt32(Session["ID_LOGIN"]);
            int totalFacturi = 0;
            int facturiLunaCurenta = 0;
            decimal totalRON = 0, totalEUR = 0, totalUSD = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        COUNT(DISTINCT F.ID_factura) AS TotalFacturi,
                        (
                            SELECT COUNT(DISTINCT F2.ID_factura)
                            FROM Facturi F2
                            WHERE MONTH(F2.Data_Facturare) = MONTH(GETDATE())
                              AND YEAR(F2.Data_Facturare) = YEAR(GETDATE())
                              AND F2.ID_LOGIN = @ID_LOGIN
                        ) AS FacturiLuna,

                        ISNULL(SUM(CASE WHEN F.Valuta_Factura = 'RON' THEN PF.Valoare ELSE 0 END), 0) AS TotalRON,
                        ISNULL(SUM(CASE WHEN F.Valuta_Factura = 'EUR' THEN PF.Valoare ELSE 0 END), 0) AS TotalEUR,
                        ISNULL(SUM(CASE WHEN F.Valuta_Factura = 'USD' THEN PF.Valoare ELSE 0 END), 0) AS TotalUSD

                    FROM Facturi F
                    INNER JOIN Produse_facturi PF ON F.ID_factura = PF.ID_Factura
                    WHERE F.ID_LOGIN = @ID_LOGIN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_LOGIN", userId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalFacturi = Convert.ToInt32(reader["TotalFacturi"]);
                            facturiLunaCurenta = Convert.ToInt32(reader["FacturiLuna"]);
                            totalRON = Convert.ToDecimal(reader["TotalRON"]);
                            totalEUR = Convert.ToDecimal(reader["TotalEUR"]);
                            totalUSD = Convert.ToDecimal(reader["TotalUSD"]);
                        }
                    }
                }
            }

            lblTotalFacturi.Text = totalFacturi.ToString();
            lblFacturiLuna.Text = facturiLunaCurenta.ToString();
            lblValoareTotalaRON.Text = $"RON: {totalRON:N2}";
            lblValoareTotalaEUR.Text = $"EUR: {totalEUR:N2}";
            lblValoareTotalaUSD.Text = $"USD: {totalUSD:N2}";
        }

        private void LoadChartData()
        {
            int userId = Convert.ToInt32(Session["ID_LOGIN"]);
            var labels = new List<string>();
            var valuesRON = new Dictionary<string, decimal>();
            var valuesEUR = new Dictionary<string, decimal>();
            var valuesUSD = new Dictionary<string, decimal>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT FORMAT(F.Data_Facturare, 'MMM yyyy') AS Luna, F.Valuta_Factura, SUM(PF.Valoare) AS Total
                    FROM Facturi F
                    INNER JOIN Produse_facturi PF ON F.ID_factura = PF.ID_Factura
                    WHERE F.ID_LOGIN = @ID_LOGIN
                    GROUP BY FORMAT(F.Data_Facturare, 'MMM yyyy'), YEAR(F.Data_Facturare), MONTH(F.Data_Facturare), F.Valuta_Factura
                    ORDER BY YEAR(F.Data_Facturare), MONTH(F.Data_Facturare)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_LOGIN", userId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string luna = reader["Luna"].ToString();
                            string valuta = reader["Valuta_Factura"].ToString();
                            decimal valoare = Convert.ToDecimal(reader["Total"]);

                            if (!labels.Contains(luna))
                                labels.Add(luna);

                            switch (valuta)
                            {
                                case "RON":
                                    valuesRON[luna] = valoare;
                                    break;
                                case "EUR":
                                    valuesEUR[luna] = valoare;
                                    break;
                                case "USD":
                                    valuesUSD[luna] = valoare;
                                    break;
                            }
                        }
                    }
                }
            }

            chartLabelsRON = "[" + string.Join(",", labels.Select(l => $"'{l}'")) + "]";
            chartValuesRON = "[" + string.Join(",", labels.Select(l => valuesRON.ContainsKey(l) ? valuesRON[l].ToString("0.##", CultureInfo.InvariantCulture) : "0")) + "]";
            chartValuesEUR = "[" + string.Join(",", labels.Select(l => valuesEUR.ContainsKey(l) ? valuesEUR[l].ToString("0.##", CultureInfo.InvariantCulture) : "0")) + "]";
            chartValuesUSD = "[" + string.Join(",", labels.Select(l => valuesUSD.ContainsKey(l) ? valuesUSD[l].ToString("0.##", CultureInfo.InvariantCulture) : "0")) + "]";
        }
    }
}
