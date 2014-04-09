using UnityEngine;
using System.Collections;

public class ChamberlinFilter : MonoBehaviour
{
    [Range(0.1f, 16.0f)]
    public float cutoffKHz = 10;
    [Range(0.1f, 0.9f)]
    public float q = 0.5f;

    float lpf;
    float bpf;
    float hpf;
    float sampleRate;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        Debug.Log(sampleRate);
    }

    void OnAudioFilterRead(float [] data, int channels)
    {
        if (sampleRate == 0) return;

        float f = 2.0f * Mathf.Sin(Mathf.PI * cutoffKHz * 1000 / sampleRate);

        for (var i = 0; i < data.Length; i += channels)
        {
            lpf += bpf * f;
            hpf = data[i] * q - lpf - bpf * q;
            bpf = hpf * f + bpf;

            data[i] = data[i+1] = Mathf.Clamp(bpf, -1.0f, 1.0f);
        }
    }
}
