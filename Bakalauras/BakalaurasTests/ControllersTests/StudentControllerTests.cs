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
    public class StudentControllerTests
    {
        [Fact]
        public void GetEntitiesReturnsCalled()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.GetEntities());

            //Action
            var controller = new StudentController(mService.Object);
            controller.GetEntities();

            //Assert
            mService.Verify(mock => mock.GetEntities(), Times.Once);

        }

        [Fact]
        public void GetEntityByIdReturnsCalled()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.GetEntityById(It.IsAny<int>()));

            //Action
            var controller = new StudentController(mService.Object);
            controller.GetEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.GetEntityById(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void DeleteEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            //Action
            var controller = new StudentController(mService.Object);
            controller.DeleteEntityById(It.IsAny<int>());

            //Assert
            mService.Verify(mock => mock.DeleteEntity(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void CreateEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.CreateEntity(It.IsAny<StudentDTO>()));

            //Action
            var controller = new StudentController(mService.Object);
            controller.CreateEntity(It.IsAny<StudentDTO>());

            //Assert
            mService.Verify(mock => mock.CreateEntity(It.IsAny<StudentDTO>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityReturnsCalled()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<StudentDTO>()));

            //Action
            var controller = new StudentController(mService.Object);
            controller.UpdateEntity(It.IsAny<int>(), It.IsAny<StudentDTO>());

            //Assert
            mService.Verify(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<StudentDTO>()), Times.Once);

        }

        [Fact]
        public void UpdateEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.UpdateEntity(It.IsAny<int>(), It.IsAny<StudentDTO>()));

            //Action
            var controller = new StudentController(mService.Object);

            //Assert
            Assert.Equal("405", controller.UpdateEntityBadUrl(It.IsAny<StudentDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void PostEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.CreateEntity(It.IsAny<StudentDTO>()));

            //Action
            var controller = new StudentController(mService.Object);

            //Assert
            Assert.Equal("405", controller.PostEntityBadUrl(It.IsAny<StudentDTO>()).StatusCode.ToString());
        }

        [Fact]
        public void DeleteEntityBadUrlReturnsStatusCode405()
        {
            //Setup
            var mService = new Mock<IStudentService>();
            mService.Setup(mock => mock.DeleteEntity(It.IsAny<int>()));

            //Action
            var controller = new StudentController(mService.Object);

            //Assert
            Assert.Equal("405", controller.DeleteEntityBadUrl().StatusCode.ToString());
        }
    }
}
