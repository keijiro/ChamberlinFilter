using UnityEngine;
using System.Collections;

public class ChamberlinFilter : MonoBehaviour
{
    [Range(0.01f, 0.999f)]
    public float cutoff = 0.5f;
    
	[Range(0.5f, 2.0f)]
    public float q = 1.0f;

    float lpf;
    float bpf;
    float hpf;
    float sampleRate;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        Debug.Log("Sample rate: " + sampleRate);
    }

	void Update()
	{
		Debug.Log ("Cutoff: " + (Mathf.Pow (2.0f, cutoff * 10 - 10) * 0.25f * sampleRate));
	}

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (sampleRate == 0) return;

        float f = 2.0f * Mathf.Sin(Mathf.PI * Mathf.Pow (2.0f, cutoff * 10 - 10) * 0.25f);
		float d = 1.0f / q;

		if (f * f + f * d * 2 >= 4.0f) return;
			
		for (var i = 0; i < data.Length; i += channels)
        {
            lpf += bpf * f;
            hpf = data[i] - lpf - bpf * d;
            bpf = hpf * f + bpf;

            data[i] = data[i+1] = Mathf.Clamp(bpf, -1.0f, 1.0f);
        }
    }
}
