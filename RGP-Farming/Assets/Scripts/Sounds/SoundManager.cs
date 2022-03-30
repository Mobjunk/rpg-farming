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
        Debug.Log($"{sounds.Count} sounds loaded.");
    }

    public void ExecuteSound(string pSoundName, int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        if(sounds.ContainsKey(pSoundName))
            sounds[pSoundName].HandleSound(pIntParameter, pAttachedObject);
        else Debug.LogError(pSoundName + " sound does not exist!");
    }
}