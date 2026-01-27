[System.Serializable]
public class StatusEffect
{
    public StatusType type;
    public int value;
    public int duration;

    public StatusEffect(StatusType type, int value, int duration)
    {
        this.type = type;
        this.value = value;
        this.duration = duration;
    }
}
