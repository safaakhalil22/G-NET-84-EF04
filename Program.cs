using G_NET_84_EF04.DbContexts;
using G_NET_84_EF04.Models;
using G_NET_84_EF04.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace G_NET_84_EF04
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var dbContext = new AppDbContext();

            var exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintMenu();

                var choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await AddCustomerAsync(dbContext);
                            break;
                        case "2":
                            await OpenAccountAsync(dbContext);
                            break;
                        case "3":
                            await UpdateAccountStatusAsync(dbContext);
                            break;
                        case "4":
                            await RemoveAccountFromCustomerAsync(dbContext);
                            break;
                        case "5":
                            await ListCustomersAsync(dbContext);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            PrintError("Invalid choice. Please enter a number from the menu.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    dbContext.ChangeTracker.Clear();
                    PrintError($"Something went wrong: {ex.Message}");
                }

                if (!exit)
                    Pause();
            }

            await dbContext.Database.CloseConnectionAsync();
            Console.WriteLine("Goodbye!");
        }

        #region Menu Operations

        //#region Add Customer
        //static async Task AddCustomerAsync(AppDbContext dbContext)
        //{
        //    Console.WriteLine("--- Add New Customer ---");

        //    var fullName = ReadText("Full Name", v => v.Length < 3 || v.Length > 150 ? "Full name must be between 3 and 150 characters." : null);

        //    var nationalId = ReadText("National ID", v => Regex.IsMatch(v, @"^\d{10,14}$") ? null : "National ID must be 10 to 14 digits.");
        //    if (await dbContext.Customers.AnyAsync(c => c.NationalId == nationalId))
        //    {
        //        PrintError("A customer with this National ID already exists.");
        //        return;
        //    }

        //    var dateOfBirth = ReadDateOfBirth("Date of Birth");

        //    var email = ReadText("Email", v => Regex.IsMatch(v, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") && v.Length <= 150 ? null : "Please enter a valid email address.");

        //    var phone = ReadText("Phone", v => Regex.IsMatch(v, @"^\+?\d{8,15}$") ? null : "Phone must be 8 to 15 digits (optionally starting with +).");

        //    var address = ReadText("Address", v => v.Length < 5 || v.Length > 250 ? "Address must be between 5 and 250 characters." : null);

        //    var customerType = ReadEnum<CustomerType>("Customer Type:");

        //    var customer = new Customer
        //    {
        //        FullName = fullName,
        //        NationalId = nationalId,
        //        DateOfBirth = dateOfBirth,
        //        Email = email,
        //        PhoneNumber = phone,
        //        Address = address,
        //        CustomerType = customerType
        //    };

        //    dbContext.Customers.Add(customer);
        //    await dbContext.SaveChangesAsync();

        //    Console.WriteLine();
        //    PrintSuccess($"Customer created successfully. CustomerId = {customer.Id}");
        //}
        //#endregion

        //#region Open Account
        //static async Task OpenAccountAsync(AppDbContext dbContext)
        //{
        //    Console.WriteLine("--- Open New Account ---");

        //    var accountNumber = ReadText("Account Number", v => Regex.IsMatch(v, @"^[A-Za-z0-9-]{4,20}$") ? null : "Account number must be 4 to 20 letters, digits or hyphens.");
        //    var accountType = ReadEnum<AccountType>("Account Type:");
        //    var branchCode = ReadText("Branch Code", v => v.Length == 0 || v.Length > 10 ? "Please enter a valid branch code." : null).ToUpperInvariant();
        //    var customerId = ReadInt("Customer Id");
        //    var ownershipType = ReadEnum<OwnershipType>("Ownership Role:");

        //    PrintInfo($"Validating branch '{branchCode}' and customer #{customerId}...");

        //    var branch = await dbContext.Branches.FirstOrDefaultAsync(b => b.Code == branchCode);
        //    if (branch is null)
        //    {
        //        PrintError($"Branch '{branchCode}' does not exist.");
        //        return;
        //    }

        //    var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        //    if (customer is null)
        //    {
        //        PrintError($"Customer #{customerId} does not exist.");
        //        return;
        //    }

        //    var account = await dbContext.Accounts
        //                                 .Include(a => a.CustomerAccounts)
        //                                 .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        //    if (account is null)
        //    {
        //        if (ownershipType == OwnershipType.CoHolder)
        //        {
        //            PrintError("A new account must be opened with a Primary owner first.");
        //            return;
        //        }

        //        account = new Account
        //        {
        //            AccountNumber = accountNumber,
        //            AccountType = accountType,
        //            BranchCode = branch.Code,
        //            CurrentBalance = 0,
        //            OpeningDate = DateOnly.FromDateTime(DateTime.Today)
        //        };

        //        account.CustomerAccounts.Add(new CustomerAccount
        //        {
        //            CustomerId = customer.Id,
        //            OwnershipType = ownershipType,
        //            OwnershipStartDate = DateOnly.FromDateTime(DateTime.Today),
        //            AccountStatus = AccountStatus.Active
        //        });

        //        dbContext.Accounts.Add(account);
        //        await dbContext.SaveChangesAsync();

        //        PrintSuccess($"Account '{accountNumber}' created and linked to customer {customer.Id} as {ownershipType} owner.");
        //        return;
        //    }

        //    if (account.BranchCode != branch.Code || account.AccountType != accountType)
        //    {
        //        PrintError($"Account '{accountNumber}' already exists as a {account.AccountType} account at branch '{account.BranchCode}'.");
        //        return;
        //    }

        //    if (account.CustomerAccounts.Any(ca => ca.CustomerId == customer.Id))
        //    {
        //        PrintError($"Customer #{customer.Id} is already linked to account '{accountNumber}'.");
        //        return;
        //    }

        //    if (ownershipType == OwnershipType.Primary && account.CustomerAccounts.Any(ca => ca.OwnershipType == OwnershipType.Primary))
        //    {
        //        PrintError($"Account '{accountNumber}' already has a Primary owner.");
        //        return;
        //    }

        //    account.CustomerAccounts.Add(new CustomerAccount
        //    {
        //        CustomerId = customer.Id,
        //        OwnershipType = ownershipType,
        //        OwnershipStartDate = DateOnly.FromDateTime(DateTime.Today),
        //        AccountStatus = AccountStatus.Active
        //    });

        //    await dbContext.SaveChangesAsync();

        //    PrintSuccess($"Existing account '{accountNumber}' linked to customer {customer.Id} as {ownershipType} owner.");
        //}
        //#endregion

        //#region Update Account Status
        //static async Task UpdateAccountStatusAsync(AppDbContext dbContext)
        //{
        //    Console.WriteLine("--- Update Account Status ---");

        //    var accountNumber = ReadText("Account Number", v => v.Length == 0 ? "Account number is required." : null);
        //    var customerId = ReadInt("Customer Id");
        //    var newStatus = ReadEnum<AccountStatus>("New Status:");

        //    var customerAccount = await dbContext.CustomerAccounts
        //                                         .FirstOrDefaultAsync(ca => ca.AccountNumber == accountNumber && ca.CustomerId == customerId);

        //    if (customerAccount is null)
        //    {
        //        PrintError($"Customer #{customerId} is not linked to account '{accountNumber}'.");
        //        return;
        //    }

        //    if (customerAccount.AccountStatus == newStatus)
        //    {
        //        PrintInfo($"The account is already {newStatus}. Nothing changed.");
        //        return;
        //    }

        //    customerAccount.AccountStatus = newStatus;
        //    await dbContext.SaveChangesAsync();

        //    PrintSuccess($"Status updated to {newStatus}.");
        //}
        //#endregion

        //#region Remove Account From Customer
        //static async Task RemoveAccountFromCustomerAsync(AppDbContext dbContext)
        //{
        //    Console.WriteLine("--- Remove Account From Customer ---");

        //    var accountNumber = ReadText("Account Number", v => v.Length == 0 ? "Account number is required." : null);
        //    var customerId = ReadInt("Customer Id");

        //    var customerAccount = await dbContext.CustomerAccounts
        //                                         .Include(ca => ca.Account)
        //                                         .FirstOrDefaultAsync(ca => ca.AccountNumber == accountNumber && ca.CustomerId == customerId);

        //    if (customerAccount is null)
        //    {
        //        PrintError($"Customer #{customerId} is not linked to account '{accountNumber}'.");
        //        return;
        //    }

        //    var hasOtherOwners = await dbContext.CustomerAccounts
        //                                        .AnyAsync(ca => ca.AccountNumber == accountNumber && ca.CustomerId != customerId);

        //    var hasTransactions = await dbContext.Transactions
        //                                         .AnyAsync(t => t.AccountNumber == accountNumber);

        //    dbContext.CustomerAccounts.Remove(customerAccount);

        //    var removeAccount = !hasOtherOwners && !hasTransactions;
        //    if (removeAccount)
        //        dbContext.Accounts.Remove(customerAccount.Account);

        //    await dbContext.SaveChangesAsync();

        //    Console.WriteLine();
        //    PrintSuccess("  Ownership link deleted.");

        //    if (removeAccount)
        //        PrintInfo($"      That was the last owner - account '{accountNumber}' was also removed.");
        //    else if (!hasOtherOwners)
        //        PrintInfo($"      That was the last owner - account '{accountNumber}' was kept because it has transaction history.");
        //}
        //#endregion

        //#region List Customers
        //static async Task ListCustomersAsync(AppDbContext dbContext)
        //{
        //    var customers = await dbContext.Customers
        //                                   .Include(c => c.CustomerAccounts)
        //                                   .ThenInclude(ca => ca.Account)
        //                                   .ThenInclude(a => a.Branch)
        //                                   .OrderBy(c => c.Id)
        //                                   .ToListAsync();

        //    Console.WriteLine("--- All Customers ---");
        //    Console.WriteLine();

        //    if (customers.Count == 0)
        //    {
        //        Console.WriteLine("  (no customers)");
        //        return;
        //    }

        //    foreach (var customer in customers)
        //    {
        //        Console.WriteLine($"  #{customer.Id} {customer.FullName} ({customer.CustomerType})");

        //        if (customer.CustomerAccounts.Count == 0)
        //        {
        //            Console.WriteLine("      (no accounts)");
        //            continue;
        //        }

        //        foreach (var customerAccount in customer.CustomerAccounts)
        //        {
        //            var account = customerAccount.Account;
        //            Console.WriteLine($"      {account.AccountNumber,-10} {account.AccountType,-9} Balance: {account.CurrentBalance,12:N2}   {customerAccount.OwnershipType,-8} {customerAccount.AccountStatus,-7} @ {account.Branch.Name}");
        //        }
        //    }
        //}
        //#endregion

        #endregion

        #region Console Helpers
        static void PrintMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    National Bank - Management");
            Console.WriteLine("========================================");
            Console.WriteLine("  1) Add a new Customer");
            Console.WriteLine("  2) Open a new Account for a Customer");
            Console.WriteLine("  3) Update Account Status (Active / Closed)");
            Console.WriteLine("  4) Remove an Account from a Customer");
            Console.WriteLine("  5) List all Customers (with accounts)");
            Console.WriteLine("  0) Exit");
            Console.WriteLine("----------------------------------------");
            Console.Write("  Enter choice: ");
        }

        static string ReadText(string label, Func<string, string?> validate)
        {
            while (true)
            {
                Console.Write($"{label,-15}: ");
                var input = Console.ReadLine()?.Trim() ?? string.Empty;

                var error = validate(input);
                if (error is null)
                    return input;

                PrintError(error);
            }
        }

        static int ReadInt(string label)
        {
            while (true)
            {
                Console.Write($"{label,-15}: ");

                if (int.TryParse(Console.ReadLine()?.Trim(), out var value) && value > 0)
                    return value;

                PrintError("Please enter a valid positive number.");
            }
        }

        static DateOnly ReadDateOfBirth(string label)
        {
            while (true)
            {
                Console.Write($"{label,-15}: (yyyy-MM-dd) ");

                if (!DateOnly.TryParseExact(Console.ReadLine()?.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    PrintError("Invalid date. Please use the format yyyy-MM-dd.");
                    continue;
                }

                if (date.Year < 1900)
                {
                    PrintError("Date of birth is too far in the past.");
                    continue;
                }

                if (date > DateOnly.FromDateTime(DateTime.Today).AddYears(-18))
                {
                    PrintError("Customer must be at least 18 years old.");
                    continue;
                }

                return date;
            }
        }

        static T ReadEnum<T>(string title) where T : struct, Enum
        {
            var values = Enum.GetValues<T>();

            Console.WriteLine(title);
            for (var i = 0; i < values.Length; i++)
                Console.WriteLine($"    {i + 1}) {values[i]}");

            while (true)
            {
                Console.Write("  Choice: ");

                if (int.TryParse(Console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= values.Length)
                    return values[choice - 1];

                PrintError("Invalid choice. Please pick one of the listed numbers.");
            }
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
        }

        static void PrintSuccess(string message) => PrintColored(message, ConsoleColor.Green);

        static void PrintInfo(string message) => PrintColored(message, ConsoleColor.Cyan);

        static void PrintError(string message) => PrintColored(message, ConsoleColor.Red);

        static void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion
    }
}
