﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Body { get; set; }

        //Navigation properties
        public virtual int DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}
