using Autofac;

namespace Economius.Cqrs
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext context;
        private readonly Queue<dynamic> commandsQueue = new(); //dynamic = ICommand but under raw type instead interface
        private bool isQueueProcessing = false;

        public CommandBus(IComponentContext context)
        {
            this.context = context;
        }

        public Task ExecuteAsync<T>(T command) where T : ICommand
        {
            //Log.Debug("Command: {command}", command);
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).Name}' can not be null.");
            }
            var handler = this.context.Resolve<ICommandHandler<T>>();
            return handler.HandleAsync(command);
        }

        public Task AddToSingleThreadQueue<T>(T command) where T : ICommand
        {
            this.commandsQueue.Enqueue(command);
            if(!this.isQueueProcessing)
            {
                Task.Run(() => this.ProcessQueue());//fire and forget
            }
            return Task.CompletedTask;
        }

        private void ProcessQueue()
        {
            //double check
            if(this.isQueueProcessing)
            {
                return;
            }
            this.isQueueProcessing = true;
            while (this.commandsQueue.Count > 0)
            {
                var command = this.commandsQueue.Dequeue();
                this.ExecuteAsync(command).Wait();
            }

            this.isQueueProcessing = false;
        }
    }
}
