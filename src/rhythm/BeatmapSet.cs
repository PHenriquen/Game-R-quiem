using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RequiemEcosDoSilencio.Rhythm;

public sealed class BeatmapSetDocument
{
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("artist")]
    public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("audio")]
    public string AudioPath { get; set; } = string.Empty;

    [JsonPropertyName("cover")]
    public string CoverPath { get; set; } = string.Empty;

    [JsonPropertyName("previewTime")]
    public double PreviewTimeSeconds { get; set; }

    [JsonPropertyName("difficulties")]
    public List<BeatmapSetDifficulty> Difficulties { get; set; } = new();
}

public sealed class BeatmapSetDifficulty
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("chart")]
    public string ChartPath { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public double Rating { get; set; }
}

public static class BeatmapSetLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static BeatmapSetDocument Load(string path)
    {
        using FileAccess? file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file is null)
            throw new InvalidOperationException($"Could not open beatmap set: {path}. Godot error: {FileAccess.GetOpenError()}");

        BeatmapSetDocument? set = JsonSerializer.Deserialize<BeatmapSetDocument>(file.GetAsText(), JsonOptions);
        if (set is null)
            throw new InvalidOperationException($"Beatmap set JSON returned no document: {path}");

        IReadOnlyList<string> errors = Validate(set);
        if (errors.Count > 0)
            throw new InvalidOperationException($"Invalid beatmap set '{path}':\n- {string.Join("\n- ", errors)}");

        set.Difficulties = set.Difficulties.OrderBy(item => item.Rating).ToList();
        return set;
    }

    public static IReadOnlyList<string> Validate(BeatmapSetDocument set)
    {
        var errors = new List<string>();

        if (set.Version != 1)
            errors.Add($"Unsupported beatmap set version {set.Version}; expected 1.");
        if (string.IsNullOrWhiteSpace(set.Id))
            errors.Add("id is required.");
        if (string.IsNullOrWhiteSpace(set.Title))
            errors.Add("title is required.");
        if (string.IsNullOrWhiteSpace(set.AudioPath))
            errors.Add("audio is required.");
        if (set.PreviewTimeSeconds < 0.0)
            errors.Add("previewTime cannot be negative.");
        if (set.Difficulties.Count == 0)
            errors.Add("at least one difficulty is required.");

        var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (BeatmapSetDifficulty difficulty in set.Difficulties)
        {
            if (string.IsNullOrWhiteSpace(difficulty.Id))
                errors.Add("difficulty id is required.");
            else if (!ids.Add(difficulty.Id))
                errors.Add($"duplicate difficulty id '{difficulty.Id}'.");
            if (string.IsNullOrWhiteSpace(difficulty.Name))
                errors.Add($"difficulty '{difficulty.Id}' name is required.");
            if (string.IsNullOrWhiteSpace(difficulty.ChartPath))
                errors.Add($"difficulty '{difficulty.Id}' chart is required.");
            if (difficulty.Rating < 0.0 || difficulty.Rating > 10.0)
                errors.Add($"difficulty '{difficulty.Id}' rating must be between 0 and 10.");
        }

        return errors;
    }
}
