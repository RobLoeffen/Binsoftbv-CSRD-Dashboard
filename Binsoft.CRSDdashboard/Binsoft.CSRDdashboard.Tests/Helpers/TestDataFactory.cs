using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Models.Responses;

namespace Binsoft.CSRDdashboard.Tests.Helpers
{
    public static class TestDataFactory
    {
        public static Ecopart CreateEcopart(
            int id = 1,
            string name = "Plastic plate",
            string material = "PET",
            ShapeType shapeType = ShapeType.RectangularPrism,
            double length = 10.0,
            double? width = 5.0,
            double? height = 2.0,
            double? radius = null,
            DateTime? createdAt = null)
        {
            return new Ecopart
            {
                Id = id,
                Name = name,
                ShapeType = shapeType,
                MaterialId = 1,
                Material = new Material { Id = 1, Name = material, Co2PerKg = 2.0, Density = 1.38 },
                Length = length,
                Width = width,
                Height = height,
                Radius = radius,
                CreatedAt = createdAt ?? DateTime.UtcNow
            };
        }

        public static EcopartResponse CreateEcopartResponse(
            int id = 1,
            string name = "Plastic plate",
            string material = "PET",
            string shapeType = "RectangularPrism",
            double length = 10.0,
            double? width = 5.0,
            double? height = 2.0,
            double? radius = null,
            double volume = 100.0,
            double mass = 138.0)
        {
            return new EcopartResponse
            {
                Id = id,
                Name = name,
                ShapeType = shapeType,
                Material = new MaterialResponse
                {
                    Id = 1,
                    Name = material,
                    Co2PerKg = 2.0,
                    Density = 1.38
                },
                Length = length,
                Width = width,
                Height = height,
                Radius = radius,
                Volume = volume,
                Mass = mass
            };
        }

        public static EcopartWithEmissionsResponse CreateEcopartWithEmissionsResponse(
            int id = 1,
            string name = "Plastic plate",
            string material = "PET",
            string shapeType = "RectangularPrism",
            double length = 10.0,
            double? width = 5.0,
            double? height = 2.0,
            double? radius = null,
            double volume = 100.0,
            double mass = 138.0,
            double totalCo2Equivalent = 20.0,
            double co2PerKg = 2.0)
        {
            return new EcopartWithEmissionsResponse
            {
                Id = id,
                Name = name,
                ShapeType = shapeType,
                Material = new MaterialResponse
                {
                    Id = 1,
                    Name = material,
                    Co2PerKg = co2PerKg,
                    Density = 1.38
                },
                Length = length,
                Width = width,
                Height = height,
                Radius = radius,
                Volume = volume,
                Mass = mass,
                TotalCo2Equivalent = totalCo2Equivalent
            };
        }

        public static Material CreateMaterial(
            int id = 1,
            string material = "PET",
            double co2PerKg = 2.0,
            double density = 1.38)
        {
            return new Material
            {
                Id = id,
                Name = material,
                Co2PerKg = co2PerKg,
                Density = density
            };
        }

        public static List<Ecopart> CreateEcopartList(int count = 2)
        {
            var ecoparts = new List<Ecopart>();
            for (int i = 1; i <= count; i++)
            {
                ecoparts.Add(CreateEcopart(
                    id: i,
                    name: $"Part{i}",
                    material: i % 2 == 0 ? "PET" : "HDPE"
                ));
            }
            return ecoparts;
        }

        public static List<EcopartResponse> CreateEcopartResponseList(int count = 2)
        {
            var responses = new List<EcopartResponse>();
            for (int i = 1; i <= count; i++)
            {
                responses.Add(CreateEcopartResponse(
                    id: i,
                    name: $"Part{i}",
                    material: i % 2 == 0 ? "PET" : "HDPE"
                ));
            }
            return responses;
        }
    }
}
