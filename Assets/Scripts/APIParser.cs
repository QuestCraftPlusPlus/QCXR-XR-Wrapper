using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SearchParser
{
    public List<SearchResults> hits;
    public int total_hits;

}

[Serializable]

public class SearchResults
{
    public string project_id;
    public string title;
    public string description;
    public string author;
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
    public string project_type;
    public List<string> game_versions;
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
    public string instanceImageURL;
    public string versionName;
    public string versionType;
    public string classpath;
    public string gameDir;
    public string assetIndex;
    public string assetsDir;
    public string mainClass;
    public bool defaultMods;
    public AndroidJavaObject raw;

    public static PojlibInstance Parse(AndroidJavaObject raw)
    {
        PojlibInstance instance = new PojlibInstance();
        instance.raw = raw;
        instance.instanceName = raw.Get<string>("instanceName");
        instance.instanceImageURL = raw.Get<string>("instanceImageURL");
        instance.versionName = raw.Get<string>("versionName");
        instance.versionType = raw.Get<string>("versionType");
        instance.classpath = raw.Get<string>("classpath");
        instance.gameDir = raw.Get<string>("gameDir");
        instance.assetIndex = raw.Get<string>("assetIndex");
        instance.assetsDir = raw.Get<string>("assetsDir");
        instance.mainClass = raw.Get<string>("mainClass");
        instance.defaultMods = raw.Get<bool>("defaultMods");
        return instance;
    }

    public PojlibMod[] GetMods()
    {
        // Mods can change at runtime
        List<PojlibMod> mods = new List<PojlibMod>();
        foreach (var mod in raw.Get<AndroidJavaObject[]>("extProjects"))
        {
            mods.Add(PojlibMod.Parse(mod));
        }

        return mods.ToArray();
    }
}

public class PojlibMod
{
    public string slug;
    public string version;
    public string download_link;
    public AndroidJavaObject raw;

    public static PojlibMod Parse(AndroidJavaObject raw)
    {
        PojlibMod mod = new PojlibMod();
        mod.raw = raw;
        
        mod.slug = raw.Get<string>("slug");
        mod.version = raw.Get<string>("version");
        mod.download_link = raw.Get<string>("download_link");
        return mod;
    }
}