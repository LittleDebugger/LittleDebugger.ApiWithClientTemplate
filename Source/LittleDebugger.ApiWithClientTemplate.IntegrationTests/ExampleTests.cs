using LittleDebugger.ApiWithClientTemplate.Client;
using LittleDebugger.ApiWithClientTemplate.Client.Contracts;
using LittleDebugger.ApiWithClientTemplate.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LittleDebugger.ApiWithClientTemplate.IntegrationTests
{
    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        public async Task ExampleTest()
        {
            var webHostBuilder = new WebHostBuilder()
                        .UseEnvironment("Development")
                        .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                var exampleClient = ClientFactory.CreateExampleClient("", client, () => Task.FromResult("ok"));


                try 
                {
                    var record = await exampleClient.GetAsync(1);
                }
                catch (Exception)
                {
                    // should get here.
                }

                var example = new ExampleModel
                {
                    Name = "Testing"
                };

               // await exampleClient.CreateAsync(example);

                var result = await exampleClient.GetAsync(example.Id);

                Assert.AreEqual(example.Name, result.Name);

                example.Name = "Updated";

                await exampleClient.UpdateAsync(example);

                result = await exampleClient.GetAsync(example.Id);

                Assert.AreEqual(example.Name, result.Name);

               await exampleClient.DeleteAsync(example.Id);

                try
                {
                    var record = await exampleClient.GetAsync(1);
                }
                catch (Exception)
                {
                    // should get here.
                }
            }
        }
    }
}
