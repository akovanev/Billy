namespace Billy.Core.Files.Models
{
    public class SearchSignatureResult
    {
        /// <summary>
        /// Represents the original request.
        /// Note: would be useful for the multiple search in parallel.
        /// </summary>
        public SearchSignatureRequest? OriginalRequest { get; set; }

        /// <summary>
        /// Represents how many occurrences were met in the file.
        /// </summary>
        public int OccurrencesCount { get; set; }
    }
}