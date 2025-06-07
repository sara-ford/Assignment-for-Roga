using System; 
using System.Collections.Generic; 
using System.Globalization; 
using System.IO; 
using System.Linq; 
using CsvHelper; 
using CsvHelper.Configuration; 

namespace DatasetAnalyzer
{
    public class Person 
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public int Age { get; set; }
        public double WeightLbs { get; set; }
        public string Gender { get; set; } 
    }

    class Program 
    {
        static void Main(string[] args) 
        {
            string filePath = "people_dataset.csv";

            try 
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null 
                };

                using var reader = new StreamReader(filePath); 
                using var csv = new CsvReader(reader, config); 
                var records = csv.GetRecords<Person>().ToList(); 

                Console.WriteLine($"Successfully imported {records.Count} records.");

                double averageAge = records.Average(p => p.Age);
                Console.WriteLine($"\nAverage age of all people: {averageAge} years"); 

                int countInWeightRange = records.Count(p => p.WeightLbs >= 120 && p.WeightLbs <= 140); 
                Console.WriteLine($"Number of people weighing between 120 and 140 lbs: {countInWeightRange}"); 

                double averageAgeInWeightRange = records
                    .Where(p => p.WeightLbs >= 120 && p.WeightLbs <= 140)
                    .Average(p => p.Age); 
                Console.WriteLine($"Average age of people weighing between 120 and 140 lbs: {averageAgeInWeightRange} years");

            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine($"Error: The file '{filePath}' was not found."); 
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error reading CSV: {ex.Message}"); 
            }
        }
    }
}