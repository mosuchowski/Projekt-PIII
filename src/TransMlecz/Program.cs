// GŁÓWNY PLIK .cs //
//MENU GŁÓWNE
using System.Collections.Generic;
using Entities;
using Functions;
string i, j;
bool t = true;
DriverList localdriverlist = new DriverList(@"Drivers.txt");
Console.WriteLine("-------Witaj-------");

while (t == true)
{
    Console.WriteLine("Wybierz co chcesz zrobić");

    Console.WriteLine("1 - Pracownicy (wyświetl)");
    Console.WriteLine("2 - Pojazdy");
    Console.WriteLine("3 - Zlecenia");
    Console.WriteLine("4 - Zakończ");
    i = Console.ReadLine();
    switch (i)
    {
        case "1":
            {
                localdriverlist.ShowDrivers();
                break;
            }
        case "2":
            {
                Messages.Message1();
                //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
                break;
            }
        case "3":
            {
                Messages.Message1();
                //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
                break;
            }
        case "4":
            {
                //ZAPISANIE ZMIAN, ZAKOŃCZENIE DZIAŁANIA PROGRAMU//
                t = false;
            }
            break;
        default:
            Console.WriteLine("Nieprawidłowa opcja ", i, ", spróbuj ponownie");
            break;





    }
}

