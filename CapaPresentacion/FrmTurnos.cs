using CapaEntidad;
using CapaPresentacion.Modales;
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
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmTurnos : Form
    {
        private Usuario _Usuario;

        public FrmTurnos(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FrmTurnos_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtHora.Text = DateTime.Now.ToString("hh:mm:ss");
            txtIdPaciente.Text = "0";
            txtIdMedico.Text = "0";
        }

        private void Limpiar()
        {
            txtIdMedico.Text = "0";
            txtdocmedico.Text = "";
            txtnombremedico.Text = "";
            txtnumeromatricula.Text = "";
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dtgvData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.cancelar.Width;
                var h = Properties.Resources.cancelar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.cancelar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dtgvData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvData.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    dtgvData.Rows.RemoveAt(indice);
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (var modal = new MdMedico())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdMedico.Text = modal._Medico.IdMedico.ToString();
                    txtdocmedico.Text = modal._Medico.DocumentoMedico;
                    txtnombremedico.Text = modal._Medico.NombreCompleto;
                    txtnumeromatricula.Text = modal._Medico.NumeroMatricula;
                }
                else
                {
                    txtdocpaciente.Select();
                }
            }
        }

        private void btnAgregarTurno_Click_1(object sender, EventArgs e)
        {
            bool medico_existe = false;

            foreach (DataGridViewRow fila in dtgvData.Rows)
            {
                if (fila.Cells["IdMedico"].Value.ToString() == txtIdMedico.Text)
                {
                    // lo siguiente comentado logra realizar que cada agregar turno pueda tener 1 medico como maximo.
                    //medico_existe = true;
                    //break;
                }
            }
            if (!medico_existe)
            {
                dtgvData.Rows.Add(new object[]
                {
                    txtIdMedico.Text,
                    txtnombremedico.Text,
                    txtdocmedico.Text,
                    txtIdPaciente.Text,
                    txtnombrepaciente.Text,
                    txtdocpaciente.Text
                });
                Limpiar();
            }
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdPaciente.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un paciente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dtgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar medicos en el turno", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_turno = new DataTable();

            detalle_turno.Columns.Add("IdMedico", typeof(string));

            foreach (DataGridViewRow row in dtgvData.Rows)
            {
                detalle_turno.Rows.Add(
                    new object[]
                    {
                       Convert.ToInt32(row.Cells["IdMedico"].Value.ToString()),
                    });
            }

            int idcorrelativo = new CN_Turno().ObtenerCorrelativo();
            string numerodocumento = string.Format("{0:00000}", idcorrelativo);


            Turno oTurno = new Turno()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                oPaciente = new Paciente() { IdPaciente = Convert.ToInt32(txtIdPaciente.Text) },
                NumeroDocumento = numerodocumento
            };
            string mensaje = string.Empty;
            bool respuesta = new CN_Turno().Registrar(oTurno, detalle_turno, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de turno generado:\n" + numerodocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    Clipboard.SetText(numerodocumento);

                txtIdPaciente.Text = "0";
                txtdocpaciente.Text = "";
                txtnombrepaciente.Text = "";
                dtgvData.Rows.Clear();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
