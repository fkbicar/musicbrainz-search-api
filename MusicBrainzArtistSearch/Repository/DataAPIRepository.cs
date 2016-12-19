using MusicBrainzArtistSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace MusicBrainzArtistSearch.Repository
{
    public class DataAPIRepository : IDataAPIRepository
    {
        private MusicBrainzDBEntities _db;

        public DataAPIRepository(MusicBrainzDBEntities db)
        {
            this._db = db;
        }

        public List<ReleaseDataModels> GetArtistReleasesOrAlbums(Guid artistId, string param)
        {
            bool artistQuery = _db.Identifiers.Any(id => id.uniqueid.Equals(artistId.ToString()));

            if (artistQuery == false)
            {
                return null;
            }

            string musicBrainzURL = "http://musicbrainz.org/ws/2/release?query=arid:" + artistId + "&fmt=json&include=all";

            var handler = new HttpClientHandler { AllowAutoRedirect = false };
            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                                             "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var response = httpClient.GetAsync(musicBrainzURL).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonResponseData = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(jsonResponseData);

                //process json object
                List<ReleaseDataModels> releaseDataList = ProcessResponse(json, param);
                if (releaseDataList.Count == 0)
                {
                    return null;
                }
                else
                {
                    return releaseDataList;
                }
            }
            else
            {
                return null;
            }
        }

        private List<ReleaseDataModels> ProcessResponse(JObject jsonObject, string type)
        {
            List<ReleaseDataModels> releaseDataList = new List<ReleaseDataModels>();

            var releaseList = from release in jsonObject["releases"] select release;
            foreach(var release in releaseList)
            {
                ReleaseDataModels releaseDataNode = new ReleaseDataModels(); //main container
                List<OtherArtistDataModels> releaseArtistList = new List<OtherArtistDataModels>(); //artist collaborators
                List<ArtistLabelDataModels> labelArtistList = new List<ArtistLabelDataModels>(); //labels

                var releaseType = (string) release["release-group"]["primary-type"];

                //break if release not Album type
                if (type.Equals("albums")) {
                    if ((releaseType == null) || !(releaseType.Equals("Album")))
                    {
                        continue;
                    }
                }

                releaseDataNode.releaseId = (string) release["id"];
                releaseDataNode.title = (string) release["title"];
                releaseDataNode.status = (string) release["status"];
                releaseDataNode.numberOfTracks = (string) release["track-count"];
                releaseDataNode.type = releaseType;

                var artistCreditList = release["artist-credit"];
                if (artistCreditList != null)
                {
                    foreach (var artistObj in artistCreditList)
                    {
                        releaseArtistList.Add(new OtherArtistDataModels((string)artistObj["artist"]["id"], (string)artistObj["artist"]["name"]));
                    }
                }

                var labelInfoList = release["label-info"];
                if (labelInfoList != null)
                {
                    foreach (var labelObj in labelInfoList)
                    {
                        labelArtistList.Add(new ArtistLabelDataModels((string)labelObj["label"]["id"], (string)labelObj["label"]["name"]));
                    }
                }

                releaseDataNode.label = labelArtistList;
                releaseDataNode.otherArtists = releaseArtistList;

                releaseDataList.Add(releaseDataNode);
            }

            return releaseDataList;
        }
    }
}