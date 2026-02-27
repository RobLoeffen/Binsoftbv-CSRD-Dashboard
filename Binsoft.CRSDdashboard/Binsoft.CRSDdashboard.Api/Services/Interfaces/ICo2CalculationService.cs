namespace Binsoft.CRSDdashboard.Api.Services.Interfaces
{
    public interface ICo2CalculationService
    {
        Task<double> CalculateCo2EquivalentAsync(string material, double weightInKg);
    }
}
