namespace Lab05.Models
{
    public class Reservation
    {
        public int Id { get; private set; }
        public int RoomId { get; private set; }
        public string OrganizerName { get; private set; }
        public string Topic { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public string Status { get; private set; }

        public Reservation(
            int id,
            int roomId,
            string organizerName,
            string topic,
            DateOnly date,
            TimeOnly startTime,
            TimeOnly endTime,
            string status)
        {
            Id = id;
            RoomId = roomId;
            OrganizerName = organizerName;
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }

        public void Update(
            int roomId,
            string organizerName,
            string topic,
            DateOnly date,
            TimeOnly startTime,
            TimeOnly endTime,
            string status)
        {
            RoomId = roomId;
            OrganizerName = organizerName;
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }
    }
}