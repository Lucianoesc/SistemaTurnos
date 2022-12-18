using CapaEntidad;
using CapaNegocio;
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
    public partial class FrmDetalleTurno : Form
    {
        
        public FrmDetalleTurno()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Turno oTurno = new CN_Turno().ObtenerTurno(txtBuscar.Text);
            if(oTurno.IdTurno != 0)
            {
                txtNumeroDocumento.Text = oTurno.NumeroDocumento;
                txtFecha.Text = oTurno.FechaRegistro;
                txtNumeroDocPaciente.Text = oTurno.oPaciente.Documento;
                txtGeneradorTurno.Text = oTurno.oUsuario.NombreCompleto;
                txtNombreCompletoPaciente.Text = oTurno.oPaciente.NombreCompletoPaciente;

                dtgvData.Rows.Clear();
                foreach(Detalle_Turno dt in oTurno.oDetalleTurno)
                {
                    
                    dtgvData.Rows.Add(new object[]
                {
                    txtNombre.Text= dt.oMedico.NombreCompleto,
                    dt.oMedico.NombreCompleto,
                    dt.oMedico.DocumentoMedico,
                    dt.oMedico.NumeroMatricula
                });
                }
                

            }    

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {

        }
    }
}
