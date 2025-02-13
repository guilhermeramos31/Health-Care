using AutoMapper;
using HealthCare.Models.MedicationEntity;
using HealthCare.Models.MedicationEntity.Dto;
using HealthCare.Models.PatientEntity;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class MedicationService(IMapper mapper, IRepositoryUow repositoryUow, IPatientService patientService)
    : IMedicationService
{
    public async Task<MedicationResult> Create(Guid patientId, MedicationRequest medicationRequest)
    {
        var patient = await patientService.GetPatientEntity(patientId);

        var medication = mapper.Map<Medication>(medicationRequest);
        medication.PatientId = patient.Id;
        medication.Patient = patient;
        medication = await repositoryUow.MedicationRepository.Create(medication);
        await repositoryUow.CommitAsync();

        return mapper.Map<MedicationResult>(medication);
    }

    public async Task<MedicationResult> Update(Guid medicationId, MedicationRequest medicationRequest)
    {
        var medication = await repositoryUow.MedicationRepository.GetMedicationById(medicationId) ??
                         throw new BadHttpRequestException("Medication not found.");
        mapper.Map(medicationRequest, medication);
        medication = repositoryUow.MedicationRepository.Update(medication);
        await repositoryUow.CommitAsync();

        return mapper.Map<MedicationResult>(medication);
    }

    public async Task Delete(Guid medicationId)
    {
        var medication = await repositoryUow.MedicationRepository.GetMedicationById(medicationId) ??
                         throw new BadHttpRequestException("Medication not found.");
        repositoryUow.MedicationRepository.Delete(medication);
        await repositoryUow.CommitAsync();
    }

    public async Task<MedicationResult> GetMedicationById(Guid medicationId)
    {
        var medication = await repositoryUow.MedicationRepository.GetMedicationById(medicationId) ??
                         throw new BadHttpRequestException("Medication not found.");

        return mapper.Map<MedicationResult>(medication);
    }

    public async Task<MedicationPageResult> GetAllMedications(Guid patientId, int pageSize, int pageNumber)
    {
        var medications = await repositoryUow.MedicationRepository.GetAllMedications(patientId, pageSize, pageNumber);

        return new()
        {
            MedicationResults = mapper.Map<List<MedicationResult>>(medications),
            TotalCount = medications.Count
        };
    }
}