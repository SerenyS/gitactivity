using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineIngredient
    {
        public long MedicineIngredientId { get; set; }
        public long MedicineId { get; set; }
        public long IngredientId { get; set; }
        public bool IsActiveIngredient { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
