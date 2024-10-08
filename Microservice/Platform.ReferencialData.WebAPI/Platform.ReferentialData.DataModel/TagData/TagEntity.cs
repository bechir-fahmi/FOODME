﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.TagData
{
    [Table("Tag")]
    public class TagEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string value { get; set; }
    }
}
