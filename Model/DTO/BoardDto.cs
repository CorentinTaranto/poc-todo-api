namespace Model.DTO;

public class BoardDto
{
    public string Id { get; set; }

    public string Title { get; set; }

    public bool IsLocked { get; set; }

    public List<TodoDto> Todos { get; set; }
}

