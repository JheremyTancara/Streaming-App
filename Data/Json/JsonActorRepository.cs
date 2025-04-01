using System.Text.Json;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Interface;

public class JsonActorRepository : IRepository<Actor, ActorDTO>
{
    private readonly string _filePath;

    public JsonActorRepository(string filePath)
    {
        _filePath = filePath;
        InitializeFile().Wait();
    }

    private async Task InitializeFile()
    {
        if (!File.Exists(_filePath))
        {
            var emptyActors = new List<Actor>();
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(emptyActors));
        }
    }

    public async Task<IEnumerable<Actor>> GetAllAsync() 
    {
        var jsonData = await File.ReadAllTextAsync(_filePath);
        if (string.IsNullOrWhiteSpace(jsonData))
        {
            return new List<Actor>();
        }
        return JsonSerializer.Deserialize<List<Actor>>(jsonData) ?? new List<Actor>();
    }

    public async Task<Actor?> GetByIdAsync(int id)
    {
        var actors = await GetAllAsync();
        return actors.FirstOrDefault(a => a.ActorID == id);
    }

    public async Task<Actor> CreateAsync(ActorDTO newActorDTO)
    {
        var actors = (await GetAllAsync()).ToList();
        var newActor = new Actor
        {
            ActorID = (actors.Any() ? actors.Max(a => a.ActorID) : 0) + 1,
            Name = newActorDTO.Name,
            Age = newActorDTO.Age,
            Bio = newActorDTO.Bio,
            ProfilePictureUrl = newActorDTO.ProfilePictureUrl
        };

        actors.Add(newActor);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(actors));
        return newActor;
    }

    public async Task Update(int id, ActorDTO actorDTO)
    {
        var actors = (await GetAllAsync()).ToList();
        var existingActor = actors.FirstOrDefault(a => a.ActorID == id);

        if (existingActor != null)
        {
            existingActor.Name = actorDTO.Name;
            existingActor.Age = actorDTO.Age;
            existingActor.Bio = actorDTO.Bio;
            existingActor.ProfilePictureUrl = actorDTO.ProfilePictureUrl;
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(actors));
        }
    }
}
