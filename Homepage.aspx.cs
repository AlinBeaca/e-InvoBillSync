using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_InvoBillSync
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void btnStart_Click(object sender, EventArgs e)
        {
            // Exemplu: redirecționează către pagina principală a aplicației
            Response.Redirect("Signin_page.aspx"); // sau orice altă pagină dorești
        }

    }
}