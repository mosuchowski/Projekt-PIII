namespace Entities
{
    public class Truck
    {
        public int Id;
        public string BrandName;
        public string ModelName;
        public int ManufactureYear;
        public int Mileage;
        public int TowingCapacity; 

        public Truck(int id, string brandName, string modelName, int manufactureYear, int mileage, int towingCapacity)
        {
            Id = id;
            BrandName = brandName;
            ModelName = modelName;
            ManufactureYear = manufactureYear;
            Mileage = mileage;
            TowingCapacity = towingCapacity;
        }
    }

    public class TruckList
    {
        public List<Truck> trucks = new List<Truck>();
        private string sourcePath;

        public TruckList(string sourcePath)
        {
            this.sourcePath = sourcePath;

            if (File.Exists(sourcePath))
            {
                foreach (string line in File.ReadLines(sourcePath))
                {
                    string[] words = line.Split(',');
                    int id = int.Parse(words[0]);
                    string brand = words[1];
                    string model = words[2];
                    int year = int.Parse(words[3]);
                    int mileage = int.Parse(words[4]);
                    int towingCapacity = int.Parse(words[5]);

                    Truck truck = new Truck(id, brand, model, year, mileage, towingCapacity);
                    trucks.Add(truck);
                }
            }
        }

        // Funkcja do wyświetlania listy ciężarówek
        public void ShowTrucks()
        {
            foreach (Truck truck in trucks)
            {
                Console.WriteLine($"ID: {truck.Id}, Marka: {truck.BrandName}, Model: {truck.ModelName}, Rok: {truck.ManufactureYear}, Przebieg: {truck.Mileage} km, Ładowność: {truck.TowingCapacity} kg");
            }
        }

        // Funkcja do dodawania nowej ciężarówki
        public void AddTruck()
        {
            int id;
            while (true)
            {
                Console.Write("Podaj ID ciężarówki (liczba dodatnia): ");
                if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                {
                    Console.WriteLine("Nieprawidłowe ID. Musi być liczbą dodatnią.");
                    continue;
                }

                if (trucks.Exists(t => t.Id == id))
                {
                    Console.WriteLine("Ciężarówka z tym ID już istnieje. Podaj inne ID.");
                    continue;
                }
                break;
            }

            Console.Write("Podaj markę ciężarówki: ");
            string brand = Console.ReadLine();

            Console.Write("Podaj model ciężarówki: ");
            string model = Console.ReadLine();

            int year;
            while (true)
            {
                Console.Write("Podaj rok produkcji: ");
                if (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
                {
                    Console.WriteLine("Nieprawidłowy rok. Podaj poprawny rok produkcji.");
                    continue;
                }
                break;
            }

            int mileage;
            while (true)
            {
                Console.Write("Podaj przebieg w km: ");
                if (!int.TryParse(Console.ReadLine(), out mileage) || mileage < 0)
                {
                    Console.WriteLine("Nieprawidłowy przebieg. Podaj liczbę dodatnią.");
                    continue;
                }
                break;
            }

            int towingCapacity;
            while (true)
            {
                Console.Write("Podaj ładowność w kg: ");
                if (!int.TryParse(Console.ReadLine(), out towingCapacity) || towingCapacity <= 0)
                {
                    Console.WriteLine("Nieprawidłowa ładowność. Podaj liczbę dodatnią.");
                    continue;
                }
                break;
            }

            Truck newTruck = new Truck(id, brand, model, year, mileage, towingCapacity);
            trucks.Add(newTruck);

            // Zapis do pliku
            using (StreamWriter sw = new StreamWriter(sourcePath, true))
            {
                if (new FileInfo(sourcePath).Length > 0)
                {
                    sw.WriteLine();
                }
                sw.Write($"{id},{brand},{model},{year},{mileage},{towingCapacity}");
            }

            Console.WriteLine("Ciężarówka została dodana.");
        }

        // Funkcja do usuwania ciężarówki
        public void RemoveTruck()
        {
            Console.Write("Podaj ID ciężarówki do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Nieprawidłowe ID. Musi być liczbą dodatnią.");
                return;
            }

            Truck truckToRemove = trucks.FirstOrDefault(t => t.Id == id);
            if (truckToRemove == null)
            {
                Console.WriteLine($"Ciężarówka z ID {id} nie istnieje.");
                return;
            }

            trucks.Remove(truckToRemove);
            Console.WriteLine($"Ciężarówka {truckToRemove.BrandName} {truckToRemove.ModelName} została usunięta.");

            // Aktualizacja pliku
            using (StreamWriter sw = new StreamWriter(sourcePath, false))
            {
                foreach (var truck in trucks)
                {
                    sw.WriteLine($"{truck.Id},{truck.BrandName},{truck.ModelName},{truck.ManufactureYear},{truck.Mileage},{truck.TowingCapacity}");
                }
            }
        }
    }
}
