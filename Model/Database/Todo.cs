using System.ComponentModel.DataAnnotations.Schema;
using Model.Enums;

namespace Model.Database;

[Table("todo", Schema = "ims")]
public class Todo : Entity
{
    public string Title { get; set; }

    public Nullable<DateTime> Deadline { get; set; }

    public string Description { get; set; }

    public Section Section { get; set; }

    [ForeignKey(nameof(Board))]
    public string BoardId { get; set; }
}

