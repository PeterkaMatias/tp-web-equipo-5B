<%@ Page Title="Ingresar Voucher" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promo.aspx.cs" Inherits="tp_web_equipo_5B.Promo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title" class="container mt-4">
        <h2 class="mb-4">¡Ingresá el código de tu voucher!</h2>

        <div class="mb-3">
            <asp:TextBox ID="txtVoucher" runat="server" CssClass="form-control" placeholder="XXXXXXXXXXXXXXX"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Button ID="btnValidarVoucher" runat="server" OnClick="btnValidar_Click" Text="Siguiente" CssClass="btn btn-primary" />
        </div>

        <div>
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
        </div>
    </main>

</asp:Content>