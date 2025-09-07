using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameInputSettings
{
    public Dictionary<KeyCode, bool> heldKeys;
    public Dictionary<KeyCode, Vector3> diffVectors;

    public FrameInputSettings(Dictionary<KeyCode, Vector3> diffVectors = null, Dictionary<KeyCode, bool> heldKeys = null)
    {
        this.diffVectors = diffVectors;
        this.heldKeys = heldKeys;
    }

    public FrameInputSettings(FrameInputSettings settingsToCopy)
    {
        this.diffVectors = settingsToCopy.diffVectors;
        this.heldKeys = settingsToCopy.heldKeys;
    }    
}
