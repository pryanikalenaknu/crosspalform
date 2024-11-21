using System.Text.Json.Serialization;

namespace Lab5.Models;

public sealed record ApiResponseListModel<T>(
    [property: JsonPropertyName("$values")] List<T> Result
);