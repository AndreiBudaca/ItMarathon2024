namespace ItMarathon.Service.HelloWorldService
{
    public interface IHelloWorldService
    {
        public Task<string> AddWordAsync(string word);
    }
}
