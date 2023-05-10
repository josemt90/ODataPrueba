using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ODataPrueba.Models
{
    public partial class Libros
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int? Anio { get; set; }
        public int? IdAutor { get; set; }

        public virtual Autores IdAutorNavigation { get; set; }
    }
}
