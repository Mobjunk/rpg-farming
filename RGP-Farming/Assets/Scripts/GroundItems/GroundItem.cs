using System;
using UnityEngine;

[Serializable]
public class GroundItem
{
    public float defaultTime = 30;
    public Item item;
    public GameObject gameObject;
    public bool respawn;
    public float currentTime;
    public State state;
    
    public GroundItem(Item item, GameObject gameObject, bool respawn = false)
    {
        this.item = item;
        this.gameObject = gameObject;
        this.respawn = respawn;
        if (!respawn) currentTime = defaultTime;
        state = State.PUBLIC;
    }

    public GroundItem(Item item, GameObject gameObject, State state, bool respawn = false)
    {
        this.item = item;
        this.gameObject = gameObject;
        this.respawn = respawn;
        if (!respawn) currentTime = defaultTime;
        this.state = state;
    }
}

public enum State
{
    HIDDEN,
    PUBLIC
}

