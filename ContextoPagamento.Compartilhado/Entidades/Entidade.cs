using Flunt.Notifications;
using System;

namespace ContextoPagamento.Compartilhado.Entidades
{
    public abstract class Entidade : Notifiable
    {
        protected Entidade()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
