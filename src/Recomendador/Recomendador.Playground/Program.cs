using System;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongodb = MongoDb.GetDatabase();

            var recomendacoes = new Recomendacoes(mongodb);

            var usuario = new Usuario()
            {
                Email = "denisferrari@azys.com.br"
            };
            var hoje = DateTime.Today;

            var haRecomendacoes = recomendacoes.HaRecomendacoes(usuario, hoje);

            if (haRecomendacoes)
                return;

            var recomendacoesFeitas = recomendacoes.ObterTodas();

            var blog = new BlogMartinFowler();

            var artigo = blog.ObterArtigoAleatorio(recomendacoesFeitas);

            var servicoEmail = new Gmail();

            servicoEmail.EnviarArtigo(usuario, artigo);

            var recomendacao = new Recomendacao()
            {
                Usuario = usuario.Email,
                Artigo = artigo.Url,
                Data = hoje
            };

            recomendacoes.Registrar(recomendacao);

            Console.WriteLine("Recomendação Registrada!");
            Console.WriteLine("");
            Console.ReadLine();
        }
    }
}
