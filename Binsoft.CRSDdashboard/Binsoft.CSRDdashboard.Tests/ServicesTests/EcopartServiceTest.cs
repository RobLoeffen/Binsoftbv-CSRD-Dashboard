using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Mappers;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Implementations;
using Binsoft.CRSDdashboard.Api.Services.Interfaces;
using FluentAssertions;
using Moq;
using Binsoft.CSRDdashboard.Tests.Helpers;

namespace Binsoft.CRSDdashboard.Api.Tests.Services
{
    public class EcopartServiceTests
    {
        private readonly Mock<IEcopartRepository> _mockEcopartRepository;
        private readonly Mock<ICo2CalculationService> _mockCo2CalculationService;
        private readonly Mock<IEcopartMapper> _mockMapper;
        private readonly EcopartService _sut;

        public EcopartServiceTests()
        {
            _mockEcopartRepository = new Mock<IEcopartRepository>();
            _mockCo2CalculationService = new Mock<ICo2CalculationService>();
            _mockMapper = new Mock<IEcopartMapper>();

            _sut = new EcopartService(
                _mockEcopartRepository.Object,
                _mockCo2CalculationService.Object,
                _mockMapper.Object);
        }

        #region GetAllEcopartsAsync Tests

        [Fact]
        public async Task GetAllEcopartsAsync_WhenEcopartsExist_ReturnsEcopartResponses()
        {
            var ecoparts = TestDataFactory.CreateEcopartList(count: 2);

            var expectedResponses = TestDataFactory.CreateEcopartResponseList(count: 2);

            _mockEcopartRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(ecoparts);

            _mockMapper
                .Setup(m => m.ToResponse(It.IsAny<Ecopart>()))
                .Returns((Ecopart e) => expectedResponses.First(r => r.Id == e.Id));

            var result = await _sut.GetAllEcopartsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedResponses);
            _mockEcopartRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllEcopartsAsync_WhenNoEcopartsExist_ReturnsEmptyCollection()
        {
            _mockEcopartRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Ecopart>());

            var result = await _sut.GetAllEcopartsAsync();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion

        #region GetEcopartByIdAsync Tests

        [Fact]
        public async Task GetEcopartByIdAsync_WhenEcopartExists_ReturnsEcopartResponse()
        {
            int ecopartId = 1;
            var ecopart = TestDataFactory.CreateEcopart(id:ecopartId);
            var expectedResponse = TestDataFactory.CreateEcopartResponse(id: ecopartId);

            _mockEcopartRepository
                .Setup(r => r.GetByIdAsync(ecopartId))
                .ReturnsAsync(ecopart);

            _mockMapper
                .Setup(m => m.ToResponse(ecopart))
                .Returns(expectedResponse);

            var result = await _sut.GetEcopartByIdAsync(ecopartId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResponse);
            _mockEcopartRepository.Verify(r => r.GetByIdAsync(ecopartId), Times.Once);
        }

        [Fact]
        public async Task GetEcopartByIdAsync_WhenEcopartDoesNotExist_ReturnsNull()
        {
            int ecopartId = 999;

            _mockEcopartRepository
                .Setup(r => r.GetByIdAsync(ecopartId))
                .ReturnsAsync((Ecopart?)null);

            var result = await _sut.GetEcopartByIdAsync(ecopartId);

            result.Should().BeNull();
            _mockMapper.Verify(m => m.ToResponse(It.IsAny<Ecopart>()), Times.Never);
        }

        #endregion

        #region GetEcopartWithEmissionsAsync Tests

        [Fact]
        public async Task GetEcopartWithEmissionsAsync_WhenEcopartExists_ReturnsEcopartWithEmissions()
        {
            int ecopartId = 1;
            double totalCo2 = 21.0;
            double co2PerKg = 2.0;

            var ecopart = TestDataFactory.CreateEcopart(
                id: ecopartId,
                weight: 10.5);

            var expectedResponse = TestDataFactory.CreateEcopartWithEmissionsResponse(
                id: ecopartId,
                totalCo2Equivalent: totalCo2,
                co2PerKg: co2PerKg);

            _mockEcopartRepository
                .Setup(r => r.GetByIdAsync(ecopartId))
                .ReturnsAsync(ecopart);

            _mockCo2CalculationService
                .Setup(s => s.CalculateCo2EquivalentAsync(ecopart.Material, ecopart.Weight))
                .ReturnsAsync((totalCo2, co2PerKg));

            _mockMapper
                .Setup(m => m.ToResponseWithEmissions(ecopart, totalCo2, co2PerKg))
                .Returns(expectedResponse);

            var result = await _sut.GetEcopartWithEmissionsAsync(ecopartId);

            result.Should().NotBeNull();
            result.TotalCo2Equivalent.Should().Be(totalCo2);
            result.Co2PerKg.Should().Be(co2PerKg);
            _mockCo2CalculationService.Verify(
                s => s.CalculateCo2EquivalentAsync(ecopart.Material, ecopart.Weight),
                Times.Once);
        }

        [Fact]
        public async Task GetEcopartWithEmissionsAsync_WhenEcopartDoesNotExist_ReturnsNull()
        {
            int ecopartId = 999;
            _mockEcopartRepository
                .Setup(r => r.GetByIdAsync(ecopartId))
                .ReturnsAsync((Ecopart?)null);

            var result = await _sut.GetEcopartWithEmissionsAsync(ecopartId);

            result.Should().BeNull();
            _mockCo2CalculationService.Verify(
                s => s.CalculateCo2EquivalentAsync(It.IsAny<string>(), It.IsAny<double>()),
                Times.Never);
        }

        [Fact]
        public async Task GetEcopartWithEmissionsAsync_WhenCalculationFails_ThrowsException()
        {
            int ecopartId = 1;
            var ecopart = TestDataFactory.CreateEcopart(
                id: ecopartId,
                material: "UnknownMaterial",
                weight: 10);

            _mockEcopartRepository
                .Setup(r => r.GetByIdAsync(ecopartId))
                .ReturnsAsync(ecopart);

            _mockCo2CalculationService
                .Setup(s => s.CalculateCo2EquivalentAsync(ecopart.Material, ecopart.Weight))
                .ThrowsAsync(new InvalidOperationException("No emission factor found"));

            Func<Task> act = async () => await _sut.GetEcopartWithEmissionsAsync(ecopartId);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("No emission factor found");
        }

        #endregion
    }
}
