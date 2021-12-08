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
            Brand testBrand = new Brand("Tesla");
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
            Car testCar1 = new Car() { Brand = new Brand("Saab"), ModelName = "900", Price = 123.45 };
            Car testCar2 = new Car() { Brand = new Brand("Volvo"), ModelName = "740", Price = 687.98 };
            Car testCar3 = new Car() { Brand = new Brand("BMW"), ModelName = "525i", Price = 998.58 };
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
            Car testCar = new Car() { Brand = new Brand(brand), ModelName = modelName, Price = 123.45 };
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
            //Arrange
            Car testCar = new Car() { Brand = new Brand("Fiat"), ModelName = "Ducato", Price = 13244.11 };
            testCar = _carsRepo.Create(testCar);

            //Act
            Car foundCar = _carsRepo.GetById(int.MaxValue);

            //Assert
            Assert.Null(foundCar);

        }
        [Fact()]
        public void GetByIdNotFoundTest()
        {
            //Arrange
            Car testCar = new Car() { Brand = new Brand("Fiat"), ModelName = "500", Price = 239.11 };
            testCar = _carsRepo.Create(testCar);
            
            //Act
            Car foundCar = _carsRepo.GetById(testCar.Id);

            //Assert
            Assert.NotNull(foundCar);
            Assert.Equal(testCar.Id, foundCar.Id);
            Assert.Equal(testCar.Brand, foundCar.Brand);
            Assert.Equal(testCar.ModelName, foundCar.ModelName);
            Assert.Equal(testCar.Price, foundCar.Price);
        }

        [Fact()]
        public void UpdateTest()
        {
            //Arrange
            Car testCar = new Car() { Brand = new Brand("Fiat"), ModelName = "Uno", Price = 1.11 };
            testCar = _carsRepo.Create(testCar);
            Car alterCar = new Car() { Id = testCar.Id, Brand = new Brand("Renault "), ModelName = "ZOE", Price = 2399.99 };

            //Act
            _carsRepo.Update(alterCar);
            Car afterUpdate = _carsRepo.GetById(testCar.Id);

            //Assert
            Assert.Equal(alterCar.Id, afterUpdate.Id);
            Assert.Equal(alterCar.Brand, afterUpdate.Brand);
            Assert.Equal(alterCar.ModelName, afterUpdate.ModelName);
            Assert.Equal(alterCar.Price, afterUpdate.Price);
        }

        [Fact()]
        public void DeleteTest()
        {
            //Arrange
            Car testCar1 = new Car() { Brand = new Brand("Saab"), ModelName = "99", Price = 123.45 };
            Car testCar2 = new Car() { Brand = new Brand("Volvo"), ModelName = "240", Price = 687.98 };
            Car testCar3 = new Car() { Brand = new Brand("BMW"), ModelName = "M3", Price = 998.58 };
            testCar1 = _carsRepo.Create(testCar1);
            testCar2 = _carsRepo.Create(testCar2);
            testCar3 = _carsRepo.Create(testCar3);

            //Act
            _carsRepo.Delete(testCar2);
            List<Car> afterList = _carsRepo.GetAll();

            //Assert
            Assert.Contains(testCar1, afterList);
            Assert.Contains(testCar3, afterList);
            Assert.DoesNotContain(testCar2, afterList);
        }

        [Fact()]
        public void DeleteAlreadyDeletedCarTest()
        {
            //Arrange
            Car testCar1 = new Car() { Brand = new Brand("Saab"), ModelName = "93", Price = 123.45 };
            Car testCar2 = new Car() { Brand = new Brand("Volvo"), ModelName = "S40", Price = 687.98 };
            Car testCar3 = new Car() { Brand = new Brand("Opel"), ModelName = "Sprint", Price = 998.58 };
            testCar1 = _carsRepo.Create(testCar1);
            testCar2 = _carsRepo.Create(testCar2);
            testCar3 = _carsRepo.Create(testCar3);

            //Act
            int listCountBefore = _carsRepo.GetAll().Count;
            _carsRepo.Delete(testCar2);
            int listCountAfterDeleteOnce = _carsRepo.GetAll().Count;
            _carsRepo.Delete(testCar2);
            List<Car> afterList = _carsRepo.GetAll();

            //Assert
            Assert.Contains(testCar1, afterList);
            Assert.Contains(testCar3, afterList);
            Assert.DoesNotContain(testCar2, afterList);
            Assert.True(listCountBefore > listCountAfterDeleteOnce);
            Assert.Equal(listCountAfterDeleteOnce, afterList.Count);
        }

        [Fact()]
        public void CanNotDeletCarThatWasNeverAddedTest()
        {
            //Arrange
            Car testCar = new Car() { Brand = new Brand("Ford"), ModelName = "GT40", Price = 1230000.45 };

            //Act
            int listCountBefore = _carsRepo.GetAll().Count;
            _carsRepo.Delete(testCar);
            int listCountAfterDelete = _carsRepo.GetAll().Count;
            List<Car> afterList = _carsRepo.GetAll();

            //Assert
            Assert.DoesNotContain(testCar, afterList);
            Assert.Equal(listCountBefore, listCountAfterDelete);
        }
    }
}