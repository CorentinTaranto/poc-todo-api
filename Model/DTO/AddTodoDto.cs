namespace Model.DTO;

public class AddTodoDto
{
    public string Title { get; set; }

    public Nullable<DateTime> Deadline { get; set; }

    public string Description { get; set; }

    public string BoardId { get; set; }
}

