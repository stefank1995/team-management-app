namespace TeamManagementApp.Models
{
    public class Card<KanbanData>
    {
        public object key { get; set; }

        public KanbanData value { get; set; }
    }
}
