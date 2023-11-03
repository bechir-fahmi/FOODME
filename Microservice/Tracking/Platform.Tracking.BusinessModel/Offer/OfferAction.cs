﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.BusinessModel.Offre
{
    public class OfferAction
    {
        public int Id { get; set; }
        public Guid UserID { get; set; }
        public Guid OffreID { get; set; }
        public DateTime TimeOfAction { get; set; }
        public string? SocialMedia { get; set; }
    }
}
