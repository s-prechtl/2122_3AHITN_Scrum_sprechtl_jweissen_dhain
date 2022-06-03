using UnityEngine;

public class Crop
{
    private const int DaysUntilFinished = 4;

    private bool _fullyGrown;
    public bool FullyGrown => _fullyGrown;

    private bool _markedForDeletion;
    public bool MarkedForDeletion => _markedForDeletion;

    private bool _dead;
    public bool IsDead => _dead;

    private bool _hydrated;

    private int _daysGrown;

    public Crop()
    {
        Debug.Log("Crop created");
        _fullyGrown = false;
        _markedForDeletion = false;
        _daysGrown = 0;
    }

    private void Grow()
    {
        Debug.Log("Crop grown");
        _daysGrown++;
    }

    public void DayLightStep(bool hydrated)
    {
        if (_daysGrown >= DaysUntilFinished)
        {
            _fullyGrown = true;
        }

        if (!hydrated)
        {
            _markedForDeletion = true;
        }
        else if (!_fullyGrown)
        {
            Grow();
        }
    }
}