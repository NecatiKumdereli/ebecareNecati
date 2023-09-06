using DataTransferObject.FollowUp;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.FollowUp
{
    public class FollowUpRules : AbstractValidator<FollowUpDTO>
    {
        public FollowUpRules()
        {
            RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Ad Alanı boş geçilemez");
            RuleFor(r => r.LaboratoryTests).NotNull().NotEmpty().WithMessage("Laboratuvar Testleri Alanı boş geçilemez");
            RuleFor(r => r.PhysicalExamination).NotNull().NotEmpty().WithMessage("Fiziksel Muayene  Alanı boş geçilemez");
           
        }
    }
}
