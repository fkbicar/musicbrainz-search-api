namespace MusicBrainzArtistSearch.Models
{
    public class OtherArtistDataModels
    {
        public string _id { get; set; }
        public string _name { get; set; }

        public OtherArtistDataModels(string id, string name)
        {
            this._id = id;
            this._name = name;
        }
    }
}