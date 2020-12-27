using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiFKRecExample.Models;

namespace MultiFKRecExample.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List(int patientID = -1)
        {
            ViewData["Patients"] = GetPatientData(patientID: patientID);
            return View();
        }

        public IActionResult Add()
        {
            ViewData["Immunizations"] = ImmunizationsController.GetImmunizations();

            if (Request.Query.Count > 0)
            {
                // Query parameters have been returned - need to process values

                // New Patient's name
                string patientName = Request.Query["patientName"];

                // Create ImmunizationID list that will be used to generate the corresponding ImmunizationRecords
                List<string> selectedImmunizationIDs = Request.Query["immunizationID"].ToList();

                // Check if the patient's name has been provided. Structure can be expanded to include other parameters if required
                List<object> paramsList = new List<object> { patientName };
                if (! paramsList.All(x => x != null))
                {
                    ViewData["error"] = $"Unable to create a patient record. Missing parameter: {(patientName == null ? "Name" : "")} ";
                    ViewData["formData"] = new Patient() { Name = patientName };
                }
                else
                {
                    // Required parameters are all present, create the patient record
                    try
                    {
                        Patient newPatient = CreatePatientRecord(patientName, selectedImmunizationIDs);
                        ViewData["Patients"] = GetPatientData();
                    }
                    catch (Exception ex)
                    {
                        ViewData["error"] = $"An error has occured during database update: {ex.Message} <br> {ex?.InnerException?.Message} ";
                    }
                }
            }

            return View();
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
                    patientData = context.Patients.Include(x => x.ImmunizationRecords).ThenInclude(x => x.ImmunizationData).ToList();
                }
            }
            return patientData;
        }


        public Patient CreatePatientRecord(string name, List<string> immunizationRecordIDs)
        {
            // Create a new Patient object
            Patient newPatient = new Patient() { Name = name };

            using (DataContext context = new DataContext())
            {
                context.Patients.Add(newPatient);

                immunizationRecordIDs.ForEach(x =>
                {
                    // Create a new immunization record bassed on supplied immunizationRecordIDs
                    ImmunizationRecord newImmunizationRecord = new ImmunizationRecord() { ImmunizationID = Int32.Parse(x) };
                    newPatient.ImmunizationRecords.Add(newImmunizationRecord);
                });

                // Return the object if successful.
                if (context.SaveChanges() > 0) return newPatient;

            }

            // If we got this far, we failed somewhere and an error needs to be displayed to the user
            return null;

        }
    }
}
