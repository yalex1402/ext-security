using System;
using System.ComponentModel.DataAnnotations;

namespace secure_lib.Data.Entities
{
    public class GeneralEntity
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}