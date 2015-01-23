using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomendador.Playground
{
    public class  ServicoRecomendacao
    {
        public IRecomendacoes Recomendacoes { get; set; }
        public IServicoEmail ServicoEmail { get; set; }
        public ILeitorFeed LeitorFeed { get; set; }

        public ServicoRecomendacao(IRecomendacoes recomendacoes, IServicoEmail servicoEmail, ILeitorFeed leitorFeed)
        {
            this.Recomendacoes = recomendacoes;
            this.ServicoEmail = servicoEmail;
            this.LeitorFeed = leitorFeed;
        }

        public void Recomendar(Usuario usuario, Blog blog, DateTime referencia)
        {
            var haRecomendacoes = this.Recomendacoes.HaRecomendacoes(usuario, referencia);

            if (haRecomendacoes)
                return;

            var todasRecomendacoes = this.Recomendacoes.ObterTodas();

            var artigo = this.LeitorFeed.ObterArtigoAleatorio(blog, todasRecomendacoes);

            var mensagem = new Mensagem(usuario, artigo);

            this.ServicoEmail.EnviarArtigo(mensagem);

            var recomendacao = new Recomendacao(usuario, artigo, referencia);

            this.Recomendacoes.Registrar(recomendacao);
        }
    }
}
