using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Common.FinanceData
{
    public record CollectApiResponse<T>(
         [property: JsonPropertyName("success")] bool Success,
         [property: JsonPropertyName("result")] T Result
    );

    public record GoldResult(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("selling")] decimal Selling
    );

    public record SilverResult(
        [property: JsonPropertyName("selling")] decimal Selling
    );

    public record CurrencyItem(
       [property: JsonPropertyName("name")] string Name,
       [property: JsonPropertyName("selling")] decimal Selling
     );
}
