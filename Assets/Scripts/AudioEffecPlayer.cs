using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffecPlayer : MonoSingleton<AudioEffecPlayer>
{

    public List<AudioSource> sources;
   public void PlayAudio(int i)
    {
        if(i > sources.Count) 
        {
            Debug.Log("Bad audio index");
            return;
        }
        sources[i].Play();


    }
}
