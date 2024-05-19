using System.Threading.Tasks;
using Model;
using NonDomain;

namespace Contracts.Services
{
    public interface IImagesService
    {
        Task<OperationResult> AddImage(Image image);
        Task<OperationResult> EditImage(Image image);
    }
}
