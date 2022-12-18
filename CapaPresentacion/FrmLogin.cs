using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaNegocio;
using CapaEntidad;
namespace CapaPresentacion
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtUsuario_MouseEnter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "DOCUMENTO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }



        private void txtContraseña_MouseEnter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "CONTRASEÑA")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseña_MouseLeave(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "")
            {
                txtContraseña.Text = "CONTRASEÑA";
                txtContraseña.ForeColor = Color.DimGray;
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void txtUsuario_MouseLeave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "DOCUMENTO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            // pedimos que nos retorne un solo usuario de la lista de clases, es decir una clase (Usuario) de la lista de clases (CN_USUARIO)
            // Utilizamos las expresiones lambda, que nos permite tomar acciones de nuestras listas, la busqueda de un objeto en este caso de
            // un usuario en nuestra lista. Le pedimos que nos devuelva un documento y una clave similares a los que estamos escribiendo en los txt
            // y con firstordefault, nos aseguramos de retornar el primero o sino que nos retorne nulo.

            Usuario osuario = new CN_Usuario().Listar().Where(u => u.Clave == txtContraseña.Text && u.Documento == txtUsuario.Text).FirstOrDefault();

            if (osuario != null)
            {
                if (osuario.Estado == false)
                {
                    MessageBox.Show("Usuario inactivo bloqueado, comunicarse con el administrador!!","Alerta de inactividad");
                    this.Dispose();
                }
                else
                {

                    FrmInicio form = new FrmInicio(osuario);
                    MessageBox.Show("BIENVENIDO " + osuario.NombreCompleto.ToString().ToUpper());
                    form.Show();
                    this.Hide();
                    form.FormClosing += frm_closing;
                    txtUsuario.Clear();
                    txtContraseña.Clear();

                }


            }


            else if (txtUsuario.Text == "DOCUMENTO" && txtContraseña.Text == "CONTRASEÑA" || txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                MessageBox.Show("Debe ingresar un DOCUMENTO Y CONTRASEÑA", "DATOS OBLIGATORIOS");
            }
            else if (txtContraseña.Text == "" || txtContraseña.Text == "CONTRASEÑA")
            {
                MessageBox.Show("Debe ingresar una contraseña", "CONTRASEÑA OBLIGATORIA");
            }
            else if (txtUsuario.Text == "" || txtUsuario.Text == "DOCUMENTO")
            {
                MessageBox.Show("Debe ingresar un documento", "DOCUMENTO OBLIGATORIO");
            }
            else
            {
                MessageBox.Show("DOCUMENTO o contraseña incorrectas");
              
            }
        }
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = false;
            txtContraseña.Text = "CONTRASEÑA";
            txtUsuario.Text = "DOCUMENTO";
            txtContraseña.ForeColor = Color.DimGray;
            txtUsuario.ForeColor = Color.DimGray;

            this.Show();

        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEntrar.PerformClick();
            }
        }

        private void txtContraseña_MouseDown(object sender, MouseEventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Minimized;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "CONTRASEÑA")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }
    }
}
