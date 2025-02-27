// GŁÓWNY PLIK .cs //
//MENU GŁÓWNE
using System.Collections.Generic;
using Entities;
using Functions;
string i, j;
bool t = true;
DriverList localdriverlist = new DriverList(@"Drivers.txt");
TruckList localtrucklist = new TruckList(@"Trucks.txt");
JobList localjoblist = new JobList(@"Jobs.txt");
Console.WriteLine("-------Witaj-------");

while (t == true)
{
    Messages.Message1();
    i = Console.ReadLine();
    switch (i)
    {
        case "1":
            {
                Messages.Message2();
                j = Console.ReadLine();
                switch (j)
                {
                    case "1":
                        {
                            localdriverlist.ShowDrivers();
                            break;
                        }
                    case "2":
                        {
                            localdriverlist.AddDriver();
                            break;
                        }
                    case "3":
                        {
                            localdriverlist.RemoveDriver();
                            break;
                        }                        
                }

                break;
            }

        case "2":
            {
                Messages.Message3();
                j = Console.ReadLine();
                switch (j)
                {
                    case "1":
                        {
                            localtrucklist.ShowTrucks();
                            break;
                        }
                    case "2":
                        {
                            localtrucklist.AddTruck();
                            break;
                        }
                    case "3":
                        {
                            localtrucklist.RemoveTruck();
                            break;
                        }
                }

                break;
            }
        case "3":
            {
                Messages.Message4();
                j = Console.ReadLine();
                switch (j)
                {
                    case "1":
                        {
                            localjoblist.ShowJobs();
                            break;
                        }
                    case "2":
                        {
                            localjoblist.AddJob();
                            break;
                        }
                    case "3":
                        {
                            localjoblist.AssignJob(localdriverlist, localtrucklist);
                            break;
                        }
                    case "4":
                        {
                            localjoblist.CompleteJob(localtrucklist,@"Trucks.txt");
                            break;
                        }



                }

                break;
            }
        case "4":
            {
                //ZAPISANIE ZMIAN, ZAKOŃCZENIE DZIAŁANIA PROGRAMU//
                t = false;
            }
            break;

        default:
            Console.WriteLine("Nieprawidłowa opcja, spróbuj ponownie");
            
            break;
            




    }
    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
    if (t)
        Console.ReadKey();
    Console.Clear();
}

