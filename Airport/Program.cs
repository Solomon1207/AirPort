using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    class Program
    {
        public static Flight[] FlightList = new Flight[100];
        public static string[] FieldName = new string[] { "Number", "Terminal", "At", "DepartureCity", "ArrivalCity", "Airline", "Status" };


        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            FlightList[0] = new Flight() { Number = 1, At = DateTime.Now, DepartureCity = "London",ArrivalCity = "Kyiv", Airline = "BritishAir", Terminal = 1, Status = FlightStatus.Arrived };
            FlightList[1] = new Flight() { Number = 2, At = DateTime.Now, DepartureCity = "Kyiv", ArrivalCity = "NY", Airline = "UkrainAir", Terminal = 2, Status = FlightStatus.Canceled };

            do
            {
                ShowMenu();
            } while (true);

        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void ShowMenu()
        {
            Console.WriteLine("Enter Ypur action : ");
            Console.WriteLine("1 - Show All");
            Console.WriteLine("2 - Show In Hour Range");
            Console.WriteLine("3 - Add");
            Console.WriteLine("4 - Edit");
            Console.WriteLine("5 - Delete");
            Console.WriteLine("6 - Exit");
            switch (Console.ReadLine())
            {
                case "1":
                    Show();
                    break;
                case "2":
                    ShowInHourRange();
                    break;
                case "3":
                    Add();
                    break;
                case "4":
                    Edit();
                    break;
                case "5":
                    Delete();
                    break;
                case "6":
                    Exit();
                    break;
                default:
                    break;
            }

        }
        public static void ShowForm()
        {
            Console.Clear();
            string OutPutString = String.Format("Number\t|\tTerminal\t|\t\tAt\t|\tDepartureCity\t|\tArrivalCity\t|\tAirline\t|\tStatus\t|\t");
            Console.WriteLine(OutPutString);

        }
        public static void Show()
        {
            ShowForm();
            Console.SetCursorPosition(0, 3);
            foreach (var item in FlightList)
            {
                if (item.Number == null)
                    continue;
                string OutPutString = String.Format("  {0}\t\t  {1}\t\t  {2}\t\t  {3}\t\t  {4}\t\t  {5}\t\t  {6}", item.Number, item.Terminal, item.At, item.DepartureCity, item.ArrivalCity, item.Airline, item.Status);
                Console.WriteLine(OutPutString);
                Console.WriteLine("\n");
            }
        }

        public static void ShowInHourRange()
        {
            ShowForm();
            Console.SetCursorPosition(0, 3);
            foreach (var item in FlightList)
            {
                if (item.Number == null)
                    continue;
                if (item.At > DateTime.Now && item.At < DateTime.Now.AddHours(1))
                {
                    string OutPutString = String.Format("  {0}\t\t  {1}\t\t  {2}\t\t  {3}\t\t  {4}\t\t  {5}\t\t  {6}", item.Number, item.Terminal, item.At, item.DepartureCity, item.ArrivalCity, item.Airline, item.Status);
                    Console.WriteLine(OutPutString);
                    Console.WriteLine("\n");
                }
            }
        }
        public static bool Add()
        {
            Console.WriteLine("Add new Flight");
            Console.Clear();
            int? index = FindFreeIndex();
            if (index == null)
                return false;
            foreach (var fieldItem in FieldName)
            {
                if(fieldItem == "At")
                    Console.WriteLine("Please enter Date & Time Flight in format (Year, Month, Day, Hour, Minute, Second)");
                else
                    Console.WriteLine("Please enter {0}", fieldItem);

                processingField(fieldItem, Console.ReadLine(), index);
            }

            return true;
        }

        public static bool Delete()
        {
            Console.Clear();
            Console.WriteLine("Enter Number Flight for Delete");
            int flightNumber = -1;
            if (int.TryParse(Console.ReadLine(), out flightNumber) || flightNumber > 0)
                return false;

            for (int i = 0; i < FlightList.Count(); i++)
            {
                if (flightNumber == FlightList[i].Number)
                    FlightList[i] = new Flight();
            }

            return true;
        }


        private static void processingField(string field, object fieldData, int? index)
        {
            if (index == null)
                return;
            switch (field)
            {
                case "Number":
                    FlightList[(int)index].Number = Convert.ToInt32(fieldData);
                    break;
                case "Terminal":
                    FlightList[(int)index].Terminal = Convert.ToInt32(fieldData);
                    break;
                case "At":
                    var timeArray = Convert.ToString(fieldData).Split(',');
                    FlightList[(int)index].At = new DateTime(Convert.ToInt32(timeArray[0]), Convert.ToInt32(timeArray[1]), Convert.ToInt32(timeArray[2]), Convert.ToInt32(timeArray[3]), Convert.ToInt32(timeArray[4]), Convert.ToInt32(timeArray[5]));
                    break;
                case "DepartureCity":
                    FlightList[(int)index].DepartureCity = fieldData.ToString();
                    break;
                case "ArrivalCity":
                    FlightList[(int)index].ArrivalCity = fieldData.ToString();
                    break;
                case "Airline":
                    FlightList[(int)index].Airline = fieldData.ToString();
                    break;
                case "Status":
                    FlightList[(int)index].Status = (FlightStatus)Convert.ToInt32(fieldData);
                    break;
            }
        }

        private static int? FindFreeIndex()
        {
            for (int i = 0; i < FlightList.Count(); i++)
            {
                if (FlightList[i].Number == null)
                    return i;
            }
            return null;
        }


        public static bool Edit()
        {
            int flightNumber = -1;
            Console.Clear();
            Console.WriteLine("Plese Enter Number of Flight");


            if (!int.TryParse(Console.ReadLine(), out flightNumber) || flightNumber < 0)
            {
                Console.WriteLine("Error pleace try again");
                return false;
            }

            for (int i = 0; i < FlightList.Count(); i++)
            {
                if (flightNumber == FlightList[i].Number)
                {
                    Console.WriteLine("Flight # {0} Found", flightNumber);
                    Console.WriteLine("Select field to edit");
                    Console.WriteLine(@"
1 : DepartureCity
2 : ArrivalCity
3 : Airline;
4 : Terminal");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Enter new DepartureCity");
                            processingField("DepartureCity", Console.ReadLine(), i);
                            break;
                        case "2":
                            Console.WriteLine("Enter new ArrivalCity");
                            processingField("ArrivalCity", Console.ReadLine(), i);
                            break;
                        case "3":
                            Console.WriteLine("Enter new Airline");
                            processingField("Airline", Console.ReadLine(), i);
                            break;
                        case "4":
                            Console.WriteLine("Enter new Terminal");
                            int newValue = -1;
                            if (int.TryParse(Console.ReadLine(), out newValue) || newValue > 0)
                                processingField("Terminal", Console.ReadLine(), i);
                            else
                                Console.WriteLine("Error");
                            break;
                        default:
                            Console.WriteLine("Enter Error");
                            break;
                    }
                    break;
                }

            }
            return true;

        }
    }
    enum FlightStatus
    {
        Checkln,
        GateClosed,
        Arrived,
        DepartedAt,
        Unknown,
        Canceled,
        etc
    }

    struct Flight
    {
        public int? Number;
        public int Terminal;
        public DateTime At;
        public string DepartureCity, ArrivalCity,  Airline;
        public FlightStatus Status;
    }


}
