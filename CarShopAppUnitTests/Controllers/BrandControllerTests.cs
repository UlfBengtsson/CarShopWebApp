using CarShopApp.Controllers;
using CarShopApp.Models;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarShopAppUnitTests.Controllers
{
    public class BrandControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IBrandService> mockBrandService;

        public BrandControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockBrandService = this.mockRepository.Create<IBrandService>();
        }

        private BrandController CreateBrandController()
        {
            return new BrandController(
                this.mockBrandService.Object);
        }

        [Fact]
        public void Index_CallsServiceAndSendsListAsModel_ReturnViewWithCorrectModelData()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            List<Brand> brands = new List<Brand>() { new Brand(), new Brand()};
            mockBrandService.Setup(b => b.GetAll()).Returns(brands);

            // Act
            var result = brandController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelList = Assert.IsType<List<Brand>>(viewResult.Model);
            Assert.Equal(brands.Count, modelList.Count);

            mockBrandService.Verify(b => b.GetAll(),Times.Once);
        }

        [Fact]
        public void Details_ExsistingBrand_ReturnViewWithModelData()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            string brandName = "Opel";
            mockBrandService.Setup(b => b.FindById(It.IsAny<int>())).Returns(new Brand() { Id = id, Name = brandName });  

            // Act
            var result = brandController.Details(
                id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Brand>(viewResult.Model);
            Assert.Equal(id, model.Id);
            Assert.Equal(brandName, model.Name);
            mockBrandService.Verify(b => b.FindById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Details_BrandNotFound_RedirectToIndex()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 999;
            Brand brand = null; 
            mockBrandService.Setup(b => b.FindById(It.IsAny<int>())).Returns(brand);

            // Act
            var result = brandController.Details(
                id);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            mockBrandService.Verify(b => b.FindById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Create_GetRequest_ReturnsView()
        {
            // Arrange
            var brandController = this.CreateBrandController();

            // Act
            var result = brandController.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_PostWithCorrectData_CallsServiceAndRedirectsToIndex()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            mockBrandService.Setup(b => b.Create(It.IsAny<CreateBrandViewModel>())).Returns(new Brand());   
            CreateBrandViewModel createBrandViewModel = new CreateBrandViewModel()
            {
                Name = "Saab"
            };

            // Act
            var result = brandController.Create(
                createBrandViewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            mockBrandService.Verify(b=>b.Create(It.IsAny<CreateBrandViewModel>()), Times.Once);
        }

        [Fact]
        public void Create_PostWithBadData_NotCallServiceAndReturnViewWithModelErrors()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            mockBrandService.Setup(b => b.Create(It.IsAny<CreateBrandViewModel>())).Returns(new Brand());
            string brandName = string.Empty;
            CreateBrandViewModel createBrandViewModel = new CreateBrandViewModel()
            {
                Name = brandName
            };
            //ModelState is not automatic in Unit Test so we must do a manual trigger
            brandController.ModelState.AddModelError("Name", "Name is empty");

            // Act
            var result = brandController.Create(
                createBrandViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelResult = Assert.IsType<CreateBrandViewModel>(viewResult.Model);
            Assert.Equal(brandName, modelResult.Name);
            mockBrandService.Verify(b => b.Create(It.IsAny<CreateBrandViewModel>()), Times.Never);
        }

        [Fact]
        public void Edit_GetEditView_ReturnViewWithModel()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            string brandName = "Opel";
            mockBrandService.Setup(b => b.FindById(It.IsAny<int>())).Returns(new Brand() { Id = id, Name = brandName });

            // Act
            var result = brandController.Edit(
                id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CreateBrandViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewData.ContainsKey("BrandId"));
            Assert.Equal(id,viewResult.ViewData["BrandId"]);
            Assert.Equal(brandName, model.Name);
            mockBrandService.Verify(b => b.FindById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Edit_BrandNotFound_RedirectToIndex()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            Brand brand = null;
            mockBrandService.Setup(b => b.FindById(It.IsAny<int>())).Returns(brand);

            // Act
            var result = brandController.Edit(
                id);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            mockBrandService.Verify(b => b.FindById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Edit_PostWithCorrectData_RedirectToIndex()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            string editBrandName = "Lexus";
            mockBrandService.Setup(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>())).Returns(true);
            CreateBrandViewModel brandViewModel = new CreateBrandViewModel()
            {
                Name = editBrandName
            };

            // Act
            var result = brandController.Edit(
                id,
                brandViewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            mockBrandService.Verify(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>()), Times.Once);
        }

        [Fact]
        public void Edit_PostWithFailService_ReturnViewWithModel()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            string editBrandName = String.Empty;
            mockBrandService.Setup(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>())).Returns(false);
            CreateBrandViewModel brandViewModel = new CreateBrandViewModel()
            {
                Name = editBrandName
            };

            // Act
            var result = brandController.Edit(
                id,
                brandViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CreateBrandViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewData.ModelState.ContainsKey("Storage"));
            Assert.True(viewResult.ViewData.ContainsKey("BrandId"));
            Assert.Equal(id, viewResult.ViewData["BrandId"]);
            Assert.Equal(editBrandName, model.Name);

            mockBrandService.Verify(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>()), Times.Once);
        }

        [Fact]
        public void Edit_PostWithBadModelState_ReturnViewWithModel()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 666;
            string editBrandName = String.Empty;
            mockBrandService.Setup(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>())).Returns(false);
            CreateBrandViewModel brandViewModel = new CreateBrandViewModel()
            {
                Name = editBrandName
            };

            brandController.ModelState.AddModelError("Name", "Bad name");

            // Act
            var result = brandController.Edit(
                id,
                brandViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CreateBrandViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewData.ModelState.ContainsKey("Name"));
            Assert.True(viewResult.ViewData.ContainsKey("BrandId"));
            Assert.Equal(id, viewResult.ViewData["BrandId"]);
            Assert.Equal(editBrandName, model.Name);

            mockBrandService.Verify(b => b.Edit(It.IsAny<int>(), It.IsAny<CreateBrandViewModel>()), Times.Never);
        }

        [Fact]
        public void Delete_CallsRemoveService_RedirectToIndex()
        {
            // Arrange
            var brandController = this.CreateBrandController();
            int id = 1;
            mockBrandService.Setup(b => b.Remove(It.IsAny<int>())).Returns(true);

            // Act
            var result = brandController.Delete(
                id);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            mockBrandService.Verify(b => b.Remove(It.IsAny<int>()), Times.Once);
        }


    }
}
