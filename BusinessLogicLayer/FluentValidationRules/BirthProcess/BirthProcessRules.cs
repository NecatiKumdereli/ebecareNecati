using DataTransferObject.BirthProcess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.BirthProcess
{
    public class BirthProcessRules : AbstractValidator<BirthProcessDTO>
    {
        public BirthProcessRules()
        {
            RuleFor(r => r.ProcessName).NotNull().NotEmpty().WithMessage("Doğum hangi tür Yapıldı Alanı boş geçilemez");
            RuleFor(r => r.DateOfProcess).NotNull().NotEmpty().WithMessage("Tarih Alanı boş geçilemez");
            RuleFor(r => r.HospitalName).NotNull().NotEmpty().WithMessage("Hastane Adı Alanı boş geçilemez");
            RuleFor(r => r.DoctorName).NotNull().NotEmpty().WithMessage("Doktor adı alanı Alanı boş geçilemez");
            
        }
    }
}
