using Model;
using Contracts.DataAccess.SpecificRepos;

namespace DataAccess
{
    public class ImagesRepository : BaseRepository<Image>, IImagesRepository
    {
        public ImagesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
