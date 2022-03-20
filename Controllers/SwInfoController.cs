using ApiSW.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSW.Controllers
{
    [ApiController]
    [Route("StarWarsInfoController")]
    public class SwInfoController : ControllerBase
    {
        private readonly StarWarsServiceClient _starWarsServiceClient;
        public SwInfoController(StarWarsServiceClient starWarsServiceClient)
        {
            _starWarsServiceClient = starWarsServiceClient;
        }

        
        [Route("get-movies-by-starship/{id}")]
        [HttpGet]
        public async Task<ActionResult<StarshipWithMoviesModel>> GetAsync(int id)
        {
            var result = await _starWarsServiceClient.GetStarshipInfoAsync(id);
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest("Not found");
        }

    }
}
