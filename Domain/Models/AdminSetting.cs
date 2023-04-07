namespace Domain.Models
{
    public class AdminSetting
    {
        public virtual CurrentGradingSystem? CurrentGradingSystems { get; set; }
        public virtual List<JuniorSchoolSubject>? JuniorSchoolSubjects { get; set; }
        public virtual List<SeniorSchoolSubject>? SeniorSchoolSubjects { get; set; }
    }
}
