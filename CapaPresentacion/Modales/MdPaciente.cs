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

namespace CapaPresentacion.Modales
{
    public partial class MdPaciente : Form
    {
        public Paciente _Paciente { get; set; }
        
        public MdPaciente()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MdPaciente_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dtgvData.Columns)
            {
                if (column.Visible == true)
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Value";
            cmbBusqueda.SelectedIndex = 0;


            List<Paciente> lista = new CN_Paciente().Listar();

            foreach (Paciente item in lista)
            {
                dtgvData.Rows.Add(new object[] {
                    item.IdPaciente,
                    item.Documento,
                    item.NombreCompletoPaciente,
                });

            }
        }

        private void dtgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;
            if (iRow >= 0 && iColum > 0)
            {
                _Paciente = new Paciente()
                {
                    IdPaciente = Convert.ToInt32(dtgvData.Rows[iRow].Cells["Id"].Value.ToString()),
                    Documento = dtgvData.Rows[iRow].Cells["Documento"].Value.ToString(),
                    NombreCompletoPaciente = dtgvData.Rows[iRow].Cells["NombreCompleto"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

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
