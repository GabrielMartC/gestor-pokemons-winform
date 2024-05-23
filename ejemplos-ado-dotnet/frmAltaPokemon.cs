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
using System.IO; //para manejar ARCHIVOS (ej imagenes)
using System.Configuration; //para usar configuraciones de App.config

namespace ejemplos_ado_dotnet
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null; //por defecto, el form se crea con un pokemon en null
        private OpenFileDialog archivo = null; //archivo que va a contener la imagen
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //vamos a tener que leer los datos de los elementos ingresados
            //para transformarlos en un objeto de tipo pokemon...


            PokemonNegocio negocio = new PokemonNegocio();  

            try
            {
                if(pokemon == null) //validamos, si el pokemon es null...
                {
                    this.pokemon = new Pokemon();   //queremos agregar un pokemon numevo... el anterior pokemon declarado ya no es null
                }
                //seteamos los datos... (sea tanto para LEER o para MODIFICAR)
                pokemon.Numero = int.Parse(tbNumero.Text);
                pokemon.Nombre = tbNombre.Text;
                pokemon.Descripcion = tbDescripcion.Text;

                //Para la imagen, primero hacemos una validacion en donde...
                //guardo imagen si la levanto localmente
                if (archivo != null && !(tbUrlImagen.Text.ToUpper().Contains("HTTP")))
                //si el archivo no es nulo y NO contiene http en la url, estas intentando guardar una imagen local
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName); //clase File para manejar archivos
                    //quiero setear la nueva ruta del archivo...
                    pokemon.UrlImagen = ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName;
                }
                else //la imagen no es local, es un url...
                {
                    pokemon.UrlImagen = tbUrlImagen.Text; //para mandar a la DB la url de la imagen

                }

                pokemon.Tipo = (Elemento)cboTipo.SelectedItem; //casteo explicito porque "cboTipo.SelectedItem" emvia un object
                pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem;

                if(pokemon.Id != 0) //validamos que queremos hacer con el pokemon
                { //estoy queriendo modificar un pokemon existente
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente!");
                }
                else
                { //estoy queriendo agregar un pokemon nuevo
                    //para mandarlo a la db (con PokemonNegocio)
                    negocio.agregar(pokemon);
                    MessageBox.Show("Agregado exitosamente!");
                }

                ////guardo imagen si la levanto localmente
                //if (archivo != null && !(tbUrlImagen.Text.ToUpper().Contains("HTTP")))
                //    //si el archivo no es nulo y NO contiene http en la url, estas intentando guardar una imagen local
                //{
                //    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName); //clase File para manejar archivos
                //    //quiero setear la nueva ruta del archivo...
                //    pokemon.UrlImagen = ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName;
                //}

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
                cboTipo.DataSource = elementoNegocio.listar(); //lo que hacemos aca es cargar al desplegable una lista de Objetos de tipo Elemento
                cboTipo.ValueMember = "Id";    //el valor clave...
                cboTipo.DisplayMember = "Descripcion"; //lo que vamos a mostrar...
                //ValueMember y DisplayMember son los nombres de las propiedades de la Class Elemento
                cboDebilidad.DataSource = elementoNegocio.listar();
                cboDebilidad.ValueMember = "Id";//repetimos como anteriomente...
                cboDebilidad.DisplayMember = "Descripcion";
                //(cboTipo y cboDebilidad) a pesar de tener los mismos valores, ambos son distintos comboBox asi que
                //para que no se rompan, los cargamos por separado para c/u

                if (pokemon != null) //validamos el pokemon
                {
                    //hay datos para modificar...
                    tbNumero.Text = pokemon.Numero.ToString();    
                    tbNombre.Text = pokemon.Nombre;
                    tbUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen); //Precarga la imagen en el modificar, si no hay imagen muestra por defecto el de vacio.
                    tbDescripcion.Text = pokemon.Descripcion;

                    //para los desplegables...
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
                else
                {
                    //es uno nuevo...
                }
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

        private void btnAgregarImagen_Click(object sender, EventArgs e) //agregar imagen local
        {
            archivo = new OpenFileDialog(); //esto genera una ventna de dialogo
            archivo.Filter = "jpg|*.jpg|png|*.png"; //especifica que tipo de datos puede abrir (ej: aca podemos corgar jpgs y pngs)
            if (archivo.ShowDialog() == DialogResult.OK) //si la ventana se abre correctamente...
            {
                tbUrlImagen.Text = archivo.FileName; //guarda la ruta completa del archivo
                cargarImagen(archivo.FileName); //previsualizar la imagen
            }
        }
    }
}
