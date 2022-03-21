using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, Sounds> sounds = new Dictionary<string, Sounds>();

    private void Awake()
    {
        IEnumerable<Sounds> allSounds = typeof(Sounds).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Sounds)) && !t.IsAbstract).Select(t => (Sounds)Activator.CreateInstance(t));
        foreach (Sounds sound in allSounds)
        {
            Debug.Log($"Added {sound.SoundName()} as a sound.");
            sounds.Add(sound.SoundName(), sound);
        }
    }

    public void ExecuteSound(string pSoundName, int pIntParamenter = 0)
    {
        if(sounds.ContainsKey(pSoundName))
            sounds[pSoundName].HandleSound(pIntParamenter);
        else Debug.LogError(pSoundName + " sound does not exist!");
    }
}