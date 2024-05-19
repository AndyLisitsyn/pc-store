using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using WEB.Models;
using Contracts.Services;
using Utils;
using WEB.ViewModels.HomeViewModels;
using Microsoft.Extensions.Logging;
using Services.Dto;

namespace WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public HomeController() { }
    }
}
