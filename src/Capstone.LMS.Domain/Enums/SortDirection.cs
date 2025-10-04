using System.Text.Json.Serialization;

namespace Capstone.LMS.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
