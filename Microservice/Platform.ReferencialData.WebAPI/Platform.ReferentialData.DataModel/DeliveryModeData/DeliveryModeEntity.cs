using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.DeliveryModeData
{
    [Table("DeliveryMode")]
    public class DeliveryModeEntity : ReferentialDataBase
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
    }
}
