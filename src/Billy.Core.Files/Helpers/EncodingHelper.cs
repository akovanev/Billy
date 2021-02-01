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

            //Uses the library based on BOM and Mozilla Universal Charset Detector.
            //Reads up to 32 bytes from the file.
            DetectionResult result = CharsetDetector.DetectFromStream(file, maxBytesToRead:32);

            if (result?.Details == null || !result.Details.Any()) return Encoding.Default;

            //Searches for the best match in case if there are more than one. Confidence belongs to [0,1].
            float bestConfidence = result.Details.Max(d => d.Confidence);

            return result.Details
                .First(d => Math.Abs(d.Confidence - bestConfidence) < 0.01)
                .Encoding ?? Encoding.Default;
        }
    }
}
