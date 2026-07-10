namespace MovieManager.BLL.Models
{
    public class MovieActorModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string? CharacterName { get; set; }
        public bool IsLeadRole { get; set; }
        public int DisplayOrder { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string ActorName { get; set; } = string.Empty;
    }
}
