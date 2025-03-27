using System;
using System.IO;

public class FileWatcher
{
    private FileSystemWatcher watcher;

    public FileWatcher(string path)
    {
        watcher = new FileSystemWatcher(path)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName,
            Filter = "*.*"
        };

        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Changed += OnChanged;
        watcher.Renamed += OnRenamed;

        watcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл {e.Name} создан.");
        LogChange($"Файл {e.Name} создан.");
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл {e.Name} удален.");
        LogChange($"Файл {e.Name} удален.");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл {e.Name} изменен.");
        LogChange($"Файл {e.Name} изменен.");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"Файл {e.OldName} переименован в {e.Name}.");
        LogChange($"Файл {e.OldName} переименован в {e.Name}.");
    }

    private void LogChange(string message)
    {
        using (StreamWriter writer = new StreamWriter("log.txt", true))
        {
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите путь к папке для отслеживания:");
        string path = Console.ReadLine();

        if (Directory.Exists(path))
        {
            FileWatcher watcher = new FileWatcher(path);
            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Указанный путь не существует.");
        }
    }
}