<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="e_InvoBillSync.Clients" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Clienți - e-InvoBillSync</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            background: linear-gradient(to bottom right, #e9f0fb, #ffffff);
            min-height: 100vh;
            display: flex;
            flex-direction: column;
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

        nav a[href="Clients.aspx"] {
            font-weight: 600;
        }

        .container {
            padding: 4vh 8vw;
        }

        h2 {
            color: #004085;
            margin-bottom: 2rem;
        }

        .clients-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 1.5rem;
        }

        .client-card {
            background-color: white;
            border-left: 5px solid #004085;
            border-radius: 10px;
            padding: 1.2rem 1.5rem;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s ease;
        }

        .client-card:hover {
            transform: scale(1.03);
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

        <div class="container">
            <h2>Clienți</h2>
            <div class="clients-grid">
                <asp:Repeater ID="rptClients" runat="server">
                    <ItemTemplate>
                        <div class="client-card">
                            <%# Container.DataItem.ToString() %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
