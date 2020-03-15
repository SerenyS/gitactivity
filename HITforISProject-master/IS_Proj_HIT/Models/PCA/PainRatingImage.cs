using System;
using System.Collections;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public class PainRatingImage
    {
        public int PainRatingId { get; set; }
        public string Image { get; set; }
        public DateTime LastModified { get; set; }

        public virtual PainRating PainRating { get; set; }
    }
}