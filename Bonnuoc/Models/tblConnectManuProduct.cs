using System;
using System.Collections.Generic;

namespace Bonnuoc.Models
{
    public partial class tblConnectManuProduct
    {
        public int id { get; set; }
        public Nullable<int> idManu { get; set; }
        public Nullable<int> idCate { get; set; }
    }
}