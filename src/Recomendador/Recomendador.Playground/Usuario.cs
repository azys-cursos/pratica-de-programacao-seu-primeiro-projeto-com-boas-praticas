using System;

namespace Recomendador.Playground
{
    public class Usuario
    {
        public string Email { get; protected set; }

        public Usuario(string email) // denisferrari
        {
            // TODO: O e-mail é válido?

            this.AlterarEmail(email);
        }

        public virtual void AlterarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            this.Email = email;
        }
    }
}
