namespace CardGame
{
    public static class StringExtensions
    {
        public static string Declone(this string name) => name.Replace("(Clone)", "");
    }
}