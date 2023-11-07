using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace module8_prct3
{
    class Program
    {
        static void Main()
        {
            // Замените путь к файлу на свой
            string filePath = "D:\\УЧЁБА\\comp science\\module8_prct3\\sales_data.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                double[] sales = new double[lines.Length];
                string[] months = new string[lines.Length];

                int validDataCount = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 2)
                    {
                        if (double.TryParse(parts[1], out sales[i]))
                        {
                            months[i] = parts[0];
                            validDataCount++;
                        }
                    }
                }

                if (validDataCount < 5)
                {
                    Console.WriteLine("Недостаточно данных для прогноза. Пожалуйста, добавьте данные для пяти предыдущих месяцев.");
                    return;
                }

                double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
                for (int i = 0; i < sales.Length; i++)
                {
                    sumX += i + 1;
                    sumY += sales[i];
                    sumXY += (i + 1) * sales[i];
                    sumX2 += (i + 1) * (i + 1);
                }

                double a = (validDataCount * sumXY - sumX * sumY) / (validDataCount * sumX2 - sumX * sumX);
                double b = (sumY - a * sumX) / validDataCount;

                double[] forecast = new double[3];
                for (int i = 0; i < forecast.Length; i++)
                {
                    int nextMonth = validDataCount + i + 1;
                    forecast[i] = a * nextMonth + b;
                }

                Console.WriteLine("Прогноз объемов продаж на следующие три месяца:");
                for (int i = 0; i < forecast.Length; i++)
                {
                    Console.WriteLine($"{months[validDataCount + i]}: {forecast[i]}");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден. Пожалуйста, укажите правильный путь к файлу TXT.");
            }
        }
    }
}
