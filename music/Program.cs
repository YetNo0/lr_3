using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static List<MusicalNote> melodyList = new List<MusicalNote>();
    static string jsonFilePath = "melody_info.json";

    static void Main()
    {
        LoadDataFromJson();

        while (true)
        {
            Console.WriteLine("1. Показать информацию о мелодии");
            Console.WriteLine("2. Добавить ноту");
            Console.WriteLine("3. Обновить длительность ноты");
            Console.WriteLine("4. Сохранить и выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowMelodyInfo();
                    break;
                case 2:
                    AddMusicalNote();
                    break;
                case 3:
                    UpdateNoteDuration();
                    break;
                case 4:
                    SaveDataToJson();
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }
        }
    }

    static void LoadDataFromJson()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            melodyList = JsonConvert.DeserializeObject<List<MusicalNote>>(jsonData);
        }
    }

    static void SaveDataToJson()
    {
        string jsonData = JsonConvert.SerializeObject(melodyList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    static void ShowMelodyInfo()
    {
        Console.WriteLine("Информация о музыкальной мелодии:");
        foreach (var note in melodyList)
        {
            Console.WriteLine($"Тон: {note.Pitch}, Длительность: {note.Duration} мс");
        }
        Console.WriteLine();
    }

    static void AddMusicalNote()
    {
        Console.Write("Введите тон звука (например, C, D, E): ");
        string pitch = Console.ReadLine();

        Console.Write("Введите длительность ноты (в миллисекундах): ");
        int duration = int.Parse(Console.ReadLine());

        MusicalNote newNote = new MusicalNote { Pitch = pitch, Duration = duration };
        melodyList.Add(newNote);

        Console.WriteLine("Нота успешно добавлена!\n");
    }

    static void UpdateNoteDuration()
    {
        Console.Write("Введите тон звука для обновления длительности ноты: ");
        string pitch = Console.ReadLine();

        MusicalNote noteToUpdate = melodyList.Find(n => n.Pitch.Equals(pitch, StringComparison.OrdinalIgnoreCase));

        if (noteToUpdate != null)
        {
            Console.Write("Введите новую длительность ноты (в миллисекундах): ");
            int newDuration = int.Parse(Console.ReadLine());

            noteToUpdate.Duration = newDuration;

            Console.WriteLine("Длительность ноты успешно обновлена!\n");
        }
        else
        {
            Console.WriteLine("Нота с таким тоном не найдена.\n");
        }
    }
}

class MusicalNote
{
    public string Pitch { get; set; }
    public int Duration { get; set; }
}
