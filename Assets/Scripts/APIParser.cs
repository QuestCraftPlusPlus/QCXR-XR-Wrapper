using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIParser
{

    public class SearchParser
    {
        public List<SearchResults> hits;
    }

    public class SearchResults
    {
        public string project_id;
        public string slug;
        public string author;
        public string title;
        public string description;
        public string icon_url;
    }

    public class MetaParser
    {
        public List<MetaInfo> results;

    }

    public class MetaInfo
    {
        public string id;
        public string project_id;
        public string name;
        public string version;
        public List<FileInfo> fileInfo;
    }

    public class FileInfo
    {
        public string filename;
        public int size;
        public string url;
    }
    
}
