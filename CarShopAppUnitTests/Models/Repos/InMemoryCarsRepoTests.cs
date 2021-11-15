﻿using Xunit;
using CarShopApp.Models.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShopApp.Models.Repos.Tests
{
    public class InMemoryCarsRepoTests
    {
        private ICarsRepo _carsRepo;

        public InMemoryCarsRepoTests()
        {
            _carsRepo = new InMemoryCarsRepo();
        }

        [Fact()]
        public void CreateTest()
        {
            //Arrange
            Car testCar = new Car();
            int testId = int.MaxValue;
            string testModel = "Models 3";
            string testBrand = "Tesla";
            double testPrice = 123.45;
            testCar.Id = testId;
            testCar.Brand = testBrand;
            testCar.ModelName = testModel;
            testCar.Price = testPrice;

            //Act
            Car resultingCar = _carsRepo.Create(testCar);

            //Assert
            Assert.NotNull(resultingCar);
            Assert.NotEqual(testId, resultingCar.Id);//Repo sets its own Id
            Assert.Equal(testBrand, resultingCar.Brand);
            Assert.Equal(testModel, resultingCar.ModelName);
            Assert.Equal(testPrice, resultingCar.Price);
        }

        [Fact()]
        public void GetAllTest()
        {
            //Arrange
            Car testCar1 = new Car() { Brand = "Saab", ModelName = "900", Price = 123.45 };
            Car testCar2 = new Car() { Brand = "Volvo", ModelName = "740", Price = 687.98 };
            Car testCar3 = new Car() { Brand = "BMW", ModelName = "525i", Price = 998.58 };
            testCar1 = _carsRepo.Create(testCar1);
            testCar2 = _carsRepo.Create(testCar2);
            testCar3 = _carsRepo.Create(testCar3);

            //Act
            var resultingList = _carsRepo.GetAll();

            //Assert
            Assert.Contains(testCar1, resultingList);
            Assert.Contains(testCar2, resultingList);
            Assert.Contains(testCar3, resultingList);
        }

        [Theory]
        [InlineData("Opel", "Accord")]
        [InlineData("Golf", "GTi")]
        [InlineData("Seat", "Leon")]
        public void GetByBrandTest(string brand, string modelName)
        {
            //Arrange
            Car testCar = new Car() { Brand = brand, ModelName = modelName, Price = 123.45 };
            Car resultCar = _carsRepo.Create(testCar);

            //Act
            var resultingList = _carsRepo.GetByBrand(brand);

            //Assert
            Assert.Contains(testCar, resultingList);
        }

        [Theory]
        [InlineData("Porsche")]
        [InlineData("Lamborghini")]
        [InlineData("Lotus")]
        public void GetByBrandNotFoundTest(string brand)
        {
            //Arrange - method param

            //Act
            var resultingList = _carsRepo.GetByBrand(brand);

            //Assert
            Assert.Empty(resultingList);
        }

        [Fact()]
        public void GetByIdFoundTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
        [Fact()]
        public void GetByIdNotFoundTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpdateTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DeleteTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DeleteAlreadyDeletedCarTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}