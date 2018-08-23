using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Rovecom.TicketConnector.Domain.SIS.SisAccountEntity
{
    [DataContract(Name = "SisApiAccountListResult")]
    public class SisApiAccountListResult
    {
        [DataMember(Name = "result")]
        public List<List<SisAccount>> Result { get; set; }
    }
}
