using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public static PlayAudio playAudio;

    private AudioManager AM;

    [SerializeField] private Character c_tempCharacter;

    private Dictionary<Character, CharacterInfo> D_cci_characterDictionary = new Dictionary<Character, CharacterInfo>();

    [SerializeField] private Character[] cA_characterArray = new Character[7];
    [SerializeField] private CharacterInfo[] ciA_characterInfoArray = new CharacterInfo[7];

    void Awake()
    {
        playAudio = this;
    }

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        for (int i = 0; i < cA_characterArray.Length; i++)
        {
            D_cci_characterDictionary.Add(cA_characterArray[i], ciA_characterInfoArray[i]);
        }
    }

    public void PlayVoice(Character c)
    {
        AM.PlayVoice(15, D_cci_characterDictionary[c]);
    }
}
