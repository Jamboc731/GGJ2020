using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioManager AM;

    [SerializeField] private Character c_tempCharacter;

    private Dictionary<Character, CharacterInfo> D_cci_characterDictionary = new Dictionary<Character, CharacterInfo>();

    [SerializeField] private Character[] cA_characterArray = new Character[7];
    [SerializeField] private CharacterInfo[] ciA_characterInfoArray = new CharacterInfo[7];


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AM.PlayVoice(15, D_cci_characterDictionary[c_tempCharacter]);
            print("pressed space");
        }
    }

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        for (int i = 0; i < cA_characterArray.Length; i++)
        {
            D_cci_characterDictionary.Add(cA_characterArray[i], ciA_characterInfoArray[i]);
        }
    }
}
