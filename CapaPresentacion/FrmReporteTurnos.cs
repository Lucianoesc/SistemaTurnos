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
    public partial class FrmReporteTurnos : Form
    {
        public FrmReporteTurnos()
        {
            InitializeComponent();
        }

        private void FrmReporteTurnos_Load(object sender, EventArgs e)
        {
            //LLENAR EL COMBO DE BUSQUEDA CON LOS NOMBRES DE LOS MEDICOS
            List<Medico> lista = new CN_Medico().Listar();

            cmbMedicos.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" });
            foreach(Medico item in lista)
            {
                cmbMedicos.Items.Add(new OpcionCombo() { Valor = item.IdMedico, Texto= item.NombreCompleto});

            }
            cmbMedicos.DisplayMember = "Texto";
            cmbMedicos.ValueMember= "Valor";
            cmbMedicos.SelectedIndex= 0;

            //LLENAR EL COMBO DE BUSQUEDA FILTRO CON CADA DATO EN EL DATA GRID VIEW
            foreach(DataGridViewColumn column in dtgvData.Columns)
            {
                cmbBusqueda.Items.Add(new OpcionCombo() { Valor = column.Name, Texto = column.HeaderText });
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
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

        private void btnbuscarmedicos_Click_1(object sender, EventArgs e)
        {
            int idmedico = Convert.ToInt32(((OpcionCombo)cmbMedicos.SelectedItem).Valor.ToString());

            List<ReporteTurnos> lista = new List<ReporteTurnos>();

            lista = new CN_Reporte().Turno(
                dtpFechaInicio.Value.ToString("MM/dd/yyyy"),
                dtpFechaFin.Value.ToString("MM/dd/yyyy"),
                idmedico
                );

            dtgvData.Rows.Clear();

            foreach (ReporteTurnos rt in lista)
            {
                dtgvData.Rows.Add(new object[]
                {
                    rt.FechaRegistro,
                    rt.NumeroDocumento,
                    rt.UsuarioRegistro,
                    rt.DocumentoPaciente,
                    rt.NombreCompleto,
                    rt.NombreMedico,
                    rt.NumeroMatricula,
                    rt.Especialidad
                });
            }
        }

        private void btnLimpiarBuscador_Click_1(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dtgvData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
