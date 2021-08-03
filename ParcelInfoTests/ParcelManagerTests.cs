using ParcelInfoService.Data.MongoRepository;
using ParcelInfoService.Managers;
using ParcelInfoService.Models;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParcelInfoService.Domain;

namespace TestForParcelInfo
{
    [TestClass]
    public class ParcelManagerTests
    {
        private Mock<ILogger<ParcelManager>> _loggerManager;
        private Mock<IParcelInfoRepository> _parcelInfoRepositoryMock;
        ParcelModel expected = new ParcelModel() { ParcelId = "1", SenderName = "Kirill",};
        Parcel expected1 = new Parcel() { ParcelId = "1", RecipientName = "Nikita", SenderName = "Kirill", Weight = 15 };

        [TestMethod]
        public async Task ParcelManager_GetParcels()
        {
            //Arrange
            _parcelInfoRepositoryMock = new Mock<IParcelInfoRepository>();
            _loggerManager = new Mock<ILogger<ParcelManager>>();
            var manager = new ParcelManager(_loggerManager.Object,_parcelInfoRepositoryMock.Object);

            
            var expected = new List<ParcelModel>()
            {
                new ParcelModel() { ParcelId = "1",SenderName = "Kirill"},
                new ParcelModel() { ParcelId = "2",SenderName = "rita"},
                new ParcelModel() { ParcelId = "3",SenderName = "masha"}
            };
            _parcelInfoRepositoryMock.Setup(x => x.GetParcels()).ReturnsAsync(expected);
            //Act
            var actual = await manager.GetParcels();
            //Assert
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].ParcelId, actual[i].ParcelId);
                Assert.AreEqual(expected[i].SenderName, actual[i].SenderName);
            }

        }
        [TestMethod]
        public async Task ParcelManager_GetParcelById()
        {
            //Arrange
            _parcelInfoRepositoryMock = new Mock<IParcelInfoRepository>();
            _loggerManager = new Mock<ILogger<ParcelManager>>();
            var manager = new ParcelManager(_loggerManager.Object, _parcelInfoRepositoryMock.Object);


              
            _parcelInfoRepositoryMock.Setup(x => x.GetParcelById("1")).ReturnsAsync(expected);
            //Act
            var actual = await manager.GetParcelById("1");
            //Assert
            Assert.AreEqual(expected.SenderName, actual.SenderName);
        }
        [TestMethod]
        public async Task ParcelManager_DeleteParcel()
        {
            //Arrange
            _parcelInfoRepositoryMock = new Mock<IParcelInfoRepository>();
            _loggerManager = new Mock<ILogger<ParcelManager>>();
            var manager = new ParcelManager(_loggerManager.Object, _parcelInfoRepositoryMock.Object);
            _parcelInfoRepositoryMock.Setup(x => x.DeleteParcel("666")).ReturnsAsync(true);
            //Act
            var actual=await manager.DeleteParcel("666");
            //Assert
            Assert.AreEqual(true, actual);
        }
        [TestMethod]
        public async Task ParcelManager_CreateParcel()
        {
            //Arrange
            _parcelInfoRepositoryMock = new Mock<IParcelInfoRepository>();
            _loggerManager = new Mock<ILogger<ParcelManager>>();
            var manager = new ParcelManager(_loggerManager.Object, _parcelInfoRepositoryMock.Object);
            
            _parcelInfoRepositoryMock.Setup(x => x.AddParcel(expected1)).ReturnsAsync(true);
            //Act
            var actual = await manager.CreateParcel(expected1);
            //Assert
            Assert.AreEqual(true, actual);
        }

    }
}
