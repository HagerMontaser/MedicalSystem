﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Models
{
    [Table("Record")]
    [Index("PID", Name = "IX_Record_PID")]
    public partial class Record
    {
        [Key]
        public int DID { get; set; }
        [Key]
        public int PID { get; set; }
       
        [StringLength(150)]
        [Unicode(false)]
        public string file_description { get; set; }
        public string attached_files { get; set; }
        [Key]
        public DateTime date { get; set; }
        
        [Unicode(false)]
        public string summary { get; set; }

       
        [Unicode(false)]
        public string prescription { get; set; }

        [Key]
        public int FNO { get; set; }
        public int? OID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Records")]
        public virtual Doctor DIDNavigation { get; set; }
        [ForeignKey("OID")]
        [InverseProperty("Records")]
        public virtual Other OIDNavigation { get; set; }
        [ForeignKey("PID")]
        [InverseProperty("Records")]
        public virtual Patient PIDNavigation { get; set; }
    }
}