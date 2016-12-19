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

        public List<ReleaseDataModels> GetArtistReleases(Guid artistId)
        {
            //var artistQuery = from a in _db.Identifiers where a.uniqueid.Equals(artistId) select a;
            bool artistQuery = _db.Identifiers.Any(id => id.uniqueid.Equals(artistId.ToString()));

            if (artistQuery == false)
            {
                return null;
            }

            var handler = new HttpClientHandler { AllowAutoRedirect = false };
            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                                             "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));

            string parameters = "http://musicbrainz.org/ws/2/release/?query=arid:" + artistId;

            var response = httpClient.GetAsync(parameters).Result;

            if (response.IsSuccessStatusCode)
            {
                //for xml
                string xmlResponseData = response.Content.ReadAsStringAsync().Result;
                XDocument doc = XDocument.Parse(xmlResponseData, LoadOptions.None);

                //process XML
                List<ReleaseDataModels> releaseDataList = ProcessResponse(doc);

                if(releaseDataList.Count == 0)
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

        private List<ReleaseDataModels> ProcessResponse(XDocument doc)
        {
            XNamespace ns = "http://musicbrainz.org/ns/mmd-2.0#";
            List<ReleaseDataModels> releaseDataList = new List<ReleaseDataModels>();

            IEnumerable<XElement> rows = doc.Descendants(ns + "release");

            foreach(XElement releaseNode in rows)
            {
                List<OtherArtistDataModels> releaseArtistList = new List<OtherArtistDataModels>();
                List<ArtistLabelDataModels> labelArtistList = new List<ArtistLabelDataModels>();
                ReleaseDataModels releaseDataNode = new ReleaseDataModels();


                string releaseId = AttributeValueNull(releaseNode, "id");
                string releaseTitle = ElementValueNull(releaseNode.Element(ns + "title"));
                string releaseStatus = ElementValueNull(releaseNode.Element(ns + "status"));

                XElement mediumList = releaseNode.Element(ns + "medium-list");
                string releaseTrackCount = ElementValueNull(mediumList.Element(ns + "track-count"));

                IEnumerable<XElement> labels = releaseNode.Descendants(ns + "label");
                foreach (XElement label in labels)
                {
                    labelArtistList.Add(new ArtistLabelDataModels(AttributeValueNull(label, "id"), ElementValueNull(label.Element(ns + "name"))));
                }


                IEnumerable<XElement> artists = releaseNode.Descendants(ns + "artist");
                foreach(XElement artist in artists)
                {
                    releaseArtistList.Add(new OtherArtistDataModels(AttributeValueNull(artist, "id"), ElementValueNull(artist.Element(ns + "name"))));
                }

                releaseDataNode.releaseId = releaseId;
                releaseDataNode.title = releaseTitle;
                releaseDataNode.status = releaseStatus;
                releaseDataNode.numberOfTracks = releaseTrackCount;

                releaseDataNode.label = labelArtistList;
                releaseDataNode.otherArtists = releaseArtistList;

                releaseDataList.Add(releaseDataNode);
            }

            return releaseDataList;
        }

        private string ElementValueNull(XElement element)
        {
            if (element != null)
                return element.Value;

            return "";
        }

        private string AttributeValueNull(XElement element, string attributeName)
        {
            if (element == null)
                return "";
            else
            {
                XAttribute attr = element.Attribute(attributeName);
                return attr == null ? "" : attr.Value;
            }
        }
    }
}