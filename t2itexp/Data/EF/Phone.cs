using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace t2itexp.Data.EF
{
    public partial class Phone
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string? Value { get; set; }
    }
}