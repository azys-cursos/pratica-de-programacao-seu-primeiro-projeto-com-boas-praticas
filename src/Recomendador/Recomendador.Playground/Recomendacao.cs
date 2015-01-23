using System;

namespace Recomendador.Playground
{
    public class Recomendacao
    {
        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Artigo { get; set; }

        public DateTime Data { get; set; }

        public Recomendacao(Usuario usuario, Artigo artigo, DateTime data)
        {
            // TODO: Validação dos params!

            this.Usuario = usuario.Email;
            this.Artigo = artigo.Url;
            this.Data = data;
        }
    }
}