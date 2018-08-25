namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Part
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public string Stock_Um { get; set; }

        public string Primary_Whs_Id { get; set; }
            
        public string Primary_Loc_Id { get; set; }

        public decimal Unit_Material_Cost { get; set; }

        public DateTime STATUS_EFF_DATE { get; set; }   

    }
}
