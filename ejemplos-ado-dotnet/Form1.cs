using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ejemplos_ado_dotnet
{
    public partial class wfPokemon : Form
    {
        public wfPokemon()
        {
            InitializeComponent();
        }

        private void wfPokemon_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            dgvPokemons.DataSource = negocio.listar();
        }
    }
}
