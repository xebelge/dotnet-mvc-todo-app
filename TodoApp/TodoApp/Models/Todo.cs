using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        public int ID { get; set; }

        [ForeignKey("TodoList")]
        public int ListID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public virtual TodoList TodoList { get; set; }
    }
}