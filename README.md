# musicbrainz-search-api

##Description
API to search for Artists and Releases from the MusicBrainz API.

##Tools/Technologies
- SQL Server 2016 (Developer Edition)
- Visual Studio 2015
- WebAPI
- Entity Framework
- Ninject (IoC)

##Notes
The application runs on IIS. I only used the built-in IIS server of Visual Studio to test my code. 
I tried to separate the layers as much as I can. If I have more time, I would separate the API calls
to MusicBrainz to its own service layer, or even on a separate project, so it can be reusable. 

Same as with my frontend assessment, I was not able to add unit tests to this project. I had to juggle 
creation of this project, with my current work. :)

The SQL Server scripts are located in MusicBrainzArtistSearch/SQL/MusicBrainzArtistDB.sql. You can run
this in one go. 

##Instructions

The API can pretty much do what was required in the documentation. It has the following endpoints:

>> /artist - returns all the artist currently stored in the database

>> /artist/search/search_criteria - returns all the artist stored in the database that matched the search string

>> /artist/search/search_criteria/page_number/page_size - returns all the artist stored in the database that matched the search string and applies pagination rule as required

>> /artist/artist_id/releases - requests information from MusicBrainz and returns all the releases of the specified artist (albums, singles, ep, compilation, etc)

>> /artist/artist_id/albums - requests information from MusicBrainz and returns only the album release of the specified artist
