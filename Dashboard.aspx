<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="e_InvoBillSync.Dashboard" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Dashboard - e-InvoBillSync</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            background: linear-gradient(to bottom right, #e9f0fb, #ffffff);
            background-attachment: fixed;
            display: flex;
            flex-direction: column;
            height: 100vh;
        }

        header {
            background-color: #004085;
            color: white;
            padding: 1rem 2rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        header h1 {
            margin: 0;
        }

        nav {
            display: flex;
            gap: 1.5rem;
        }

        nav a {
            color: white;
            text-decoration: none;
            font-weight: 500;
            transition: color 0.3s ease;
        }

        nav a:hover {
            color: #cce5ff;
        }

        nav a[href="Dashboard.aspx"] {
            font-weight: 600;
        }

        .dashboard-wrapper {
            display: flex;
            flex-direction: column;
            flex: 1;
            padding: 4vh 8vw;
        }

        .metrics {
            display: flex;
            gap: 1.5rem;
        }

        .metric-card {
            background-color: white;
            border-left: 5px solid #004085;
            border-radius: 10px;
            padding: 1rem 1.5rem;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            animation: fadeInUp 1s ease forwards;
            flex: 1;
        }

        .metric-card h3 {
            margin: 0;
            color: #004085;
        }

        .metric-card p {
            font-size: 1.2rem;
            font-weight: bold;
            margin-top: .5rem;
        }

        .illustration {
            margin-top: 2rem;
            display: flex;
            justify-content: center;
            align-items: center;
            animation: zoomIn 2s ease;
        }

        .illustration img {
            max-width: 90%;
            height: auto;
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        @keyframes zoomIn {
            0% { transform: scale(0.8); opacity: 0; }
            100% { transform: scale(1); opacity: 1; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>e-InvoBillSync</h1>
            <nav>
                <a href="Dashboard.aspx">Dashboard</a>
                <a href="Sablon_page.aspx">Sablon</a>
                <a href="Invoice_page.aspx">Facturi</a>
                <a href="Invoice_report_page.aspx">Rapoarte</a>
                <a href="Stock.aspx">Produse & Stocuri</a>
                <a href="Clients.aspx">Clienți</a>
            </nav>
        </header>

        <div class="dashboard-wrapper">
            <div class="metrics">
                <div class="metric-card">
                    <h3>Total Facturi</h3>
                    <p><asp:Label ID="lblTotalFacturi" runat="server" Text="--" /></p>
                </div>
                <div class="metric-card">
                    <h3>Valoare Totală</h3>
                    <asp:Label ID="lblValoareTotalaRON" runat="server" Text="--" /><br />
                    <asp:Label ID="lblValoareTotalaEUR" runat="server" Text="--" /><br />
                    <asp:Label ID="lblValoareTotalaUSD" runat="server" Text="--" />
                </div>
                <div class="metric-card">
                    <h3>Facturi Luna Curentă</h3>
                    <p><asp:Label ID="lblFacturiLuna" runat="server" Text="--" /></p>
                </div>
            </div>

            <div class="metric-card" style="margin-top:2rem;">
                <h3>Evoluție Facturi</h3>
                <canvas id="valutaChart" height="100"></canvas>
            </div>

            
        </div>

   <script>
       const ctx = document.getElementById('valutaChart').getContext('2d');
       const valutaChart = new Chart(ctx, {
           type: 'bar',
           data: {
               labels: <%= chartLabelsRON %>,
            datasets: [
                {
                    label: 'RON',
                    data: <%= chartValuesRON %>,
                    backgroundColor: 'rgba(0, 64, 133, 0.7)'
                },
                {
                    label: 'EUR',
                    data: <%= chartValuesEUR %>,
                    backgroundColor: 'rgba(40, 167, 69, 0.7)'
                },
                {
                    label: 'USD',
                    data: <%= chartValuesUSD %>,
                    backgroundColor: 'rgba(255, 193, 7, 0.7)'
                }
            ]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    stacked: false
                },
                y: {
                    beginAtZero: true,
                    stacked: false
                }
            },
            plugins: {
                legend: {
                    position: 'top'
                },
                tooltip: {
                    mode: 'index',
                    intersect: false
                }
            }
        }
    });
   </script>



    </form>
</body>
</html>
