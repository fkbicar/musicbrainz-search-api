using MusicBrainzArtistSearch.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicBrainzArtistSearch.Controllers
{
    [RoutePrefix("artist")]
    public class ArtistController : ApiController
    {
        private IDataEntityRepository _dataRepo;
        private IDataAPIRepository _apiRepo;

        public ArtistController(IDataEntityRepository dataRepo, IDataAPIRepository apiRepo)
        {
            this._dataRepo = dataRepo;
            this._apiRepo = apiRepo;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var allArtists = _dataRepo.GetAllArtist();
            if (allArtists != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, allArtists);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [Route("search/{query?}")]
        [HttpGet]
        public HttpResponseMessage Get(string query)
        {
            //return if query string is empty
            if(query == null || query.Equals(""))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }

            var searchResult = _dataRepo.GetAllArtistByName(query);
            if (searchResult != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [Route("search/{query?}/{pageNumber:int?}/{pageSize:int?}")]
        [HttpGet]
        public HttpResponseMessage Get(string query, int pageNumber, int pageSize)
        {
            //return if query string is empty
            if (query == null || query.Equals("") || pageNumber <= 0 || pageSize <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }

            var searchResult = _dataRepo.GetAllArtistByNameWithPagination(query, pageNumber, pageSize);
            if (searchResult != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var singleArtist = _dataRepo.GetArtistById(id);
            if (singleArtist != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, singleArtist);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [Route("{artistId:guid?}/releases")]
        [HttpGet]
        public HttpResponseMessage GetArtistReleases(Guid artistId)
        {
            var uri = Request.RequestUri;
            var paramList = uri.ToString().Split('/');
            string param = paramList[paramList.Length - 1];

            var releases = _apiRepo.GetArtistReleasesOrAlbums(artistId, param);
            if (releases != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, releases);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [Route("{artistId:guid?}/albums")]
        [HttpGet]
        public HttpResponseMessage GetArtistAlbums(Guid artistId)
        {
            var uri = Request.RequestUri;
            var paramList = uri.ToString().Split('/');
            string param = paramList[paramList.Length - 1];

            var releases = _apiRepo.GetArtistReleasesOrAlbums(artistId, param);
            if (releases != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, releases);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }
    }
}
