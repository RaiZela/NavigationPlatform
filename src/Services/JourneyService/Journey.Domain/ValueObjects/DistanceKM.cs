namespace Journey.Domain.ValueObjects;

public record DistanceKM
{
    public decimal Value { get; }
    public DistanceKM(decimal value) => Value = value;

    public static DistanceKM Of(decimal value)
    {
        return new DistanceKM(value);
    }
}