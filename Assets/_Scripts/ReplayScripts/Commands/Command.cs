using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected FrameInputSettings frameInputSettings;

    public abstract FrameInputSettings Execute();
}
