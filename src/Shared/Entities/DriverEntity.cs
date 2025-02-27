using System;
using System.Collections.Generic;
using System.IO;

namespace Entities
{
    public class Driver
    {
        public int Id;
        public string Name;
        public string Surname;
        public bool CertificateA;
        public bool CertificateB;

        public Driver(int Id, string Name, string Surname, bool CertificateA, bool CertificateB)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = Surname;
            this.CertificateA = CertificateA;
            this.CertificateB = CertificateB;
        }
    }

    public class DriverList
    {
        public List<Driver> drivers = new List<Driver>();
        private string sourcePath;

        public DriverList(string sourcePath)
        {
            this.sourcePath = sourcePath;
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Plik z kierowcami nie istnieje. Tworzenie nowego...");
                File.Create(sourcePath).Close();
                return;
            }

            foreach (string line in File.ReadLines(sourcePath))
            {
                string[] words = line.Split(',');
                if (words.Length < 5) continue; // Pominięcie błędnych linii

                try
                {
                    int id = int.Parse(words[0]);
                    string name = words[1];
                    string surname = words[2];
                    bool certificateA = bool.Parse(words[3]);
                    bool certificateB = bool.Parse(words[4]);

                    Driver e = new Driver(id, name, surname, certificateA, certificateB);
                    drivers.Add(e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd odczytu linii: {line}. Szczegóły: {ex.Message}");
                }
            }
        }

        public void ShowDrivers()
        {
            if (drivers.Count == 0)
            {
                Console.WriteLine("Brak kierowców w systemie.");
                return;
            }

            foreach (Driver e in drivers)
            {
                Console.WriteLine($"Id: {e.Id}, Name: {e.Name}, Surname: {e.Surname}, Certificate A: {e.CertificateA}, Certificate B: {e.CertificateB}");
            }
        }

        public void AddDriver()
        {
            int id;
            while (true)
            {
                Console.Write("Podaj ID kierowcy: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Nieprawidłowe ID. Musi być liczbą.");
                    continue;
                }

                // Sprawdzamy, czy ID już istnieje
                if (drivers.Exists(d => d.Id == id))
                {
                    Console.WriteLine("Kierowca z tym ID już istnieje. Wprowadź inne ID.");
                    continue;
                }

                break;
            }

            Console.Write("Podaj imię kierowcy: ");
            string name = Console.ReadLine();

            Console.Write("Podaj nazwisko kierowcy: ");
            string surname = Console.ReadLine();

            Console.Write("Czy kierowca ma Certyfikat A? (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool certA))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wpisz 'true' lub 'false'.");
                return;
            }

            Console.Write("Czy kierowca ma Certyfikat B? (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool certB))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wpisz 'true' lub 'false'.");
                return;
            }

            // Tworzenie nowego obiektu kierowcy
            Driver newDriver = new Driver(id, name, surname, certA, certB);
            drivers.Add(newDriver);

            // Sprawdzamy, czy plik jest pusty
            bool fileIsEmpty = new FileInfo(sourcePath).Length == 0;

            // Zapis do pliku z nową linią
            using (StreamWriter sw = new StreamWriter(sourcePath, true))
            {
                if (!fileIsEmpty)
                {
                    sw.WriteLine(); // Dodanie nowej linii, jeśli plik nie jest pusty
                }
                sw.Write($"{id},{name},{surname},{certA},{certB}");
            }

            Console.WriteLine("Kierowca został dodany.");
        }
        public void RemoveDriver()
{
    Console.Write("Podaj ID kierowcy do usunięcia: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Nieprawidłowe ID. Musi być liczbą.");
        return;
    }

    // Szukamy kierowcy w liście
    Driver driverToRemove = drivers.FirstOrDefault(d => d.Id == id);

    if (driverToRemove == null)
    {
        Console.WriteLine($"Kierowca z ID {id} nie istnieje.");
        return;
    }

    // Usuwamy kierowcę z listy
    drivers.Remove(driverToRemove);
    Console.WriteLine($"Kierowca {driverToRemove.Name} {driverToRemove.Surname} został usunięty.");

    // Aktualizacja pliku (zapisujemy nową listę kierowców)
    using (StreamWriter sw = new StreamWriter(sourcePath, false))
    {
        foreach (var driver in drivers)
        {
            sw.WriteLine($"{driver.Id},{driver.Name},{driver.Surname},{driver.CertificateA},{driver.CertificateB}");
        }
    }
}
    }
}