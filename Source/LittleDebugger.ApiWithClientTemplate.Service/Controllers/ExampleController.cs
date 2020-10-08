using LittleDebugger.ApiWithClientTemplate.Client;
using LittleDebugger.ApiWithClientTemplate.Contract.Models;
using LittleDebugger.ApiWithClientTemplate.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LittleDebugger.ApiWithClientTemplate.Service.Controllers
{
    public class ExampleController : Controller, IExampleController
    {
        private readonly ApiWithClientTemplateDatabaseContext _context;

        public ExampleController(ApiWithClientTemplateDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("[Controller]/{id}")]
        public async Task<ExampleModel> Get(int id)
        {
            return await _context.Example.FindAsync(id).ConfigureAwait(false);
        }

        [HttpPost("[Controller]")]
        public async Task<int> Create([FromBody] ExampleModel record)
        {
            _context.Example.Attach(record);
            await _context.SaveChangesAsync();
            
            return record.Id;
        }

        [HttpPut("[Controller]")]
        public async Task<StatusCodeResult> Update([FromBody] ExampleModel record)
        {
            var existinRecord = await _context.Example.FindAsync(record?.Id).ConfigureAwait(false);
            if (existinRecord == null)
            {
                return NotFound();
            }

            _context.Entry(existinRecord).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _context.Entry(record).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("[Controller]/{id}")]
        public async Task<StatusCodeResult> Delete(int id)
        {
            var record = await _context.Example.FindAsync(id).ConfigureAwait(false);
            if (record == null)
            {
                return NotFound();
            }

            _context.Example.Remove(record);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
