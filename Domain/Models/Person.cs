namespace Api.Models;

public abstract class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;

    public virtual string GetInfo()
    {
        return $"{Name}, Age: {Age}, Bio: {Bio}";
    }
}