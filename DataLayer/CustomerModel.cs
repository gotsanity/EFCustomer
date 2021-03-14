using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataLayer
{
    public class CustomerModel
    {
        [Browsable(false)]
        public int CustomerId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        [Browsable(false)]
        public int? AddressId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
    }
}
