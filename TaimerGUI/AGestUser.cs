﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TaimerGUI {
    public partial class AGestUser : Form {

        AAddUser childForm = null;
        int selectedRow = 0;
        List<Taimer.User> usuarios;

        public AGestUser() {
            InitializeComponent();
        }

        private void AGestUser_Load(object sender, EventArgs e) {

            clearLabels();

            usuarios = new List<Taimer.User>();

            Taimer.User user1 = new Taimer.User("Pepe Gotera", "11111111X", "asd@asd.com", "patata", 1, "Informatica");
            Taimer.User user2 = new Taimer.User("Jaimito", "22222222D", "qwe@rty.com", "patata", 2, "Informatica");

            usuarios.Add(user1);
            usuarios.Add(user2);

            foreach (Taimer.User user in usuarios) {
                dgUsers.Rows.Add(user.Nombre, user.DNI);
                dgUsers.Rows[dgUsers.RowCount-1].Tag = user;
            }
        }

        public void setChild(AAddUser form) {
            childForm = form;
        }

        private void btNewUser_Click(object sender, EventArgs e) {
            if (childForm != null) {
                Hide();
                childForm.Show();

                AdminForm parent = (AdminForm)this.MdiParent;
                parent.positionChilds();
            }
        }

        // Nombre

        private void lbUserName_Click(object sender, EventArgs e) {
            tbName.Text = lbUserName.Text;
            lbUserName.Visible = false;
            tbName.Visible = true;
            tbName.Focus();
        }

        private void tbName_Validated(object sender, EventArgs e) {
            lbUserName.Text = tbName.Text;
            tbName.Visible = false;
            lbUserName.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        // Email

        private void lbEmail_Click(object sender, EventArgs e) {
            tbEmail.Text = lbEmail.Text;
            lbEmail.Visible = false;
            tbEmail.Visible = true;
            tbEmail.Focus();
        }

        private void tbEmail_Validated(object sender, EventArgs e) {
            lbEmail.Text = tbEmail.Text;
            tbEmail.Visible = false;
            lbEmail.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        // Dni

        private void lbDni_Click(object sender, EventArgs e) {
            tbDni.Text = lbDni.Text;
            lbDni.Visible = false;
            tbDni.Visible = true;
            tbDni.Focus();
        }

        private void tbDni_Validated(object sender, EventArgs e) {
            lbDni.Text = tbDni.Text;
            tbDni.Visible = false;
            lbDni.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        // Curso

        private void lbCurso_Click(object sender, EventArgs e) {
            udCurso.Text = lbCurso.Text;
            lbCurso.Visible = false;
            udCurso.Visible = true;
            udCurso.Focus();

        }

        private void tbCurso_Validated(object sender, EventArgs e) {
            lbCurso.Text = udCurso.Value.ToString();
            udCurso.Visible = false;
            lbCurso.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        // Titulacion

        private void lbTitu_Click(object sender, EventArgs e) {
            tbTitu.Text = lbTitu.Text;
            lbTitu.Visible = false;
            tbTitu.Visible = true;
            tbTitu.Focus();
        }

        private void tbTitu_Validated(object sender, EventArgs e) {
            lbTitu.Text = tbTitu.Text;
            tbTitu.Visible = false;
            lbTitu.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        private void lbPass_Click(object sender, EventArgs e) {
            tbPass.Text = lbPass.Text;
            lbPass.Visible = false;
            tbPass.Visible = true;
            tbPass.Focus();
        }

        private void tbPass_Validated(object sender, EventArgs e) {
            lbPass.Text = tbPass.Text;
            tbPass.Visible = false;
            lbPass.Visible = true;
            btCancel.Enabled = true;
            btConfirm.Enabled = true;
        }

        private void dgUsers_CellClick(object sender, DataGridViewCellEventArgs e) {

            if (e.RowIndex >= 0 && e.RowIndex < dgUsers.Rows.Count) {

                selectedRow = e.RowIndex;

                Taimer.User user = (Taimer.User) dgUsers.Rows[selectedRow].Tag;

                fillLabels(user);

                btCancel.Enabled = true;
            }
        }

        private void btCancel_Click(object sender, EventArgs e) {

            Taimer.User user = (Taimer.User)dgUsers.Rows[selectedRow].Tag;

            fillLabels(user);

            btCancel.Enabled = false;
        }

        private void clearLabels() {
            lbUserName.Text = "";
            lbEmail.Text = "";
            lbPass.Text = "";
            lbDni.Text = "";
            lbCurso.Text = "";
            lbTitu.Text = "";
        }

        private void fillLabels(Taimer.User user) {
            lbUserName.Text     = user.Nombre;
            lbEmail.Text        = user.Email;
            lbPass.Text         = user.Password;
            lbDni.Text          = user.DNI;
            lbCurso.Text        = user.Curso.ToString();
            lbTitu.Text         = user.Titulacion;
        }

        private void btConfirm_Click(object sender, EventArgs e) {

            if (dataIsValid()) {

                Taimer.User user = (Taimer.User)dgUsers.Rows[selectedRow].Tag;

                dgUsers.Rows[selectedRow].Cells["Nombre"].Value = lbUserName.Text;
                dgUsers.Rows[selectedRow].Cells["Dni"].Value = lbDni.Text;

                user.Nombre = lbUserName.Text;
                user.DNI = lbDni.Text;
                user.Email = lbEmail.Text;
                user.Password = lbPass.Text;
                user.Curso = Convert.ToInt32(lbCurso.Text);
                user.Titulacion = lbTitu.Text;

                
            }
        }

        private bool dataIsValid() {
            bool valid = true;

            // Nombre
            if (lbName.Text == "") {
                lbErrName.Visible = true;
                valid = false;
            } else {
                lbErrName.Visible = false;
            }

            // Email
            Regex emailRegex = new Regex("[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}");
            if (lbEmail.Text == "") {
                lbErrEmail.Visible = true;
                lbErrEmailBad.Visible = false;
                valid = false;
            } else if (!emailRegex.IsMatch(lbEmail.Text)) {
                lbErrEmail.Visible = false;
                lbErrEmailBad.Visible = true;
                valid = false;
            } else {
                lbErrEmail.Visible = false;
                lbErrEmailBad.Visible = false;
            }

            // DNI
            Regex dniRegex = new Regex("[0-9]{8}[A-Z]");
            if (lbDni.Text == "") {
                lbErrDni.Visible = true;
                lbErrDniBad.Visible = true;
                valid = false;
            } else if (!dniRegex.IsMatch(lbDni.Text)) {
                lbErrDni.Visible = false;
                lbErrDniBad.Visible = true;
                valid = false;
            } else {
                lbErrDni.Visible = false;
                lbErrDniBad.Visible = false;
            }

            // Titulacion
            if (lbTitu.Text == "") {
                lbErrTitulacion.Visible = true;
                valid = false;
            } else {
                lbErrTitulacion.Visible = false;
            }

            // Titulacion
            if (lbPass.Text == "") {
                lbErrPass.Visible = true;
                valid = false;
            } else {
                lbErrPass.Visible = false;
            }

            return valid;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dgUsers.Rows) {
                Taimer.User user = (Taimer.User)row.Tag;
                String texto = textBox1.Text;
                if (user.Nombre.Contains(texto) || user.DNI.Contains(texto) ||
                    user.Email.Contains(texto) || user.Password.Contains(texto) || 
                    user.Curso.ToString().Contains(texto) || user.Titulacion.Contains(texto)) {

                    row.Visible = true;
                } else {
                    row.Visible = false;
                }
            }
        }

        public void addUser(Taimer.User user) {

            usuarios.Add(user);

            dgUsers.Rows.Add(user.Nombre, user.DNI);
            dgUsers.Rows[dgUsers.RowCount - 1].Tag = user;
        }
    }
}