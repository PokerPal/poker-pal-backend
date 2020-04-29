using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

using Persistence;
using Persistence.Interfaces;

namespace Migrations
{
    /// <summary>
    /// Program that offers migration commands.
    /// </summary>
    public class Program
    {
        private static readonly Dictionary<string, Action<string[]>> Commands =
            new Dictionary<string, Action<string[]>>
            {
                ["clear"] = Clear,
                ["migrate"] = Migrate,
                ["check"] = Check,
                ["local"] = Local,
            };

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">String parameters.</param>
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Available commands:");
                foreach (var key in Commands.Keys)
                {
                    var commandArguments =
                        (Commands[key].Method
                            .GetCustomAttributes(typeof(CommandArgumentsAttribute), false)
                            .FirstOrDefault() as CommandArgumentsAttribute)?.Arguments
                        .Select(a => $"[{a}]") ?? new string[0];
                    var commandDescription =
                        (Commands[key].Method
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute)?.Description ?? string.Empty;

                    var commandAndArguments =
                        $"{key} {string.Join(", ", commandArguments)}".PadRight(40, ' ');

                    Console.WriteLine($"{commandAndArguments} {commandDescription}");
                }

                return;
            }

            if (Commands.ContainsKey(args[0]))
            {
                Commands[args[0]](args);
            }
            else
            {
                Console.WriteLine(
                    "Invalid command, run without arguments to see a list of available commands.");
            }
        }

        internal static IDatabaseContextFactory<DatabaseContext> GetDatabaseContextFactory(
            string connectionString)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseContextFactory(options =>
            {
                options.UseConnectionString(connectionString);
            });

            var sp = serviceCollection.BuildServiceProvider();
            return sp.GetService<IDatabaseContextFactory<DatabaseContext>>();
        }

        [Description(
            "Print out all the differences between the local entities and the local migrations. " +
            "If there are any, then a migration is needed.")]
        internal static void Local(string[] args)
        {
            var connectionString = "postgres://none:none@postgres/none";

            var databaseContextFactory = GetDatabaseContextFactory(connectionString);

            using (var context = databaseContextFactory.CreateDatabaseContext())
            {
                var migrationsAssembly = context.GetService<IMigrationsAssembly>();
                var differ = context.GetService<IMigrationsModelDiffer>();
                var generator = context.GetService<IMigrationsSqlGenerator>();

                if (migrationsAssembly.ModelSnapshot == null)
                {
                    Console.WriteLine($"You have got no migrations.");
                    return;
                }

                var differences = differ.GetDifferences(
                    migrationsAssembly.ModelSnapshot.Model,
                    context.Model);

                Console.WriteLine($"There are {differences.Count} differences.");

                if (differences.Count > 0)
                {
                    Console.WriteLine($"You need to create a migration.");

                    var commands = generator.Generate(differences);

                    foreach (var command in commands)
                    {
                        Console.WriteLine(command.CommandText.Trim());
                    }
                }
            }
        }

        [CommandArguments("base 64 connection string")]
        [Description("Connects to the database and checks what migrations have been applied.")]
        internal static void Check(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No connection string specified.");
                return;
            }

            var connectionString = args[1];

            var databaseContextFactory = GetDatabaseContextFactory(connectionString);

            using (var context = databaseContextFactory.CreateDatabaseContext())
            {
                var previouslyAppliedMigrations = context.Database.GetAppliedMigrations();
                Console.WriteLine(
                    $"Previously Applied migrations: ({previouslyAppliedMigrations.Count()})");

                foreach (var m in previouslyAppliedMigrations)
                {
                    Console.WriteLine(m);
                }

                Console.WriteLine();

                var pendingMigrations = context.Database.GetPendingMigrations();
                Console.WriteLine($"Pending migrations: ({pendingMigrations.Count()})");

                foreach (var m in pendingMigrations)
                {
                    Console.WriteLine(m);
                }

                Console.WriteLine();
            }
        }

        [CommandArguments("base 64 connection string")]
        [Description("Connects to the database and applies all pending migrations.")]
        internal static void Migrate(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No connection string specified.");
                return;
            }

            var connectionString = args[1];

            var databaseContextFactory = GetDatabaseContextFactory(connectionString);

            using (var context = databaseContextFactory.CreateDatabaseContext())
            {
                var previouslyAppliedMigrations = context.Database.GetAppliedMigrations();
                Console.WriteLine(
                    $"Previously Applied migrations: ({previouslyAppliedMigrations.Count()})");

                foreach (var m in previouslyAppliedMigrations)
                {
                    Console.WriteLine(m);
                }

                Console.WriteLine();

                var pendingMigrations = context.Database.GetPendingMigrations();
                Console.WriteLine($"Pending migrations: ({pendingMigrations.Count()})");

                foreach (var m in pendingMigrations)
                {
                    Console.WriteLine(m);
                }

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Migrating...");
                context.Database.Migrate();
                Console.WriteLine("Done.");

                var appliedMigrations = context.Database.GetAppliedMigrations();
                Console.WriteLine($"Applied migrations: ({appliedMigrations.Count()})");

                foreach (var m in appliedMigrations)
                {
                    Console.WriteLine(m);
                }

                Console.WriteLine();
            }
        }

        [CommandArguments("base 64 connection string")]
        [Description("Connects to the database and clears all tables and data.")]
        internal static void Clear(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No connection string specified.");
                return;
            }

            var connectionString = args[1];

            var databaseContextFactory = GetDatabaseContextFactory(connectionString);

            using (var context = databaseContextFactory.CreateDatabaseContext())
            {
                Console.WriteLine("Deleting...");
                context.Database.EnsureDeleted();
                Console.WriteLine("Deleted.");
            }
        }
    }
}
