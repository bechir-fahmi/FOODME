using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel;

    public class VendorGeneralInformationDTO
    {
    [ProtoMember(1)]
    public Guid VendorId { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public string Logo { get; set; }
    [ProtoMember(4)]
    public string AndLink { get; set; }
    public string IOSLink { get; set; }
    public string WebLink { get; set; }
}

