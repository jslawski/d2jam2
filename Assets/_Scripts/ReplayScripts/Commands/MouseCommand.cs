using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCommand : Command
{
    private Dictionary<KeyCode, Vector3> _diffVectors;    

    public MouseCommand(FrameInputSettings settings, Dictionary<KeyCode, Vector3> diffVectors)
    {
        this.frameInputSettings = settings;
        this._diffVectors = diffVectors;
    }

    public MouseCommand(MouseCommand commandToCopy)
    {
        this.frameInputSettings = commandToCopy.frameInputSettings;
        this._diffVectors = commandToCopy._diffVectors;
    }

    public override FrameInputSettings Execute()
    {
        this.frameInputSettings.diffVectors = this._diffVectors;
        
        return this.frameInputSettings;
    }
}
