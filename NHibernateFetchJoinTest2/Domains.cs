using System;
using System.Collections.Generic;

namespace NHibernateFetchJoinTest2.Domains
{
    public class Document
    {
        public virtual int Id { get; set; }
        public virtual string DocumentDescription { get; set; }
        public virtual IList<Period> Periods { get; set; }
    }

    public class Period
    {
        public virtual Document Document { get; set; }

        public virtual int Id { get; set; }
        public virtual string PeriodDescription { get; set; }
    }
}
