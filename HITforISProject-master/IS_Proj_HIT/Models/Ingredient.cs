using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            MedicineIngredients = new HashSet<MedicineIngredient>();
        }

        public long IngredientId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; }
    }
}
