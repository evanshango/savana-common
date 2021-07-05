using System;
using System.ComponentModel.DataAnnotations;

namespace Savana.Common.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required] public string Uuid { get; set; }
        [Required] public bool Active { get; set; }
        [Required] public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}