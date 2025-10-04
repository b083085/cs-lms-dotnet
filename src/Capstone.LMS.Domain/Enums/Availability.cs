using System.Text.Json.Serialization;

namespace Capstone.LMS.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Availability
    {
        Available = 0,
        Unavailable = 1
    }
}
