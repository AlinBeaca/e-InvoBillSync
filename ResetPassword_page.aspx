<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword_page.aspx.cs" Inherits="e_InvoBillSync.ResetPassword_page" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Setare parolă nouă - e-InvoBillSync</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
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
            font-size: 1.5rem;
        }

        .content-wrapper {
            display: flex;
            flex: 1;
            align-items: center;
            justify-content: center;
            gap: 4vw;
            padding: 4vh 8vw;
        }


        .form-wrapper {
            background-color: white;
            padding: 2rem;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
            width: 360px;
            animation: fadeSlide 1s ease-in-out;
        }

        @keyframes fadeSlide {
            0% { opacity: 0; transform: translateY(-20px); }
            100% { opacity: 1; transform: translateY(0); }
        }

        .form-wrapper h2 {
            margin-bottom: 1.5rem;
            color: #004085;
        }

        label {
            display: block;
            margin-bottom: .5rem;
            font-weight: 500;
        }

        input[type="password"] {
            width: 100%;
            padding: 0.6rem;
            border: 1px solid #ccc;
            border-radius: 5px;
            margin-bottom: 1rem;
        }

        .btn-save {
            width: 100%;
            background-color: #004085;
            color: white;
            padding: 0.7rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
        }

        .form-info {
            text-align: center;
            font-size: 0.9rem;
            margin-top: 1rem;
        }

        .animated-illustration {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .animated-illustration img {
            max-width: 70%;
            height: auto;
            animation: zoomIn 2s ease;
        }

        @keyframes zoomIn {
            0% { transform: scale(0.8); opacity: 0; }
            100% { transform: scale(1); opacity: 1; }
        }

        .message-label {
            color: red;
            font-size: 0.9rem;
            margin-bottom: 1rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>e-InvoBillSync</h1>
        </header>

        <div class="content-wrapper">
            <div class="form-wrapper">
                <h2>Setare parolă nouă</h2>

                <asp:Label ID="lblMessage" runat="server" CssClass="message-label" Visible="false" />

                <label for="txtParolaNoua">Parola nouă</label>
                <asp:TextBox ID="txtParolaNoua" runat="server" TextMode="Password" placeholder="Introdu parola nouă" />

                <asp:Button ID="btnSalveazaParola" runat="server" CssClass="btn-save" Text="Salvează parola" OnClick="btnSalveazaParola_Click" />

                <div class="form-info">
                    <a href="Signin_page.aspx">Ți-ai amintit parola? Conectează-te</a>
                </div>
            </div>

            <div class="animated-illustration">
                <img src="Reset_image.png" alt="Reset Illustration" />
            </div>
        </div>
    </form>
</body>
</html>
