namespace DefaultNamespace
{
    public class Crop
    {
        private const int DaysUntilFinished = 4;

        private bool _fullyGrown;
        public bool FullyGrown => _fullyGrown;
        
        private bool _markedForDeletion;
        public bool MarkedForDeletion => _markedForDeletion;

        private int _daysGrown;

        public Crop()
        {
            _fullyGrown = false;
            _markedForDeletion = false;
            _daysGrown = 0;
        }

        private void Grow()
        {
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
}