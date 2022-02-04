using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Database;

[Table("board", Schema = "ims")]
public class Board : Entity
{
    public string Title { get; set; }

    public bool IsLocked { get; set; }

    public virtual List<Todo> Todos { get; set; }
}

