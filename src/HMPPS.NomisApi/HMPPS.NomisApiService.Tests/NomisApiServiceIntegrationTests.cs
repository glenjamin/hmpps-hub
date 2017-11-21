using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HMPPS.ErrorReporting;

namespace HMPPS.NomisApiService.Tests
{
    [TestClass]
    public class NomisApiServiceIntegrationTests
    {
        [TestMethod]
        public void NomisApiService_GetPrisonerLocationDetails()
        {
            // PrisonerId: A1417AE
            // JSON respornse expected: {"establishment":{"code":"BMI","desc":"BIRMINGHAM (HMP)"}}

            var nomisApiService = CreateNomisApiService();
            var establishment = nomisApiService.GetPrisonerLocationDetails("A1417AE");

            Assert.AreEqual(establishment.Code, "BMI");
            Assert.AreEqual(establishment.Desc, "BIRMINGHAM (HMP)");

        }

        [TestMethod]
        public void NomisApiService_GetPrisonerLocationDetails_InvalidPrisonerId()
        {
            // PrisonerId: A1417AEx
            // Exception expected: 
            var nomisApiService = CreateNomisApiService();
            Assert.ThrowsException<AggregateException>(() => nomisApiService.GetPrisonerLocationDetails("A1417AEx"));
        }


        [TestMethod]
        public void NomisApiService_GetPrisonerAccounts()
        {
            // PrisonId: BMI,  PrisonerId: A1417AE
            // JSON respornse expected: { "spends": 20637, "cash": 38763,"savings": 5000 }

            var nomisApiService = CreateNomisApiService();
            var accounts = nomisApiService.GetPrisonerAccounts("BMI", "A1417AE");

            Assert.IsInstanceOfType(accounts.Spends, typeof(decimal));
            Assert.IsInstanceOfType(accounts.Cash, typeof(decimal));
            Assert.IsInstanceOfType(accounts.Savings, typeof(decimal));

        }

        [TestMethod]
        public void NomisApiService_GetPrisonerAccounts_InvalidPrisonId()
        {
            // PrisonerId: A1417AEx
            // Exception expected: 
            var nomisApiService = CreateNomisApiService();
            Assert.ThrowsException<AggregateException>(() => nomisApiService.GetPrisonerAccounts("BMIx", "A1417AE"));
        }

        [TestMethod]
        public void NomisApiService_GetPrisonerAccounts_InvalidPrisonerId()
        {
            // PrisonerId: A1417AEx
            // Exception expected: 
            var nomisApiService = CreateNomisApiService();
            Assert.ThrowsException<AggregateException>(() => nomisApiService.GetPrisonerAccounts("BMI", "A1417AEx"));
        }

        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        private Services.NomisApiService CreateNomisApiService()
        {
            var nomisApiService = new Services.NomisApiService(new Mock<ILogManager>().Object, false);
            nomisApiService.ApiBaseUrl = TestContext.Properties["HMPPS.NomisApiService.BaseUrl"].ToString();
            nomisApiService.ClientToken = TestContext.Properties["HMPPS.NomisApiService.ClientToken"].ToString();
            nomisApiService.SecretPkcs8 = TestContext.Properties["HMPPS.NomisApiService.SecretKey"].ToString();
            nomisApiService.InitializeClient();
            return nomisApiService;
        }
    }
}
