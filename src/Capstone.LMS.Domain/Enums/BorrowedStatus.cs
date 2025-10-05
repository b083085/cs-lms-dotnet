using System.Text.Json.Serialization;

namespace Capstone.LMS.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BorrowedStatus
    {
        Pending = 0,
        Borrowed = 1,
        Returned = 2,
        Overdue = 3
    }
}
