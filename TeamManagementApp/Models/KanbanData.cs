namespace TeamManagementApp.Models
{
    public class KanbanData
    {
        public int Id { get; set; }
        public string? Summary { get; set; }
        public int RankId { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
    }
}
