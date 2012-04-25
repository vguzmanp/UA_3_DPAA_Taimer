﻿<%@ Page Title="Mensajes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
CodeBehind="Mensajes.aspx.cs" Inherits="WebTaimer.TabMensajes.Mensajes" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">

        #TextArea1
        {
            z-index: 1;
            left: 520px;
            top: 588px;
            position: absolute;
            height: 45px;
            width: 606px;
        }
        .style1
        {
            width: 208px;
        }
        .style2
        {
            width: 48px;
            height: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Mensajes
    </h2>

    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <asp:Panel ID="panelConversacion" runat="server" GroupingText="Usuarios">
                    <asp:ListBox ID="listaUsuarios" runat="server"
        
    style="z-index: 1; left: 0px; top: 0px; position: relative; height: 400px; width: 180px;">
                    </asp:ListBox>
                    <br />
                    <br />
                    <asp:Button ID="botonConversar" runat="server" Text="Conversar" Width="87px" />
                    <asp:Button ID="botonOpciones" runat="server" Text="Opciones" Width="87px"/>
                </asp:Panel>
            
            </td>
            <td>
                <asp:Panel ID="panelMensajes" runat="server" 
                    GroupingText="Conversación con [ Nombre del usuario ]">
                    <asp:Panel ID="panelMensajeria" runat="server" Height="344px" 
                        BorderStyle="Dotted">
                        <style>
                            .parrafosMensaje 
                            {
                                width: 427px;
                            }
                        </style>
                        
                        <div style="height: 342px; overflow: auto;">

                            <div style="width: 550px; margin: 0 auto; margin-top: 20px; background-color: White; border: 4px ridge White;">
                                <img alt="imagen usuario" class="style2" style="border: 4px ridge White; margin: 10px; float: left;"
                                    src="../Images/default.jpg" />
                                <div style="width: 441px; float: right; margin: 10px;">
                                    <p class="parrafosMensaje" style="color: Gray">Mensaje enviado por: [Amigo]</p>
                                    <p class="parrafosMensaje">
                                        Blablabla bla balbalbalbablabababbañbab
                                        <br /> Blablabla bla balbalbalbablabababbañbab
                                        <br /> Blablabla bla balbalbalbablabababbañbab
                                        <br /> Blablabla bla balbalbalbablabababbañbab
                                    </p>
                                </div>
                            </div>
                            
                        </div>
                    </asp:Panel>
                    <br />
                    <textarea ID="textareaMensaje" name="S1" rows="2"
                        
                        style="z-index: 1; left: 0px; top: 0px; position: relative; width: 619px; resize: none;"></textarea>
                    <asp:Button ID="botonEnviarComentario" runat="server" 
                        style="z-index: 1; left: 5px; top: 3px; position: relative; height: 23px; width: 45px;" 
                        Text="Enviar" />
                    <br />
                </asp:Panel>
            </td>
        </tr>
    </table>

    </asp:Content>
