using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_File_Hash_Application.MultithreadFileHasher
{
    public class RecursiveFileHasher
    {
        public bool HashFiles(string srcPath, string resultFileName, IFileEncryptor fileEncryptor)
        {
            var fileProcessor = new RecursiveFileProcessor();
            var innerFilesPaths = fileProcessor.GetAllFilesInPath(srcPath);

            var result = new StringBuilder();
            var totalTime = 0d;
            var totalSize = 0d;
            var areAllFilesProcessed = true;
            var areAllFilesProcessedLocker = new object();
            Parallel.ForEach(innerFilesPaths, path =>
            {
                var elapsedTIme = Stopwatch.StartNew();

                using (var fileStream = OpenFileStream(path))
                {
                    switch (fileStream)
                    {
                        case null when !areAllFilesProcessed:
                            return;
                        case null:
                            lock (areAllFilesProcessedLocker)
                                areAllFilesProcessed = false;
                            return;
                    }

                    var hash = fileEncryptor.Encrypt(fileStream);
                    if (hash is null) return;

                    elapsedTIme.Stop();
                    lock (result)
                    {
                        totalTime += elapsedTIme.ElapsedMilliseconds;
                        totalSize += fileStream.Length;
                        result.AppendLine($"Hash: {hash}\tFileName: {path}");
                    }
                }
            });

            result.AppendLine($"Performance: {totalSize / totalTime / 1000} MB/s (by CPU time)");
            Console.WriteLine($"Total time: {totalTime}");
            Console.WriteLine($"Total tize: {totalSize}");

            try
            {
                File.WriteAllText(resultFileName, result.ToString());
                return areAllFilesProcessed;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.Message);
            }

            return false;

            FileStream OpenFileStream(string path)
            {
                try
                {
                    return File.OpenRead(path);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}