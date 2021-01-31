using System;
using System.IO;
using System.Linq;
using System.Text;
using UtfUnknown;

namespace Billy.Core.Files.Helpers
{
    public class EncodingHelper : IEncodingHelper
    {
        /// <summary>
        /// Tries to detect the current Encoding.
        /// </summary>
        public Encoding DetectEncoding(string filename)
        {
            using FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            DetectionResult result = CharsetDetector.DetectFromStream(file, maxBytesToRead:32);
            
            if (result?.Details == null || !result.Details.Any()) return Encoding.Default;

            float bestConfidence = result.Details.Max(d => d.Confidence);
            return result.Details
                .First(d => Math.Abs(d.Confidence - bestConfidence) < 0.01)
                .Encoding ?? Encoding.Default;
        }

        public int GetBytesCount(Encoding encoding)
            => encoding.GetByteCount("x");
    }
}
