using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Mediator
{
    public class Response
    {
        public IActionResult Result { get; set; } 

        public List<string> Errors { get; set; } = new List<string>();
    }
}
