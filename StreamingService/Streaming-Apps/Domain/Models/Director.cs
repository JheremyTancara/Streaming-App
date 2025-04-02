namespace Api.Models;

public class Director : Person
{
    public int DirectorID { get; set; } 
    public int NumberOfAwards { get; set; }

    public override string GetInfo()
    {
        return $"{base.GetInfo()}, Awards: {NumberOfAwards}";
    }
}