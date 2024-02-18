using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isPlaying = false;

    public void horn()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
            StartCoroutine(WaitForAudioFinish());
        }
    }

    private IEnumerator WaitForAudioFinish()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        isPlaying = false;
    }
}
