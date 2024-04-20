using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;

namespace ItMarathon.Service.HelloWorldService
{
    public class HelloWorldService : IHelloWorldService
    {
        private IRepository<HelloWorld> helloWorldRepository;
        private IUnitOfWork unitOfWork;

        public HelloWorldService(IRepository<HelloWorld> helloWorldRepository, IUnitOfWork unitOfWork)
        {
            this.helloWorldRepository = helloWorldRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> AddWordAsync(string word)
        {
            helloWorldRepository.Add(new HelloWorld { Word = word, CreateDate = DateTime.Now });
            await unitOfWork.CommitAsync();
            return word;
        }
    }
}
