using System.ComponentModel.DataAnnotations;

namespace Lab05.Models;

public class Room
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string BuildingCode { get; private set; }
    public int Floor { get; private set; }
    public int Capacity { get; private set; }
    public bool HasProjector { get; private set; }
    public bool IsActive { get; private set; }

    public Room(
        int id,
        string name,
        string buildingCode,
        int floor,
        int capacity,
        bool hasProjector,
        bool isActive)
    {
        Id = id;
        Name = name;
        BuildingCode = buildingCode;
        Floor = floor;
        Capacity = capacity;
        HasProjector = hasProjector;
        IsActive = isActive;
    }

    public void Update(
        string name,
        string buildingCode,
        int floor,
        int capacity,
        bool hasProjector,
        bool isActive)
    {
        Name = name;
        BuildingCode = buildingCode;
        Floor = floor;
        Capacity = capacity;
        HasProjector = hasProjector;
        IsActive = isActive;
    }
}