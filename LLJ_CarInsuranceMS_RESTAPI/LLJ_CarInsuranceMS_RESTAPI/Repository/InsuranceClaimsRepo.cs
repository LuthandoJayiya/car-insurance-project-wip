using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;

namespace LLJ_CarInsuranceMS_RESTAPI.Repository
{
    public class InsuranceClaimsRepo
    {
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;

        public InsuranceClaimsRepo(LLJ_CarInsuranceMS_EFDBContext context)
        {
            _context = context;
        }


        public virtual List<InsuranceClaim> GetAllInsuranceClaims()
        {
            List<InsuranceClaim> allInsuranceClaims = _context.InsuranceClaims.ToList();

            return allInsuranceClaims;
        }

        public virtual InsuranceClaim GetInsuranceClaimById(int id)
        {
            var insuranceClaim = _context.InsuranceClaims.Find(id);

            return insuranceClaim;
        }


        public virtual List<InsuranceClaimsDriverVM> GetAllInsuranceClaimsDrivers()
        {
            List<InsuranceClaimsDriverVM> allInsuranceClaimsWithDrivers = new List<InsuranceClaimsDriverVM>();

            var claimDriverQuery =
                (from IClaim in _context.InsuranceClaims
                 join Drivers in _context.Drivers
                 on IClaim.DriverId equals Drivers.DriverId
                 select new
                 {
                     ClaimId = IClaim.ClaimId,
                     AccidentDate = IClaim.AccidentDate,
                     ClaimStatus = IClaim.ClaimStatus,
                     ClaimType = IClaim.ClaimType,
                     ClaimName = IClaim.ClaimName,
                     DriverName = Drivers.DriverFirstName,
                     LicenseNumber = Drivers.LicenseNumber,
                     ContactInfo = Drivers.ContactInfo,
                     AccidentsReported = Drivers.AccidentsReported,
                     RiskProfile = Drivers.RiskProfile
                 }).ToList();

            foreach (var claimDriver in claimDriverQuery)
            {
                allInsuranceClaimsWithDrivers.Add(new InsuranceClaimsDriverVM()
                {
                    ClaimId = claimDriver.ClaimId,
                    AccidentDate = claimDriver.AccidentDate,
                    ClaimStatus = claimDriver.ClaimStatus,
                    ClaimName = claimDriver.ClaimName,
                    DriverName = claimDriver.DriverName,
                    LicenseNumber = claimDriver.LicenseNumber,
                    ContactInfo = claimDriver.ContactInfo,
                    AccidentsReported = claimDriver.AccidentsReported,
                    RiskProfile = claimDriver.RiskProfile
                });
            }
            return allInsuranceClaimsWithDrivers;
        }

        public virtual List<InsuranceClaimsSurveyorVM> GetAllInsuranceClaimsSurveyors()
        {
            List<InsuranceClaimsSurveyorVM> allInsuranceClaimsWithSurveyors = new List<InsuranceClaimsSurveyorVM>();

            var claimSurveyorQuery =
                (from IClaim in _context.InsuranceClaims
                 join Surveyors in _context.Surveyors
                 on IClaim.SurveyorId equals Surveyors.SurveyorId
                 select new
                 {
                     ClaimId = IClaim.ClaimId,
                     AccidentDate = IClaim.AccidentDate,
                     ClaimStatus = IClaim.ClaimStatus,
                     ClaimType = IClaim.ClaimType,
                     ClaimName = IClaim.ClaimName,
                     SurveyorName = Surveyors.Name,
                     LicenseNumber = Surveyors.LicenseNumber,
                     ContactInfo = Surveyors.ContactInfo,
                 }).ToList();

            foreach (var claimSurveyor in claimSurveyorQuery)
            {
                allInsuranceClaimsWithSurveyors.Add(new InsuranceClaimsSurveyorVM()
                {
                    ClaimId = claimSurveyor.ClaimId,
                    AccidentDate = claimSurveyor.AccidentDate,
                    ClaimStatus = claimSurveyor.ClaimStatus,
                    ClaimName = claimSurveyor.ClaimName,
                    SurveyorName = claimSurveyor.SurveyorName,
                    LicenseNumber = claimSurveyor.LicenseNumber,
                    ContactInfo = claimSurveyor.ContactInfo,


                });
            }
            return allInsuranceClaimsWithSurveyors;
        }

        public virtual List<InsuranceClaimRepairShopVM> GetAllInsuranceClaimsRepairshops()
        {
            List<InsuranceClaimRepairShopVM> allInsuranceClaimsWithRepairshops = new List<InsuranceClaimRepairShopVM>();
            var claimShopQuery =
                (from IClaim in _context.InsuranceClaims
                 join RShops in _context.RepairShops
                 on IClaim.ShopId equals RShops.ShopId
                 select new
                 {
                     ClaimId = IClaim.ClaimId,
                     AccidentDate = IClaim.AccidentDate,
                     ClaimStatus = IClaim.ClaimStatus,
                     ClaimType = IClaim.ClaimType,
                     ClaimName = IClaim.ClaimName,
                     ShopName = RShops.ShopName,
                     Location = RShops.Location,
                     ContactInfo = RShops.ContactInfo,
                 }).ToList();

            foreach (var claimShop in claimShopQuery)
            {
                allInsuranceClaimsWithRepairshops.Add(new InsuranceClaimRepairShopVM()
                {
                    ClaimId = claimShop.ClaimId,
                    AccidentDate = claimShop.AccidentDate,
                    ClaimStatus = claimShop.ClaimStatus,
                    ClaimName = claimShop.ClaimName,
                    ShopName = claimShop.ShopName,
                    Location = claimShop.Location,
                    ContactInfo = claimShop.ContactInfo,


                });
            }
            return allInsuranceClaimsWithRepairshops;
        }
    }
}
