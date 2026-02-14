using Tesseract.Core;
using UnityEngine;

namespace Tesseract.Audio
{
    [System.Serializable]
    public class WeightedAudioList
    {
        public WeightedAudioClip[] weightedItems;
        
        public int WeightSum
        {
            get
            {
                if (m_WeightSum < 0)
                    CalculateWeightSum();
                return m_WeightSum;
            }
        }

        protected int m_WeightSum = -1;

        public AudioClip GetWeightedSelectionOrNull()
        {
            if (weightedItems == null || weightedItems.Length == 0)
                return null;

            WeightedAudioClip item = weightedItems.SelectByWeight(WeightSum, t => t.weight);
            return item.clip;
        }

        protected void CalculateWeightSum()
        {
            m_WeightSum = 0;
            int count = weightedItems.Length;
            for (int i = 0; i < count; i++)
                m_WeightSum += weightedItems[i].weight;
        }
    }
}
