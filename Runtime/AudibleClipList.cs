namespace Tesseract.Audio
{
    public class AudibleClipList
    {
        public readonly int maxLength;
        private int m_CurrentLength;
        private float[] m_AudibleEndTimes;

        public AudibleClipList(int length)
        {
            maxLength = length;
            m_AudibleEndTimes = new float[length];
        }

        public void UpdateAudibles(float currentTime)
        {
            if (m_CurrentLength == 0)
                return;

            m_CurrentLength = 0;
            for (int i = 0; i < m_AudibleEndTimes.Length; i++)
            {
                if (m_AudibleEndTimes[i] <= currentTime)
                    m_AudibleEndTimes[i] = 0;
                else
                    m_CurrentLength++;
            }
        }

        public bool IsFull()
        {
            return m_CurrentLength >= maxLength;
        }

        public void AddClip(float endTime)
        {
            if (IsFull())
                return;

            for (int i = 0; i < m_AudibleEndTimes.Length; i++)
            {
                if (m_AudibleEndTimes[i] == 0)
                {
                    m_CurrentLength++;
                    m_AudibleEndTimes[i] = endTime;
                    return;
                }
            }
        }
    }
}
