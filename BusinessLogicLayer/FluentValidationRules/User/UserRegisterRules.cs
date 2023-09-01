using DataTransferObject.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.FluentValidationRules.User
{
    public class UserRegisterRules : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterRules() 
        {
            RuleFor(p => p.Name).NotNull().WithName("İsim")
                .MinimumLength(5).WithMessage("İsim minimum 5 karakter içermelidir");

            RuleFor(p => p.Surname).NotNull().NotEmpty().WithName("Soyisim");

            //RuleFor(p => p.Password).NotNull().NotEmpty().WithName("Şifre").Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
            //    .WithMessage("Parolanız en az 1 büyük harf ve küçük harf, en az bir sayı ve minimum 8 karakterden oluşmalıdır");
        
        
        }
    }
}
