using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteControllerWI : MonoBehaviour
{
    public float scale = 1f;
    public float smoothing = 0.3f;
    public Volume volume;

    private Vignette vignette;
    private Quaternion last;
    private Vector3 tlast;
    private float lastAngle;
    private float lastTrans;
    private float avgAngle;
    private float avgTrans;
    public float curAvg;

    private void Start()
    {
        if (volume.profile.TryGet(out Vignette vign)) vignette = vign;
    }

    void Update()
    {
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
        var trans = Vector3.Distance(tlast, tcurrent) * scale;
        last = current;
        tlast = tcurrent;

        var weight = Mathf.Min(1, Time.deltaTime / smoothing);
        var lastWeight = 1 - weight;
        avgAngle = (lastAngle * lastWeight) + (angle * weight);
        avgTrans = (lastTrans * lastWeight) + (trans * weight);
        lastAngle = avgAngle;
        lastTrans = avgTrans;

        //Debug.Log(avgAngle);
        if (avgAngle == 0)
        {
            curAvg = avgTrans * 33;
        } else if (avgTrans == 0)
        {
            curAvg = avgAngle;
        } else
        {
            curAvg = (avgAngle + (avgTrans * 33)) / 2;
        }

        vignette.intensity.value = curAvg * scale;
    }

}
