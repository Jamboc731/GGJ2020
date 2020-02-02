using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

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

    public static AudioManager x;

    [SerializeField] private AudioClip[] acA_Voice;
    [Space]
    [SerializeField] private AudioClip[] acA_Music;
    [SerializeField] private AudioClip[] acA_SFX;
    [SerializeField] private AudioClip[] acA_ButtonEffect = new AudioClip[4];

    [SerializeField] private AudioSource as_Voice;
    [SerializeField] private AudioSource as_SFX;
    [SerializeField] private AudioSource as_Music;

    private bool b_CanRepeatVoice;
    private bool b_VoiceCoroutine = true;

    private void Start()
    {
        x = this;
    }

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

    public void PlayRandomButtonSound()
    {

    }

    public void PlaySFX(int _ac_audioClipID)
    {
        as_SFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        as_SFX.clip = acA_SFX[_ac_audioClipID];
        as_SFX.PlayDelayed(UnityEngine.Random.Range(0.8f, 1.5f));
    }
    public void PlaySFX(int _ac_audioClipID, float _f_lowerRange, float _f_upperRange)
    {
        as_SFX.pitch = UnityEngine.Random.Range(_f_lowerRange, _f_upperRange);
        as_SFX.clip = acA_SFX[_ac_audioClipID];
        as_SFX.PlayDelayed(UnityEngine.Random.Range(_f_lowerRange, _f_upperRange));
    }
    public void PlaySFX(AudioClip _ac_audioClipID)
    {
        as_SFX.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        as_SFX.PlayOneShot(_ac_audioClipID);
    }
    public void PlaySFX(AudioClip _ac_audioClipID, float _f_lowerRange, float _f_upperRange)
    {
        as_SFX.pitch = UnityEngine.Random.Range(_f_lowerRange, _f_upperRange);
        as_SFX.PlayOneShot(_ac_audioClipID);
    }

    public void PlayVoice(int _i_voicesToPlay, CharacterInfo _cv_chosenVoice)
    {
        for (int i = 0; i < _i_voicesToPlay; i++)
        {
            StartCoroutine(PlayVoiceCoroutine(i - UnityEngine.Random.Range(_cv_chosenVoice.f_maxDelay, _cv_chosenVoice.f_minDelay) * i, _cv_chosenVoice));
        }

    }
    IEnumerator PlayVoiceCoroutine(float _f_RepeatTime, CharacterInfo _cv_chosenVoice)
    {
        yield return new WaitForSeconds(_f_RepeatTime);
        as_Voice.pitch = UnityEngine.Random.Range(_cv_chosenVoice.f_minPitch, _cv_chosenVoice.f_maxPitch);
        as_Voice.PlayOneShot(acA_Voice[_cv_chosenVoice.i_voiceClip]);
    }

}
