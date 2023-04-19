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
    public List<MetaInfo> results;

}

[System.Serializable]

public class MetaInfo
{
    public string id;
    public string project_id;
    public string name;
    public string version;
    public List<FileInfo> fileInfo;
}

[System.Serializable]

public class FileInfo
{
    public string filename;
    public int size;
    public string url;
}
