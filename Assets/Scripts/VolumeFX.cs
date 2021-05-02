using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; 

public class VolumeFX : MonoBehaviour
{

    public Volume m_Volume;
    public Bloom bloom;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Volume = GetComponent<Volume>();
        m_Volume.profile.TryGet(out bloom);
    }

    // Update is called once per frame
    private float sinInc = 0;
    void Update()
    {
        sinInc += 0.5f;

        bloom.intensity.value = (float)Mathf.Abs(Mathf.Sin(sinInc / 15) * 2);
    }
}
