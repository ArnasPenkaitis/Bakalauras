using System;
using System.Collections.Generic;
using System.Text;
using Bakalauras.Controllers;
using Bakalauras.Services.Interfaces;
using Bakalauras.Modeling.DTO;
using Moq;
using Xunit;

namespace BakalaurasTests.ControllersTests
{
    public class VisualizationControllerTests
    {
        [Fact]
        public void GetEntitiesReturnsCalled()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            var mblob = new Mock<IBlobService>();
            mService.Setup(mock => mock.GetEntities());

            //Action
            var controller = new VisualizationController(mService.Object, mblob.Object);
            controller.GetEntities();

            //Assert
            mService.Verify(mock => mock.GetEntities(), Times.Once);

        }

        [Fact]
        public void GetEntityByIdReturnsCalled()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            var mblob = new Mock<IBlobService>();

            mService.Setup(mock => mock.GetEntityById(It.IsAny<int>()));

            //Action
            var controller = new VisualizationController(mService.Object, mblob.Object);
            controller.GetEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.GetEntityById(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void DeleteEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            var mblob = new Mock<IBlobService>();

            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            //Action
            var controller = new VisualizationController(mService.Object, mblob.Object);
            controller.DeleteEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.DeleteEntity(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async void CreateEntityReturnsCalled()
        {
            //Setup
            var item = new Mock<VisualizationDTO>();
            item.Object.Id = It.IsAny<int>();
            item.Object.content = It.IsAny<string>();
            item.Object.FileUrl = It.IsAny<string>();

            var mService = new Mock<IVisualizationService>();
            var mblob = new Mock<IBlobService>();

            mService.Setup(mock => mock.CreateEntity(item.Object, item.Object.content, It.IsAny<string>()));

            //Action
            var controller = new VisualizationController(mService.Object, mblob.Object);
            await controller.CreateEntity(item.Object, It.IsAny<string>());

            //Assert
            mService.Verify(mock => mock.CreateEntity(item.Object, item.Object.content, It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            var mblob = new Mock<IBlobService>();

            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<VisualizationDTO>()));

            //Action
            var controller = new VisualizationController(mService.Object, mblob.Object);
            controller.UpdateEntity(It.IsAny<int>(), It.IsAny<VisualizationDTO>());

            //Assert
            mService.Verify(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<VisualizationDTO>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<VisualizationDTO>()));

            var blobService = new Mock<IBlobService>();

            //Action
            var controller = new VisualizationController(mService.Object, blobService.Object);

            //Assert
            Assert.Equal("405", controller.UpdateEntityBadUrl(It.IsAny<VisualizationDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void PostEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            mService.Setup(mock => mock.CreateEntity(It.IsAny<VisualizationDTO>(), It.IsAny<string>(), It.IsAny<string>()));

            var blobService = new Mock<IBlobService>();

            //Action
            var controller = new VisualizationController(mService.Object, blobService.Object);

            //Assert
            Assert.Equal("405", controller.PostEntityBadUrl(It.IsAny<VisualizationDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void DeleteEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IVisualizationService>();
            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            var blobService = new Mock<IBlobService>();

            //Action
            var controller = new VisualizationController(mService.Object, blobService.Object);

            //Assert
            Assert.Equal("405", controller.DeleteEntityBadUrl().StatusCode.ToString());
        }
    }
}
