using Model.Enums;

namespace Model.DTO;

public class UpdateTodoDto
{
    public string Title { get; set; }

    public Nullable<DateTime> Deadline { get; set; }

    public string Description { get; set; }
}

