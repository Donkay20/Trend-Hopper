using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is responsible for the logic of sending notes down the "lanes", and is responsible for timings and when notes can/should be hit.
Probably not a good idea to edit stuff in here unless the logic for hitting notes gets really bad somehow idk
Uses a lot of the DryWetMidi plugin stuff, refer to its documentation

This class still needs to be edited to add pre- and post- animation stuff
*/

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab; public GameObject goldPrefab; private GameObject selectedPrefab;
    public GameObject statusTextPrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    public float spawnDelay = 0.0f; //make sure this is the same as the song delay
    int spawnIndex = 0;
    int inputIndex = 0;

    void Start()
    {

    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add(((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool missedNote = false;

        if (DressUpStatBonuses.peaking) {
            selectedPrefab = goldPrefab;
        } else {
            selectedPrefab = notePrefab;
        }

        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime) {
                var note = Instantiate(selectedPrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            } 
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError + DressUpStatBonuses.leniency;    //the margin of error is how long the note is in a hit-able range
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0) - spawnDelay;

            if (Input.GetKeyDown(input)) //this area is for imposing timing restrictions to inputs
            {
                if (Math.Abs(audioTime - timeStamp) - spawnDelay < marginOfError / 3) //perfect timing
                {
                    Hit();
                    RhythmFeedback.Instance.showResult("Based!");
                    print($"Hit on {inputIndex} note");
                    if (notes[inputIndex].gameObject.tag == "gold") {
                        DressUpStatBonuses.peakBonus++;
                    }
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    missedNote = false;
                }
                else if (Math.Abs(audioTime - timeStamp) - spawnDelay < marginOfError / 2) //ok timing
                {
                    OK();
                    RhythmFeedback.Instance.showResult("mid");
                    print($"OK on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    if (notes[inputIndex].gameObject.tag == "gold") {
                        DressUpStatBonuses.peakBonus++;
                    }
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    missedNote = false;
                }
            }

            if (((timeStamp + marginOfError)-spawnDelay <= audioTime) && !missedNote) //ngl, I don't know what's going on here but it works so whatever
            {
                Miss();
                RhythmFeedback.Instance.showResult("cringe..");
                print($"Missed {inputIndex} note");
                inputIndex++;
                missedNote = true;
            }
        }
        else  //if the song is finished, wait x seconds and then load the results screen
        {     //there is a better way to code this which I will fix later but not rn, will prob do so for prototype 2
            Invoke(nameof(delayLoadSuccess), 12.0f);
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void OK()
    {
        ScoreManager.OK();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }

    private void delayLoadSuccess() //after a song is finished, load the results screen
    {
        SceneManager.LoadScene("Results");
    }
}

