﻿using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ChangesBody : BaseEntity
    {
        public int TrimestirId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
        public Trimestir Trimestir { get; set; }

    }
}
