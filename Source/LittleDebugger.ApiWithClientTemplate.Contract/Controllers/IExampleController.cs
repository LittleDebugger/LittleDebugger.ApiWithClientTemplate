using LittleDebugger.ApiWithClientTemplate.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LittleDebugger.ApiWithClientTemplate.Client
{
    public interface IExampleController
    {
        [HttpGet("[Controller]/{id}")]
        Task<ExampleModel> Get(int id);

        [HttpPost("[Controller]")]
        Task<int> Create([FromBody] ExampleModel record);

        [HttpPut("[Controller]")]
        Task<StatusCodeResult> Update([FromBody] ExampleModel record);

        [HttpDelete("[Controller]/{id}")]
        Task<StatusCodeResult> Delete(int id);
    }
}
