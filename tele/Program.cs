using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static List<TVShow> scheduleList = new List<TVShow>();
    static string jsonFilePath = "tv_schedule.json";

    static void Main()
    {
        LoadDataFromJson();

        while (true)
        {
            Console.WriteLine("1. Показать расписание");
            Console.WriteLine("2. Добавить передачу");
            Console.WriteLine("3. Обновить время передачи");
            Console.WriteLine("4. Сохранить и выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowTVSchedule();
                    break;
                case 2:
                    AddTVShow();
                    break;
                case 3:
                    UpdateTVShowTime();
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
            scheduleList = JsonConvert.DeserializeObject<List<TVShow>>(jsonData);
        }
    }

    static void SaveDataToJson()
    {
        string jsonData = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    static void ShowTVSchedule()
    {
        Console.WriteLine("Расписание телепередач:");
        foreach (var tvShow in scheduleList)
        {
            Console.WriteLine($"День: {tvShow.DayOfWeek}, Канал: {tvShow.ChannelNumber}, Время: {tvShow.StartTime}, Название: {tvShow.ShowName}, Жанр: {tvShow.Genre}");
        }
        Console.WriteLine();
    }

    static void AddTVShow()
    {
        Console.Write("Введите день недели: ");
        string dayOfWeek = Console.ReadLine();

        Console.Write("Введите номер канала: ");
        int channelNumber = int.Parse(Console.ReadLine());

        Console.Write("Введите время начала передачи: ");
        string startTime = Console.ReadLine();

        Console.Write("Введите название передачи: ");
        string showName = Console.ReadLine();

        Console.Write("Введите жанр передачи: ");
        string genre = Console.ReadLine();

        TVShow newTVShow = new TVShow { DayOfWeek = dayOfWeek, ChannelNumber = channelNumber, StartTime = startTime, ShowName = showName, Genre = genre };
        scheduleList.Add(newTVShow);

        Console.WriteLine("Передача успешно добавлена!\n");
    }

    static void UpdateTVShowTime()
    {
        Console.Write("Введите название передачи для обновления времени: ");
        string showName = Console.ReadLine();

        TVShow tvShowToUpdate = scheduleList.Find(t => t.ShowName.Equals(showName, StringComparison.OrdinalIgnoreCase));

        if (tvShowToUpdate != null)
        {
            Console.Write("Введите новое время передачи: ");
            string newTime = Console.ReadLine();

            tvShowToUpdate.StartTime = newTime;

            Console.WriteLine("Время передачи успешно обновлено!\n");
        }
        else
        {
            Console.WriteLine("Передача с таким названием не найдена.\n");
        }
    }
}

class TVShow
{
    public string DayOfWeek { get; set; }
    public int ChannelNumber { get; set; }
    public string StartTime { get; set; }
    public string ShowName { get; set; }
    public string Genre { get; set; }
}
