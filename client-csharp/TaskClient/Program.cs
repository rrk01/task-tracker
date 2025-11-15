using System.Net.Http.Json;

class Program
{
    // Base URL of Spring Boot API
    private static readonly string BaseUrl =
        Environment.GetEnvironmentVariable("TASK_API_BASE")
        ?? "http://localhost:8080";

    static async Task Main()
    {
        using var http = new HttpClient { BaseAddress = new Uri(BaseUrl) };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("== Task Tracker Client ==");
            Console.WriteLine($"API: {BaseUrl}");
            Console.WriteLine();
            Console.WriteLine("1) Check API health");
            Console.WriteLine("9) Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CheckHealth(http);
                    break;

                case "9":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static async Task CheckHealth(HttpClient http)
    {
        Console.Clear();
        Console.WriteLine("Checking /tasks/health ...");
        Console.WriteLine();

        try
        {
            // Hit Spring Boot endpoint
            var response = await http.GetAsync("/tasks/health");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request failed: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
            else
            {
                // read JSON as text and print it
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response:");
                Console.WriteLine(body);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error calling API:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }
}