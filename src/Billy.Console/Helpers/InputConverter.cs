using System;
using System.IO;
using System.Linq;
using Billy.Core.Files.Models;

namespace Billy.Console.Helpers
{
    /// <summary>
    /// Validates the application run input and converts it to the specified Request object.
    /// </summary>
    internal class InputConverter
    {
        public static SearchSignatureRequest ToSearchSignatureRequest(string[] args)
        {
            if (args is null || !args.Any())
                throw new NullReferenceException("The file should be specified");

            if (!File.Exists(args[0]))
                throw new FileNotFoundException("The file does not exist");

            return new SearchSignatureRequest
            {
                FileFullName = args[0],
                Signature = Path.GetFileNameWithoutExtension(args[0])
            };
        }
    }
}
