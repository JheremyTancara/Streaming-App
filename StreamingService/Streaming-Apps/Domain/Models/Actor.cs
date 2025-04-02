namespace Api.Models;

public class Actor : Person
{
    public int ActorID { get; set; } 
    public List<string> Movies { get; set; } = new List<string>(); 

    public override string GetInfo()
    {
        return $"{base.GetInfo()}, Movies: {string.Join(", ", Movies)}";
    }
}