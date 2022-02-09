using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Contract", menuName = "Contracts/New Contract")]
public class AbstractContractData : ScriptableObject
{
    /// <summary>
    /// The type of contract
    /// </summary>
    public ContractTypes contractType;
    /// <summary>
    /// The difficulty of the contract
    /// </summary>
    public ContractDifficultys contractDifficulty;
    /// <summary>
    /// The item that has to be collected thats linked to this contract
    /// </summary>
    public AbstractItemData linkedItem;
    /// <summary>
    /// The npc that gives the contract
    /// </summary>
    public NpcData linkedNpc;
    /// <summary>
    /// The rewards the player will get from this contract
    /// </summary>
    public List<GameItem> rewards = new List<GameItem>();
    /// <summary>
    /// The amount of coins a contract should give
    /// </summary>
    [Header("Only use these variables if you want the contract to give coins")]
    public bool receiveCoins = false;
    public int minCoins;
    public int maxCoins;
}