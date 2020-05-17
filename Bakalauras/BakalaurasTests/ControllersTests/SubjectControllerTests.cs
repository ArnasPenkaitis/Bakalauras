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
    public class SubjectControllerTests
    {
        [Fact]
        public void GetEntitiesReturnsCalled()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.GetEntities(It.IsAny<int>()));

            //Action
            var controller = new SubjectController(mService.Object);
            controller.GetEntities(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.GetEntities(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void GetEntityByIdReturnsCalled()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.GetEntityById(It.IsAny<int>()));

            //Action
            var controller = new SubjectController(mService.Object);
            controller.GetEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.GetEntityById(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void DeleteEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            //Action
            var controller = new SubjectController(mService.Object);
            controller.DeleteEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.DeleteEntity(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void CreateEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.CreateEntity(It.IsAny<int>(),It.IsAny<SubjectDTO>()));

            //Action
            var controller = new SubjectController(mService.Object);
            controller.CreateEntity(It.IsAny<int>(),It.IsAny<SubjectDTO>());

            //Assert
            mService.Verify(mock => mock.CreateEntity(It.IsAny<int>(),It.IsAny<SubjectDTO>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<SubjectDTO>()));

            //Action
            var controller = new SubjectController(mService.Object);
            controller.UpdateEntity(It.IsAny<int>(), It.IsAny<SubjectDTO>());

            //Assert
            mService.Verify(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<SubjectDTO>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<SubjectDTO>()));

            //Action
            var controller = new SubjectController(mService.Object);

            //Assert
            Assert.Equal("405", controller.UpdateEntityBadUrl(It.IsAny<SubjectDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void PostEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.CreateEntity(It.IsAny<int>(), It.IsAny<SubjectDTO>()));

            //Action
            var controller = new SubjectController(mService.Object);

            //Assert
            Assert.Equal("405", controller.PostEntityBadUrl(It.IsAny<SubjectDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void DeleteEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<ISubjectService>();
            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            //Action
            var controller = new SubjectController(mService.Object);

            //Assert
            Assert.Equal("405", controller.DeleteEntityBadUrl().StatusCode.ToString());
        }
    }
}
