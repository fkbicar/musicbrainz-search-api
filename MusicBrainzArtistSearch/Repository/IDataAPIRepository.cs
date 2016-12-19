using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;
using MusicBrainzArtistSearch.Models;
using System.Collections.Generic;

namespace MusicBrainzArtistSearch.Repository
{
    public interface IDataAPIRepository
    {
        List<ReleaseDataModels> GetArtistReleases(Guid artistId);
    }
}