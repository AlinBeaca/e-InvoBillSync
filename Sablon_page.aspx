<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sablon_page.aspx.cs" Inherits="e_InvoBillSync.Sablon_page" %>
<!DOCTYPE html>
<html lang="ro">
<head>
    <meta charset="UTF-8" />
    <title>e-InvoBillSync - Sablon</title>
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

        nav a[href="Sablon_page.aspx"] {
            font-weight: 600;
        }

        .main-container {
            max-width: 900px;
            margin: 2rem auto;
            background: white;
            border-radius: 8px;
            padding: 2rem;
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
        }

        .section-title {
            font-size: 1.3rem;
            font-weight: 600;
            margin-bottom: 1rem;
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

        .form-group input {
            width: 100%;
            padding: .6rem;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn-save {
            background-color: #004085;
            color: white;
            padding: 0.75rem 2rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
        }
        .btn-outline {
            background: white;
            color: #004085;
            border: 2px solid #004085;
            padding: 0.6875rem 1.9375rem; 
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
            margin-right: 1rem;
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
            <div class="section-title">Detalii Companie</div>
            <div class="form-group"><label>Furnizor</label><asp:TextBox ID="txtFurnizor" runat="server" /></div>
            <div class="form-group"><label>CUI</label><asp:TextBox ID="txtCUI" runat="server" /></div>
            <div class="form-group"><label>Nr. Registrul Comertului</label><asp:TextBox ID="txtNrRegistrulComertului" runat="server" /></div>
            <div class="form-group"><label>Oras</label><asp:TextBox ID="txtOras" runat="server" /></div>
            <div class="form-group"><label>Adresa</label><asp:TextBox ID="txtAdresa" runat="server" /></div>
            <div class="form-group"><label>Judet</label><asp:TextBox ID="txtJudet" runat="server" /></div>
            <div class="form-group"><label>Date firma</label><asp:TextBox ID="txtDateFirma" runat="server" /></div>
            <div class="form-group"><label>Capital Social</label><asp:TextBox ID="txtCapitalSocial" runat="server" /></div>

            <div class="section-title">Detalii Reprezentant</div>
            <div class="form-group"><label>Nume</label><asp:TextBox ID="txtNume" runat="server" /></div>
            <div class="form-group"><label>Prenume</label><asp:TextBox ID="txtPrenume" runat="server" /></div>
            <div class="form-group"><label>Telefon</label><asp:TextBox ID="txtTelefon" runat="server" /></div>

            <div class="section-title">Detalii Cont Bancar</div>
            <div class="form-group"><label>Cod IBAN</label><asp:TextBox ID="txtCodIBAN" runat="server" /></div>
            <div class="form-group"><label>Banca</label><asp:TextBox ID="txtBanca" runat="server" /></div>

             <div class="text-end mt-4">
     <asp:Button ID="btnPreiaDate" runat="server" CssClass="btn-outline" Text="Preia Date" OnClick="BtnPreiaDate_Click" />
     <asp:Button ID="btnSalveaza" runat="server" CssClass="btn-save" Text="Salvează" OnClick="SaveButton_Click" />
 </div>
        </div>
    </form>
</body>
</html>
