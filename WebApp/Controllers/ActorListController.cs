using Akka.Actor;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorListController
    {
        public ActorListController(ActorSystem actorSystem)
        {
            ActorSystem = actorSystem;
        }

        public ActorSystem ActorSystem { get; }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            var root = await ActorSystem.ActorSelection("/").ResolveOne(1.Seconds());

            return PrintChildrenPath(root as ActorRefWithCell);
        }

        static IEnumerable<string> PrintChildrenPath(ActorRefWithCell actor)
        {
            foreach (var item in actor.Children)
            {
                yield return item.ToString();
                
                foreach (var s in PrintChildrenPath(item as ActorRefWithCell))
                {
                    yield return s;
                };
            }
        }

    }
}
