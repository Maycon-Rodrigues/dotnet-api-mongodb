using MongoDB.Driver.GeoJsonObjectModel;

namespace find_my_restaurant.Data.Collections
{
    public class Restaurant
    {
        public Restaurant(string name, double latitude, double longitude)
        {
            this.Name = name;
            this.Location = new GeoJson2DGeographicCoordinates(latitude, longitude);
        }
        public string Name { get; set; }
        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}