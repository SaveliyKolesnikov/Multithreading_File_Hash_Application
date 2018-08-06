// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Multithreading_File_Hash_Application.MultithreadFileHasher
{
    public class RecursiveFileProcessor
    {
        public IEnumerable<string> GetAllFilesInPath(string path)
        {
            var result = Enumerable.Empty<string>();

            if (File.Exists(path))
            {
                result = new[] {ProcessFile(path)};
            }
            else if (Directory.Exists(path))
            {
                result = result.Concat(ProcessDirectory(path));
            }

            return result;
        }


        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        private IEnumerable<string> ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            var fileEntries = Directory.GetFiles(targetDirectory);
            var files = fileEntries.Select(ProcessFile);

            // Recurse into subdirectories of this directory.
            var subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            files = subdirectoryEntries.Aggregate(files,
                (current, subdirectory) => current.Concat(ProcessDirectory(subdirectory)));

            return files;
        }

        // Logic for processing found files here.
        private string ProcessFile(string path) => path;
    }
}