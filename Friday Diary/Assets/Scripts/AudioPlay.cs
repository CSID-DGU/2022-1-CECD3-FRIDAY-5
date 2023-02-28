using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    AudioSource audioSrc;
    private void Start() {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }
 public void Play(){
    if(audioSrc != null){
        audioSrc.Play();
    }
 }
}
