using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_web_equipo_5B
{
    public partial class Promo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            string codigo = txtVoucher.Text.Trim();
            VoucherNegocio negocio = new VoucherNegocio();

            if (negocio.ValidarCodigo(codigo))
            {
                Session["codigoVoucher"] = codigo;
                Response.Redirect("Premios.aspx");
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }
    }
}