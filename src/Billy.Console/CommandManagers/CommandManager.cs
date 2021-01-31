using System;
using System.Reactive.Linq;
using Billy.Console.Configuration;
using Billy.Console.Helpers;
using Billy.Core.Commands;
using Billy.Core.Files.Commands;
using Billy.Core.Files.Models;
using Billy.Core.Files.Processors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Billy.Console.CommandManagers
{
    internal class CommandManager : ICommandManager
    {
        private readonly ISearchProcessor _searchProcessor;
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger _logger;

        public CommandManager(
            ISearchProcessor searchProcessor,
            IOptions<AppSettings> settings)
        {
            _searchProcessor = searchProcessor;
            _settings = settings;
            _logger = LoggerFactory.Create(builder => builder.AddConsole())
                .CreateLogger(nameof(CommandManager));
        }

        public SearchSignatureResult ParseAndRun(string[] args)
        {
            SearchSignatureRequest request = InputConverter.ToSearchSignatureRequest(args);

            var command = new SearchSignatureCommand(_searchProcessor);

            var invoker = new CommandInvoker<SearchSignatureRequest, SearchSignatureResult>(command);

            SubscribeOnInvoker(invoker);

            return invoker.ExecuteCommand(request);
        }

        private void SubscribeOnInvoker<TRequest, TResponse>(CommandInvoker<TRequest, TResponse> invoker)
        {
            if (_settings.Value.LogCommandExecutionTime)
            {
                var sequence = Observable.FromEventPattern<CommandArgs>(
                    handler => invoker.OnExecuted += handler,
                    handler => invoker.OnExecuted -= handler);
                sequence.Subscribe(
                    data => _logger.Log(LogLevel.Information, MessageHelper.GetCommandArgsMessage(data.EventArgs)),
                    ex => _logger.Log(LogLevel.Error, ex.Message));
            }
        }
    }
}