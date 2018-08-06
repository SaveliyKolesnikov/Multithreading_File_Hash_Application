using System;
using System.IO;
using Multithreading_File_Hash_Application.MultithreadFileHasher;

namespace Multithreading_File_Hash_Application
{
    internal class MultithreadingFileHashApp
    {
        public static void Main(string[] args)
        {
            var srcPath = string.Empty;
            var outputFile = "HashedFiles.txt";
            
            switch (args.Length)
            {
                case 2:
                    outputFile = args[1];
                    goto case 1;
                case 1:
                    srcPath = args[0];
                    break;
                case 0:
                    srcPath = Directory.GetCurrentDirectory();
                    break;
                default:
                    Console.WriteLine("Invalid number of parameters.");
                    Console.WriteLine("Using: calchash.exe <directory name> (<output file name>)");
                    break;
            }


            var pathFileHasher = new RecursiveFileHasher();
            var isHashSuccessful = pathFileHasher.HashFiles(srcPath, outputFile, new FileSHA256Encryptor());

            if (isHashSuccessful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Files have been processed  successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Files haven't been processed.");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}