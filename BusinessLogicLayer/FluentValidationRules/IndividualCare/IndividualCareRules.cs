using DataTransferObject.IndividualCare;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.IndividualCare
{
    public class IndividualCareRules : AbstractValidator<IndividualCareDTO>
    {
        public IndividualCareRules()
        {
            RuleFor(r => r.DateCreated).NotNull().NotEmpty().WithMessage("Oluşturulma Tarihi Alanı boş geçilemez");
            RuleFor(r => r.Tips).NotNull().NotEmpty().WithMessage("İpuçları Alanı boş geçilemez");
            RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("Açıklama Alanı boş geçilemez");

        }
    }
}
