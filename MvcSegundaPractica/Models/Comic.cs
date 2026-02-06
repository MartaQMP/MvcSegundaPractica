using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcSegundaPractica.Models
{
    public class Comic
    {
        public int IdComic { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }
}
