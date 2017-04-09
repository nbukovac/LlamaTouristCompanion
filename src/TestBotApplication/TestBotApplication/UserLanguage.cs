namespace TestBotApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLanguage")]
    public partial class UserLanguage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        public int LanguageId { get; set; }

        public virtual Languages Languages { get; set; }
    }
}
