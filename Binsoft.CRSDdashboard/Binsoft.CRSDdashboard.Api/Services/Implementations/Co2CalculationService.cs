using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Interfaces;

namespace Binsoft.CRSDdashboard.Api.Services.Implementations
{
    public class Co2CalculationService : ICo2CalculationService
    {
        private readonly IEmissionFactorRepository _emissionFactorRepository;

        public Co2CalculationService(IEmissionFactorRepository emissionFactorRepository)
        {
            _emissionFactorRepository = emissionFactorRepository;
        }

        public async Task<(double totalCo2, double co2PerKg)> CalculateCo2EquivalentAsync(string material, double weightInKg)
        {
            var emissionFactor = await _emissionFactorRepository.GetByMaterialAsync(material);

            if (emissionFactor == null)
            {
                throw new InvalidOperationException($"No emission factor found for material: {material}");
            }

            var totalCo2 = weightInKg * emissionFactor.Co2PerKg;

            return (totalCo2, emissionFactor.Co2PerKg);
        }
    }
}
