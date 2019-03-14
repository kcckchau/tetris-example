using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip landShapeSound;
    public AudioClip moveSound;
    public AudioClip errorSound;
    public AudioClip gameOverSound;
    public AudioClip clearRowSound;
    public AudioClip[] bgmClips;
    public AudioClip[] vocalClips;
    public AudioClip gameOverVocal;
    public AudioClip levelUpVocal;


    public AudioSource bgm;

    public bool bgmEnabled = true;
    public bool fxEnabled = true;
    [Range(0,1)]
    public float bgmVolumne = 1.0f;
    [Range(0, 1)]
    public float fxVolumne = 1.0f;

    // Use this for initialization
    void Start()
    {
        PlayMusic(bgmClips[Random.Range(0, bgmClips.Length-1)]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayMusic(AudioClip clip)
    {
        if (bgmEnabled)
        {
            bgm.clip = clip;
            bgm.loop = true;
            bgm.volume = bgmVolumne;
            bgm.Play();
        }
    }

    void PlaySound(AudioClip clip)
    {
        if(fxEnabled)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void PlayMoveSound()
    {
        if(moveSound)
            PlaySound(moveSound);
    }

    public void PlayErrorSound()
    {
        PlaySound(errorSound);
    }

    public void PlayLandShapeSound()
    {
        PlaySound(landShapeSound);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }

    public void PlayGameOverVocal()
    {
        PlaySound(gameOverVocal);
    }

    public void PlayRowClearVocal()
    {
        PlaySound(vocalClips[Random.Range(0, 2)]);
    }
}
