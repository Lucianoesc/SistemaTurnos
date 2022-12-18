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
    public partial class FrmEspecialidades : Form
    {
        public FrmEspecialidades()
        {
            InitializeComponent();
        }

        private void FrmEspecialidades_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Value";
            cmbEstado.SelectedIndex = 0;


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


            //Mostrar todos los usuarios
            List<Especialidad> lista = new CN_Especialidad().Listar();

            foreach (Especialidad item in lista)
            {
                dtgvData.Rows.Add(new object[] {"",item.IdEspecialidad,
                item.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo" : "Inactivo"
                });

            }
        }
        private void limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cmbEstado.SelectedIndex = 0;

            txtDescripcion.Select();

        }

        private void dtgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.editar.Width;
                var h = Properties.Resources.editar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.editar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dtgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dtgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDescripcion.Text = dtgvData.Rows[indice].Cells["Descripcion"].Value.ToString();

                    foreach (OpcionCombo oc in cmbEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dtgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cmbEstado.Items.IndexOf(oc);
                            cmbEstado.SelectedIndex = indice_combo;
                            break;

                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
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

        private void btnLimpiarBuscador_Click_1(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dtgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Especialidad obj = new Especialidad()
            {
                IdEspecialidad = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbEstado.SelectedItem).Valor) == 1 ? true : false


            };

            if (obj.IdEspecialidad == 0)
            {
                int idgenerado = new CN_Especialidad().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    dtgvData.Rows.Add(new object[] {"",idgenerado,txtDescripcion.Text,
                ((OpcionCombo)cmbEstado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cmbEstado.SelectedItem).Texto.ToString()
                });
                    limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Especialidad().Editar(obj, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dtgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbEstado.SelectedItem).Texto.ToString();
                    // CN_Bitacora Guardar = new OpcionCombo(txtnombreCompleto.Text, txtId.Text.ToString(), "Usuario Editado", "Editado exitoso", "FrmMantenimiento");

                    limpiar();

                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea realmente eliminar la especialidad?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Especialidad obj = new Especialidad()
                    {

                        IdEspecialidad = Convert.ToInt32(txtId.Text)

                    };
                    bool respuesta = new CN_Especialidad().Eliminar(obj, out mensaje);
                    if (respuesta)
                    {
                        //CN_Bitacora Guardar = new CN_Bitacora(txtnombreCompleto.Text, txtId.Text.ToString(), "Usuario Eliminado", "Eliminacion exitosa", "FrmMantenimiento");
                        dtgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
