using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiFKRecExample.Models
{
    // This table links the data from Patients table to data in Immunizations table. Each Patient can have multiple immunization records, with each immunization
    // corresponding to a value in the Immunizations table.
    [Table("ImmunizationRecords")]
    public class ImmunizationRecord
    {
        [Key]
        [Column("RecordID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientImmunizationRecordID { get; set; }

        [Column("PatientID", TypeName = "int")]
        [Required]
        public int PatientID { get; set; }

        [Column("ImmunizationID", TypeName = "int")]
        [Required]
        public int ImmunizationID { get; set; }

        [ForeignKey(nameof(ImmunizationID))]
        public virtual Immunization ImmunizationData { get; set; }

        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patient.ImmunizationRecords))]
        public virtual Patient PatientData { get; set; }

        public ImmunizationRecord() { }
    }
}
