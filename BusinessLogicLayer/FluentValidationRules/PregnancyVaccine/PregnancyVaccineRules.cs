using DataTransferObject.PregnancyVaccine;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.PregnancyVaccine
{
    public class PregnancyVaccineRules : AbstractValidator<PregnancyVaccineDTO>
    {
        public PregnancyVaccineRules()
        {
            RuleFor(r => r.VaccineName).NotNull().NotEmpty().WithMessage("Aşı adı Alanı boş geçilemez");
            RuleFor(r => r.NurseName).NotNull().NotEmpty().WithMessage("Hemşire Adı Alanı boş geçilemez");
            RuleFor(r => r.RecommendedWeek).NotNull().NotEmpty().WithMessage("Önerilen Hafta Alanı boş geçilemez");
            RuleFor(r => r.DosageInformation).NotNull().NotEmpty().WithMessage("Dozaj Bilgisi Alanı boş geçilemez");
            RuleFor(r => r.DateUpdated).NotNull().NotEmpty().WithMessage("Güncelleme tarihi Alanı boş geçilemez");
        }
    }
}
