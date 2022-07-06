using System.Collections;

internal interface IImmortality
{
    public bool IsImmortal { get; set; }
    public float ImmortalityStartTime { get; }
    public float ImmortalityTimer { get; set; }

    public IEnumerator ProcessImmortality();

    public void ResetImmortality(bool resetTimer);
}
