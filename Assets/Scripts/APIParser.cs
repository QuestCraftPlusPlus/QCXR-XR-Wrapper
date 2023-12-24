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
    public List<Deps> dependencies { get; set; }
    public string id { get; set; }
}

[Serializable]

public class Deps
{
    public string version_id;
    public string project_id;
    public string file_name;
    public string dependency_type;
}

[Serializable]

public class FileInfo
{
    public string url;
	public string filename;
}
