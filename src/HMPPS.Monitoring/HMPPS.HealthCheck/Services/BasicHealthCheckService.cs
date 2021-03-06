using System;
using HMPPS.ErrorReporting;
using MongoDB.Driver;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HMPPS.HealthCheck.Services
{
    public class BasicHealthCheckService : BaseHealthCheckService
    {
        private readonly HealthCheckConfig _config;

        public BasicHealthCheckService(ILogManager logManager, HealthCheckConfig config)
            : base(logManager) 
        {
            _config = config;
        }

        public HealthCheckFacet SitecoreSql()
        {
            var healthy = Sitecore.Context.Site != null &&
                          Sitecore.Context.Database != null &&
                          Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath) != null;

            return new HealthCheckFacet
            {
                Name = "SitecoreSql",
                Healthy = healthy
            };
        }

        public HealthCheckFacet SitecoreMongo()
        {
            var checkResult = new HealthCheckFacet
            {
                Name = "SitecoreMongo",
                Healthy = true,
            };


            var client = new MongoClient(_config.MongoDbConnectionString);

            var server = new MongoServer(MongoServerSettings.FromClientSettings(client.Settings));
            client.ListDatabases(CancellationToken.None);//get mongo to do something first, otherwise the server ping will fail the first time after the app starts
            server.Ping();

            return checkResult;
        }

        public HealthCheckFacet AzureSearch()
        {
            var coreIndex = ContentSearchManager.GetIndex("sitecore_core_index");

            var healthCheckResults = Task.Run(() =>
            {
                using (var context = coreIndex.CreateSearchContext())
                {
                    return context.GetQueryable<SearchResultItem>()
                        .Where(c => c.Content.Equals("fake health check query"))
                        .ToList();
                }
            });

            if (!healthCheckResults.Wait(TimeSpan.FromSeconds(5)))
            {
                throw new TimeoutException("Azure search health check operation timed out");
            }

            return new HealthCheckFacet
            {
                Name = "AzureSearch",
                Healthy = true
            };
        }

        protected override MethodInfo GetHealthCheckMethod(string checkName)
        {
            return GetType().GetMethod(checkName);
        }
    }
}
