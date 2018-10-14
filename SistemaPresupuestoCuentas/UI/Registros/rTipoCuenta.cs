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
    public partial class rTipoCuenta : Form
    {

        private RepositorioBase<TipoCuenta> repositorio;
        public rTipoCuenta()
        {
            InitializeComponent();
        }

        private void LlenaCampo(TipoCuenta tipoCuenta)
        {
            TipoCuentaIDnumericUpDown.Value = tipoCuenta.TipoId;
            DescripcionTextBox.Text = tipoCuenta.Descripcion;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            repositorio = new RepositorioBase<TipoCuenta>();
            TipoCuenta tipoCuenta = repositorio.Buscar((int)TipoCuentaIDnumericUpDown.Value);
            return (tipoCuenta != null);
        }
       

        private TipoCuenta LlenaClase()
        {
            TipoCuenta tipoCuenta = new TipoCuenta()
            {
                TipoId = Convert.ToInt32(TipoCuentaIDnumericUpDown.Value),
                Descripcion = DescripcionTextBox.Text
            };
            return tipoCuenta;
        }



        private void Limpiar()
        {
            errorProviderP.Clear();
            TipoCuentaIDnumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
        }

        private bool GuardarValidar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                errorProviderP.SetError(DescripcionTextBox, "El Campo Descripcion No puede Estar Vacio!");
                DescripcionTextBox.Focus();
                paso = false;
            }
            return paso;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuargar_Click(object sender, EventArgs e)
        {
            repositorio = new RepositorioBase<TipoCuenta>();
            TipoCuenta tipoCuenta;
            bool paso = false;
            tipoCuenta = LlenaClase();
            if (!GuardarValidar())
                return;
            if (TipoCuentaIDnumericUpDown.Value == 0)
                paso = repositorio.Guardar(tipoCuenta);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede Modificar un Tipo de cuenta no Existente", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = repositorio.Modificar(tipoCuenta);
            }
            if (paso)
            {
                MessageBox.Show("Guardado!!", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
                MessageBox.Show("No se Guardo el Tipo De Cuenta!!", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            int id;
            repositorio = new RepositorioBase<TipoCuenta>();
            int.TryParse(TipoCuentaIDnumericUpDown.Text, out id);
            if (!ExisteEnLaBaseDeDatos())
            {
                errorProviderP.SetError(TipoCuentaIDnumericUpDown, "Esta Cuenta No Existe");
                TipoCuentaIDnumericUpDown.Focus();
                return;
            }
            if (repositorio.Eliminar(id))
            {
                MessageBox.Show("Tipo De Cuenta Eliminada!!", "Exitoso!!!", MessageBoxButtons.OK);
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar El Tipo De Cuenta!!", "Fallo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id;
            repositorio = new RepositorioBase<TipoCuenta>();
            int.TryParse(TipoCuentaIDnumericUpDown.Text, out id);
            TipoCuenta tiposCuenta = new TipoCuenta();
            tiposCuenta = repositorio.Buscar(id);

            if (tiposCuenta != null)
            {
                MessageBox.Show("Tipo de Cuenta Encontrada.!!", "Exito!!!", MessageBoxButtons.OK);
                LlenaCampo(tiposCuenta);
            }
            else
                MessageBox.Show("Tipo de Cuenta no Encontrada", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
