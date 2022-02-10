using System.Collections.Generic;
using UnityEngine;

public abstract class NpcInteraction : InteractionManager
{
    public virtual void OnTriggerEnter2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Add(this);
    }

    public virtual void OnTriggerExit2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Remove(this);
    }

    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        Npc npc = GetComponent<Npc>();
        CharacterContractManager characterContractManager = pCharacterManager.GetComponent<CharacterContractManager>();
        List<Contract> contracts = characterContractManager.GetListForNpcData(npc.NpcData);

        if (contracts.Count > 0) ContractManager.Instance().CompleteContract(contracts[0]);
        else HandleOthers();
    }

    public abstract void HandleOthers();
}
