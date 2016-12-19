namespace MusicBrainzArtistSearch.Models
{
    public class ArtistLabelDataModels
    {
        public string _id { get; set; }
        public string _name { get; set; }

        public ArtistLabelDataModels(string id, string name)
        {
            this._id = id;
            this._name = name;
        }
    }
}