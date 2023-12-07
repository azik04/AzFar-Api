namespace Final.Domain.Entity;

public class StadiumPhotos
{
    public long? Id { get; set; }

    public string? PhotoPath { get; set; }

    public long? StadiumId { get; set; }
    public Stadium Stadium { get; set; }
}
