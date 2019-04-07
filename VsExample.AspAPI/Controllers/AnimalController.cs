using Jack.DataScience.Data.MongoDB;
using Microsoft.AspNetCore.Mvc;
using MvcAngular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Authorization;
using VsExample.AspAPI.Dtos;
using MongoDB.Driver;
using Jack.DataScience.Data.MongoDB;

namespace VsExample.AspAPI.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class AnimalController: Controller
    {
        private readonly MongoContext mongoContext;
        public AnimalController(MongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        // npm run api

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public Animal GetOneAnimal([FromBody] GetAnimalRequest getAnimalRequest)
        {
            return mongoContext.Collection<Animal>().GetOneById(getAnimalRequest._id);
        }

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public List<Animal> ListAnimal()
        {
            return mongoContext.Collection<Animal>().AsQueryable().ToList();
        }
    }
}
