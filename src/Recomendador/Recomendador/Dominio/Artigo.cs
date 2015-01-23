namespace Recomendador.Dominio
{
    public class Artigo
    {
        public string Titulo { get; set; }

        public string Url { get; set; }

        public Artigo(string titulo, string url)
        {
            // TODO: Validação!

            this.Titulo = titulo;
            this.Url = url;
        }
    }
}
