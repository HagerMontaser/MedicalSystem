﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace MedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public PatientController(MedicalSystemContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            //using (var ms = new MemoryStream())
            //{
            //    foreach (var pat in patients)
            //        foreach (var record in pat.Records)
            //            if (record.attached_files != null)
            //                using (MemoryStream stream = new MemoryStream())
            //                {
            //                    using (StreamReader streamReader = new StreamReader(stream))
            //                    {
            //                        var x = streamReader.ReadToEnd();
            //                    }
            //                }
                //record.attached_files = Encoding.Default.GetString(record.attached_files);
                // var fileBytes = ms.ToArray();
            //}
            return patients;
        }

        //get patients who need to make lab tests 
        [HttpGet("LabPatients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsForLab()
        {
            var patients = _context.Patients.Where(p => p.Records.Any(p => p.attached_files == null && p.file_description != string.Empty && p.testType == "T")).ToListAsync();
            return await patients;
        }

        //get patients who need to make scans
        [HttpGet("ScanPatients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsForScan()
        {
            var patients = await _context.Patients.Where(p => p.Records.Any(p => p.attached_files == null && p.file_description != string.Empty && p.testType == "S")).ToListAsync();
            return patients;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.ID)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            var patientMail = _context.Patients.Where(a => a.email == patient.email).FirstOrDefault();
            if (patientMail != null)
                return BadRequest("This email already exists !");

            var patientPhone = _context.Patients.Where(a => a.phone == patient.phone).FirstOrDefault();
            if (patientPhone != null)
                return BadRequest("This phone already exists !");

            patient.password = AccountUser.hashPassword(patient.password);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.ID }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.ID == id);
        }
    }
}
