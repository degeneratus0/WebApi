using System;
using System.Collections.Generic;

namespace WebApi
{
    public partial class Reftable
    {
        public Reftable()
        {
            Maintable = new HashSet<Maintable>();
        }

        public int IdrefTable { get; set; }
        public string Reference { get; set; }

        public virtual ICollection<Maintable> Maintable { get; set; }
    }
}
