using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Commons.MultiStages
{
    public abstract class Stage<T> : Entity
        where T : IStageState
    {
        public ulong ServerId { get; private set; }
        public ulong UserId { get; private set; }
        public T State { get; private set; }

        public Stage(ulong serverId, ulong userId, T startState)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            this.State = startState;
        }

        public void UpdateState(T newState)
        {
            if(this.State.Compare(newState))
            {
                //todo maybe throw exception?
                return;
            }
            this.State = newState;
            this.Update();
        }
    }
}
