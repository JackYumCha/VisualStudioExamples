﻿using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Data.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;
using VsExample.AspAPI.Dtos;
using Xunit;

namespace VsExample.Data.Tests
{
    public class VsExmaplesTests
    {
        private IContainer BuildContainer()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<MongoOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MongoModule>();
            return autoFacContainer.ContainerBuilder.Build();
        }

        [Fact]
        public void LoadAnimalsToMongoDB()
        {
            var services = BuildContainer();
            var mongo = services.Resolve<MongoContext>();

            var colAnimal = mongo.Collection<Animal>();

            colAnimal.InsertOne(new Animal()
            {
                ImageUrl = "https://cms.qz.com/wp-content/uploads/2018/05/china-pandas-eyes-turned-white-in-sichuan-2018-e1525405988661.jpg?quality=75&strip=all&w=1900&h=1068",
                Name = "Panda",
                _id = "panda"
            });


            colAnimal.InsertOne(new Animal()
            {
                ImageUrl = "http://www.krugerpark.co.za/images/1-lion-charge-gc590a.jpg",
                Name = "Lion",
                _id = "lion"
            });

            colAnimal.InsertOne(new Animal()
            {
                ImageUrl = "https://ichef.bbci.co.uk/news/660/cpsprodpb/E9DF/production/_96317895_gettyimages-164067218.jpg",
                Name = "Monkey",
                _id = "monkey"
            });
        }
    }
}
