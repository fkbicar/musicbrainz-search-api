using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBrainzArtistSearch.Models
{
    public class ResultDataModels
    {

        public List<ArtistDataModels> results { get; set; }
        public int numberOfSearchResults { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int numberOfPages { get; set; }

        public ResultDataModels(
            List<ArtistDataModels> results, 
            int numberOfSearchResults)
        {
            this.results = results;
            this.numberOfSearchResults = numberOfSearchResults;
        }

        public ResultDataModels(
            List<ArtistDataModels> results,
            int numberOfSearchResults,
            int page,
            int pageSize)
        {
            this.results = results;
            this.numberOfSearchResults = numberOfSearchResults;
            this.page = page;
            this.pageSize = pageSize;
        }

        //we can improve this and separate it as a different class
        //but at this point, this is just a simple (hardcoded) implementation
        public ResultDataModels CalculatePagination()
        {
            int calculatedPages = 0;

            //calculate the number of pages based on the pageSize indicated
            calculatedPages = this.numberOfSearchResults / this.pageSize;
            if((this.numberOfSearchResults % this.pageSize) != 0) //means there are extra items to place on the next page
            {
                calculatedPages = calculatedPages + 1;
            }

            this.numberOfPages = calculatedPages;

            return this;
        }
    }
}