namespace Recomendador.Dominio
{
    public interface IServicoEmail
    {
        void EnviarArtigo(Mensagem mensagem);
    }
}