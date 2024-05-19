using System.Threading.Tasks;
using AutoMapper;
using Contracts.DataAccess.SpecificRepos;
using Contracts.Services;
using NonDomain;
using Model;

namespace Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesRepository _imagesRepository;

        public ImagesService(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public Task<OperationResult> AddImage(Image image)
        {
            return _imagesRepository.InsertAsync(image, true);
        }

        public Task<OperationResult> EditImage(Image image)
        {
            return _imagesRepository.UpdateAsync(image, true);
        }
    }
}
