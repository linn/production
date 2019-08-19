namespace Linn.Production.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources.External;

    public class SalesArticleProxy : ISalesArticleService
    {
        private readonly IRestClient restClient;

        private readonly string rootUri;

        public SalesArticleProxy(IRestClient restClient, string rootUri)
        {
            this.restClient = restClient;
            this.rootUri = rootUri;
        }

        public string GetDescriptionFromPartNumber(string partNumber)
        {
            var uri = new Uri($"{this.rootUri}/products/maint/sales-articles/{partNumber}", UriKind.RelativeOrAbsolute);
            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            var json = new JsonSerializer();
            var resource = json.Deserialize<SalesArticleResource>(response.Value);
            return resource.Description;
        }
    }
}