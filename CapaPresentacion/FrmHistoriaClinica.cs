using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmHistoriaClinica : Form
    {
        private static Usuario usuarioActual;
        public FrmHistoriaClinica(Usuario objusuario)
        {
            usuarioActual = objusuario;
            InitializeComponent();
        }
        private void limpiar()
        {
            txtnombrepaciente.Text = "";
            txtdocpaciente.Text = "";
            txtrecordatorio.Text = "";
            txtEvolucion.Text = "";
            txtPrescripcion.Text = "";
        }

        private void FrmHistoriaClinica_Load(object sender, EventArgs e)
        {
            txtnombreusuario.Text= usuarioActual.NombreCompleto.ToString();
            txtidmedico.Text= usuarioActual.IdUsuario.ToString();
        }


        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnbuscarpaciente_Click_1(object sender, EventArgs e)
        {
            using (var modal = new MdPaciente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdPaciente.Text = modal._Paciente.IdPaciente.ToString();
                    txtdocpaciente.Text = modal._Paciente.Documento;
                    txtnombrepaciente.Text = modal._Paciente.NombreCompletoPaciente;
                }
                else
                {
                    txtdocpaciente.Select();
                }


            }
        }

        private void btnAgregarTurno_Click_1(object sender, EventArgs e)
        {
            CN_HistoriaClinica Guardar = new CN_HistoriaClinica(txtIdPaciente.Text, txtnombrepaciente.Text, this.txtrecordatorio.Text, this.txtEvolucion.Text, this.txtPrescripcion.Text, txtnombreusuario.Text, txtidmedico.Text, "Atencion del paciente", "Turno exitoso", "FrmHistoriaClinica");
            limpiar();
        }
    }
}
