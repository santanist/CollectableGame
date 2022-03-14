using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelected : MonoBehaviour
{
    AudioSource AudioSlctd;

    private void Start()
    {
        AudioSlctd = GetComponent<AudioSource>();
    }
    public void PlayAudioSlctd()
    {
        AudioSlctd.Play();
    }

}
