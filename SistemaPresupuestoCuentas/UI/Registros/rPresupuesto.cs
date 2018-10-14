using SistemaPresupuestoCuentas.BLL;
using SistemaPresupuestoCuentas.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPresupuestoCuentas.UI.Registros
{
    public partial class rPresupuesto : Form
    {
        private RepositorioBase<Presupuesto> repositorio;
        public rPresupuesto()
        {
            InitializeComponent();
            LlenaComboBox();
        }

        private Presupuesto LlenaClase()
        {
            Presupuesto presupuesto = new Presupuesto()
            {
                PresupuestoId = Convert.ToInt32(PresupuestoIDnumericUpDown.Value),
                Descripcion = DescripcionTextBox.Text,
                Fecha = DateTime.Now,
                Monto = Convert.ToDouble(ValorTextBox.Text)
            };
            return presupuesto;
        }

        private void Limpiar()
        {
            errorProviderP.Clear();
            PresupuestoIDnumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            CuentaComboBox.SelectedIndex = 0;
            ValorTextBox.Text = string.Empty;
        }

        private void LlenaComboBox()
        {
            RepositorioBase<Cuenta> rCuentas = new RepositorioBase<Cuenta>();
            CuentaComboBox.DataSource = rCuentas.GetList(x => true);
            CuentaComboBox.ValueMember = "CuentaId";
            CuentaComboBox.DisplayMember = "Descripcion";
        }

        private void rPresupuesto_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarCuenta_Click(object sender, EventArgs e)
        {
            rCuenta RCuenta = new rCuenta();
            RCuenta.ShowDialog();
        }

        private void LlenaCampo(Presupuesto presupuesto)
        {

            PresupuestoIDnumericUpDown.Value = presupuesto.PresupuestoId;
            DescripcionTextBox.Text = presupuesto.Descripcion;
            FechaTimePicker.Text = presupuesto.Fecha.ToString();
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id;
            repositorio = new RepositorioBase<Presupuesto>();
            Presupuesto presupuesto = new Presupuesto();
            int.TryParse(PresupuestoIDnumericUpDown.Text, out id);
            presupuesto = repositorio.Buscar(id);

            if (presupuesto != null)
            {
                MessageBox.Show("Presupuesto Encontrado!!", "Exito!!!", MessageBoxButtons.OK);
                LlenaCampo(presupuesto);
            }
            else
                MessageBox.Show("Presupuesto no Encontrado", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
