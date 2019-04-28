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
using System.Text.RegularExpressions;
using Serilog;

namespace VsExample.AspAPI.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class AnimalController : Controller
    {
        private readonly MongoContext mongoContext;
        private readonly ILogger logger;
        public AnimalController(MongoContext mongoContext, ILogger logger)
        {
            this.mongoContext = mongoContext;
            this.logger = logger;
            logger.Information($"Constructor of {nameof(AnimalController)}");
        }

        // npm run api

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public Animal GetOneAnimal([FromBody] GetAnimalRequest getAnimalRequest)
        {
            logger.Warning($"{nameof(GetOneAnimal)}: {getAnimalRequest._id}");
            return mongoContext.Collection<Animal>().GetOneById(getAnimalRequest._id);
        }

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public Animal CreateOneAnimal([FromBody] Animal animal)
        {
            logger.Information($"{nameof(CreateOneAnimal)}: {animal.Name}");
            mongoContext.Collection<Animal>().UpsertOne(animal);
            return animal;
        }

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public Animal DeleteOneAnimalById([FromBody] Animal animal)
        {
            logger.Information($"{nameof(DeleteOneAnimalById)}: {animal._id}");
            mongoContext.Collection<Animal>().DeleteOneById(animal._id);
            return animal;
        }

        /// <summary>
        /// a full pagination demo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public ListAnimalResponse ListAnimal([FromBody] ListAnimalRequest request)
        {
            logger.Information($"{nameof(ListAnimal)}");
            var result = new ListAnimalResponse();

            IQueryable<Animal> linq = mongoContext.Collection<Animal>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                linq = linq.Where(a => Regex.IsMatch(a.Name, request.Filter, RegexOptions.IgnoreCase));
            }

            var count = linq.Count();

            
            result.NumberOfPages = (int)Math.Ceiling(count / (double)request.NumberPerPage);
            result.PageIndex = request.PageIndex;
            if (result.PageIndex >= result.NumberOfPages) result.PageIndex = result.NumberOfPages - 1;
            if (result.PageIndex < 0) result.PageIndex = 0;
            

            result.Items = linq
                .Skip(result.PageIndex * request.NumberPerPage)
                .Take(request.NumberPerPage)
                .ToList();

            return result;
        }
    }
}
