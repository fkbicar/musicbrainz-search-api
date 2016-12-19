using System.Linq;
using MusicBrainzArtistSearch.Models;
using System.Collections.Generic;

namespace MusicBrainzArtistSearch.Repository
{
    public interface IDataEntityRepository
    {
        List<ResultDataModels> GetAllArtist();
        List<ResultDataModels> GetAllArtistByName(string name);
        List<ResultDataModels>  GetAllArtistByNameWithPagination(string name, int pageNumber, int pageSize);
        List<ResultDataModels> GetArtistById(int id);
    }
}