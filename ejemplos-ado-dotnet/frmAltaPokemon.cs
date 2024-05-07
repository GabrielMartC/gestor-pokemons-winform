using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;

namespace ejemplos_ado_dotnet
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //vamos a tener que leer los datos de los elementos ingresados
            //para transformarlos en un objeto de tipo pokemon...

            Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();  

            try
            {
                //seteamos los datos...
                poke.Numero = int.Parse(tbNumero.Text);
                poke.Nombre = tbNombre.Text;
                poke.Descripcion = tbDescripcion.Text;

                poke.Tipo = (Elemento)cboTipo.SelectedItem; //casteo explicito porque "cboTipo.SelectedItem" emvia un object
                poke.Debilidad = (Elemento)cboDebilidad.SelectedItem;

                //para mandarlo a la db (con PokemonNegocio)
                negocio.agregar(poke);
                MessageBox.Show("Agregado exitosamente!");
                this.Close();
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            //cargamos los tipo/debilidades para los combos desplegables
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {
                cboTipo.DataSource = elementoNegocio.listar();
                cboDebilidad.DataSource = elementoNegocio.listar();
                //a pesar de tener los mismos valores, ambos son distintos comboBox asi que
                //para que no se rompan, los cargamos por separado para c/u
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
