using GrainInterfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Orleans.Runtime;

namespace Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger _logger;
        private readonly IPersistentState<HelloCounter> _state;

        public HelloGrain(ILogger<HelloGrain> logger, [PersistentState("counter", "counter")] IPersistentState<HelloCounter> state)
        {
            this._logger = logger;
            _state = state;
        }

        async Task<string> IHello.SayHello(string greeting)
        {
            _state.State.Counter = 0;

            await _state.WriteStateAsync();

            _logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            
            return $"\n Client said: '{greeting}', so HelloGrain says: Hello!";
        }

        public Task<int> GetCounter()
        {
            return Task.FromResult(_state.State.Counter);
        }
    }

    public class HelloCounter
    {
        public int Counter { get; set; }
    }

}