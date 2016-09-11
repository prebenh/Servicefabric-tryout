using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using MyStatefulService;
using MyStatelessService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            //fabric:/ApplicationName/ServiceName
            var statelessUri = new Uri("fabric:/Application1/MyStatelessService");
            var statefulUri = new Uri("fabric:/Application1/MyStatefulService");

            //Stateless
            var statelessClient = ServiceProxy.Create<IMyStatelessService>(statelessUri);
            var message = await statelessClient.HelloWorld();

            //Stateful
            var partitionKey = new ServicePartitionKey(1);
            var statefulClient = ServiceProxy.Create<IMyStatefulService>(statefulUri, partitionKey);
            var count = await statefulClient.GetCount();

            return new string[] { message, count.ToString() };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}