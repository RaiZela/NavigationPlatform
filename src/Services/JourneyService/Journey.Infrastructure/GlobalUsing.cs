global using Journey.Application.Data;
global using Journey.Domain.Abstractions;
global using Journey.Domain.Models;
global using Journey.Domain.ValueObjects;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;
global using System.Security.Claims;
global using JourneyEntity = Journey.Domain.Models.Journey;
global using Journey.Infrastructure.Data;
global using Journey.Infrastructure.Data.Interceptors;
global using Microsoft.Extensions.Configuration;

