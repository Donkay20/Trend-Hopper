using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

/*
This class is responsible for loading the MIDI file and grabbing the notes from it. Prob nothing much to touch in here tbh
*/

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError = 0.1; // in seconds

    public int inputDelayInMilliseconds;
    public int maxIndex;

    public string fileLocation;
    public float noteTime;

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ReadFromFile();
    }

    private void ReadFromFile() 
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()   //grabs notes & timings from the midi and copies them to an array, then puts them in their lanes
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        int maxIndex = notes.Count;
        setMaxIndex();
        print($"{notes.Count}");
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong() //plays the song I think, smile
    {
        audioSource.Play();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public int getMaxIndex()    //I put this here but I don't think I need it anymore, we'll see
                                //actually I might need it for the results screen so it isn't hardcoded
    {
        return maxIndex;
    }

    public void setMaxIndex() 
    {
        ScoreManager.Instance.maxCombo = maxIndex;
    }

    void Update()
    {

    }
}