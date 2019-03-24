using Microsoft.AspNetCore.Mvc;
using MvcAngular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Authorization;
using VsExample.AspAPI.Dtos;

namespace VsExample.AspAPI.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class AnimalController: Controller
    {

        [HttpPost]
        [Role(RoleEnum.User, RoleEnum.Administrator)]
        public Animal GetOneAnimal()
        {
            return new Animal()
            {
                Name = "cat",
                ImageUrl = "https://www.catster.com/wp-content/uploads/2018/07/Savannah-cat-long-body-shot.jpg"
            };
        }
    }
}
