using Microsoft.Extensions.DependencyInjection;
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

                var services = new ServiceCollection();
                services.AddInfra(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
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