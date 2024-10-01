using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] public AudioClip zombieDamage;

    public static AudioManager instance; 

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip);
    }
}
