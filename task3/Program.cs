using System.Text.Json;
using System.Text.Json.Nodes;
using System;
using System.Collections.Generic;
using System.IO;

namespace test3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                //                args = new string[]
                //{
                //                    "C:\\Users\\AKH\\Windows1\\dd\\tasks\\task3\\values.json",
                //                    "C:\\Users\\AKH\\Windows1\\dd\\tasks\\task3\\tests.json",
                //                    "C:\\Users\\AKH\\Windows1\\dd\\tasks\\task3\\report.json"
                //                };
                Console.WriteLine("Нужно передать пути к трем файлам: values.json tests.json и report.json");
                Console.ReadKey();
                return;
            }

            string valuesText = File.ReadAllText(args[0]);
            string testsText = File.ReadAllText(args[1]);

            JsonNode valuesJson = JsonNode.Parse(valuesText);
            JsonNode testsJson = JsonNode.Parse(testsText);

            var results = new Dictionary<int, string>();

            JsonArray valuesArray = valuesJson["values"].AsArray();

            for(int i = 0; i < valuesArray.Count; i++)
            {
                int id = valuesArray[i]["id"].GetValue<int>();
                string value = valuesArray[i]["value"].GetValue<string>();

                results[id] = value;
            }

            JsonArray testsArray = testsJson["tests"].AsArray();

            FillTestValues(testsArray, results);

            File.WriteAllText(args[2], testsJson.ToJsonString(new JsonSerializerOptions
            {
                WriteIndented = true
            }));

            Console.WriteLine("Файл report.json создан");
        }

        private static void FillTestValues(JsonArray tests, Dictionary<int, string> results)
        {
            for (int i = 0; i < tests.Count; i++)
            {
                JsonNode test = tests[i];

                int id = test["id"].GetValue<int>();

                if (results.ContainsKey(id))
                {
                    test["value"] = results[id];
                }

                // Если у текущего теста есть вложенные элементы, рекурсивно их обрабатываем
                if (test["values"] != null)
                {
                    JsonArray innerTests = test["values"].AsArray();
                    FillTestValues(innerTests, results);
                }
            }
        }
    }
}