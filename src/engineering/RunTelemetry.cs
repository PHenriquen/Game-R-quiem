using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Requiem.Engineering;

/// <summary>
/// Local-only gameplay telemetry for balancing and profiling runs.
/// No network transport is included: data stays on the developer/player
/// machine unless a future opt-in export is explicitly implemented.
/// </summary>
public sealed class RunTelemetry
{
    private readonly List<TelemetryEvent> _events = new();
    private readonly DateTimeOffset _startedAt = DateTimeOffset.UtcNow;

    public IReadOnlyList<TelemetryEvent> Events => _events;

    public void Record(string type, IReadOnlyDictionary<string, object?>? data = null)
    {
        _events.Add(new TelemetryEvent(
            TimestampUtc: DateTimeOffset.UtcNow,
            Type: type,
            Data: data is null ? new Dictionary<string, object?>() : new Dictionary<string, object?>(data)
        ));
    }

    public void RecordCombat(string action, bool perfect, double damage, string? target = null)
    {
        Record("combat", new Dictionary<string, object?>
        {
            ["action"] = action,
            ["perfect"] = perfect,
            ["damage"] = damage,
            ["target"] = target,
        });
    }

    public void RecordCadence(string rank, int streak)
    {
        Record("cadence", new Dictionary<string, object?>
        {
            ["rank"] = rank,
            ["streak"] = streak,
        });
    }

    public void RecordRoom(string roomId, double durationSeconds, int damageTaken)
    {
        Record("room", new Dictionary<string, object?>
        {
            ["room_id"] = roomId,
            ["duration_seconds"] = durationSeconds,
            ["damage_taken"] = damageTaken,
        });
    }

    public void ExportJson(string path)
    {
        var snapshot = new RunSnapshot(
            StartedAtUtc: _startedAt,
            EndedAtUtc: DateTimeOffset.UtcNow,
            Events: _events
        );

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(path, JsonSerializer.Serialize(snapshot, new JsonSerializerOptions
        {
            WriteIndented = true,
        }));
    }
}

public sealed record TelemetryEvent(
    DateTimeOffset TimestampUtc,
    string Type,
    Dictionary<string, object?> Data
);

public sealed record RunSnapshot(
    DateTimeOffset StartedAtUtc,
    DateTimeOffset EndedAtUtc,
    IReadOnlyList<TelemetryEvent> Events
);
