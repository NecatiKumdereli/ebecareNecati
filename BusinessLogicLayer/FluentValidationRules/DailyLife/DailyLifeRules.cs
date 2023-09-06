using DataTransferObject.DailyLife;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.DailyLife
{
    public class DailyLifeRules : AbstractValidator<DailyLifeDTO>
    {
        public DailyLifeRules()
        {
            RuleFor(r => r.Topic).NotNull().NotEmpty().WithMessage("Konu Alanı boş geçilemez");
            RuleFor(r => r.Tips).NotNull().NotEmpty().WithMessage("İpucu Adı Alanı boş geçilemez");
            RuleFor(r => r.DateCreated).NotNull().NotEmpty().WithMessage("Oluşturulma Tarihi alanı Alanı boş geçilemez");
        }
    }
}
