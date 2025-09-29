namespace DotNetTrip.Utils
{
    public static class WatchDog
    {
        private static readonly List<string> memoryLogs = new();

        public static Action<string> LogActions;

        static WatchDog()
        {
            LogActions += LogToConsole;
            LogActions += LogToFile;
            LogActions += LogToMemory;
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine($"[Console] {message}");
        }

        private static void LogToFile(string message)
        {
            // Monta o caminho absoluto para wwwroot/logs
            string logsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");

            // Se a pasta não existir, cria
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }

            // Caminho completo do arquivo
            string filePath = Path.Combine(logsDir, "logs.txt");

            // Escreve a mensagem
            File.AppendAllText(filePath, $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {message}{Environment.NewLine}");
        }


        private static void LogToMemory(string message)
        {
            memoryLogs.Add($"[Memory] {message}");
        }

        public static IEnumerable<string> GetMemoryLogs()
        {
            return memoryLogs;
        }
    }
}
