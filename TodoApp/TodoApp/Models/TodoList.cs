using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoApp.Models
{
    public class TodoList
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
