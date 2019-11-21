using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RiskManagement.Model
{
    [DataContract]
    public class Iteration
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public List<Risk> Risks { get; set; }
        [DataMember]
        public int Number { get; set; }
    }
}
