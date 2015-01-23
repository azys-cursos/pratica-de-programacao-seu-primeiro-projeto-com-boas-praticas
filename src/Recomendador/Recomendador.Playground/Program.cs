using System;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dependências
            var mongodb = MongoDb.GetDatabase();
            var contaSmtp = new ContaSmtp("Recomendador", "r2d2@azys.com.br", "dfs465789ds47815");

            // Processo
            var recomendacoes = new Recomendacoes(mongodb);

            var usuario = new Usuario("denisferrari@azys.com.br");

            var hoje = DateTime.Today; // *

            var haRecomendacoes = recomendacoes.HaRecomendacoes(usuario, hoje);

            if (haRecomendacoes)
                return;

            var recomendacoesFeitas = recomendacoes.ObterTodas();

            var blogMartinFowler = new Blog("http://martinfowler.com/feed.atom", "articles");

            var leitorFeed = new LeitorFeed(blogMartinFowler);

            var artigo = leitorFeed.ObterArtigoAleatorio(recomendacoesFeitas);

            var servicoEmail = new Gmail(contaSmtp);

            var mensagemEmail = new MensagemEmail(usuario, artigo);

            servicoEmail.EnviarArtigo(mensagemEmail);

            var recomendacao = new Recomendacao(usuario, artigo, hoje); // *

            recomendacoes.Registrar(recomendacao);

            Console.WriteLine("Recomendação Registrada!");
            Console.WriteLine("");
            Console.ReadLine();
        }
    }
}
