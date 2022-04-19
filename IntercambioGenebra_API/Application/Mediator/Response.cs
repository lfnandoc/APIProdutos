using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Mediator
{
    public class Response
    {
        public IActionResult Result { get; set; }
        public List<string> Errors { get; set; } = new();

        public IActionResult GetResult()
        {
            if (Errors.Any())
                return new UnprocessableEntityObjectResult(Errors);

            return Result;
        }

    }
}