using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiFKRecExample.Models;

namespace MultiFKRecExample.Controllers
{
    public class ImmunizationsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Immunizations"] = GetImmunizations();
            ViewData["Patients"] = GetPatientData(patientID: 1);
            ViewData["ImmunizationRecords"] = GetImmunizationRecords();

            return View();
        }

        public static List<Immunization> GetImmunizations()
        {
            List<Immunization> allImmunizations;

            using (DataContext context = new DataContext())
            {
                allImmunizations = context.Immunizations.ToList();
            }

            return allImmunizations; 
        }

        public List<ImmunizationRecord> GetImmunizationRecords()
        {
            List<ImmunizationRecord> allImmunizationRecords;


            using (DataContext context = new DataContext())
            {
                allImmunizationRecords = context.ImmunizationRecords.Include(x => x.ImmunizationData).ToList();
            }

            return allImmunizationRecords;

        }

        public List<Patient> GetPatientData(int patientID = -1)
        {
            List<Patient> patientData;
            using (DataContext context = new DataContext())
            {
                if (patientID != -1)
                {
                    patientData = context.Patients.Where(x => x.PatientID == patientID).Include(x => x.ImmunizationRecords).ThenInclude(x => x.ImmunizationData).ToList();
                }
                else
                {
                    patientData = context.Patients.ToList();
                }
            }
            return patientData;
        }
    }
}
