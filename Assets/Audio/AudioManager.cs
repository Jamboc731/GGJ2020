using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] acA_Voice;
    [SerializeField] private AudioClip[] acA_Music;
    [SerializeField] private AudioClip[] acA_SFX;

    [SerializeField] private AudioSource as_Voice;
    [SerializeField] private AudioSource as_SFX;
    [SerializeField] private AudioSource as_Music;

    private bool b_CanRepeatVoice;
    private bool b_VoiceCoroutine;

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
        as_SFX.pitch = Random.Range(0.7f, 1.3f);
        as_SFX.clip = acA_SFX[_ac_audioClipID];
        as_SFX.PlayDelayed(Random.Range(0.8f, 1.5f));
    }

    public void PlayVoice(int _ac_audioClipID, float _f_RepeatTime)
    {
        if (b_VoiceCoroutine)
        {
            StartCoroutine(PlayVoiceCoroutine(_ac_audioClipID, _f_RepeatTime));
        }
        if (b_CanRepeatVoice)
        {
            as_Voice.pitch = Random.Range(0.6f, 1.4f);
            as_Voice.clip = acA_Voice[_ac_audioClipID];
            as_Voice.PlayDelayed(Random.Range(0.8f, 1.5f));
            PlayVoice(_ac_audioClipID, _f_RepeatTime);
        }
    }
    IEnumerator PlayVoiceCoroutine(int _ac_audioClipID, float _f_RepeatTime)
    {
        b_CanRepeatVoice = true;
        b_VoiceCoroutine = false;
        PlayVoice(_ac_audioClipID, _f_RepeatTime);
        yield return new WaitForSeconds(_f_RepeatTime);
        b_CanRepeatVoice = false;
        b_VoiceCoroutine = true;
    }

}
