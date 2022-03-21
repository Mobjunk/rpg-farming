using System.Collections.Generic;
using UnityEngine;

public class DrawFishingLine : MonoBehaviour
{
    private Vector3 _startPoint = Vector3.zero;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _fishOnPoint;
    private bool _fishOn = false;

    private LineRenderer _lineRenderer;
    private SpriteRenderer _spriteRenderer;
    private List<LineSegment> _ropeSegments = new List<LineSegment>();
    private float _lineSegLen = 0.25f;
    [SerializeField, Range(0, 35)] private int _segmentLength = 35;
    private float _lineWidth = 0.02f;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFishOn()
    {
        _fishOn = true;
        _spriteRenderer.enabled = false;
    }
    
    public void Draw(Vector3 pStartPoint, int pSegmentLength = 6)
    {
        _segmentLength = pSegmentLength;
        _startPoint = pStartPoint;

        for (int index = 0; index < _segmentLength; index++)
        {
            _ropeSegments.Add(new LineSegment(pStartPoint));
            pStartPoint.y -= _lineSegLen;
        }
    }

    void Update()
    {
        if(!_startPoint.Equals(Vector3.zero)) DrawLine();
    }

    private void FixedUpdate()
    {
        if(!_startPoint.Equals(Vector3.zero)) Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int index = 1; index < _segmentLength; index++)
        {
            LineSegment firstSegment = _ropeSegments[index];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            _ropeSegments[index] = firstSegment;
        }

        //CONSTRAINTS
        for (int index = 0; index < 50; index++)
            ApplyConstraint();
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        LineSegment firstSegment = _ropeSegments[0];
        firstSegment.posNow = _startPoint;
        _ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        LineSegment endSegment = _ropeSegments[_ropeSegments.Count - 1];
        endSegment.posNow = _fishOn ? _fishOnPoint.position : _endPoint.position;
        _ropeSegments[_ropeSegments.Count - 1] = endSegment;

        for (int index = 0; index < _segmentLength - 1; index++)
        {
            LineSegment firstSeg = _ropeSegments[index];
            LineSegment secondSeg = _ropeSegments[index + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - _lineSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > _lineSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < _lineSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (index != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                _ropeSegments[index] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                _ropeSegments[index + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                _ropeSegments[index + 1] = secondSeg;
            }
        }
    }

    private void DrawLine()
    {
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;

        Vector3[] ropePositions = new Vector3[_segmentLength];
        for (int index = 0; index < _segmentLength; index++)
            ropePositions[index] = _ropeSegments[index].posNow;

        _lineRenderer.positionCount = ropePositions.Length;
        _lineRenderer.SetPositions(ropePositions);
    }

    public struct LineSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public LineSegment(Vector2 pPosition)
        {
            posNow = pPosition;
            posOld = pPosition;
        }
    }
}