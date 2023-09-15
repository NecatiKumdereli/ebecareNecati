using Core.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PersonalIntroductionForm : BaseEntity
    {
        public int UserId { get; set; }
        public string QuestionnaireNumber { get; set; }
        public DateTime QuestionnaireDate { get; set; }
        public string Age { get; set; }
        public string EducationStatus { get; set; }
        public string WorkingStatus { get; set; }
        public string Work { get; set; }
        public string SocialSecurity { get; set; }
        public string IncomeLevel { get; set; }
        public string FamilyType { get; set; }
        public string ChronicDisease { get; set; }
        public string ChronicDiseaseType { get; set; }
        public string PregnancyWeek { get; set; }
        public bool PregnancyPlanning { get; set; }
        public string StatePregnanccy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
    }
}
