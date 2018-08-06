# Multithreading_File_Hash_Application
A console application calculating the SHA-256 hash of all files in a specified directory and subdirectories.

Application requirements:
- The application shall be a Windows console application written in C#.
- The application shall use multithreading to maximize performance when running on multicore processors.

Command line parameters:
> calchash.exe [input_directory_name] [ouptut_file_name]

Output shall be a text file containing the following:
- hashes of each file in the input directory
- Performance in MB/s by CPU time (note that CPU time may be greater than elapsed real time when a multithreaded application is running on a multicore processor).

Output file format:
   <hash 1> <file name 1>
   <hash 2> <file name 2>
   ...
   <hash n> <file name n>
   Performance: <value> MB/s (by CPU time)
