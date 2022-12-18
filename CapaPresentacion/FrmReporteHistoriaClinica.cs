using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
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
    public partial class FrmReporteHistoriaClinica : Form
    {
        public FrmReporteHistoriaClinica()
        {
            InitializeComponent();
        }


        private void FrmReporteHistoriaClinica_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dtgvData.Columns)
            {
                if (column.Visible == true && column.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Value";
            cmbBusqueda.SelectedIndex = 0;



            List<Historia_Clinica> lista = new CN_ReporteHistoriaClinica().Listar();

            foreach (Historia_Clinica item in lista)
            {
                dtgvData.Rows.Add(new object[] {
                    item.idEvento,
                    item.Fecha,
                    item.Hora,
                    item.IdPaciente,
                    item.Paciente,
                    item.Recordatorio,
                    item.Evolucion,
                    item.Prescripcion,
                    item.IdUsuario,
                    item.Usuario,
                    item.Evento,
                    item.Detalle,
                    item.Origen
                });

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dtgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dtgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
