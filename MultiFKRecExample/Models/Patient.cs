using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiFKRecExample.Models
{
    public class Patient
    {
        [Key]
        [Column("PatientID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientID { get; set; }

        [Column("Name", TypeName = "varchar(max)")]
        [Required]
        public string Name { get; set; }

        [InverseProperty(nameof(ImmunizationRecord.PatientData))]
        public virtual List<ImmunizationRecord> ImmunizationRecords { get; set; }

        public Patient()
        {
           ImmunizationRecords = new List<ImmunizationRecord>();
        }

    }
}
