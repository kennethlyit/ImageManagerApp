//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public int ImageID { get; set; }
        public int VendorID { get; set; }
        public int UseCaseID { get; set; }
        public string Notes { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual UseCase UseCase { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
