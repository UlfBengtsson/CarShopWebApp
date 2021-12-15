using System.Collections.Generic;

namespace CarShopApp.Models.ViewModels
{
    public class InsuranceCoverageViewModel
    {
        public Car Car { get; set; }
        public List<Insurance> CurrentInsurances { get; set; }
        public List<Insurance> PossibleInsurances { get; set; }

        public InsuranceCoverageViewModel()
        {
            CurrentInsurances = new List<Insurance>();
            PossibleInsurances = new List<Insurance>();
        }
    }
}
