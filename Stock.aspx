<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="e_InvoBillSync.Stock" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="UTF-8" />
    <title>e-InvoBillSync - Produse & Stocuri</title>
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

        nav a[href="Stock.aspx"] {
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
            margin-bottom: 1rem;
        }

        .form-group label {
            display: block;
            margin-bottom: .3rem;
            font-weight: 500;
        }

        .form-group input {
            width: 100%;
            padding: .6rem;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn-container {
            text-align: right;
            margin-top: 1rem;
        }

        .btn {
            background-color: #004085;
            color: white;
            padding: 0.6rem 1.2rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
            margin-left: .5rem;
        }

        .report-container {
            margin-top: 2rem;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 1rem;
        }

        .table th, .table td {
            border: 1px solid #dee2e6;
            padding: 0.75rem;
            text-align: left;
        }

        .table th {
            background-color: #004085;
            color: white;
            font-weight: 600;
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

        <div class="main-container">
            <div class="section-title">Adăugare / Gestionare Produse</div>

            <div class="form-group"><label>ID Stoc:</label><asp:TextBox ID="txtIDStoc" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Denumire:</label><asp:TextBox ID="txtDenumire" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Depozit:</label><asp:TextBox ID="txtDepozit" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Categorie:</label><asp:TextBox ID="txtCategorie" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Unitate Măsură:</label><asp:TextBox ID="txtUnitateMasura" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Oraș:</label><asp:TextBox ID="txtOras" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Cantitate:</label><asp:TextBox ID="txtCanitate" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Preț Unitar:</label><asp:TextBox ID="txtPret" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>TVA:</label><asp:TextBox ID="txtTVA" runat="server" CssClass="form-control" /></div>
            <div class="form-group"><label>Subtotal:</label><asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control" /></div>

            <div class="btn-container">
                <asp:Button ID="btnsave" runat="server" Text="Salvează" CssClass="btn" OnClick="btnsave_Click" />
                <asp:Button ID="btnupdate" runat="server" Text="Actualizează" CssClass="btn" OnClick="btnupdate_Click" />
                <asp:Button ID="btnshow" runat="server" Text="Afișează" CssClass="btn" OnClick="btnshow_Click" />
                <asp:Button ID="btndelete" runat="server" Text="Șterge" CssClass="btn" OnClick="btndelete_Click" />
            </div>

            <div class="report-container">
                <asp:GridView ID="grdShow2" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
            </div>
        </div>
    </form>
</body>
</html>
