namespace Billy.Core.Files.Models
{
    public class SearchSignatureRequest
    {
        public string FileFullName { get; set; } = "";
        public string Signature { get; set; } = "";
    }
}
