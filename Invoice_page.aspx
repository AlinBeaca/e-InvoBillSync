<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice_page.aspx.cs" Inherits="e_InvoBillSync.Invoice_page" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <title>e-InvoBillSync - Facturare</title>
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
        nav a {
            color: white;
            margin-left: 1.5rem;
            text-decoration: none;
            font-weight: 500;
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
        .form-row {
            display: flex;
            flex-wrap: wrap;
            gap: 2rem;
        }
        .form-col {
            flex: 1 1 45%;
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
            padding: 0.75rem 2rem;
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
                <a href="Sablon_page.aspx">Sablon</a>
                <a href="Invoice_page.aspx" style="font-weight: 600;">Facturi</a>
                <a href="Invoice_report_page.aspx">Rapoarte</a>
                <a href="Stock.aspx">Produse & Stocuri</a>
                <a href="Clients.aspx">Clienți</a>
            </nav>
        </header>

        <div class="main-container">
           <asp:HiddenField ID="hfFacturaID" runat="server" />
           <asp:HiddenField ID="hfProdusID" runat="server" />
           <asp:HiddenField ID="hfTransportID" runat="server" />


            <div class="section-title">Detalii Factură</div>
            <div class="form-row">
                <div class="form-col form-group"><label>Tip Factură</label><asp:TextBox ID="txtTipFactura" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Valută</label>
                    <asp:DropDownList ID="ddlValuta" runat="server" CssClass="form-control">
                        <asp:ListItem Text="RON" Value="RON" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="USD" Value="USD" />
                    </asp:DropDownList>
                </div>
                <div class="form-col form-group"><label>Data Facturare</label><asp:TextBox ID="txtDataFactura" runat="server" TextMode="Date" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Termen Plata</label><asp:TextBox ID="txtTermenPlata" runat="server" TextMode="Date" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Nume Client</label><asp:TextBox ID="txtClient" runat="server" CssClass="form-control" /></div>
            </div>

            <div class="section-title">Produse și Transport</div>
            <div class="form-row">
                <div class="form-col form-group"><label>Denumire Produs</label><asp:TextBox ID="txtDenumire" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Cantitate</label><asp:TextBox ID="txtCantitate" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Delegat</label><asp:TextBox ID="txtDelegat" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Serie CI</label><asp:TextBox ID="txtSerieCI" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Număr înmatriculare</label><asp:TextBox ID="txtTransport" runat="server" CssClass="form-control" /></div>
            </div>

            <div class="section-title">Detalii Client</div>
            <div class="form-row">
                <div class="form-col form-group"><label>Nume Client (Detalii)</label><asp:TextBox ID="txtNume" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Tip Client</label><asp:TextBox ID="txtTipclient" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>CUI</label><asp:TextBox ID="txtCUI" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>CNP</label><asp:TextBox ID="txtCNP" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Nr Registru Comerț</label><asp:TextBox ID="txtNrRegistru" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Sediu</label><asp:TextBox ID="txtSediu" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Oraș</label><asp:TextBox ID="txtOras" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Județ</label><asp:TextBox ID="txtJudet" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Țară</label><asp:TextBox ID="txtTara" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Cont</label><asp:TextBox ID="txtCont" runat="server" CssClass="form-control" /></div>
                <div class="form-col form-group"><label>Banca</label><asp:TextBox ID="txtBanca" runat="server" CssClass="form-control" /></div>
            </div>

            <div class="text-end mt-4">
                <asp:Button ID="btnPreiaDate" runat="server" CssClass="btn-outline" Text="Preia Date" OnClick="BtnPreiaDate_Click" />
                <asp:Button ID="btnSalveaza" runat="server" CssClass="btn-save" Text="Salvează" OnClick="BtnSalveaza_Click" />
            </div>
        </div>
    </form>
</body>
</html>
