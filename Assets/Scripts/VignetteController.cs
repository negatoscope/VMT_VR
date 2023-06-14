using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;
using System.Collections.Generic;

public class VignetteController : MonoBehaviour
{
    public float scale = 0.07f;
    public float weight = 0.1f;
    public float sampleEvery = 0.1f;
    public Volume volume;

    private float nextSample;

    private Vignette vignette;
    private readonly float[] rotationBuffer = new float[5];
    private readonly float[] translationBuffer = new float[5];
    private int rbIndex;
    private int tbIndex;

    private Quaternion last;
    private Vector3 tlast;

    public float RAvg;
    public float TAvg;
    public float curAvg;

    private void Start()
    {
        if (volume.profile.TryGet(out Vignette vign)) vignette = vign;
    }

    void Update()
    {
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, curAvg, weight);

        // Should not sample yet
        if (Time.time < nextSample)
        {
            return;
        }
        nextSample += sampleEvery;

        // Initial fill
        if (last == null || tlast == null)
        {
            last = transform.rotation;
            tlast = transform.position;
            return;
        }

        var current = transform.rotation;
        var tcurrent = transform.position;
        var angle = Quaternion.Angle(last, current) * scale;
        var translation = Vector3.Distance(tlast, tcurrent) * scale;
        last = current;
        tlast = tcurrent;

        AddToBuffer(angle);
        AddToTBuffer(translation);
        RAvg = rotationBuffer.Average();
        TAvg = translationBuffer.Average();

        if (RAvg == 0)
        {
            curAvg = TAvg * 33;
        } else if (TAvg == 0)
        {
            curAvg = RAvg;
        } else
        {
            // there could be a better way to avoid dips when rotating and translating at the same time
            curAvg = (RAvg + (TAvg * 33))/1.5f;
        }
    }

    void AddToBuffer(float val)
    {
        rbIndex = (rbIndex + 1) % rotationBuffer.Length;
        rotationBuffer[rbIndex] = val;
    }

    void AddToTBuffer(float val)
    {
        tbIndex = (tbIndex + 1) % translationBuffer.Length;
        translationBuffer[tbIndex] = val;
    }
}
