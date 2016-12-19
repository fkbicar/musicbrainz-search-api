using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBrainzArtistSearch.Models
{
    public class ReleaseDataModels
    {
        public string releaseId { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string numberOfTracks { get; set; }
        public string type { get; set; }
        public List<OtherArtistDataModels> otherArtists { get; set; }
        public List<ArtistLabelDataModels> label { get; set; }
        
        public ReleaseDataModels()
        {

        }

    }
}