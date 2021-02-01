using System.Text;

namespace Billy.Core.Files.Helpers
{
    public interface IEncodingHelper
    {
        Encoding DetectEncoding(string filename);
    }
}