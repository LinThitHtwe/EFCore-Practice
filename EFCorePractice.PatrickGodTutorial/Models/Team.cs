using System.Text.Json.Serialization;

namespace EFCorePractice.PatrickGodTutorial.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Character> Character { get; set; }
    }
}
