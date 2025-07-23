<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_report_page.aspx.cs" Inherits="e_InvoBillSync.Invoice_report_page" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="UTF-8" />
    <title>e-InvoBillSync - Rapoarte Facturi</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            margin: 0;
            font-family: 'Poppins', sans-serif;
            background-color: #f8f9fa;
            color: #333;
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

        nav a[href="Invoice_report_page.aspx"] {
            font-weight: 600;
        }

        .main-container {
            max-width: 1000px;
            margin: 2rem auto;
            background: white;
            border-radius: 8px;
            padding: 2rem;
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
        }

        .section-title {
            font-size: 1.3rem;
            font-weight: 600;
            margin-bottom: 1.5rem;
            border-bottom: 2px solid #004085;
            padding-bottom: .5rem;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .form-group label {
            display: block;
            margin-bottom: .3rem;
            font-weight: 500;
        }

        .form-control {
            width: 100%;
            padding: .6rem;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn-save {
            background-color: #004085;
            color: white;
            padding: 0.6rem 1.5rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
        }

        .btn-outline {
            background: white;
            color: #004085;
            border: 2px solid #004085;
            padding: 0.6rem 1.5rem;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
            margin-right: 1rem;
        }

        .text-end {
            text-align: right;
            margin-top: 1rem;
        }

        .report-container {
            margin-top: 2rem;
        }

        .grid-container {
            margin-top: 2rem;
            overflow-x: auto;
            border-radius: 8px;
            border: 1px solid #ccc;
        }

        .styled-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 0.95rem;
        }

        .styled-grid th, .styled-grid td {
            padding: 0.75rem 1rem;
            border: 1px solid #dee2e6;
            text-align: left;
        }

        .styled-grid th {
            background-color: #004085;
            color: white;
            font-weight: 600;
        }

        .styled-grid tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

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

        <div class="main-container">
            <div class="section-title">Raport Factură</div>

            <div class="form-group">
                <label for="txtInvoiceID">ID Factură:</label>
                <asp:TextBox ID="txtInvoiceID" runat="server" CssClass="form-control" />
            </div>

            <div class="text-end">
                <asp:Button ID="btnGeneratePDF" runat="server" CssClass="btn-outline" Text="Generează PDF" OnClick="btnGeneratePDF_Click" />
                <asp:Button ID="btnshow" runat="server" CssClass="btn-save" Text="Afișează" OnClick="Btnshow_Click" />
            </div>

            <div class="grid-container">
                <asp:GridView ID="grdShow1" runat="server" AutoGenerateColumns="true" CssClass="styled-grid" />
            </div>

            <div class="report-container">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="500px" />
            </div>
        </div>
    </form>
</body>
</html>
