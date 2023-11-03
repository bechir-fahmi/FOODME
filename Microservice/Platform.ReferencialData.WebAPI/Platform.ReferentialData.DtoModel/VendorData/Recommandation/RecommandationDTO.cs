using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.BrandData.Recommandation
{
    public class RecommandationDTO
    {
            public Guid BrandModelId { get; set; }
            public string BrandName { get; set; }
            public string BrandLogo { get; set; }
            public double BrandScore { get; set; }
            public double RatingMean { get; set; }
            public double DistanceMean { get; set; }

    }
}
