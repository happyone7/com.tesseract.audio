namespace Tesseract.Audio
{
    public class SoundCategory
    {
        public string Name;
        public int MaxAudible;

        public SoundCategory(string name, int maxAudible)
        {
            Name = name;
            MaxAudible = maxAudible;
        }
    }
}
