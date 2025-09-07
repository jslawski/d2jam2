using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCommand : Command
{
    private Dictionary<KeyCode, bool> _heldKeys;

    public KeyboardCommand(FrameInputSettings settings, Dictionary<KeyCode, bool> heldKeys)
    {
        this.frameInputSettings = settings;
        this._heldKeys = heldKeys;
    }

    public KeyboardCommand(KeyboardCommand commandToCopy)
    {
        this.frameInputSettings = commandToCopy.frameInputSettings;
        this._heldKeys = commandToCopy._heldKeys;
    }

    public override FrameInputSettings Execute()
    {
        this.frameInputSettings.heldKeys = this._heldKeys;
        return this.frameInputSettings;
    }
}
