using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("GaiaApi");
    }

    [BindProperty]
    public double FieldA { get; set; }

    [BindProperty]
    public double FieldB { get; set; }

    [BindProperty]
    public string Operation { get; set; } = "add";

    public string? Result { get; set; }
    public List<OperationHistoryItem> HistoryItems { get; set; } = new();

    [BindProperty]
    public int OperationCount { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        var request = new { FieldA, FieldB, Operation };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("api/operations/calculate", content);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            Result = doc.RootElement.GetProperty("result").ToString();
        }

        // Get the operation history
        var historyResponse = await _httpClient.GetAsync($"api/operations/history/{Operation}?take=3");
        if (historyResponse.IsSuccessStatusCode)
        {
            var historyJson = await historyResponse.Content.ReadAsStringAsync();
            HistoryItems = JsonSerializer.Deserialize<List<OperationHistoryItem>>(historyJson) ?? new();
        }


        // Get the operation count for the current month
        var countResponse = await _httpClient.GetAsync($"api/operations/count/{Operation}");
        if (countResponse.IsSuccessStatusCode)
        {
            var countJson = await countResponse.Content.ReadAsStringAsync();
            var countObject = JsonSerializer.Deserialize<Dictionary<string, int>>(countJson);
            OperationCount = (countObject?["count"]).GetValueOrDefault();
        }

        return Page();
    }

    public class OperationHistoryItem
    {

        public int Id { get; set; }
        public double FieldA { get; set; }
        public double FieldB { get; set; }
        public string Operation { get; set; } = "";
        public string Result { get; set; } = "";
        public DateTime Timestamp { get; set; }

    }
}
