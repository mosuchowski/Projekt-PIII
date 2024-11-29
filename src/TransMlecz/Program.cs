// GŁÓWNY PLIK .cs //
//MENU GŁÓWNE
int string i = "0", j = "0" ;
bool t = true;
Console.Writeline("-------Witaj-------");
Console.Writeline("Wybierz co chcesz zrobić");

Console.Writeline("1 - Pracownicy");
Console.Writeline("2 - Pojazdy");
Console.WriteLine("3 - Zlecenia");
while (t==true;)
{
    i = Console.ReadLine()
    switch (i)
    {
        case 1:
            Display.Komunikat1();
            j = Console.ReadLine();
            //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
            break;
        case 2:
            Console.Writeline("1 - Wyświetl");
            Console.Writeline("2 - Dodaj");
            Console.Writeline("3 - Usuń");
            Console.Writeline("4 - Edytuj");
            j = Console.ReadLine();
            //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
            break;
        case 3:
            Console.Writeline("1 - Wyświetl");
            Console.Writeline("2 - Dodaj");
            Console.Writeline("3 - Usuń");
            Console.Writeline("4 - Edytuj");
            j = Console.ReadLine();
            //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
            break;
        case 4: 
            //ZAPISANIE ZMIAN, ZAKOŃCZENIE DZIAŁANIA PROGRAMU//
            t=false;
            break;
        default:
            Console.Writeline("Nieprawidłowa opcja " << i << ", spróbuj ponownie");
            break;





    }
}


