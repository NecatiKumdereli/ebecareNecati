using DataTransferObject.BabyDevelopment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.BabyDevelopment
{
    public class BabyDevelopmentRules : AbstractValidator<BabyDevelopmentDTO>
    {
        public BabyDevelopmentRules()
        {
            RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Ad Alanı boş geçilemez");
            RuleFor(r => r.Month).NotNull().NotEmpty().WithMessage("Ay Alanı boş geçilemez");
            RuleFor(r => r.Weight).NotNull().NotEmpty().WithMessage("Ağırlık Alanı boş geçilemez");
            RuleFor(r => r.Heigth).NotNull().NotEmpty().WithMessage("Uzunluk alanı Alanı boş geçilemez");
            RuleFor(r => r.Day).NotNull().NotEmpty().WithMessage("Gün Alanı boş geçilemez");
            
        }
    }
}
