using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public enum Character
{
    john,
    bart,
    brett,
    liouse,
    nat,
    byron,
    jame
}


[Serializable]
public struct CharacterInfo
{
    public int i_voiceClip;
    public float f_minPitch;
    public float f_maxPitch;
    public float f_minDelay;
    public float f_maxDelay;
    public Sprite face;
}



public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] acA_Voice;
    [SerializeField] private AudioClip[] acA_Music;
    [SerializeField] private AudioClip[] acA_SFX;

    [SerializeField] private AudioSource as_Voice;
    [SerializeField] private AudioSource as_SFX;
    [SerializeField] private AudioSource as_Music;

    private bool b_CanRepeatVoice;
    private bool b_VoiceCoroutine = true;



    public void ToggleSFX()
    {
        as_SFX.volume = as_SFX.volume == 1 ? 0 : 1;
    }
    public void ToggleVoice()
    {
        as_Voice.volume = as_Voice.volume == 1 ? 0 : 1;
    }
    public void ToggleMusic()
    {
        as_Music.volume = as_Music.volume == 1 ? 0 : 1;
    }

    public void PlaySFX(int _ac_audioClipID)
    {
        as_SFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        as_SFX.clip = acA_SFX[_ac_audioClipID];
        as_SFX.PlayDelayed(UnityEngine.Random.Range(0.8f, 1.5f));
    }

    public void PlayVoice(int _ac_audioClipID, float _f_RepeatTime, int _i_voicesToPlay, CharacterInfo _cv_chosenVoice)
    {

        for (int i = 0; i < _i_voicesToPlay; i++)
        {
            StartCoroutine(PlayVoiceCoroutine(i - UnityEngine.Random.Range(_cv_chosenVoice.f_maxDelay, _cv_chosenVoice.f_minDelay) * i, _cv_chosenVoice));
        }

        /*
        if (b_VoiceCoroutine)
        {
            StartCoroutine(PlayVoiceCoroutine(_ac_audioClipID, _f_RepeatTime));
        }
        if (b_CanRepeatVoice)
        {
        as_Voice.clip = acA_Voice[_ac_audioClipID];
            as_Voice.pitch = Random.Range(0.6f, 1.4f);
            PlayVoice(_ac_audioClipID, _f_RepeatTime);
        }
        */
    }
    IEnumerator PlayVoiceCoroutine(float _f_RepeatTime, CharacterInfo _cv_chosenVoice)
    {
        yield return new WaitForSeconds(_f_RepeatTime);
        as_Voice.pitch = UnityEngine.Random.Range(_cv_chosenVoice.f_minPitch, _cv_chosenVoice.f_maxPitch);
        as_Voice.PlayOneShot(acA_Voice[_cv_chosenVoice.i_voiceClip]);
        /*
        b_CanRepeatVoice = true;
        b_VoiceCoroutine = false;
        PlayVoice(_ac_audioClipID, _f_RepeatTime);
        b_CanRepeatVoice = false;
        */
    }

}
