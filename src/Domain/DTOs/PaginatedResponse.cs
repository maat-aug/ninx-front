using System.Text.Json.Serialization;

namespace NinxERP.Domain.DTOs; // Ou o namespace que você está usando no Front

public class PaginatedResponse<T>
{
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new();

    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; set; } // CORRIGIDO: Nome exato da sua API

    [JsonPropertyName("totalPages")]
    public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageNumber > 1;

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedResponse()
    {
        Data = new List<T>();
    }

    public PaginatedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
}