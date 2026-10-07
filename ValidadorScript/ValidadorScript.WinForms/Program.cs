using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using ValidadorScript.Infra.Configuracao;
using ValidadorScript.Infra.IoC;

namespace ValidadorScript.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var credenciais = configuration.GetSection("Credenciais").Get<CredenciaisDatabase>()
                    ?? throw new InvalidOperationException("Seção Credenciais não encontrada no appsettings.json.");

                var services = new ServiceCollection();
                services.AddInfra(credenciais);
                services.AddTransient<MainForm>();

                using var provider = services.BuildServiceProvider();
                System.Windows.Forms.Application.Run(provider.GetRequiredService<MainForm>());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível iniciar o Validador de Script.\n\n{ex.Message}",
                    "Validador de Script",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}