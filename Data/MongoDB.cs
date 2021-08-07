using System;
using find_my_restaurant.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace find_my_restaurant.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }
        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString:URL"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["DBName"]);
                MapClasses();
            }
            catch (System.Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Restaurant)))
            {
                BsonClassMap.RegisterClassMap<Restaurant>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}