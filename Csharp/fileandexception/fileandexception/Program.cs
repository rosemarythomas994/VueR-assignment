using System;
using System.IO;

namespace FileAndExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filePath = "sample.txt";

            string filePath = @"E:\revv\Csharp\fileandexception\fileandexception\sample.txt";

            try
            {
                //  Create and Write to a file
                Console.WriteLine("\n[1] Creating and writing to file...");
                File.WriteAllText(filePath, "Hello, this is the first line....\n");

                // Append text to file
                Console.WriteLine("[2] Appending to file...");
                File.AppendAllText(filePath, "This is an appended line.,...\n");

                // Read from file
                Console.WriteLine("[3] Reading from file...");
                string content = File.ReadAllText(filePath);
                Console.WriteLine("File Content:\n" + content);

                // Append text to file
                Console.WriteLine("[2] Appending to file...");
                File.AppendAllText(filePath, "This is an appended line.,...\n");
                // Delete file (optional)
                //Console.WriteLine("[4] Deleting file...");
                //File.Delete(filePath);
                //Console.WriteLine("File deleted successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(" Access denied: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(" File not found: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(" I/O error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("\n Finally block: Cleanup or closing resources if needed.");
            }

        
        }
    }
}
