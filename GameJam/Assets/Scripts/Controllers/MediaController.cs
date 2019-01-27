using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaController : MonoBehaviour
{
    public AudioSource AudioSource;

    private void Start(){
        AudioSource.Play();
    }
}
