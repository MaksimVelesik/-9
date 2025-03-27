using System;
using System.IO;

public class FileManager
{
    public void CreateFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public void CopyFile(string source, string destination)
    {
        File.Copy(source, destination, true);
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
        else
            throw new FileNotFoundException("Файл не существует.");
    }

    public void MoveFile(string source, string destination)
    {
        File.Move(source, destination);
    }

    public void RenameFile(string current, string newName)
    {
        File.Move(current, newName);
    }

    public void DeleteFilesByPattern(string directory, string pattern)
    {
        foreach (var file in Directory.GetFiles(directory, pattern))
            File.Delete(file);
    }

    public string[] GetFilesInDirectory(string directory)
    {
        return Directory.GetFiles(directory);
    }
}

public class FileInfoProvider
{
    public long GetFileSize(string path) => new FileInfo(path).Length;
    public DateTime GetCreationDate(string path) => File.GetCreationTime(path);
    public DateTime GetLastModifiedDate(string path) => File.GetLastWriteTime(path);
    public bool CompareFilesBySize(string path1, string path2) => new FileInfo(path1).Length == new FileInfo(path2).Length;
    public FileAttributes GetFileAttributes(string path) => File.GetAttributes(path);
}

class Program
{
    static void Main()
    {
        string fileName = "velesik.ma";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        string copyPath = Path.Combine(Directory.GetCurrentDirectory(), "copy_" + fileName);
        string renamedPath = Path.Combine(Directory.GetCurrentDirectory(), "familia.io");

        FileManager fileManager = new FileManager();
        FileInfoProvider fileInfoProvider = new FileInfoProvider();

        fileManager.CreateFile(filePath, "Hello, this is a test file!");

        try
        {
            fileManager.DeleteFile(filePath);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }

        fileManager.CreateFile(filePath, "Hello again!");
        Console.WriteLine($"Размер файла: {fileInfoProvider.GetFileSize(filePath)} байт");
        Console.WriteLine($"Дата создания: {fileInfoProvider.GetCreationDate(filePath)}");
        Console.WriteLine($"Дата последнего изменения: {fileInfoProvider.GetLastModifiedDate(filePath)}");

        fileManager.CopyFile(filePath, copyPath);
        Console.WriteLine($"Копия файла {(File.Exists(copyPath) ? "существует." : "не существует.")}");

        fileManager.RenameFile(copyPath, renamedPath);

        try
        {
            fileManager.DeleteFile(copyPath);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }

        bool areEqual = fileInfoProvider.CompareFilesBySize(filePath, renamedPath);
        Console.WriteLine($"Файлы одинаковы по размеру: {areEqual}");

        fileManager.DeleteFilesByPattern(Directory.GetCurrentDirectory(), "*.ii");

        Console.WriteLine("Файлы в директории:");
        foreach (var file in fileManager.GetFilesInDirectory(Directory.GetCurrentDirectory()))
        {
            Console.WriteLine(file);
        }

        File.SetAttributes(filePath, FileAttributes.ReadOnly);
        try
        {
            File.AppendAllText(filePath, "Trying to write.");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine("Ошибка записи: " + e.Message);
        }

        Console.WriteLine($"Атрибуты файла: {fileInfoProvider.GetFileAttributes(filePath)}");
    }
}