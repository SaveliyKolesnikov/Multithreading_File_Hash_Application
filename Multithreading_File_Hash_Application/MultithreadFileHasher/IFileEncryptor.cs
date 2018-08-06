using System.IO;

namespace Multithreading_File_Hash_Application.MultithreadFileHasher
{
    public interface IFileEncryptor
    {
        string Encrypt(FileStream path);
    }
}