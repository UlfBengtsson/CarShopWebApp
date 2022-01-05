using CarShopApp.Models;
using CarShopApp.Models.Repos;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarShopAppUnitTests.Models.Services
{
    public class CarsServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ICarsRepo> mockCarsRepo;
        private Mock<IBrandRepo> mockBrandRepo;
        private Mock<IInsuranceRepo> mockInsuranceRepo;

        public CarsServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCarsRepo = this.mockRepository.Create<ICarsRepo>();
            this.mockBrandRepo = this.mockRepository.Create<IBrandRepo>();
            this.mockInsuranceRepo = this.mockRepository.Create<IInsuranceRepo>();
        }

        private CarsService CreateService()
        {
            return new CarsService(
                this.mockCarsRepo.Object,
                this.mockBrandRepo.Object,
                this.mockInsuranceRepo.Object);
        }

        [Fact]
        public void Create_CorrectInput_ReturnNewCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.Create(It.IsAny<Car>())).Returns(new Car());
            CreateCarViewModel createCar = new CreateCarViewModel()
            { BrandId = 1, ModelName = "Saab", Price = 123.4 };

            // Act
            var result = service.Create(
                createCar);

            // Assert
            Assert.NotNull(result);
            mockCarsRepo.Verify(c => c.Create(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void GetAll_CallOk_ReturnsListCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.GetAll()).Returns(new List<Car>());

            // Act
            var result = service.GetAll();

            // Assert
            Assert.NotNull(result);
            mockCarsRepo.Verify(c => c.GetAll(), Times.Once);
        }

        [Fact]
        public void FindById_IdFoundCar_ReturnCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(new Car()); 
            int id = 0;

            // Act
            var result = service.FindById(
                id);

            // Assert
            Assert.NotNull(result);
            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void FindById_IdNotFoundCar_ReturnNull()
        {
            // Arrange
            var service = this.CreateService();
            Car car = null;
            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(car);
            int id = 0;

            // Act
            var result = service.FindById(
                id);

            // Assert
            Assert.Null(result);
            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void FindByBrand_ValidBrandName_ReturnListCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.GetByBrand(It.IsAny<string>())).Returns(new List<Car>());
            string brand = "Saab";

            // Act
            var result = service.FindByBrand(
                brand);

            // Assert
            Assert.NotNull(result);
            mockCarsRepo.Verify(c => c.GetByBrand(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void FindByBrand_InvalidBrandName_Null()
        {
            // Arrange
            var service = this.CreateService();
            string brand = null;

            // Act
            var result = service.FindByBrand(
                brand);

            // Assert
            Assert.Null(result);
            mockCarsRepo.Verify(c => c.GetByBrand(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Edit_UpdatesExsistingCar_ReturnUpdatedCar()
        {
            // Arrange
            var service = this.CreateService();
            Car originalCar = new Car() {Id = 1, BrandId=1, ModelName="Saab", Price=123.4 };
            Car returnCar = null;//Car object that is going to be returned from Repo.Edit
            
            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(originalCar);
            mockBrandRepo.Setup(b => b.Read(It.IsAny<int>())).Returns(new Brand());
            mockCarsRepo.Setup(c => c.Update(It.IsAny<Car>())).Callback<Car>(r => returnCar = r);
            
            int id = 1;
            int newBrandId = 2;
            string newModelName = "Volvo";
            double newPrice = 56.7;
            CreateCarViewModel editCar = new CreateCarViewModel()
            {
                BrandId = newBrandId,
                ModelName = newModelName,
                Price = newPrice
            };

            // Act
            service.Edit(
                id,
                editCar);//Edit method is a void so here Moq lets us to get hold of the data sent to Repo.Edit with the CallBack method and our returnCar varible.

            // Assert
            Assert.Equal(newBrandId, returnCar.BrandId);
            Assert.Equal(newModelName, returnCar.ModelName);
            Assert.Equal(newPrice, returnCar.Price);

            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
            mockBrandRepo.Verify(b => b.Read(It.IsAny<int>()), Times.Once);
            mockCarsRepo.Verify(c => c.Update(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void Edit_NoneExsistingCar_NotCallUpdateOnCarRepo()
        {
            // Arrange
            var service = this.CreateService();
            Car originalCar = null;

            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(originalCar);          

            int id = 1;
            int newBrandId = 2;
            string newModelName = "Volvo";
            double newPrice = 56.7;
            CreateCarViewModel editCar = new CreateCarViewModel()
            {
                BrandId = newBrandId,
                ModelName = newModelName,
                Price = newPrice
            };

            // Act
            service.Edit(
                id,
                editCar);

            // Assert

            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
            mockBrandRepo.Verify(b => b.Read(It.IsAny<int>()), Times.Never);
            mockCarsRepo.Verify(c => c.Update(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void Remove_DeleteExsistingCar_CallRepoDelete()
        {
            // Arrange
            var service = this.CreateService();
            Car car = new Car();
            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(car);
            mockCarsRepo.Setup(c => c.Delete(It.IsAny<Car>()));
            int id = 1;

            // Act
            service.Remove(
                id);

            // Assert

            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
            mockCarsRepo.Verify(c => c.Delete(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void Remove_DeleteNoneExsistingCar_NotCallRepoDelete()
        {
            // Arrange
            var service = this.CreateService();
            Car car = null;
            mockCarsRepo.Setup(c => c.GetById(It.IsAny<int>())).Returns(car);
            int id = 1;

            // Act
            service.Remove(
                id);

            // Assert

            mockCarsRepo.Verify(c => c.GetById(It.IsAny<int>()), Times.Once);
            mockCarsRepo.Verify(c => c.Delete(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void LastAdded_GetLastCar_ReturnLastCar()
        {
            // Arrange
            var service = this.CreateService();
            int lastId = 3;
            int lastBrandId = 4;
            string lastBrandName = "Volvo";
            double lastPrice = 56.7;
            List<Car> cars = new List<Car>()
            {
                new Car() {Id = 1, BrandId= 2, ModelName = "Saab", Price = 123.4},
                new Car() {Id = lastId, BrandId= lastBrandId, ModelName = lastBrandName, Price = lastPrice}
            };
            mockCarsRepo.Setup(c => c.GetAll()).Returns(cars);

            // Act
            var result = service.LastAdded();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lastId, result.Id);
            Assert.Equal(lastBrandId, result.BrandId);
            Assert.Equal(lastPrice, result.Price);
            mockCarsRepo.Verify(c => c.GetAll(), Times.Once);
        }

        [Fact]
        public void LastAdded_NoCars_ReturnNull()
        {
            // Arrange
            var service = this.CreateService();
           
            List<Car> cars = new List<Car>();
            mockCarsRepo.Setup(c => c.GetAll()).Returns(cars);

            // Act
            var result = service.LastAdded();

            // Assert
            Assert.Null(result);
            mockCarsRepo.Verify(c => c.GetAll(), Times.Once);
        }

        [Fact]
        public void InsuranceCoverage_CurrentAndPossibleInsurancesInCorrectLists_ReturnViewModelInCorrectState()
        {
            // Arrange
            var service = this.CreateService();
            Insurance insuranceA = new Insurance() { Id = 1};
            Insurance insuranceB = new Insurance() { Id = 2 };
            List<Insurance> insuranceList = new List<Insurance>() { insuranceA, insuranceB };
            mockInsuranceRepo.Setup(c => c.GetAll()).Returns(insuranceList);
            
            Car car = new Car()
            {
                Insurances = new List<CarInsurance>() { new CarInsurance() { InsuranceId = insuranceA.Id } }
            };

            // Act
            var result = service.InsuranceCoverage(
                car);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Car);
            Assert.NotNull(result.CurrentInsurances);
            Assert.NotNull(result.PossibleInsurances);
            Assert.Contains(insuranceA, result.CurrentInsurances);
            Assert.Contains(insuranceB, result.PossibleInsurances);
            Assert.DoesNotContain(insuranceA, result.PossibleInsurances);
            Assert.DoesNotContain(insuranceB, result.CurrentInsurances);

            mockInsuranceRepo.Verify(ins => ins.GetAll(), Times.Once);
        }

        [Fact]
        public void RemoveCarInsurance_RemovessInsuranceFromCar_UpdatesCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.Update(It.IsAny<Car>()));
            mockInsuranceRepo.Setup(ins => ins.Delete(It.IsAny<Insurance>()));
            int insuranceId = 1;
            Car car = new Car() { Insurances = new List<CarInsurance>()
            {
                new CarInsurance() { InsuranceId = 99},
                new CarInsurance() { InsuranceId = insuranceId}
            } };

            // Act
            service.RemoveCarInsurance(
                car,
                insuranceId);

            // Assert

            mockCarsRepo.Verify(c => c.Update(It.IsAny<Car>()), Times.Once);
            mockInsuranceRepo.Verify(ins => ins.Delete(It.IsAny<Insurance>()), Times.Never);
        }

        [Fact]
        public void AddCarInsurance_AddsInsuranceToCar_UpdatesCar()
        {
            // Arrange
            var service = this.CreateService();
            mockCarsRepo.Setup(c => c.Update(It.IsAny<Car>()));
            mockInsuranceRepo.Setup(ins => ins.Create(It.IsAny<Insurance>()));
            int insuranceId = 333;
            Car car = new Car()
            {
                Id = 666,
                Insurances = new List<CarInsurance>()
            {
                new CarInsurance() { InsuranceId = 99},
                new CarInsurance() { InsuranceId = 101}
            }
            };

            // Act
            service.AddCarInsurance(
                car,
                insuranceId);

            // Assert
            mockCarsRepo.Verify(c => c.Update(It.IsAny<Car>()), Times.Once);
            mockInsuranceRepo.Verify(ins => ins.Create(It.IsAny<Insurance>()), Times.Never);
        }
    }
}
