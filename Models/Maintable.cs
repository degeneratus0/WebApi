using System;
using System.Collections.Generic;

namespace WebApi
{
    public partial class Maintable
    {
        public int IdmainTable { get; set; }
        public string Data { get; set; }
        public int? IdReference { get; set; }

        public virtual Reftable IdReferenceNavigation { get; set; }
    }
}
