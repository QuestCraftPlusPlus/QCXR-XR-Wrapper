using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SearchParser
{
    public List<SearchResults> hits;
}

[System.Serializable]

public class SearchResults
{
    public string project_id;
    public string slug;
    public string author;
    public string title;
    public string description;
    public string icon_url;
}

[System.Serializable]

public class MetaParser
{
    public string title;
    public string icon_url;
    public string description;
    public string slug;
    public List<MetaInfo> results;

}

[System.Serializable]

public class MetaInfo
{
    public string id;
    public string project_id;
    public string version;
    public List<FileInfo> files;
}

[System.Serializable]

public class FileInfo
{
    public string filename;
    public string version;
    public string url;
    public int size;
}
