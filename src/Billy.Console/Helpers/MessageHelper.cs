using Billy.Core.Commands;
using Billy.Core.Files.Models;

namespace Billy.Console.Helpers
{
    internal class MessageHelper
    {
        public static string GetSearchSignatureResultMessage(SearchSignatureResult result)
            => $"The number of occurrences of '{result.OriginalRequest?.Signature}' in the file is {result.OccurrencesCount}";

        public static string GetCommandArgsMessage(CommandArgs args)
            => $"Command {args?.Command}. Time {args?.TimeSpan.Milliseconds} ms. {args?.Message}";

        public static string GetErrorMessage(string error)
            => $"An error occured. {error}";
    }
}