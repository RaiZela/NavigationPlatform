﻿using System.Text.Json.Serialization;

namespace NotificationService.Enum;

//TODO ELEMINATE CODE MORE THAN ONCE
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransportType
{
    None = 0,
    Car = 1,
    Bus = 2,
    Train = 3,
    Ferry = 4,
    Tram = 5,
    Air = 6
}
