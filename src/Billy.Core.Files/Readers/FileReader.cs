using System.Collections.Generic;
using System.IO;

namespace Billy.Core.Files.Readers
{
    public class FileReader : IFileReader
    {
        public virtual IEnumerable<byte[]> ReadChunks(string path, Models.FileInfo fileInfo)
        {
            //Opens the file stream.
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //Creates the buffered stream for reading a chunk.
            using var bufferStream = new BufferedStream(fileStream, fileInfo.ChunkSize);

            //Sets the file pointer to the start position specific for the detected encoding.
            bufferStream.Position = fileInfo.Preamble;

            do
            {
                //Creates the byte array for the buffer Stream.
                byte[] buffer = new byte[bufferStream.BufferSize];

                //Reads data from the file.
                int byteRead = bufferStream.Read(buffer);

                //If the End of File.
                if (byteRead <= 0) yield break;

                //Creates the memory stream to return the chunk.
                using var memoryStream = new MemoryStream();

                //Position 'End of File' means the last chunk here.
                bool lastChunk = bufferStream.Position == fileStream.Length;

                if (lastChunk)
                {
                    memoryStream.Write(buffer, 0, byteRead);
                    yield return memoryStream.ToArray();
                    yield break;
                }

                //Gets the actual chunk size.
                //For utf8 and utf16 it does not include the bytes split in the end of the read data.
                int chunkSize = GetActualChunkSize(buffer, byteRead);

                memoryStream.Write(buffer, 0, chunkSize);
                
                yield return memoryStream.ToArray();

                //Calculates the actual shift.
                int shift = fileInfo.BackShift + byteRead - chunkSize;

                //Returns the pointer to shift back.
                bufferStream.Position -= GetActualChunkSize(buffer, shift);

            } while (bufferStream.Position < fileStream.Length);
        }

        protected internal virtual int GetActualChunkSize(byte[] buffer, int byteRead) => byteRead;
    }
}