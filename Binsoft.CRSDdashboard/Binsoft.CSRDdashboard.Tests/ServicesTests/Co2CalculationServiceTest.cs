using FluentAssertions;
using Moq;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Implementations;
using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CSRDdashboard.Tests.Helpers;

namespace Binsoft.CSRDdashboard.Api.Tests.ServicesTests
{
    public class Co2CalculationServiceTest
    {
        private readonly Mock<IEmissionFactorRepository> _mockEmissionFactorRepository;
        private readonly Co2CalculationService _sut;

        public Co2CalculationServiceTest()
        {
            _mockEmissionFactorRepository = new Mock<IEmissionFactorRepository>();
            _sut = new Co2CalculationService(_mockEmissionFactorRepository.Object);
        }

        #region CalculateCo2EquivalentAsync Tests
        [Fact]
        public async Task CalculateCo2EquivalentAsync_WhenEmissionFactorExists_ReturnsTotalCo2AndCo2PerKg()
        {
            var EF = TestDataFactory.CreateEmissionFactor();
            double weightInKg = 10.0;
            double expectedTotalCo2 = weightInKg * EF.Co2PerKg;

            object value = _mockEmissionFactorRepository
                .Setup(r => r.GetByMaterialAsync(EF.Material))
                .ReturnsAsync(EF);

            var result = await _sut.CalculateCo2EquivalentAsync(EF.Material, weightInKg);

            result.totalCo2.Should().BeApproximately(expectedTotalCo2, 0.001);
            result.co2PerKg.Should().Be(EF.Co2PerKg);
        }

        [Fact]
        public async Task CalculateCo2EquivalentAsync_WhenNoEmissionFactorExists_ReturnApprioriateError()
        {
            string material = "UnknownMaterial";
            double weightInKg = 10.0;

            _mockEmissionFactorRepository
                .Setup(r => r.GetByMaterialAsync(material))
                .ReturnsAsync((EmissionFactor?)null);

            Func<Task> act = async () => await _sut.CalculateCo2EquivalentAsync(material, weightInKg);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"No emission factor found for material: {material}");
        }
        #endregion
    }
}
