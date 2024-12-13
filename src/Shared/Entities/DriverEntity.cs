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

        public DriverList(string sourcePath)
        {
            foreach (string line in File.ReadLines(sourcePath))
            {
                string[] words = line.Split(',');
                int id = int.Parse(words[0]);
                string name = words[1];
                string surname = words[2];
                bool certificateA = bool.Parse(words[3]);
                bool certificateB = bool.Parse(words[4]);

                Driver e = new Driver(id, name, surname, certificateA, certificateB);
                drivers.Add(e);


            }
        }
        public void ShowDrivers()
        {
            foreach (Driver e in drivers)
            {
                Console.WriteLine("chuj kurwa");
            }
        }
    }
}
        
