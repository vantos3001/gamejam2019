using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerScene : MonoBehaviour
{
   public VideoPlayer VideoPlayer;
   
   private void Awake(){
      VideoPlayer = FindObjectOfType<VideoPlayer>();
   }

   private void Start(){
      VideoPlayer.Play();
   }
}
