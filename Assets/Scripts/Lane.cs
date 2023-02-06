using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab; public GameObject statusTextPrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
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
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool missedNote = false;

        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError / 3)
                {
                    Hit();
                    RhythmFeedback.Instance.showResult("Based!");
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    missedNote = false;
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError / 2)
                {
                    OK();
                    RhythmFeedback.Instance.showResult("mid");
                    print($"OK on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                    missedNote = false;
                }
            }

            if (timeStamp + marginOfError <= audioTime && !missedNote)
            {
                Miss();
                RhythmFeedback.Instance.showResult("cringe..");
                print($"Missed {inputIndex} note");
                inputIndex++;
                missedNote = true;
            }
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

    // void showResult(string text) {
    //     if (statusTextPrefab) 
    //     {
    //         GameObject prefab = Instantiate(statusTextPrefab, transform.position, Quaternion.identity);
    //         prefab.GetComponentInChildren<TextMesh>().text = text;
    //     }
    // }
}

