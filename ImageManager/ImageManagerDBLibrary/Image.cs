//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageManagerDBLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public int ImageID { get; set; }
        public int VendorID { get; set; }
        public int UseID { get; set; }
        public string Notes { get; set; }
        public System.DateTime Date { get; set; }
        public string ImageName { get; set; }
    
        public virtual Use Use { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}