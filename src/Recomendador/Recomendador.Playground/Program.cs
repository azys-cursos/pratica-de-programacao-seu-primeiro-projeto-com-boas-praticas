using System;

namespace Recomendador.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dependências Globais
            var mongodb = MongoDb.GetDatabase();
            var contaSmtp = new ContaSmtp("Recomendador", "r2d2@azys.com.br", "dfs465789ds47815");

            // Dependências do Processo
            IRecomendacoes recomendacoes = new Recomendacoes(mongodb);
            IServicoEmail servicoEmail = new Gmail(contaSmtp);
            ILeitorFeed leitorFeed = new LeitorFeed();

            // Processo
            var usuario = new Usuario("denisferrari@azys.com.br");
            var blog = new Blog("http://martinfowler.com/feed.atom", "articles");
            var referencia = DateTime.Today;

            var servico = new ServicoRecomendacao(recomendacoes, servicoEmail, leitorFeed);

            servico.Recomendar(usuario, blog, referencia);

            Console.WriteLine("Recomendação Registrada!");
            Console.ReadLine();
        }
    }
}
