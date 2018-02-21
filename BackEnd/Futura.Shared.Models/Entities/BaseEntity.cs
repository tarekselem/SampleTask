using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }
    }
}
