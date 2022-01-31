using ConsoleTables;
using DataMigrationUtility.Data;
using DataMigrationUtility.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataMigrationUtility.UI
{
    public class Operation
    {
        public static async Task ApplyMigrations(int start, int end, CancellationTokenSource cancellationToken)
        {

            int total = end - start + 1;
            int temp = start;
            if (!cancellationToken.IsCancellationRequested)
            {
                while (total != 0)
                {
                    var SourceTableData = new List<SourceTable>();

                    try
                    {
                        int tempEnd = (total > 100) ? (temp + 100) : end + 1;
                        var newContext = new DataMigrationUtilityDbContext();
                        SourceTableData = await newContext.SourceTable
                        .Where(x => (x.ID >= temp && x.ID < tempEnd)).ToListAsync();
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("ArgumentNullException :" + ex.Message);
                    }

                    if (total > 100)
                    {
                        temp += 100;
                        total -= 100;
                    }
                    else
                        total = 0;

                    try
                    {
                        Task t = Task.Factory.StartNew(() => AddData(SourceTableData, cancellationToken));
                        await t;
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("ArgumentNullException :" + ex.Message);
                    }
                }
            }
        }

        private static async Task AddData(List<SourceTable> SourceTableData, CancellationTokenSource cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var IdData = SourceTableData.Select(x => x.ID).ToArray();

                var DestinationTableData = new List<DestinationTable>();
                int i = 0;
                foreach (var item in SourceTableData)
                {
                    DestinationTableData.Add(new DestinationTable()
                    {
                        SourceTableID = IdData[i],
                        Sum = await Sum(item.FirstNumber, item.SecondNumber)
                    });
                    i++;
                }
                using (var newContext = new DataMigrationUtilityDbContext())
                {
                    await newContext.DestinationTable.AddRangeAsync(DestinationTableData);
                    newContext.SaveChanges();
                }
            }
        }

        public static MigrationTable CreateNewMigration(int StartNumber, int EndNumber)
        {
            var newMigrationData = new MigrationTable()
            {
                Start = StartNumber,
                End = EndNumber,
                Status = "OnGoing"
            };

            using (var newContext = new DataMigrationUtilityDbContext())
            {
                newContext.MigrationTable.Add(newMigrationData);
                newContext.SaveChanges();
                return newMigrationData;
            }
        }

        public static void ShowStatus()
        {
            var newContext = new DataMigrationUtilityDbContext();
            var MigrataionData = newContext.MigrationTable.ToList();

            var table = new ConsoleTable("Id", "Start", "End", "Status");
            foreach (var status in MigrataionData)
            {
                table.AddRow($"{status.ID}", $"{status.Start}", $"{status.End}", $"{status.Status}");
            }
            Console.WriteLine(table);
        }

        public static async Task<int> Sum(int FirstNumber, int SecondNumber)
        {
            await Task.Delay(50);
            return FirstNumber + SecondNumber;
        }

        public static void TakeValidInputs(out int StartNumber, out int EndNumber)
        {
            Console.Write("Enter Start Number : ");
            var IDAsString = Console.ReadLine();

            while (!int.TryParse(IDAsString, out StartNumber) || StartNumber < 0 || StartNumber > 1000000)
            {
                Console.WriteLine("Enter Valid Number!\n");
                Console.Write("Enter Start Number : ");
                IDAsString = Console.ReadLine();
            }

            Console.Write("Enter End Number : ");
            IDAsString = Console.ReadLine();

            while (!int.TryParse(IDAsString, out EndNumber) || EndNumber < 0 || EndNumber > 1000000)
            {
                Console.WriteLine("Enter Valid Number!\n");
                Console.Write("Enter End Number : ");
                IDAsString = Console.ReadLine();
            }
        }

        public static void UpdateCanceledMigration(int newId)
        {
            using (var newContext = new DataMigrationUtilityDbContext())
            {
                var MigrationData = newContext.MigrationTable.Find(newId);
                MigrationData.Status = "Canceled";
                newContext.MigrationTable.Attach(MigrationData);
                newContext.SaveChanges();
            }
        }

        public static void UpdateCompletedMigration(int StartNumber, int EndNumber, int newId)
        {
            Console.WriteLine($"Migration {StartNumber} to {EndNumber} Completed");

            using (var newContext = new DataMigrationUtilityDbContext())
            {
                var MigrationData = newContext.MigrationTable.Find(newId);
                MigrationData.Status = "Completed";
                newContext.MigrationTable.Attach(MigrationData);
                newContext.SaveChanges();
            }
        }
    }
}