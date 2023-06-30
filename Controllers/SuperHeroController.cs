using GraphQLClientAPI.Collection;
using GraphQLClientAPI.Common.helpers;
using GraphQLClientAPI.Common.utilities;
using GraphQLClientAPI.Consumer;
using GraphQLClientAPI.Controllers.GraphQLEnum;
using GraphQLClientAPI.GraphQLIO;
using GraphQLClientAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace GraphQLClientAPI.Controllers
{
    [Route("[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroConsumer _consumer;
        private IMemoryCache _memoryCache;  //Single Server Caching
        //private IDistributedCache _distributedCache; // Redis Server Caching
        private readonly ILogger<SuperHeroController> _logger;

        public SuperHeroController(SuperHeroConsumer consumer, IMemoryCache memoryCache)
        {
            _consumer = consumer;
            _memoryCache = memoryCache;
            //_distributedCache = distributedCache;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
          
            SuperheroSortInput createHero = new SuperheroSortInput();
            createHero.Name = SortEnumType.ASC;

            SuperheroFilterInput filterHero = new SuperheroFilterInput();
            filterHero.Name = new StringOperationFilterInput()
            {
                eq = "Batman"
            };
            #region Redis Server Caching
            //var data = await _distributedCache.GetRecordAsync<List<ResponseSuperheroType>>(CacheKeys.Superheroes);
            //if (data is null)
            //{
            //    Thread.Sleep(10000);
            //    data = await _consumer.GetAllSuperHero();
            //    await _distributedCache.SetRecordAsync(CacheKeys.Superheroes, data);
            //}
            //return Ok(data);
            #endregion
            #region Single Server Caching

            if (!_memoryCache.TryGetValue(CacheKeys.Superheroes, out var memory))
            {
                var owners = await _consumer.GetAllSuperHero();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set(CacheKeys.Superheroes, owners, cacheEntryOptions);

                return Ok(owners);
            }
            else
            {
                return Ok(memory);
            }
            #endregion

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetHeroById([FromBody] StringWrapper model)
        {
            if (Guid.TryParse(model.Value, out Guid heroid))
            {
                SuperheroFilterInput superheroFilterInput = new SuperheroFilterInput();
                superheroFilterInput.Id = new UuidOperationFilterInput()
                {
                    eq = heroid
                };
                var result = await _consumer.GetSuperheroesFilter(superheroFilterInput);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateHero([FromBody] Superhero model)
        {
            var result = await _consumer.CreateHero(model);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateHero([FromBody] SuperheroDtoVM superheroDtoVM)
        {
            var result = await _consumer.UpdateHero(superheroDtoVM);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteHero([FromBody] StringWrapper model)
        {
          
            var result = await _consumer.RemoveHero(model.Value);

            return Ok(result);
        }
    }
}