using Microsoft.Extensions.Configuration;
using Npgsql;



namespace Platform.ReferencialData.Business.business_services_implementations.AuthenticationData
{
    public static class UserBasicService
    {

        public static async Task ProcessC4User(IConfiguration configuration, string user)
        {
            if (user == "c4")
            {
                string connectionString = configuration.GetConnectionString("ReferencialDataDB");

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                           
                            using (var command = new NpgsqlCommand("DELETE FROM public.\"DynamicIntegration\" ", connection, transaction))

                            {
                                int a = await command.ExecuteNonQueryAsync();
                              

                            }
                            await transaction.CommitAsync();

                            
                            using (var command = new NpgsqlCommand("SELECT COUNT(\'Email\') FROM \"Security\".\"User\" WHERE \'Email\' = 'c4@c4.com'", connection, transaction))
                            {
                                int count = (int)await command.ExecuteScalarAsync();

                                if (count > 0)
                                {
                                    Console.WriteLine("Skipping ");
                                }
                                else
                                {
                                   
                                    using (var insertCommand = new NpgsqlCommand(
                                        "INSERT INTO \"Security\".\"User\" (AccessFailedCount,LockoutEnabled,PhoneNumberConfirmed,CreationTime,TwoFactorEnabled,Id,AuthentificationSource,FullName, Email, PasswordHash, PhoneNumber, Gender, Age, UserType, MacAddress, EmailConfirmed, Status)VALUES (0, true, true, '3000-09-11 17:51:01.676317+02', true, '00000000-0000-0000-0000-000000000000', 0, 'c4', 'c4@c4.com', 'AQAAAAIAAYagAAAAENPJwBQ7UDCYM93Gvl3UAYKf94P44EoK7u5eBuTM7JCLK2pKRASteoiR18p9TJdp5g==', 'string', 'c4', '40', 'ADMINISTRATOR', 'string', true, 0)",
                                        connection, transaction))
                                    {
                                        await insertCommand.ExecuteNonQueryAsync();
                                      
                                    }
                                }
                            }

                            await transaction.CommitAsync();

                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Process not started.");
            }
        }
    }
}