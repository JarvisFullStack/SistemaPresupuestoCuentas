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
    public partial class rCuenta : Form
    {

        private RepositorioBase<Cuenta> repositorio;
        public rCuenta()
        {
            InitializeComponent();
            LlenarComboBox();
        }
        private void Limpiar()
        {
            CuentaIDnumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            MontoNumericUpDown1.Value = 0;
        }

        private void LlenarComboBox()
        {
            RepositorioBase<TipoCuenta> bll = new RepositorioBase<TipoCuenta>();
            TipoCuentaComboBox.DataSource = bll.GetList(x => true);
            TipoCuentaComboBox.ValueMember = "TipoId";
            TipoCuentaComboBox.DisplayMember = "Descripcion";
        }

        private void LlenaCampo(Cuenta cuenta)
        {

            CuentaIDnumericUpDown.Value = cuenta.CuentaId;
            DescripcionTextBox.Text = cuenta.Descripcion;
            TipoCuentaComboBox.SelectedIndex = cuenta.TipoId;
            MontoNumericUpDown1.Value = Convert.ToDecimal(cuenta.Monto);
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            repositorio = new RepositorioBase<Cuenta>();
            Cuenta cuenta = repositorio.Buscar((int)CuentaIDnumericUpDown.Value);
            return (cuenta != null);
        }
        

        private Cuenta LlenaClase()
        {
            Cuenta cuenta = new Cuenta()
            {
                CuentaId = Convert.ToInt32(CuentaIDnumericUpDown.Value),
                Descripcion = DescripcionTextBox.Text,
                TipoId = (int)TipoCuentaComboBox.SelectedValue,
                Monto = Convert.ToDouble(MontoNumericUpDown1.Value)
            };
            return cuenta;
        }

        private bool GuardarValidar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                errorProviderP.SetError(DescripcionTextBox, "Debe llenar este campo");
                paso = false;
            }
            if (MontoNumericUpDown1.Value == 0)
            {
                errorProviderP.SetError(MontoNumericUpDown1, "Debe llenar este campo");
                paso = false;
            }
            return paso;
        }

        private void rCuenta_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuargar_Click(object sender, EventArgs e)
        {
            repositorio = new RepositorioBase<Cuenta>();
            Cuenta cuenta;
            bool paso = false;

            cuenta = LlenaClase();
            if (!GuardarValidar())
                return;

            if (CuentaIDnumericUpDown.Value >= 0)
                paso = repositorio.Guardar(cuenta);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("Error al intentar modificar", "Errpr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = repositorio.Modificar(cuenta);
            }
            if (paso)
            {
                MessageBox.Show("Guardado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
                MessageBox.Show("Error al intentar guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;
            repositorio = new RepositorioBase<Cuenta>();
            int.TryParse(CuentaIDnumericUpDown.Text, out id);
            if (!ExisteEnLaBaseDeDatos())
            {
                errorProviderP.SetError(CuentaIDnumericUpDown, "Cuenta no existente");
                CuentaIDnumericUpDown.Focus();
                return;
            }
            if (repositorio.Eliminar(id))
                MessageBox.Show("Eliminada Corractamente!", "Correcto", MessageBoxButtons.OK);
            else
                MessageBox.Show("Error al intentar eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAgregarTipoCuenta_Click(object sender, EventArgs e)
        {
            rTipoCuenta tipoCuenta = new rTipoCuenta();
            tipoCuenta.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id;
            repositorio = new RepositorioBase<Cuenta>();
            Cuenta cuenta = new Cuenta();
            int.TryParse(CuentaIDnumericUpDown.Text, out id);
            cuenta = repositorio.Buscar(id);

            if (cuenta != null)
            {
                MessageBox.Show("Cuenta Encontrada.!!", "Exito!!!", MessageBoxButtons.OK);
                LlenaCampo(cuenta);
            }
            else
                MessageBox.Show("Cuenta no Encontrada", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
