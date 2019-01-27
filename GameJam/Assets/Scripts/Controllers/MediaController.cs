using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaController : MonoBehaviour
{
    public AudioSource mainTheme;
    public AudioSource chewing;
    public AudioSource gameOver;

    private void Start(){
        mainTheme.Play();
    }
    
    
}
