namespace Billy.Console.Helpers
{
    /// <summary>
    /// Defines the output way.
    /// </summary>
    internal class OutputWriter
    {
        public static void Print(string? message)
            => System.Console.WriteLine(message);
    }
}