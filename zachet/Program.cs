using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static List<AcademicRecord> academicRecordsList = new List<AcademicRecord>();
    static string jsonFilePath = "academic_records.json";

    static void Main()
    {
        LoadDataFromJson();

        while (true)
        {
            Console.WriteLine("1. Показать зачетную книжку");
            Console.WriteLine("2. Добавить запись");
            Console.WriteLine("3. Обновить оценку");
            Console.WriteLine("4. Сохранить и выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowAcademicRecords();
                    break;
                case 2:
                    AddAcademicRecord();
                    break;
                case 3:
                    UpdateGrade();
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
            academicRecordsList = JsonConvert.DeserializeObject<List<AcademicRecord>>(jsonData);
        }
    }

    static void SaveDataToJson()
    {
        string jsonData = JsonConvert.SerializeObject(academicRecordsList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    static void ShowAcademicRecords()
    {
        Console.WriteLine("Зачетная книжка студента:");
        foreach (var record in academicRecordsList)
        {
            Console.WriteLine($"Дисциплина: {record.Subject}, Часов: {record.Hours}, Вид отчетности: {record.AssessmentType}, Оценка: {record.Grade}");
        }
        Console.WriteLine();
    }

    static void AddAcademicRecord()
    {
        Console.Write("Введите дисциплину: ");
        string subject = Console.ReadLine();

        Console.Write("Введите количество часов: ");
        int hours = int.Parse(Console.ReadLine());

        Console.Write("Введите вид отчетности (экзамен, зачет, курсовая работа): ");
        string assessmentType = Console.ReadLine();

        Console.Write("Введите оценку: ");
        int grade = int.Parse(Console.ReadLine());

        AcademicRecord newRecord = new AcademicRecord { Subject = subject, Hours = hours, AssessmentType = assessmentType, Grade = grade };
        academicRecordsList.Add(newRecord);

        Console.WriteLine("Запись успешно добавлена!\n");
    }

    static void UpdateGrade()
    {
        Console.Write("Введите дисциплину для обновления оценки: ");
        string subject = Console.ReadLine();

        AcademicRecord recordToUpdate = academicRecordsList.Find(r => r.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase));

        if (recordToUpdate != null)
        {
            Console.Write("Введите новую оценку: ");
            int newGrade = int.Parse(Console.ReadLine());

            recordToUpdate.Grade = newGrade;

            Console.WriteLine("Оценка успешно обновлена!\n");
        }
        else
        {
            Console.WriteLine("Запись с такой дисциплиной не найдена.\n");
        }
    }
}

class AcademicRecord
{
    public string Subject { get; set; }
    public int Hours { get; set; }
    public string AssessmentType { get; set; }
    public int Grade { get; set; }
}
