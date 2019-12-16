namespace undancer.Commons
{
    public static class KButtonExtensions
    {

        public static void SetText(this KButton button,string text)
        {
            button.GetComponentInChildren<LocText>().text = text;
        }

    }
}