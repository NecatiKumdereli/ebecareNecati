using DataTransferObject.ChangesBody;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.ChangesBody
{
    public class ChangesBodyRules : AbstractValidator<ChangesBodyDTO>
    {
        public ChangesBodyRules()
        {
            RuleFor(r => r.WeekOfPregnancy).NotNull().NotEmpty().WithMessage("Doğum Haftası Alanı boş geçilemez");
            RuleFor(r => r.Menstruation).NotNull().NotEmpty().WithMessage("Adet Alanı boş geçilemez");
            RuleFor(r => r.Description).NotNull().NotEmpty().WithMessage("Açıklama Adı Alanı boş geçilemez");
          
        }
    }
}
