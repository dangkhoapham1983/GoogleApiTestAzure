using BIVALE.ApiFunctions.HistoryHttpTrigger;
using BIVALE.BLL.Interfaces;
using BIVALE.BLL.Services;
using BIVALE.DTO;
using BIVALE.GoogleClient.Interfaces;
using BIVALE.GoogleClient.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BIVALE.DITests
{
    [TestClass]
    public class HistoryHttpTriggerTests
    {
        [TestMethod]
        public async Task TestShouldBark()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            var response = await HistoryHttpTrigger.Run(request, traceWriter, new HistoryServices(), null, new GoogleServices());
			var content = await response.Content.ReadAsAsync<string>();
            Assert.AreEqual("Bark!", content);
        }

        [TestMethod]
        public async Task TestShouldMeow()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            var response = await HistoryHttpTrigger.Run(request, traceWriter, new HistoryServices(),null, new GoogleServices());
            var content = await response.Content.ReadAsAsync<string>();
            Assert.AreEqual("Meow!", content);
        }

        [TestMethod]
        public async Task TestShouldMoo()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            Mock<IHistoryServices> cow = new Mock<IHistoryServices>();
            cow.Setup(x => x.GetHistorys()).Returns(new List<HistoryDTO>());
			Mock<IGoogleServices> cow1 = new Mock<IGoogleServices>();
			cow.Setup(x => x.GetHistorys()).Returns(new List<HistoryDTO>());
			var response = await HistoryHttpTrigger.Run(request, traceWriter, cow.Object, null, cow1.Object);
            var content = await response.Content.ReadAsAsync<string>();
            Assert.AreEqual("Moo!", content);
        }
    }
}
