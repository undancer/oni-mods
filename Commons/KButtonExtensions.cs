namespace undancer.Commons
{
    public static class KButtonExtensions
    {
        public static void SetText(this KButton button, LocString text)
        {
            button.GetComponentInChildren<LocText>().SetText(text);
        }
    }
}