<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup_page.aspx.cs" Inherits="e_InvoBillSync.Signup_page" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Inregistrare - e-InvoBillSync</title>
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
            justify-content: space-between;
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
            0% {
                opacity: 0;
                transform: translateY(-20px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
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

        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 0.6rem;
            border: 1px solid #ccc;
            border-radius: 5px;
            margin-bottom: 1rem;
        }

        .btn-register {
            width: 100%;
            background-color: #004085;
            color: white;
            padding: 0.7rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            cursor: pointer;
        }

        .form-links {
            text-align: center;
            font-size: 0.9rem;
            margin-top: 1rem;
        }

        .form-links a {
            color: #004085;
            text-decoration: none;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>e-InvoBillSync</h1>
        </header>

        <div class="content-wrapper">
            <div class="form-wrapper">
                <h2>Inregistrare</h2>

                <label for="txtemail">Email</label>
                <asp:TextBox ID="txtemail" runat="server" placeholder="Adresa de email" />

                <label for="txtpass">Parolă</label>
                <asp:TextBox ID="txtpass" runat="server" TextMode="Password" placeholder="Parola" OnTextChanged="Password_TextChanged" />

                <label for="txtusername">Nume de utilizator</label>
                <asp:TextBox ID="txtusername" runat="server" placeholder="Nume de utilizator" />

                <asp:Button ID="Button2" runat="server" CssClass="btn-register" Text="Înregistrează-te" OnClick="Btnregister1_Click" />

                <div class="form-links">
                    <a href="Signin_page.aspx">Ai deja cont? Conectează-te</a>
                </div>
            </div>
            <asp:Label ID="lblid" runat="server" Visible="false" />
            <div class="animated-illustration">
                <img src="Inregistrare_image.png" alt="Register Illustration" />
            </div>
        </div>
    </form>
</body>
</html>
