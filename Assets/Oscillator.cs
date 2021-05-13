using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oscillator : MonoBehaviour
{
    public double frequency = 0.0;
    public FreqAdjust knob;
    // use the .getCurrentOsc() method to determine which one to use in OnAudioFilterRead()
    public MasterSwitcher oscButtons;
    private string oscType;
    //the type of wave that the ocillator produces
    public double gain = 0.05;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000;

    // The elapsed time for each ocillator
        // TODO: write this data to a file for the experiment
    private float sinElapsed = 0.0f;
    private float squareElapsed = 0.0f;
    private float triangleElapsed = 0.0f;


    public void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        increment = frequency * 2 * Math.PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase = phase + increment;
            if (oscType == "sin")
                data[i] = (float)(gain * Mathf.Sin((float)phase));
            else if (oscType == "square")
            {
                if (gain * Mathf.Sin((float)phase) >= 0)
                    data[i] = (float)gain * 0.6f;
                else
                    data[i] = (-(float)gain) * 0.6f;
            }
            else if (oscType == "triangle")
                data[i] = (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));
            // if we have stereo, we copy the mono data to each channel
            if (channels == 2) data[i + 1] = data[i];
            if (phase > 2 * Math.PI) phase = 0.0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        oscType = "sin";
    }

    // Update is called once per frame
    void Update()
    {
        // Change time elapsed depending on what synth is being used
            // used for the experiment
        if (oscType == "sin") sinElapsed += Time.deltaTime;
        else if (oscType == "triangle") triangleElapsed += Time.deltaTime;
        else if (oscType == "square") squareElapsed += Time.deltaTime;

        frequency = knob.getFreq();
        oscType = oscButtons.currentOSC;
    }

}
