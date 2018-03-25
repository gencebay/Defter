using System.Xml;
using Xunit;

namespace Defter.Api.Tests
{
    [Trait("Category", "XPath")]
    public class XPathQueryTests
    {
        [Fact]
        public void SampleModelXPathQuery()
        {
            var xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:web=\"http://www.webserviceX.NET\">\r\n  <soapenv:Header />\r\n  <soapenv:Body>\r\n    <web:GetCitiesByCountry>\r\n      <!--Optional:-->\r\n      <web:CountryName>turkey</web:CountryName>\r\n    </web:GetCitiesByCountry>\r\n  </soapenv:Body>\r\n</soapenv:Envelope>";

            XmlDocument requetXml = new XmlDocument();
            requetXml.LoadXml(xml);

            // Add the namespace.  
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(requetXml.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("web", "http://www.webserviceX.NET");

            var parameterNode = requetXml.SelectSingleNode("//soapenv:Envelope/soapenv:Body/web:GetCitiesByCountry/web:CountryName", nsmgr);
            var paramValue = parameterNode.InnerText;

            Assert.Equal("turkey", paramValue);
        }
    }
}