using DjK.WeatherApp.Core.Repository.Abstractions;
using DjK.WeatherApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Tests.Services
{
    [TestFixture]
    public class FavouritiesServiceTests
    {
        Mock<ICityRepository> cityRepositoryFake;


        [SetUp]
        public void SetUp()
        {
            cityRepositoryFake = new Mock<ICityRepository>();
        }

        [Test]
        public async Task Save_favourite_city_to_a_repository()
        {
            // Arrange
            const string testCityName = "Test CityName";
            var sut = CreateFavouritiesService();

            // Act
            await sut.SaveFavouriteCity(testCityName);

            // Assert
            cityRepositoryFake
                .Verify(r => r.SaveFavouriteCity(
                    It.Is<string>(s => s == testCityName)),
                Times.Once);
        }


        [Test]
        public async Task Load_favourite_city_from_a_repository()
        {
            // Arrange
            const string testCityName = "Test CityName";
            cityRepositoryFake.Setup(r => r.LoadFavouriteCity()).ReturnsAsync(testCityName);
            var sut = CreateFavouritiesService();

            // Act
            var result = await sut.LoadFavouriteCity();

            Assert.That(result, Is.EqualTo(testCityName));
        }


        private FavouritiesService CreateFavouritiesService() =>
            new(cityRepositoryFake.Object);

    }
}
