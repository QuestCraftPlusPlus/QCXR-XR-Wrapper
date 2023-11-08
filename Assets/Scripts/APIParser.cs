using System;
using System.Collections.Generic;

[Serializable]
public class SearchParser
{
    public List<SearchResults> hits;
}

[Serializable]

public class SearchResults
{
    public string project_id;
    public string title;
    public string description;
    public string icon_url;
    public List<string> versions;
}

[Serializable]

public class MetaParser
{
    public string title;
    public string icon_url;
    public string description;
    public string slug;
}

[Serializable]

public class MetaInfo
{
    public List<string> game_versions { get; set; }
    public List<FileInfo> files { get; set; }
    public List<string> loaders { get; set; }
}

[Serializable]

public class Deps
{
	public List<MetaParser> projects { get; set; }
}

[Serializable]

public class FileInfo
{
    public string url;
	public string filename;
}
