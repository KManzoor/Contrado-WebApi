using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entites
{
    public class ProductManagement
    {
        public int? ProductID { get; set; }
        public int? CategoryID { get; set; }
        public string PName { get; set; }
        public string PDesc { get; set; }
        public string PCategoryName { get; set; }
    }

    public class ProductAttribute
    {
        public int AttributeId { get; set; }
        public int ProductID { get; set; }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}