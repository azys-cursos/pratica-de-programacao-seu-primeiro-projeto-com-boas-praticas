namespace Recomendador.Playground
{
    public class Blog
    {
        public string Feed { get; set; }

        public string TermoFiltragem { get; set; }

        public Blog(string feed, string termoFiltragem)
        {
            this.Feed = feed;
            this.TermoFiltragem = termoFiltragem;
        }
    }
}