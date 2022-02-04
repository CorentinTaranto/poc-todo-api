using Model.Enums;

namespace Model.DTO;

public class TodoDto
{
    public string Id { get; set; }

    public string Title { get; set; }

    public Nullable<DateTime> Deadline { get; set; }

    public string Description { get; set; }

    public Section Section { get; set; }

    public string BoardId { get; set; }
}

