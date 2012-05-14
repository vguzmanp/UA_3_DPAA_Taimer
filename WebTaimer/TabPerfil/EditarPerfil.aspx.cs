﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taimer;

namespace WebTaimer.TabPerfil
{
    public partial class EditarPerfil : System.Web.UI.Page
    {
        protected void botonModificarDatos_Click(object sender, EventArgs e)
        {
            User user = (User)Session["usuario"];
            string name=UserName.Text, titulacion=Titulacion.Text, email=Email.Text;
            string pass=PasswordAnterior.Text, pass2=NuevoPassword.Text, pass2Check=ConfirmarNuevoPassword.Text;
            string frase = FrasePersonal.Text;
            int curso = Curso.SelectedIndex + 1;
            bool error = false, cambio = false;

            if (name != "")
            {
                user.Nombre = name;
                cambio = true;
            }

            if (titulacion != "")
            {
                user.Nombre = titulacion;
                cambio = true;
            }

            if (email != "")
            {
                EmailValidation.Validate();

                if (!EmailValidation.IsValid)
                    error = true;

                else
                {
                    user.Email = email;
                    cambio = true;
                }
            }
            
            if (pass != "")
            {
                if (pass != user.Password)
                {
                    PasswordRequired.IsValid = false;
                    error = true;
                }
                else if (pass2 != "")
                {
                    NuevoPassValidator.Validate();
                    if (NuevoPassValidator.IsValid)
                    {
                        if (pass2 != pass2Check)
                        {
                            ConfirmarNuevoPassRequiered.IsValid = false;
                            error = true;
                        }
                        else
                        {
                            user.Password = pass2;
                            cambio = true;
                        }
                    }
                    else
                        error = true;
                }
                else
                {                    
                    NuevoPassRequiered.IsValid = false;
                    error = true;
                }
            }

            if (curso != user.Curso)
            {
                user.Curso = curso;
                cambio = true;
            }

            if (frase != "")
            {
                
            }
            
            if (!error && cambio)
            {
                Response.Write(user.Nombre+'\n'+user.Email+'\n'+user.Titulacion+'\n'+user.Password+'\n'+user.Curso);
            }
        }
    }
}