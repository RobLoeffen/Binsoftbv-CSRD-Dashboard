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
        private readonly Mock<IMaterialRepository> _mockMaterialRepository;
        private readonly Co2CalculationService _sut;

        public Co2CalculationServiceTest()
        {
            _mockMaterialRepository = new Mock<IMaterialRepository>();
            _sut = new Co2CalculationService(_mockMaterialRepository.Object);
        }

        #region CalculateCo2EquivalentAsync Tests
        [Fact]
        public async Task CalculateCo2EquivalentAsync_WhenEmissionFactorExists_ReturnsTotalCo2()
        {
            var material = TestDataFactory.CreateMaterial();
            double weightInKg = 10.0;
            double expectedTotalCo2 = weightInKg * material.Co2PerKg;

            _mockMaterialRepository
                .Setup(r => r.GetByNameAsync(material.Name))
                .ReturnsAsync(material);

            var result = await _sut.CalculateCo2EquivalentAsync(material.Name, weightInKg);

            result.Should().BeApproximately(expectedTotalCo2, 0.001);
        }

        [Fact]
        public async Task CalculateCo2EquivalentAsync_WhenNoEmissionFactorExists_ReturnApprioriateError()
        {
            string material = "UnknownMaterial";
            double weightInKg = 10.0;

            _mockMaterialRepository
                .Setup(r => r.GetByNameAsync(material))
                .ReturnsAsync((Material?)null);

            Func<Task> act = async () => await _sut.CalculateCo2EquivalentAsync(material, weightInKg);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"No material found: {material}");
        }
        #endregion
    }
}
