using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Utils;

namespace WEB.Controllers
{
    public class MediaController : Controller
    {
        private readonly string _imageryRootPath;
        private readonly string _imageryProductsPath;
        private readonly string _imageryCategoriesPath;

        public MediaController(IConfiguration config)
        {
            var imagesCatalogPathsSection = config.GetSection(ConfigConstants.ImagesCatalogPathsSectionConfigName);
            _imageryRootPath = imagesCatalogPathsSection.GetValue<string>(ConfigConstants.ImagesCatalogRootPathConfigName);
            _imageryProductsPath = imagesCatalogPathsSection.GetValue<string>(ConfigConstants.ImagesCatalogProductsPathConfigName);
            _imageryCategoriesPath = imagesCatalogPathsSection.GetValue<string>(ConfigConstants.ImagesCatalogCategoriesPathConfigName);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProductImage(string path)
        {
            var imagePath = Path.Combine(_imageryRootPath, _imageryProductsPath, path);
            return File(imagePath, "image/jpeg");
        }

        [HttpGet]
        public IActionResult GetCategoryImage(string path)
        {
            var imagePath = Path.Combine(_imageryRootPath, _imageryCategoriesPath, path);
            return File(imagePath, "image/jpeg");
        }
    }
}
