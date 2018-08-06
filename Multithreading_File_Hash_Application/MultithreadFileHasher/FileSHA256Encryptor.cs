using System;
using System.IO;
using System.Security.Cryptography;

namespace Multithreading_File_Hash_Application.MultithreadFileHasher
{
    public class FileSHA256Encryptor : IFileEncryptor
    {
        public string Encrypt(FileStream fileStream)
        {
            try
            {
                using (var sha = new SHA256Managed())
                {
                    fileStream.Position = 0;
                    var hash = sha.ComputeHash(fileStream);
                    return BitConverter.ToString(hash).Replace("-", string.Empty);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return null;

        }
    }
}