using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBrainzArtistSearch.Models
{
    public class ArtistDataModels
    {
        public string name { get; set; }
        public string country { get; set; }
        public List<string> alias { get; set; }

        public ArtistDataModels(
            string name, 
            string country, 
            List<string> alias)
        {
            this.name = name;
            this.country = country;
            this.alias = alias;
        }
    }
}