using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RequiemEcosDoSilencio.Rhythm;

public sealed class BeatmapDocument
{
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("setId")]
    public string SetId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("artist")]
    public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = "Echo";

    [JsonPropertyName("bpm")]
    public double Bpm { get; set; } = 120.0;

    [JsonPropertyName("offset")]
    public double OffsetSeconds { get; set; }

    [JsonPropertyName("beatsPerBar")]
    public int BeatsPerBar { get; set; } = 4;

    [JsonPropertyName("duration")]
    public double DurationSeconds { get; set; }

    [JsonPropertyName("audio")]
    public string AudioPath { get; set; } = string.Empty;

    [JsonPropertyName("events")]
    public List<BeatmapEvent> Events { get; set; } = new();
}

public sealed class BeatmapEvent
{
    [JsonPropertyName("time")]
    public double TimeSeconds { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("lane")]
    public int Lane { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public static class BeatmapEventTypes
{
    public const string Pulse = "pulse";
    public const string EnemySpawn = "enemy_spawn";
    public const string EnemyTelegraph = "enemy_telegraph";
    public const string ArenaShift = "arena_shift";
    public const string CardWindow = "card_window";
    public const string Accent = "accent";
    public const string Checkpoint = "checkpoint";

    public static readonly HashSet<string> Known = new(StringComparer.OrdinalIgnoreCase)
    {
        Pulse,
        EnemySpawn,
        EnemyTelegraph,
        ArenaShift,
        CardWindow,
        Accent,
        Checkpoint
    };
}
