//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestEngine.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customtopic
    {
        public int topicid { get; set; }
        public string topicname { get; set; }
        public string topicdesc { get; set; }
        public Nullable<int> examid { get; set; }
        public Nullable<int> sectionid { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> refid { get; set; }
        public Nullable<double> scores { get; set; }
        public Nullable<int> refid1 { get; set; }
        public Nullable<int> mastertopicid { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<int> foreigntopicid { get; set; }
        public Nullable<bool> marge { get; set; }
        public Nullable<bool> merge { get; set; }
        public string customtopicdesc { get; set; }
    }
}
