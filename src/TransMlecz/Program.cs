// GŁÓWNY PLIK .cs //
//MENU GŁÓWNE
int i = 0, j = 0;
bool t = true;
Console.Writeline("-------Witaj-------");
Console.Writeline("Wybierz co chcesz zrobić");

Console.Writeline("1 - Pracownicy");
Console.Writeline("2 - Pojazdy");
Console.WriteLine("3 - Zlecenia");
while (t==true;)
{
    cin >> i;
    switch (i)
    {
        case 1:
            Console.Writeline("1 - Wyświetl");
            Console.Writeline("2 - Dodaj");
            Console.Writeline("3 - Usuń");
            Console.Writeline("4 - Edytuj");
            cin >> j;
            //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
            break;
        case 2:
            Console.Writeline("1 - Wyświetl");
            Console.Writeline("2 - Dodaj");
            Console.Writeline("3 - Usuń");
            Console.Writeline("4 - Edytuj");
            cin >> j;
            //ODWOŁANIE DO ODPOWIEDNIEJ FUNKCJI//
            break;
        case 3:
            Console.Writeline("1 - Wyświetl");
            Console.Writeline("2 - Dodaj");
            Console.Writeline("3 - Usuń");
            Console.Writeline("4 - Edytuj");
            cin >> j;
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


