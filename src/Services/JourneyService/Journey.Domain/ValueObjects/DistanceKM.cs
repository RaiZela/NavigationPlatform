namespace Journey.Domain.ValueObjects;

public record DistanceKM
{
    public decimal Value { get; }
    public DistanceKM(decimal value)
    {
        if (value <= 0)
            throw new DomainException("Distance must be greater than zero.");
        Value = value;
    }

    public static DistanceKM Of(decimal value)
    {
        return new DistanceKM(value);
    }
}