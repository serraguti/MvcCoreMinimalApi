namespace MvcCoreMinimalApi.Models
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public int IdSerie { get; set; }
        public string Imagen { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
