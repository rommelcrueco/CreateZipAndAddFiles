using System.IO;
using System.IO.Compression;

namespace ConsoleApp.CreateZipAndAddFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string zipFilePath = @"C:\Temp\ZipAndAddFiles\destination.zip";
            string sourceFolderPath = @"C:\Temp\ZipAndAddFiles\sourceFolder"; // Change this to your source folder path

            // Create a new zip file
            using (var zipStream = new FileStream(zipFilePath, FileMode.Create))
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Get the files from the source folder
                string[] files = Directory.GetFiles(sourceFolderPath, "*", SearchOption.AllDirectories);

                // Add each file to the zip archive
                foreach (var file in files)
                {
                    AddFileToZip(archive, file);
                }
            }
        }

        static void AddFileToZip(ZipArchive archive, string filePath)
        {
            // Create a new entry in the zip archive for the file
            var entry = archive.CreateEntry(Path.GetFileName(filePath));

            // Open the file and create a stream to read its contents
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var entryStream = entry.Open())
            {
                // Copy the file data into the entry stream, which is added to the zip archive
                fileStream.CopyTo(entryStream);
            }
        }
    }
}
