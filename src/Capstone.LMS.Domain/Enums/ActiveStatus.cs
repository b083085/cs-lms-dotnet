using System.Text.Json.Serialization;

namespace Capstone.LMS.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActiveStatus
    {
        Active = 0,
        Suspended = 1
    }
}
