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
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmInicio : Form
    {
        private static Usuario usuarioActual;
        public FrmInicio(Usuario objusuario)
        {
            usuarioActual = objusuario;
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);

        private Form formActivo = null;
        public void abrirFormsHijos(Form formHijo)
        {
            if (formActivo != null)
                formActivo.Close();
            formActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            PanelFormularioHijo.Controls.Add(formHijo);
            PanelFormularioHijo.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
            formActivo.FormClosed += new FormClosedEventHandler(CierreForm);

        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Minimized;
        }

        private void panelFormHijo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void FrmInicio_Load(object sender, EventArgs e)
        {
            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);
            lblRol.Text = usuarioActual.oRol.Descripcion;




            lblusuario.Text = usuarioActual.NombreCompleto;
            lbldocumento.Text = usuarioActual.Documento;
            lblcorreo.Text = usuarioActual.Correo;



            if (lblRol.Text == "ADMINISTRATIVO")
            {
                picDoctoreslogo.Visible = false;
                picAdminLogoInicio.Visible = false;
                picAdminLogo.Visible = false;
                picDef.Visible = false;
                picAdmin.Visible = false;

                picAdminLogoInicio.Visible = false;
                btnMantenimiento.Visible = false;
                btnMantenimiento.Enabled = false;


                btnEfectuarTurno.Visible = false;
                btnEfectuarTurno.Enabled = false;
                picEfectuarTurno.Visible = false;
                picEfectuarTurno.Enabled = false;
                lblEfectuarTurno.Visible = false;
                picVertodoslosturnosmed.Visible = false;
                picVertodoslosturnosmed.Enabled = false;
                lblVerTodoslosturnos.Visible = false;
                pichistorialmedicomed.Visible = false;
                pichistorialmedicomed.Enabled = false;
                lblverhistoriaclinicamed.Visible = false;
            }
            if (lblRol.Text == "ADMINISTRADOR")
            {
                picDoctoreslogo.Visible = false;
                picRecepcion.Visible = false;
                picDef.Visible = false;
                picDoctor.Visible = false;
                picSecretario.Visible = false;
                picPacientes.Visible = false;
                picPacientes.Enabled = false;
                lblAgregarPacientes.Visible=false;
                picMedicos.Visible = false;
                picMedicos.Enabled = false;
                lblAgregarMedicos.Visible=false;
                picTurnos.Visible = false;
                picTurnos.Enabled = false;
                lblCrearTurno.Visible = false;
                picDetalles.Visible = false;
                picDetalles.Enabled = false;
                lblDetalleDelTurno.Visible = false;
                picReportesTurnos.Visible = false;
                picReportesTurnos.Enabled = false;
                lblTodosLosTurnosRecep.Visible = false;
                picHistoriaClinicaRecepcion.Visible = false;
                picHistoriaClinicaRecepcion.Enabled = false;
                lblVerHistoriaClinicaRecep.Visible = false;

                picEfectuarTurno.Visible = false;
                picEfectuarTurno.Enabled = false;
                lblEfectuarTurno.Visible = false;
                picVertodoslosturnosmed.Visible = false;
                picVertodoslosturnosmed.Enabled = false;
                lblVerTodoslosturnos.Visible = false;
                pichistorialmedicomed.Visible = false;
                pichistorialmedicomed.Enabled = false;
                lblverhistoriaclinicamed.Visible = false;

            }

            if (lblRol.Text == "MEDICO")
            {
                picAdminLogoInicio.Visible = false;
                picAdminLogo.Visible = false;
                picDef.Visible = false;
                picAdmin.Visible = false;
                picSecretario.Visible = false;
                picRecepcion.Visible = false;

                picRecepcion.Visible = false;
                btnPacientes.Visible = false;
                btnPacientes.Enabled = false;
                btnMedicos.Visible = false;
                btnMedicos.Enabled = false;
                btnMantenimiento.Visible = false;
                btnMantenimiento.Enabled = false;
                btnEspecialidades.Visible = false;
                btnEspecialidades.Enabled = false;
                btnCrearTurno.Visible = false;
                btnCrearTurno.Enabled = false;
                btnDetalleTurno.Visible = false;
                btnDetalleTurno.Enabled = false;
                picPacientes.Visible = false;
                picPacientes.Enabled = false;
                lblAgregarPacientes.Visible = false;
                picMedicos.Visible = false;
                picMedicos.Enabled = false;
                lblAgregarMedicos.Visible = false;
                picTurnos.Visible = false;
                picTurnos.Enabled = false;
                lblCrearTurno.Visible = false;
                picDetalles.Visible = false;
                picDetalles.Enabled = false;
                lblDetalleDelTurno.Visible = false;
                picReportesTurnos.Visible = false;
                picReportesTurnos.Enabled = false;
                lblTodosLosTurnosRecep.Visible = false;
                picHistoriaClinicaRecepcion.Visible = false;
                picHistoriaClinicaRecepcion.Enabled = false;
                lblVerHistoriaClinicaRecep.Visible = false;



            }

        }

        private void btnSalirProgram_Click(object sender, EventArgs e)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHistoriaClinica_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmHistoriaClinica(usuarioActual));
            btnEfectuarTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmPacientes());
            btnPacientes.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnMedicos_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmMedicos());
            btnMedicos.BackColor = Color.FromArgb(0, 100, 182);
        }


        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmMantenimiento());
            btnMantenimiento.BackColor = Color.FromArgb(0, 100, 182);
        }
        private void CierreForm(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["FrmMantenimiento"] == null)
                btnPacientes.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmPacientes"] == null)
                btnPacientes.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmMedicos"] == null)
                btnMedicos.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmMantenimiento"] == null)
                btnMantenimiento.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmHistoriaClinica"] == null)
                btnEfectuarTurno.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmTurnos"] == null)
                btnCrearTurno.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmEspecialidades"] == null)
                btnEspecialidades.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmDetalleTurno"] == null)
                btnDetalleTurno.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmReporteTurnos"] == null)
                btnReporteTurnos.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmReporteTurnos"] == null)
                btnReporteTurnos.BackColor = Color.FromArgb(11, 7, 19);
            if (Application.OpenForms["FrmReporteHistoriaClinica"] == null)
                btnHistoriaClinicaVER.BackColor = Color.FromArgb(11, 7, 19);
        }

        private void PanelFormularioHijo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void btnEspecialidades_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmEspecialidades());
            btnEspecialidades.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnCrearTurno_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmTurnos(usuarioActual));
            btnCrearTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnDetalleTurno_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmDetalleTurno());
            btnDetalleTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnReporteTurnos_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteTurnos());
            btnReporteTurnos.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void btnHistoriaClinicaVER_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteHistoriaClinica());
            btnHistoriaClinicaVER.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmPacientes());
            btnPacientes.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmMedicos());
            btnMedicos.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmTurnos(usuarioActual));
            btnCrearTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmDetalleTurno());
            btnDetalleTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteTurnos());
            btnReporteTurnos.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteHistoriaClinica());
            btnHistoriaClinicaVER.BackColor = Color.FromArgb(0, 100, 182);
        }
        private void picVertodoslosturnos_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteTurnos());
            btnReporteTurnos.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void picEfectuarTurno_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmHistoriaClinica(usuarioActual));
            btnEfectuarTurno.BackColor = Color.FromArgb(0, 100, 182);
        }

        private void pichistorialmedicomed_Click(object sender, EventArgs e)
        {
            abrirFormsHijos(new FrmReporteHistoriaClinica());
            btnHistoriaClinicaVER.BackColor = Color.FromArgb(0, 100, 182);
        }

    }
}
