using UnityEngine;
using System;
using System.Collections.Generic;

public class PathRequestManager : Singleton<PathRequestManager>
{
    private Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();

    private PathRequest _currentPathRequest;

    private PathfinderManager _pathfinderManager;

    private bool _isProcessingPath;

    private void Awake()
    {
        _pathfinderManager = GetComponent<PathfinderManager>();
    }

    public void RequestPath(Vector3 pPathStart, Vector3 pPathEnd, Action<Vector2[], bool> pCallback)
    {
        PathRequest newRequest = new PathRequest(pPathStart, pPathEnd, pCallback);
        
        _pathRequestQueue.Enqueue(newRequest);
        TryProcessNext();
    }

    private void TryProcessNext()
    {
        if (!_isProcessingPath && _pathRequestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinderManager.StartFindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }
    }

    public void FinishedProcessingPath(Vector2[] pPath, bool pSuccess)
    {
        _currentPathRequest.Callback(pPath, pSuccess);
        _isProcessingPath = false;
        TryProcessNext();
    }
}

public class PathRequest
{
    public Vector3 PathStart;
    public Vector3 PathEnd;
    public Action<Vector2[], bool> Callback;

    public PathRequest(Vector3 pPathStart, Vector3 pPathEnd, Action<Vector2[], bool> pCallback)
    {
        PathStart = pPathStart;
        PathEnd = pPathEnd;
        Callback = pCallback;
    }
}