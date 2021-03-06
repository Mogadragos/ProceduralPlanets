// Stockage du maximum et minimum de valeurs fournies
public class MinMax
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMax()
    {
        Reset();
    }

    public void Reset()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float v)
    {
        if(v > Max)
        {
            Max = v;
        }
        if(v < Min)
        {
            Min = v;
        }
    }
}
