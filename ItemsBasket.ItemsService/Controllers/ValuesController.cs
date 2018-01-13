using System.Collections.Generic;
using ItemsBasket.ItemsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemsBasket.ItemsService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return new List<Item>
            {
                new Item(1, "AC/DC: Back in black", "https://busites_www.s3.amazonaws.com/acdccom/content/articles/acdc-back-in-black-album-cover-650.jpg", 10),
                new Item(2, "Chimay Blu", "http://www.eurosaga.com/wp-content/uploads/2013/03/chimay_tappo_blu_33_cl.jpg", 5),
                new Item(3, "Spalding Basketball", "http://cdn.sweatband.com/spalding_nba_platinum_legacy_fiba_basketball_spalding_nba_platinum_legacy_fiba_basketball_2000x2000.jpg", 50),
                new Item(4, "Jack Wolfskin boots", "https://asset3.surfcdn.com/jack-wolfskin-boots-jack-wolfskin-icy-park-texapore-boots-mocca.jpg?w=1200&h=1200&r=4&q=80&o=b1iUkSfrF8mOpMPadM3DUAsuT2Ej&V=1TBW", 100),
                new Item(5, "Cute kitten", "https://i.ytimg.com/vi/6FQsIfE7sZM/maxresdefault.jpg", 100000000)
            };
        }
    }
}
