<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signin_page.aspx.cs" Inherits="e_InvoBillSync.Signin_page" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Conectare - e-InvoBillSync</title>
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

        .btn-login {
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
            display: flex;
            justify-content: space-between;
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
            max-width: 90%;
            height: auto;
            animation: zoomIn 2s ease;
        }

        @keyframes zoomIn {
            0% { transform: scale(0.8); opacity: 0; }
            100% { transform: scale(1); opacity: 1; }
        }

        .custom-popup {
            position: fixed;
            top: 20px;
            right: 20px;
            background-color: #dc3545;
            color: white;
            padding: 12px 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.3);
            font-weight: 500;
            z-index: 9999;
            opacity: 0;
            animation: fadeInOut 4s ease forwards;
        }

        @keyframes fadeInOut {
            0% { opacity: 0; transform: translateY(-10px); }
            10% { opacity: 1; transform: translateY(0); }
            90% { opacity: 1; transform: translateY(0); }
            100% { opacity: 0; transform: translateY(-10px); }
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
                <h2>Conectare</h2>

                <label for="txtusername">Username</label>
                <asp:TextBox ID="txtusername" runat="server" placeholder="Introduceți username-ul" />

                <label for="txtpass">Parolă</label>
                <asp:TextBox ID="txtpass" runat="server" TextMode="Password" placeholder="Introduceți parola" OnTextChanged="txtpass_TextChanged" />

                <asp:Button ID="Button1" runat="server" CssClass="btn-login" Text="Conectare" OnClick="BtnLogin_Click" />

                <div class="form-links">
                    <a href="ForgetPassword_page.aspx">Ai uitat parola?</a>
                    <a href="Signup_page.aspx">Înregistrare</a>
                </div>
            </div>

            <div class="animated-illustration">
                <img src="Conectare_image.png" alt="Login Illustration" />
            </div>
        </div>

        <script>
            function showErrorPopup(message) {
                const popup = document.createElement("div");
                popup.className = "custom-popup";
                popup.innerText = message;
                document.body.appendChild(popup);
                setTimeout(() => popup.remove(), 4000);
            }
        </script>
    </form>
</body>
</html>
