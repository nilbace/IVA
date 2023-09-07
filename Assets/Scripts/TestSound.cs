using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    AudioSource audio;
    public AudioClip some;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip audioClip1;
    public AudioClip audioClip2;
    int i = 0;

    private void OnTriggerEnter(Collider other) {
        //audio.PlayOneShot(some);

        i++;

        if(i%2 == 0)
            Managers.Sound.Play(audioClip1, Define.Sound.Bgm);
        else
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
