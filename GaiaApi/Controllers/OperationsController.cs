using Microsoft.AspNetCore.Mvc;
using GaiaApi.Data;
using GaiaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Azure;

namespace GaiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly GaiaDbContext _context;

        public OperationsController(GaiaDbContext context)
        {
            _context = context;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            object result;
            string strResult;

            var op = OperationFactory.GetOperationByName(request.Operation.ToLower());
            strResult = op != null ? op.Execute(request.FieldA.ToString(), request.FieldB.ToString()) : "Invalid Operation";
           
            var record = new OperationRecord
            {
                FieldA = request.FieldA,
                FieldB = request.FieldB,
                Operation = request.Operation,
                Result = strResult
            };

            _context.Operations.Add(record);
            await _context.SaveChangesAsync();

            return Ok(new { result = strResult });
        }

        [HttpGet("history/{operation}")]
        public async Task<IActionResult> GetHistory(string operation, [FromQuery] int take = 3)
        {
            var recentOperations = await _context.Operations
                .Where(op => op.Operation == operation)
                .OrderByDescending(op => op.Timestamp)
                .Take(take)
                .ToListAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var obj = JsonSerializer.Serialize(recentOperations, options);
            return Ok(obj);
        }


        [HttpGet("count/{operation}")]
        public async Task<IActionResult> GetOperationCount(string operation)
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

            var operationCount = await _context.Operations
                .Where(op => op.Operation == operation && op.Timestamp >= startOfMonth)
                .CountAsync();

            return Ok(new { count = operationCount });
        }

    }
}
