using UnityEngine;
using System.Collections;

public class ChamberlinFilter : MonoBehaviour
{
	public enum FilterType
	{
		LowPassFilter,
		BandPassFilter,
		HighPassFilter
	}

	public FilterType filterType = FilterType.BandPassFilter;

    [Range(0.0f, 1.0f)]
    public float cutoff = 0.5f;
    
	[Range(0.5f, 2.0f)]
    public float q = 1.0f;

	float sampleRate;
	float filterF;
	float filterD;
	float filterL;
    float filterB;
    float filterH;
	bool stability;

	public float CutoffFrequency {
		get { return Mathf.Pow(2, cutoff * 10 - 10) * 0.25f * sampleRate; }
	}

	public bool Stability {
		get { return stability; }
	}

    void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
		Update();
		audio.Play();
    }

	void Update()
	{
		filterF = Mathf.Sin(Mathf.PI * Mathf.Pow(2, cutoff * 10 - 10) * 0.25f) * 2;
		filterD = 1 / q;
		stability = (filterF * filterF + filterF * filterD * 2 < 4);
	}

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!stability) return;
			
		for (var i = 0; i < data.Length; i += channels)
        {
            filterL += filterB * filterF;
            filterH = data[i] - filterL - filterB * filterD;
            filterB = filterH * filterF + filterB;

			float o;
			if (filterType == FilterType.LowPassFilter)
				o = filterL;
			else if (filterType == FilterType.BandPassFilter)
				o = filterB;
			else
				o = filterH;

			for (var c = 0; c < channels; c++)
	            data[i + c] = o;
        }
    }
}