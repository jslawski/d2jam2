using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class RecordedData 
{
    private static string initialPositions;
    private static string initialRotations;
    private static List<FrameInputSettings> frameData;

    public static void CreateNewRecordedData(List<BodyPartController> bodyPartControllers)
    {
        RecordedData.frameData = new List<FrameInputSettings>();
        RecordedData.RecordInitialPositions(bodyPartControllers);
        RecordedData.RecordInitialRotations(bodyPartControllers);
    }

    public static void RecordInitialPositions(List<BodyPartController> bodyPartControllers)
    {
        RecordedData.initialPositions = string.Empty;
        for (int i = 0; i < bodyPartControllers.Count; i++)
        { 
            RecordedData.initialPositions += bodyPartControllers[i].transform.position.x + "," + bodyPartControllers[i].transform.position.y + "," + bodyPartControllers[i].transform.position.z + ",";
        }        
    }

    public static void RecordInitialRotations(List<BodyPartController> bodyPartControllers)
    {
        RecordedData.initialRotations = string.Empty;
        for (int i = 0; i < bodyPartControllers.Count; i++)
        {
            RecordedData.initialRotations += bodyPartControllers[i].transform.rotation.eulerAngles.x + "," + bodyPartControllers[i].transform.rotation.eulerAngles.y + "," + bodyPartControllers[i].transform.rotation.eulerAngles.z + ",";
        }
    }

    public static void AddFrameInputSettings(FrameInputSettings newFrameInputSettings)
    {
        if (RecordedData.frameData == null)
        {
            Debug.LogError("ERROR: Attempted to record data before calling CreateNewRecordedData()");
            return;
        }

        RecordedData.frameData.Add(newFrameInputSettings);
    }

    public static void SaveDataToFile()
    {
        List<string> replayDataString = new List<string>();
        replayDataString.Add((RecordedData.initialPositions + '\n'));
        replayDataString.Add((RecordedData.initialRotations + '\n'));

        for (int i = 0; i < RecordedData.frameData.Count; i++)
        {
            replayDataString.Add((RecordedData.GetFrameInputDataString(RecordedData.frameData[i]) + '\n'));            
        }

        File.WriteAllLines((Application.persistentDataPath + "\\draft.txt"), replayDataString, Encoding.UTF8);
        
        /*
        StreamWriter writer = File.CreateText(Application.persistentDataPath + "/draft.txt");

        //Save Initial Positions
        writer.WriteLine(RecordedData.initialPositions, );

        //Save Initial Rotations
        writer.WriteLine(RecordedData.initialRotations);

        //Save Frame Data
        for (int i = 0; i < RecordedData.frameData.Count; i++)
        {
            writer.WriteLine(RecordedData.GetFrameInputDataString(RecordedData.frameData[i]));
        }

        writer.Close();
        */
    }

    public static string GetReplayDataString()
    {
        string replayDataString = string.Empty;
        replayDataString += (RecordedData.initialPositions + '\n');
        replayDataString += (RecordedData.initialRotations + '\n');

        for (int i = 0; i < RecordedData.frameData.Count; i++)
        {
            replayDataString += (RecordedData.GetFrameInputDataString(RecordedData.frameData[i]) + '\n');
        }

        return replayDataString;
    }

    private static string GetFrameInputDataString(FrameInputSettings frameInputData)
    {
        string returnString = string.Empty;

        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.W);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.A);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.S);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.D);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.Z);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.X);
        returnString += RecordedData.GetKeyStringValues(frameInputData, KeyCode.C);

        return returnString;
    }

    private static string GetKeyStringValues(FrameInputSettings frameInputData, KeyCode keyCode)
    {
        string returnString = string.Empty;

        returnString += Convert.ToInt32(frameInputData.heldKeys[keyCode]).ToString() + ",";
        returnString += frameInputData.diffVectors[keyCode].x.ToString() + ",";
        returnString += frameInputData.diffVectors[keyCode].y.ToString() + ",";

        return returnString;
    }
}