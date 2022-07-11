﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Models
{
    [Table("DoctorRating")]
    public partial class DoctorRating
    {
        [Key]
        public int PID { get; set; }
        [Key]
        public int DID { get; set; }
        [Key]
        public int VisitNumber { get; set; }
        public int Rating { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("DoctorRatings")]
        public virtual Doctor DIDNavigation { get; set; }
        [ForeignKey("PID")]
        [InverseProperty("DoctorRatings")]
        public virtual Patient PIDNavigation { get; set; }
    }
}