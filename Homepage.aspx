<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="e_InvoBillSync.Homepage" %>

<!DOCTYPE html>
<html lang="ro">
<head runat="server">
    <meta charset="utf-8" />
    <title>Acasă - e-InvoBillSync</title>
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

        .nav-links a {
            color: white;
            text-decoration: none;
            margin-left: 1.5rem;
            font-weight: 500;
        }

        .content-wrapper {
            display: flex;
            flex: 1;
            align-items: center;
            justify-content: space-between;
            padding: 5vh 8vw;
        }

        .welcome-box {
            background-color: white;
            padding: 2rem;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
            width: 400px;
            animation: fadeSlide 1s ease-in-out;
        }

        .welcome-box h2 {
            margin-bottom: 1rem;
            color: #004085;
        }

        .btn-start {
            background-color: #004085;
            color: white;
            padding: 0.7rem 1.5rem;
            border: none;
            border-radius: 5px;
            font-size: 1rem;
            margin-top: 1.2rem;
            cursor: pointer;
        }

        .illustration {
            flex: 1;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .illustration img {
            max-width: 90%;
            height: auto;
            animation: zoomIn 2s ease;
        }

        @keyframes fadeSlide {
            from { opacity: 0; transform: translateY(-20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        @keyframes zoomIn {
            from { transform: scale(0.8); opacity: 0; }
            to { transform: scale(1); opacity: 1; }
        }

        
        .modal-overlay {
            display: none;
            position: fixed;
            top: 0; left: 0;
            width: 100vw; height: 100vh;
            background-color: rgba(0,0,0,0.5);
            z-index: 9999;
            justify-content: center;
            align-items: center;
        }

        .modal-content {
            background-color: white;
            padding: 2rem;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0,0,0,0.3);
            text-align: center;
            max-width: 400px;
            animation: slideUp 0.5s ease;
        }

        @keyframes slideUp {
            from { transform: translateY(40px); opacity: 0; }
            to { transform: translateY(0); opacity: 1; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>e-InvoBillSync</h1>
            <div class="nav-links">
                <a href="Signin_page.aspx">Conectare</a>
                <a href="Signup_page.aspx">Înregistrare</a>
            </div>
        </header>

        <div class="content-wrapper">
            <div class="welcome-box">
                <h2>Bine ai venit!</h2>
                <p>Gestionează-ți ușor facturile și transporturile prin platforma noastră modernă și intuitivă.</p>
                <asp:Button ID="btnStart" runat="server" Text="Începe acum" CssClass="btn-start" OnClientClick="openWelcomeModal(); return false;" />
            </div>

            <div class="illustration">
                <img src="Homepage_image.png" alt="Homepage Illustration" />
            </div>
        </div>

        <!-- Modal -->
        <div id="welcomeModal" class="modal-overlay">
            <div class="modal-content">
                <h3>Bun venit în e-InvoBillSync!</h3>
                <p>Platforma ta de încredere pentru gestionarea documentelor digitale.</p>
                <p>Veți fi redirecționat în câteva secunde...</p>
            </div>
        </div>

        <script>
            function openWelcomeModal() {
                const modal = document.getElementById("welcomeModal");
                modal.style.display = "flex";
                setTimeout(() => {
                    window.location.href = "Signin_page.aspx";
                }, 3000);
            }
        </script>
    </form>
</body>
</html>
