using Journey.Domain.Enums;
using Journey.Domain.ValueObjects;

namespace Journey.Application.Dtos;

public record JourneyDto(
            Guid? Id,
            string StartLocation,
            DateTime StartTime,
            string ArrivalLocation,
            DateTime ArrivalTime,
            TransportType TransportType,
            DistanceKM DistanceKm);