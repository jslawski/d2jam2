using System;
using System.Collections.Generic;

[Serializable]
public class VideoData
{
    public string username;
    public int videoIndex;
    public int numLikes;
    public string backgroundName;
    public string bgmName;
    public int bgmSampleIndex;
    public string hairName;
    public string eyebrowsName;
    public string eyesName;
    public string noseName;
    public string mouthName;
    public string bodyTextureName;
}

[Serializable]
public class PostedVideosData
{ 
    public List<VideoData> entries;
}
