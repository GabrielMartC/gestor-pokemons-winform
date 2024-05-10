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

                poke.UrlImagen = tbUrlImagen.Text; //para mandar a la DB la url de la imagen

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

        private void tbUrlImagen_Leave(object sender, EventArgs e) 
        //cuando terminemos de ingresar la url en el textbox, nos va a mostrar
        //una preview de la imagen (va a intentar cargarla)
        {
            cargarImagen(tbUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        //ESTE METODO ESTA REPETIDO (ya aparece en Form1.cs), pero por ahora sirve. Lo
        //ideal seria que este dentro de una clase "helper" en la cual almacenar los
        //metodos para usarlos en distintas clases
        {
            try
            {
                pbPokemonPreview.Load(imagen); 
            }
            catch (Exception)  
            {
                pbPokemonPreview.Load("https://i0.wp.com/sunrisedaycamp.org/wp-content/uploads/2020/10/placeholder.png?ssl=1");
            }
        }
    }
}
