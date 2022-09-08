using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4.Core
{
    public class CinemaRoom
    {
        public static string[] typeOfRoom = new string[] { "Стандарт",
                                                           "VIP", "IMAX" };
        string type;
        private int rows;
        private int columns;
        private string name;
        private int[,] ticketCost;

        [JsonProperty("CinemaRoomRows")]
        public int Rows
        {
            get
            {
                return rows;
            }
        }

        [JsonProperty("CinemaRoomColumns")]
        public int Columns
        {
            get
            {
                return columns;
            }
        }

        [JsonProperty("CinemaRoomName")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [JsonProperty("CinemaRoomType")]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        [JsonProperty("CinemaRoomTicketCost")]
        public int[,] TicketCost
        {
            get
            {
                return ticketCost;
            }
            set
            {
                ticketCost = value;
            }
        }

        [JsonConstructor]
        public CinemaRoom(string name, int rows, int columns, string type,
                          int[,] ticketCost)
        {
            this.name = name;
            this.rows = rows;
            this.columns = columns;
            this.type = type;
            this.ticketCost = ticketCost;
        }

        //private void PlaceCost() /// Задает цены на места в кинотеатре.
        //{
        //    int checkedRows = 0;
        //    while (checkedRows < ticketCost.GetLength(0))
        //    {
        //        Console.WriteLine($"Введите через пробел стоимости " +
        //                          $"мест в ряду {checkedRows}");
        //        string[] numbers;
        //        numbers = Console.ReadLine().Split();
        //        if (numbers.Length != ticketCost.GetLength(1))
        //        {
        //            Console.WriteLine("Вы ввели некорректное " +
        //                              "количесвто мест в ряде.");
        //            continue;
        //        }
        //        int j;
        //        for (j = 0; j < numbers.Length; ++j)
        //        {
        //            if (!int.TryParse(numbers[j], out ticketCost[checkedRows, j]))
        //            {
        //                Console.WriteLine($"{numbers[j]} не число\n");
        //                break;
        //            }
        //            if (ticketCost[checkedRows, j] <= 0)
        //            {
        //                Console.WriteLine("Цена не может быть " +
        //                                  "отрицательной или нулевой.\n");
        //                break;
        //            }
        //        }
        //        if (j == numbers.Length)
        //        {
        //            ++checkedRows;
        //        }
        //    }
        //}

        public void PrintPrices()
        {
            int i;
            for (i = 0; i < this.Rows; ++i)
            {
                Console.Write(i + "\t");
                for (int j = 0; j < this.Columns; ++j)
                {
                    Console.Write("\t" + (this.TicketCost[i, j]
                                          .ToString()));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            int lengthOfTab = i.ToString().Length;
            while (lengthOfTab >= 0)
            {
                Console.Write(" ");
                lengthOfTab--;
            }
            Console.Write("\t");
            for (int j = 0; j < this.Columns; ++j)
            {
                Console.Write("\t" + j);
            }
            Console.WriteLine();
        }
    }
}
