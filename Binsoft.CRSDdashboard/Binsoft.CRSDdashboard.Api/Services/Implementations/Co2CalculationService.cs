using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Interfaces;

namespace Binsoft.CRSDdashboard.Api.Services.Implementations
{
    public class Co2CalculationService : ICo2CalculationService
    {
        private readonly IMaterialRepository _materialRepository;

        public Co2CalculationService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<double> CalculateCo2EquivalentAsync(string material, double weightInKg)
        {
            var materialEntity = await _materialRepository.GetByNameAsync(material);

            if (materialEntity == null)
            {
                throw new InvalidOperationException($"No material found: {material}");
            }

            var totalCo2 = weightInKg * materialEntity.Co2PerKg;

            return totalCo2;
        }
    }
}
