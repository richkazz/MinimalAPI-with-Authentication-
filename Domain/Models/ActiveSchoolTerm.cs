﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ActiveSchoolTerm
    {
        [Key]
        public int Id { get; set; }

        public int ActiveTerm{get;set;}
    }
}
