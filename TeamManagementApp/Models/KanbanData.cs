using System.ComponentModel.DataAnnotations;

namespace TeamManagementApp.Models
{
    public class KanbanData
    {
        [Key]
        public string Id { get; set; }
        public string? Summary { get; set; }
        public int RankId { get; set; }
        public string AssigneeId { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }

        public KanbanData()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
