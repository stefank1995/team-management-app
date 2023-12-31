﻿namespace TeamManagementApp.Models
{
	public class KanbanData
	{
		public string Id { get; set; }
		public string Summary { get; set; } = string.Empty;
		public int RankId { get; set; }
		public string AssigneeId { get; set; }
		public string Assignee { get; set; }
		public string? AssignedById { get; set; }
		public string? AssignedBy { get; set; }
		public string Priority { get; set; } = "Low";
		public string Status { get; set; }

		public KanbanData()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
