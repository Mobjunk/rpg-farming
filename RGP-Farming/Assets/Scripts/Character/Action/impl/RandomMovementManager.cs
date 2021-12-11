using UnityEngine;

public class RandomMovementManager : CharacterAction
{
    /// <summary>
    /// How long the npc needs to walk into the same direction
    /// </summary>
    private float movementTime = 0;
    /// <summary>
    /// Start position of the npc
    /// </summary>
    private Vector3 startPosition;
    /// <summary>
    /// The current direction the npc is walking int
    /// </summary>
    private Vector2 walkingDirection;
    /// <summary>
    /// The npc attached to the random walking
    /// </summary>
    private Npc npc;
    /// <summary>
    /// Debug the positions and distance
    /// </summary>
    bool debug = false;

    public RandomMovementManager(CharacterManager characterManager) : base(characterManager)
    {
        npc = (Npc) characterManager;
        startPosition = npc.transform.position;
    }

    public override void Update()
    {
        base.Update();

        if (movementTime > 0)
        {
            
            Vector3 currentPosition = npc.transform.position;
            Vector2 newPosition = new Vector2(currentPosition.x + walkingDirection.x, currentPosition.y + walkingDirection.y);
            float distance = Vector2.Distance(startPosition, newPosition);
            bool outsideWalkingRadius = distance > npc.NpcData.walkingRandius;

            if (debug)
            {
                Debug.Log("startPosition: " + startPosition);
                Debug.Log("newPos: " + newPosition);
                Debug.LogError(outsideWalkingRadius + ", " + distance);
            }

            Vector2 dir = walkingDirection;
            
            if (outsideWalkingRadius) dir = Vector2.zero;
            
            CharacterManager.CharacterMovementMananger.Move(dir);
            movementTime -= Time.deltaTime;
        } else if (movementTime <= 0)
        {
            walkingDirection = GetRandomDirection();
            movementTime = 1;
        }
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    /// <summary>
    /// Handles grabbing a random walking direction
    /// </summary>
    /// <returns>Random walking direction</returns>
    private Vector2 GetRandomDirection()
    {
        //Has a 4 in 10 chance to choose a random walking direction
        //Else the npc will idle
        int randomRoll = Random.Range(0, 11);
        switch (randomRoll)
        {
            case 0:
                return new Vector2(0, -1);
            case 1:
                return new Vector2(-1, 0);
            case 2:
                return new Vector2(1, 0);
            case 3:
                return new Vector2(0, 1);
        }
        return Vector2.zero;
    }
}