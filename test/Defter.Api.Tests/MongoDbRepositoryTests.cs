using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NetCoreStack.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NetCoreStack.Data.Context;
using NetCoreStack.Data.Types;
using Defter.SharedLibrary.Models;
using System.Linq;
using System.IO;
using Defter.Api.Hosting;
using System.Xml;
using System.Xml.Linq;

namespace Defter.Api.Tests
{
    [Trait("Category", "MongoDbUnitTests")]
    public class MongoDbRepositoryTests
    {
        private readonly IOptions<DbSettings> _settings;
        private readonly IServiceProvider _resolver;

        public MongoDbRepositoryTests()
        {
            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            services.AddApiFeatures(configuration);
            _resolver = services.BuildServiceProvider();
            _settings = _resolver.GetService<IOptions<DbSettings>>();
        }

        private static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        [Fact]
        public void CreateUnitOfWorkBsonRepository()
        {
            using (var unitOfWork = new MongoUnitOfWork(new MongoDbContext(new DataContextConfigurationAccessor(_settings), new DefaultCollectionNameSelector(), null)))
            {
                Assert.NotNull(unitOfWork);
            }
        }

        [Fact]
        public void CreateUnitOfWorkSelectBson()
        {
            using (var unitOfWork = new MongoUnitOfWork(new MongoDbContext(new DataContextConfigurationAccessor(_settings), new DefaultCollectionNameSelector(), null)))
            {
                var logs = unitOfWork.Repository<DefterLog>().ToList();
                Assert.True(logs.Count > 0);
            }
        }

        [Fact]
        public void GetDefterLogAndParse()
        {
            using (var unitOfWork = new MongoUnitOfWork(new MongoDbContext(new DataContextConfigurationAccessor(_settings), new DefaultCollectionNameSelector(), null)))
            {
                var log = unitOfWork.Repository<DefterLog>().FirstOrDefault();
                if (log == null)
                {
                    return;
                }
                
                string requestContent = Encoding.UTF8.GetString(Convert.FromBase64String(log.RequestContent));
                string responseContent = Encoding.UTF8.GetString(Convert.FromBase64String(log.ResponseContent));

                XmlDocument requetXml = new XmlDocument();
                XmlDocument responseXml = new XmlDocument();

                requetXml.LoadXml(requestContent);
                responseXml.LoadXml(responseContent);

                Assert.True(true);
            }
        }
    }
}