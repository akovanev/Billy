using System.Collections.Generic;
using System.IO;
using Billy.Core.Files.Models;

namespace Billy.Core.Files.Readers
{
    public class FileReader : IFileReader
    {
        public IEnumerable<byte[]> ReadChunks(string path, ChunkInfo chunkInfo)
        {
            //Opens the file stream.
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //Creates the buffered stream for reading a chunk.
            var bufferStream = new BufferedStream(fileStream, chunkInfo.Size);

            //Sets the file pointer to the start position specific for the detected encoding.
            bufferStream.Position = chunkInfo.Preamble;

            do
            {
                //Creates the byte array for the buffer Stream.
                byte[] buffer = new byte[bufferStream.BufferSize];

                //Reads data from the file.
                int byteRead = bufferStream.Read(buffer);

                //If the End File.
                if (byteRead <= 0) yield break;

                //Gets the actual chunk size.
                //For utf8 and utf16 it does not include the bytes split in the end of the read data.
                int chunkSize = GetActualChunkSize(buffer, byteRead);

                //Creates the memory stream to return the chunk.
                using var memoryStream = new MemoryStream();
                
                memoryStream.Write(buffer, 0, chunkSize);
                
                yield return memoryStream.ToArray();

                if(bufferStream.Position == fileStream.Length) yield break;

                //Calculates the actual shift.
                int shift = chunkInfo.BackShift + byteRead - chunkSize;

                //Returns the pointer to shift back.
                bufferStream.Position -= GetActualChunkSize(buffer, shift);

            } while (bufferStream.Position < fileStream.Length);
        }

        protected internal virtual int GetActualChunkSize(byte[] buffer, int byteRead) => byteRead;
    }
}