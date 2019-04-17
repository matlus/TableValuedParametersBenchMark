namespace ConsoleApp2
{
    public enum Genre
    {
        Action,
        Animation,
        Drama,
        Musical,
        [EnumDescription("Sci-Fi")]
        [EnumDescription("SciFi")]
        SciFi,
        Thriller
    }


}
