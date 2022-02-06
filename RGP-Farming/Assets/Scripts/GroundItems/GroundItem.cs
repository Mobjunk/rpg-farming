using System;
using UnityEngine;

[Serializable]
public class GroundItem
{
    public float DefaultTime = 30;
    public GameItem gameItem;
    public GameObject GameObject;
    public bool Respawn;
    public float CurrentTime;
    public State State;
    
    public GroundItem(GameItem pGameItem, GameObject pGameObject, bool pRespawn = false)
    {
        this.gameItem = pGameItem;
        this.GameObject = pGameObject;
        this.Respawn = pRespawn;
        if (!pRespawn) CurrentTime = DefaultTime;
        State = State.PUBLIC;
    }

    public GroundItem(GameItem pGameItem, GameObject pGameObject, State pState, bool pRespawn = false)
    {
        this.gameItem = pGameItem;
        this.GameObject = pGameObject;
        this.Respawn = pRespawn;
        if (!pRespawn) CurrentTime = DefaultTime;
        this.State = pState;
    }
}

public enum State
{
    HIDDEN,
    PUBLIC
}

