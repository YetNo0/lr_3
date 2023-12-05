using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static List<Product> productList = new List<Product>();
    static string jsonFilePath = "price_list.json";

    static void Main()
    {
        LoadDataFromJson();

        while (true)
        {
            Console.WriteLine("1. Показать прайс-лист");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Обновить количество товара");
            Console.WriteLine("4. Сохранить и выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowPriceList();
                    break;
                case 2:
                    AddProduct();
                    break;
                case 3:
                    UpdateProductQuantity();
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
            productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);
        }
    }

    static void SaveDataToJson()
    {
        string jsonData = JsonConvert.SerializeObject(productList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    static void ShowPriceList()
    {
        Console.WriteLine("Прайс-лист:");
        foreach (var product in productList)
        {
            Console.WriteLine($"Название: {product.Name}, Цена: {product.Price}, Единица измерения: {product.Unit}, Количество: {product.Quantity}");
        }
        Console.WriteLine();
    }

    static void AddProduct()
    {
        Console.Write("Введите название товара: ");
        string name = Console.ReadLine();

        Console.Write("Введите цену за единицу: ");
        double price = double.Parse(Console.ReadLine());

        Console.Write("Введите единицу измерения: ");
        string unit = Console.ReadLine();

        Console.Write("Введите количество: ");
        int quantity = int.Parse(Console.ReadLine());

        Product newProduct = new Product { Name = name, Price = price, Unit = unit, Quantity = quantity };
        productList.Add(newProduct);

        Console.WriteLine("Товар успешно добавлен!\n");
    }

    static void UpdateProductQuantity()
    {
        Console.Write("Введите название товара для обновления количества: ");
        string productName = Console.ReadLine();

        Product productToUpdate = productList.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (productToUpdate != null)
        {
            Console.Write("Введите новое количество товара: ");
            int newQuantity = int.Parse(Console.ReadLine());

            productToUpdate.Quantity = newQuantity;

            Console.WriteLine("Количество товара успешно обновлено!\n");
        }
        else
        {
            Console.WriteLine("Товар с таким названием не найден.\n");
        }
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Unit { get; set; }
    public int Quantity { get; set; }
}
