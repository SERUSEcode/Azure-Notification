using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Model;

namespace ServerSideAPI.Controllers
{
    [ApiController]
    [Route("api/Situation")]
    public class HomeController : Controller
    {
        private readonly Rootobject result;


        [HttpGet]
        public IEnumerable<Rootobject> GetAll()
        {
            return (IEnumerable<Rootobject>)result;
        }
    }
}
