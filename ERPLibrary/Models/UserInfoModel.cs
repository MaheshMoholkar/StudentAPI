using Newtonsoft.Json;

namespace ERPLibrary.Models
{
    public class UserInfoModel
    {
        [JsonProperty(nameof(FirstName))]
        public string? FirstName { get; set; }

        [JsonProperty(nameof(LastName))]
        public string? LastName { get; set; }

        [JsonProperty(nameof(Gender))]
        public string? Gender { get; set; }

        [JsonProperty(nameof(DateOfBirth))]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(nameof(Email))]
        public string? Email { get; set; }

        [JsonProperty(nameof(Address))]
        public string? Address { get; set; }

        [JsonProperty(nameof(City))]
        public string? City { get; set; }

        [JsonProperty(nameof(State))]
        public string? State { get; set; }

        [JsonProperty(nameof(PinCode))]
        public string? PinCode { get; set; }

        [JsonProperty(nameof(PhoneNumber))]
        public string? PhoneNumber { get; set; }

        [JsonProperty(nameof(Designation))]
        public string? Designation { get; set; }

        [JsonProperty(nameof(Department))]
        public string? Department { get; set; }
    }
}
