using MediatR;

namespace IntercambioGenebraAPI.Mediator
{
    public class Response<TObject>
    {
        public TObject? Result { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
