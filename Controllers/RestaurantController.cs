using find_my_restaurant.Data.Collections;
using find_my_restaurant.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace find_my_restaurant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Restaurant> _restaurantCollection;
        public RestaurantController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _restaurantCollection = _mongoDB.DB.GetCollection<Restaurant>(typeof(Restaurant).Name.ToLower());
        }

        [HttpGet]
        public ActionResult GetRestaurants()
        {
            var restaurants = _restaurantCollection.Find(Builders<Restaurant>.Filter.Empty).ToList();
            return Ok(restaurants);
        }

        [HttpPost]
        public ActionResult StoreRestaurant([FromBody] RestaurantDto dto)
        {
            var restaurant = new Restaurant(dto.Name, dto.Latitude, dto.Longitude);
            _restaurantCollection.InsertOne(restaurant);
            return StatusCode(201, "Restaurant created with success");
        }

    }
}