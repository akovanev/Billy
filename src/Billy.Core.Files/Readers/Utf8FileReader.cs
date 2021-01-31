namespace Billy.Core.Files.Readers
{
    public class Utf8FileReader : FileReader
    {
        protected internal override int GetActualChunkSize(byte[] buffer, int byteRead)
        {
            int masked = buffer[byteRead - 1] & 0xC0; //11000000 mask.

            if (masked == 0xC0) //11000000 => 1st unicode byte.
            {
                byteRead--; //Excludes the byte.
            }
            else if (masked == 0x80) //10000000 => 2nd, 3rd or 4th unicode byte.
            {
                byteRead--; //Excludes the byte we just checked.

                //Excludes all 10xxxxxx bytes.
                while ((buffer[byteRead - 1] & 0xC0) == 0x80) byteRead--;

                //Excludes first unicode byte.
                byteRead--;
            }

            return byteRead;
        }
    }
}