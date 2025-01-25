using HtmlAgilityPack;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Ensure console supports Unicode
        string url = "https://www.tv2.dk/";

        try
        {
            HtmlWeb web = new HtmlWeb
            {
                OverrideEncoding = System.Text.Encoding.UTF8 // Ensure UTF-8 encoding
            };
            HtmlDocument document = web.Load(url);

            var headlines = document.DocumentNode
                .SelectNodes("//h2") // Adjust selector as needed
                ?.Select(node => WebUtility.HtmlDecode(node.InnerText.Trim())) // Decode HTML entities
                .Select(NormalizeQuotes) // Normalize quotes
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();

            if (headlines != null && headlines.Any())
            {
                Console.WriteLine("TV2 Headlines:");
                foreach (var headline in headlines)
                {
                    Console.WriteLine($"• {headline}");
                }
            }
            else
            {
                Console.WriteLine("No headlines found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static string NormalizeQuotes(string input)
    {
        return input.Replace('“', '"')
                    .Replace('”', '"')
                    .Replace('‘', '\'')
                    .Replace('’', '\'');
    }
}
