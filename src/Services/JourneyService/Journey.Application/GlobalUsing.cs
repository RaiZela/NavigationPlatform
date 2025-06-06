﻿global using BuildingBlocks.CQRS;
global using BuildingBlocks.Enums;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.IntegrationEvents.Events.FavoriteJourneyEvents;
global using BuildingBlocks.IntegrationEvents.Events.JourneyEvents;
global using BuildingBlocks.IntegrationEvents.Models.Journey;
global using BuildingBlocks.Pagination;
global using FluentValidation;
global using Journey.Application.Behaviors;
global using Journey.Application.Data.Interfaces;
global using Journey.Application.Data.Services.Auth;
global using Journey.Application.Dtos;
global using Journey.Application.Exceptions;
global using Journey.Application.Exceptionsl;
global using Journey.Application.Journeys.Queries.GetJourney;
global using Journey.Domain.Events;
global using Journey.Domain.Models;
global using Mapster;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.Reflection;
global using JourneyEntity = Journey.Domain.Models.Journey.Journey;

