using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateTC;
using AudioTC;

public class Test : MonoBehaviour
{
    public ClipsData clip;
    private void Start()
    {
        AudioLocator.GetAudioPlayer().Play(clip);
    }
}
