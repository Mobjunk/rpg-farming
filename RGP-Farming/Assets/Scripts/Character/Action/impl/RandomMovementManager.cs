using UnityEngine;

public class RandomMovementManager : CharacterAction
{
    /// <summary>
    /// How long the npc needs to walk into the same direction
    /// </summary>
    private float _movementTime = 0;
    /// <summary>
    /// Start position of the npc
    /// </summary>
    private Vector3 _startPosition;
    /// <summary>
    /// The current direction the npc is walking int
    /// </summary>
    private Vector2 _walkingDirection;
    /// <summary>
    /// The npc attached to the random walking
    /// </summary>
    private Npc _npc;
    /// <summary>
    /// Debug the positions and distance
    /// </summary>
    bool _debug = false;

    public RandomMovementManager(CharacterManager pCharacterManager) : base(pCharacterManager)
    {
        _npc = (Npc) pCharacterManager;
        _startPosition = _npc.transform.position;
    }

    public override void Update()
    {
        base.Update();

        if (_movementTime > 0)
        {
            
            Vector3 currentPosition = _npc.transform.position;
            Vector2 newPosition = new Vector2(currentPosition.x + _walkingDirection.x, currentPosition.y + _walkingDirection.y);
            float distance = Vector2.Distance(_startPosition, newPosition);
            bool outsideWalkingRadius = distance > _npc.NpcData.walkingRandius;

            if (_debug)
            {
                Debug.Log("startPosition: " + _startPosition);
                Debug.Log("newPos: " + newPosition);
                Debug.LogError(outsideWalkingRadius + ", " + distance);
            }

            Vector2 dir = _walkingDirection;
            
            if (outsideWalkingRadius) dir = Vector2.zero;
            
            CharacterManager.CharacterMovementMananger.Move(dir);
            _movementTime -= Time.deltaTime;
        } else if (_movementTime <= 0)
        {
            _walkingDirection = GetRandomDirection();
            _movementTime = 1;
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