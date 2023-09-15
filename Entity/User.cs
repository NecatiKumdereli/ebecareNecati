using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User : BaseEntity
    {
        public int RolId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PasswordRefreshToken { get; set; }
        public bool FormStatus { get; set; }
        public string DeviceToken { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Role Role { get; set; }
        public HashSet<ModuleRoles> ModuleRoles { get; set;}
        public HashSet<PregnancyImpression> PregnancyImpressions { get; set;}
        public HashSet<TestVaccineUser> TestVaccines { get; set;}
        public HashSet<MenstrualDate> MenstrualDates { get; set;}
        public HashSet<Communication> Communications { get; set;}
        public HashSet<RiskAssessmentUser> RiskAssessmentUsers { get; set;}
        public HashSet<PrenatalUser> PrenatalUsers { get; set;}
        public HashSet<TilburgUser> TilburgUsers { get; set;}
        public HashSet<UserFollowUp> UserFollowUps { get; set;}
        public HashSet<PersonalIntroducitonForm> PersonalIntroducitonForms { get; set;}
        public HashSet<EvalutionForm> EvalutionForm { get; set;}
        public HashSet<FrequencyOfUsing> FrequencyOfUsings { get; set; }



    }
}
