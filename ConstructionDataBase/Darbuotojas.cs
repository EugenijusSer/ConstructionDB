//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConstructionDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Darbuotojas
    {
        public long AK { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string Tel_nr { get; set; }
        public int Alga { get; set; }
        public Nullable<int> Statybviete { get; set; }
    
        public virtual Statybviete StatybvietesID { get; set; }
        public virtual Priziuretojas PriziuretojoAK { get; set; }
        public virtual Statybininkas StatybininkoAK { get; set; }
    }
}