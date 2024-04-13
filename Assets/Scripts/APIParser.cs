using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class PojlibInstance
{
    public string instanceName;
    public string versionName;
    public string modsDirName;
    public string versionType;
    public string classpath;
    public string gameDir;
    public string assetIndex;
    public string assetsDir;
    public string mainClass;
    public bool defaultMods;
    public AndroidJavaObject raw;

    public static PojlibInstance parse(AndroidJavaObject raw)
    {
        PojlibInstance instance = new PojlibInstance();
        instance.raw = raw;
        instance.instanceName = raw.Get<string>("instanceName");
        instance.versionName = raw.Get<string>("versionName");
        instance.modsDirName = raw.Get<string>("modsDirName");
        instance.versionType = raw.Get<string>("versionType");
        instance.classpath = raw.Get<string>("classpath");
        instance.gameDir = raw.Get<string>("gameDir");
        instance.assetIndex = raw.Get<string>("assetIndex");
        instance.assetsDir = raw.Get<string>("assetsDir");
        instance.mainClass = raw.Get<string>("mainClass");
        instance.defaultMods = raw.Get<bool>("defaultMods");
        return instance;
    }

    public AndroidJavaObject[] getMods()
    {
        // Mods can change at runtime
        return raw.Call<AndroidJavaObject[]>("toArray");
    }
}