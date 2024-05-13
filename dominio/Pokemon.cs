using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pokemon
    {
        public int Id {  get; set; }

        //annotations: en este caso, sirve para cambiar el nombre de una clase
        //en una columna (algo asi como usar un 'as' en SQL) pero solo en la parte
        //visual.
        [DisplayName("Número")] //siempre va a arriba de la property
        public int Numero { get; set; }
        
        public string Nombre {  get; set; }
        
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }    
        
        public string UrlImagen { get; set; }
        public Elemento Tipo { get; set; } //puede ser agua, fuego, etc...
        public Elemento Debilidad { get; set; }



    }
}
