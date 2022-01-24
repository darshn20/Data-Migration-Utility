using System;
using System.Threading;
using System.Threading.Tasks;
using DataMigrationUtility.Data;
using DataMigrationUtility.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using ConsoleTables;

namespace DataMigrationUtility.UI
{
    class Program : Operation
    {
        static async Task Main()
        {
            Console.WriteLine("WELCOME to Data Migration Utility App");
            Console.WriteLine("----------------------------------------------------");
            
            ConsoleKeyInfo Operation;
            do
            {
                var cancellationToken = new CancellationTokenSource();
                var token = cancellationToken.Token;

                int StartNumber, EndNumber;
                Console.WriteLine("Enter Start number and End number for migratation\n");
                int i = 0;
                do
                {
                    if (i != 0)
                    {
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("End Number must be greater than Start Number");
                        Console.WriteLine("----------------------------------------------------");
                    }
                    TakeValidInputs(out StartNumber, out EndNumber);
                    i++;
                }
                while (StartNumber > EndNumber);

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("\t Type CANCEL for Cancelling migration");
                Console.WriteLine("\t Type STATUS for Information about migrations");
                Console.WriteLine("----------------------------------------------------");

                MigrationTable newMigrationData = CreateNewMigration(StartNumber, EndNumber);

                int newId = newMigrationData.ID;

                Task applyMigration = ApplyMigrations(StartNumber, EndNumber);

                Task CancelStatusTask = Task.Run(() =>
                {
                    while (true)
                    {
                        if (!applyMigration.IsCompleted)
                        {
                            string str = Console.ReadLine();
                            if (str.Equals("cancel",StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("\nMigration Canceled\n");

                                cancellationToken.Cancel();
                                break;
                            }
                            else if (str.Equals("status",StringComparison.OrdinalIgnoreCase))
                            {
                                ShowStatus();
                            }
                        }
                    }
                });

                Task newTask = await Task.WhenAny(applyMigration, CancelStatusTask);

                if (newTask.IsCompleted && token.IsCancellationRequested)
                {
                    UpdateCanceledMigration(newId);
                }
                else
                {
                    UpdateCompletedMigration(StartNumber, EndNumber, newId);
                }

                Console.WriteLine("Press any key to continue");
                Operation = Console.ReadKey();
               

            } while (Operation.Key != ConsoleKey.Escape);

            Console.WriteLine("Application Ended");
            Console.WriteLine("\nTHANK YOU!");
        }
    }
}
