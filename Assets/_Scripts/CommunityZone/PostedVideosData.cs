using System;
using System.Collections.Generic;

[Serializable]
public class VideoData
{
    public string username;
    public int videoIndex;
    public int numLikes;
    public int backgroundIndex;
    public int bgmIndex;
    public int bgmSampleIndex;
    public int hairIndex;
    public int eyebrowsIndex;
    public int eyesIndex;
    public int noseIndex;
    public int mouthIndex;
    public int bodyTextureIndex;
}

[Serializable]
public class PostedVideosData
{ 
    public List<VideoData> entries;
}
