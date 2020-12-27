using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiFKRecExample.Models
{
    // This table relates the ImmunizationID to the ImmunizationDesc, i.e. the name of the specific immunization.
    [Table("Immunizations")]
    public class Immunization
    {
        [Key]
        [Column("ImmunizationID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImmunizationID { get; set; }

        [Column("ImmunizationDesc", TypeName = "varchar(max)")]
        [Required]
        public string ImmunizationDesc { get; set; }

        public Immunization() { }       
        
    }
}
