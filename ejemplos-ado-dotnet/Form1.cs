﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ejemplos_ado_dotnet
{
    public partial class wfPokemon : Form
    {
        private List<Pokemon> listaPokemon; /*los datos que vamos a obtener de la DB, ahora van a estar en una 
                                             lista privada.*/
        public wfPokemon()
        {
            InitializeComponent();
        }

        private void wfPokemon_Load(object sender, EventArgs e)
        {
            cargar();
              
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        /*cuando seleccionamos un elemento en la lista, cambia la imagen. Tenemos que tomar el elemento seleccionado
         en la lista*/
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                                         // grilla actual,la fila actual, dame el objeto enlazado (devuelve justamente un Object)
                                         //para ello justamente al principio, hacemos casteo explicito.
            cargarImagen(seleccionado.UrlImagen); //cada vez que haga click, va a cambiar la imagen
        }

        private void cargar() //carga de pokemons de la DB al dataGridView
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar(); //va a la DB y te devuelve una lista de datos.
                dgvPokemons.DataSource = listaPokemon; //DataSource: recibe un origen de datos, y lo modela en la tabla.
                dgvPokemons.Columns["UrlImagen"].Visible = false; // Oculte la columna del urlImagen 
                dgvPokemons.Columns["Id"].Visible = false; //idem anterior

                /*ya que tenemos los pokemons cargados, en el picture box cargamos una imagen.
                 "listaPokemon[0]" el 1er elemento de la lista de pokemons
                 "UrlImagen" su atributo de Url*/
                cargarImagen(listaPokemon[0].UrlImagen); //presecciona la imagen del 1er elemento (1er pokemon)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbPokemon.Load(imagen); //carga la imagen al picture box solicitada
            }
            catch (Exception)  //si NO hay imagen, carga una imagen por defecto de vacia
            {
                pbPokemon.Load("https://i0.wp.com/sunrisedaycamp.org/wp-content/uploads/2020/10/placeholder.png?ssl=1");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog(); //indicador de todo ok
            cargar();   //actualizar la carga del poke en el dataGridView
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; //el pokemon que esta seleccionado e la lista

            frmAltaPokemon modificar = new frmAltaPokemon(seleccionado); //creamos el form para dar
                                                                         //el alta, pero con los datos
                                                                         //del el pokemon seleccionado 
                                                                         //en la lista

            modificar.ShowDialog(); 
            cargar();   
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e) //en una app real, solo debe haber 1 solo tipo de eliminacion...
        {
            eliminar();
            
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
            
        }

        private void eliminar (bool logico = false) //metodo que va a ejecutar la logica principal de los eliminar
        //si no le mando un parametro, va a tomar falso por defecto: significa que la eliminacion NO es logica

        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                //la linea de abajo devuelve un dialog result. Sirve para agregar una interaccion en el messageBo
                //En este caso, para preguntar si de VERDAD quiere eliminar al pokemon.
                DialogResult respuesta = MessageBox.Show("De verdad queres eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes) //Si dice que si, lo borra. Si dice que no, no hace nada.
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; //el poke seleccionado en la grilla

                    if (logico)
                    {
                        negocio.eliminarLogico(seleccionado.Id);
                    }
                    else
                    {
                        negocio.eliminar(seleccionado.Id);
                    }

                    cargar();//se actualiza la grilla
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
