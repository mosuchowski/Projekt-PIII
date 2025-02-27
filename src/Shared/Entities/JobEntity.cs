using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Entities
{
    public class Job
    {
        public int Id;
        public string Description;
        public int Distance;
        public int DriverId;
        public int TruckId;
        public bool IsCompleted;
        public bool RequiresCertA;
        public bool RequiresCertB;
        public int Weight; // Zmieniono typ na int

        public Job(int id, string description, int distance, int driverId, int truckId, bool isCompleted, bool requiresCertA, bool requiresCertB, int weight)
        {
            Id = id;
            Description = description;
            Distance = distance;
            DriverId = driverId;
            TruckId = truckId;
            IsCompleted = isCompleted;
            RequiresCertA = requiresCertA;
            RequiresCertB = requiresCertB;
            Weight = weight;
        }
    }

    public class JobList
    {
        public List<Job> jobs = new List<Job>();
        private readonly string jobFilePath;

        public JobList(string sourcePath)
        {
            jobFilePath = sourcePath;
            LoadJobs();
        }

        private void LoadJobs()
        {
            if (!File.Exists(jobFilePath)) return;
            foreach (string line in File.ReadLines(jobFilePath))
            {
                string[] words = line.Split(',');
                int id = int.Parse(words[0]);
                string description = words[1];
                int distance = int.Parse(words[2]);
                int driverId = int.Parse(words[3]);
                int truckId = int.Parse(words[4]);
                bool isCompleted = bool.Parse(words[5]);
                bool requiresCertA = bool.Parse(words[6]);
                bool requiresCertB = bool.Parse(words[7]);
                int weight = int.Parse(words[8]); // Zmieniono na int

                jobs.Add(new Job(id, description, distance, driverId, truckId, isCompleted, requiresCertA, requiresCertB, weight));
            }
        }
        public void ShowJobs()
        {
            if (jobs.Count == 0)
            {
                Console.WriteLine("Brak dostępnych zleceń.");
                return;
            }

            Console.WriteLine("Lista zleceń:");
            Console.WriteLine("ID | Opis | Dystans (km) | Masa (kg) | Kierowca ID | Ciężarówka ID | Status | Cert A | Cert B");

            foreach (var job in jobs)
            {
                string status = job.IsCompleted ? "Zakończone" : "W toku";
                Console.WriteLine($"{job.Id} | {job.Description} | {job.Distance} km | {job.Weight} kg | {job.DriverId} | {job.TruckId} | {status} | {job.RequiresCertA} | {job.RequiresCertB}");
            }
        }
        public void AddJob()
        {
            int id = jobs.Count > 0 ? jobs.Max(j => j.Id) + 1 : 1;

            Console.Write("Podaj opis zlecenia: ");
            string description = Console.ReadLine();

            int distance;
            while (true)
            {
                Console.Write("Podaj dystans w km: ");
                if (int.TryParse(Console.ReadLine(), out distance) && distance > 0)
                    break;
                Console.WriteLine("Nieprawidłowy dystans. Podaj liczbę dodatnią.");
            }

            Console.Write("Czy zlecenie wymaga Certyfikatu A? (tak/nie): ");
            bool requiresCertA = Console.ReadLine().Trim().ToLower() == "tak";

            Console.Write("Czy zlecenie wymaga Certyfikatu B? (tak/nie): ");
            bool requiresCertB = Console.ReadLine().Trim().ToLower() == "tak";

            int weight;
            while (true)
            {
                Console.Write("Podaj masę towaru w kg: ");
                if (int.TryParse(Console.ReadLine(), out weight) && weight > 0)
                    break;
                Console.WriteLine("Nieprawidłowa masa. Podaj liczbę dodatnią.");
            }

            Job newJob = new Job(id, description, distance, 0, 0, false, requiresCertA, requiresCertB, weight);
            jobs.Add(newJob);

            using (StreamWriter sw = new StreamWriter(jobFilePath, true))
            {
                sw.WriteLine($"{id},{description},{distance},0,0,false,{requiresCertA},{requiresCertB},{weight}");
            }

            Console.WriteLine("Zlecenie zostało dodane. Przypisz kierowcę i pojazd osobno.");
        }
        public void AssignJob(DriverList driverList, TruckList truckList)
        {
            if (jobs.Count == 0)
            {
                Console.WriteLine("Brak dostępnych zleceń do przypisania.");
                return;
            }

            ShowJobs(); // Pokazuje listę zleceń, aby użytkownik wiedział, które ID podać

            Console.Write("Podaj ID zlecenia do przypisania: ");
            if (!int.TryParse(Console.ReadLine(), out int jobId))
            {
                Console.WriteLine("Nieprawidłowe ID zlecenia.");
                return;
            }

            Job job = jobs.FirstOrDefault(j => j.Id == jobId);
            if (job == null)
            {
                Console.WriteLine("Nie znaleziono zlecenia o podanym ID.");
                return;
            }

            if (job.IsCompleted)
            {
                Console.WriteLine("Zlecenie już zostało zakończone.");
                return;
            }

            driverList.ShowDrivers();
            Console.Write("Podaj ID kierowcy do przypisania: ");
            if (!int.TryParse(Console.ReadLine(), out int driverId))
            {
                Console.WriteLine("Nieprawidłowe ID kierowcy.");
                return;
            }

            Driver driver = driverList.drivers.FirstOrDefault(d => d.Id == driverId);
            if (driver == null)
            {
                Console.WriteLine("Nie znaleziono kierowcy.");
                return;
            }

            if ((job.RequiresCertA && !driver.CertificateA) || (job.RequiresCertB && !driver.CertificateB))
            {
                Console.WriteLine("Kierowca nie posiada wymaganych certyfikatów.");
                return;
            }

            truckList.ShowTrucks();
            Console.Write("Podaj ID pojazdu do przypisania: ");
            if (!int.TryParse(Console.ReadLine(), out int truckId))
            {
                Console.WriteLine("Nieprawidłowe ID pojazdu.");
                return;
            }

            Truck truck = truckList.trucks.FirstOrDefault(t => t.Id == truckId);
            if (truck == null)
            {
                Console.WriteLine("Nie znaleziono pojazdu.");
                return;
            }

            if (job.Weight > truck.TowingCapacity)
            {
                Console.WriteLine($"Ciężarówka {truck.BrandName} {truck.ModelName} nie może przewieźć {job.Weight} kg (max: {truck.TowingCapacity} kg).");
                return;
            }

            job.DriverId = driverId;
            job.TruckId = truckId;

            File.WriteAllLines(jobFilePath, jobs.Select(j => $"{j.Id},{j.Description},{j.Distance},{j.DriverId},{j.TruckId},{j.IsCompleted},{j.RequiresCertA},{j.RequiresCertB},{j.Weight}"));

            Console.WriteLine($"Zlecenie {jobId} przypisano do kierowcy {driver.Name} {driver.Surname} i pojazdu {truck.BrandName} {truck.ModelName}.");
        }
        public void CompleteJob(TruckList truckList, string truckFilePath)
        {
            if (jobs.Count == 0)
            {
                Console.WriteLine("Brak dostępnych zleceń.");
                return;
            }

            ShowJobs();
            Console.Write("Podaj ID zlecenia do zakończenia: ");
            if (!int.TryParse(Console.ReadLine(), out int jobId))
            {
                Console.WriteLine("Nieprawidłowe ID zlecenia.");
                return;
            }

            Job job = jobs.FirstOrDefault(j => j.Id == jobId);
            if (job == null)
            {
                Console.WriteLine("Nie znaleziono zlecenia.");
                return;
            }

            if (job.IsCompleted)
            {
                Console.WriteLine("Zlecenie już zostało zakończone.");
                return;
            }

            Truck truck = truckList.trucks.FirstOrDefault(t => t.Id == job.TruckId);
            if (truck == null)
            {
                Console.WriteLine("Nie znaleziono przypisanej ciężarówki.");
                return;
            }

            // Oznaczenie zlecenia jako zakończone
            job.IsCompleted = true;

            // Zwiększenie przebiegu ciężarówki o dystans zlecenia
            truck.Mileage += job.Distance;

            // Aktualizacja pliku zleceń
            File.WriteAllLines(jobFilePath, jobs.Select(j =>
                $"{j.Id},{j.Description},{j.Distance},{j.DriverId},{j.TruckId},{j.IsCompleted},{j.RequiresCertA},{j.RequiresCertB},{j.Weight}"));

            // Aktualizacja pliku ciężarówek
            File.WriteAllLines(truckFilePath, truckList.trucks.Select(t =>
                $"{t.Id},{t.BrandName},{t.ModelName},{t.ManufactureYear},{t.Mileage},{t.TowingCapacity}"));

            Console.WriteLine($"Zlecenie {jobId} zakończone. Przebieg ciężarówki {truck.BrandName} {truck.ModelName} zwiększony o {job.Distance} km.");
        }

    }
}