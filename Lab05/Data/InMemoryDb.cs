using Lab05.Models;

namespace Lab05.Data;

public class InMemoryDb
{
    public static List<Room> Rooms { get; } =
    [
        new Room(1, "Conference A1", "A", 1, 12, true, true),
        new Room(2, "Lab B204", "B", 2, 24, true, true),
        new Room(3, "Meeting C101", "C", 1, 8, false, true),
        new Room(4, "Auditorium A3", "A", 3, 60, true, true),
        new Room(5, "Room B105", "B", 1, 16, false, false)
    ];

    public static List<Reservation> Reservations { get; } =
    [
        new Reservation(1, 1, "Jan Nowak", "Sprint Planning", new DateOnly(2026, 5, 10), new TimeOnly(9, 0), new TimeOnly(10, 0), "planned"),
        new Reservation(2, 2, "Anna Kowalska", "REST Workshop", new DateOnly(2026, 5, 10), new TimeOnly(10, 0), new TimeOnly(12, 30), "confirmed"),
        new Reservation(3, 3, "Piotr Zielinski", "Recruitment Meeting", new DateOnly(2026, 5, 11), new TimeOnly(11, 0), new TimeOnly(12, 0), "cancelled"),
        new Reservation(4, 1, "Maria Wisniewska", "Architecture Review", new DateOnly(2026, 5, 12), new TimeOnly(14, 0), new TimeOnly(15, 30), "confirmed"),
        new Reservation(5, 4, "Tomasz Lewandowski", "Company Presentation", new DateOnly(2026, 5, 15), new TimeOnly(13, 0), new TimeOnly(15, 0), "planned")
    ];
}