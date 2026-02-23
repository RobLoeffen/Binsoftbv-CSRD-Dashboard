namespace Binsoft.CRSDdashboard.Api.Services.Interfaces
{
    public interface ICo2CalculationService
    {
        Task<(double totalCo2, double co2PerKg)> CalculateCo2EquivalentAsync(string material, double weightInKg);
    }
}
