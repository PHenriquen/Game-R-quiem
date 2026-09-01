using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RequiemEcosDoSilencio.Rhythm;

public static class BeatmapLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static BeatmapDocument Load(string path)
    {
        using FileAccess? file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file is null)
            throw new InvalidOperationException($"Could not open beatmap: {path}. Godot error: {FileAccess.GetOpenError()}");

        string json = file.GetAsText();
        BeatmapDocument? map = JsonSerializer.Deserialize<BeatmapDocument>(json, JsonOptions);
        if (map is null)
            throw new InvalidOperationException($"Beatmap JSON returned no document: {path}");

        IReadOnlyList<string> errors = Validate(map);
        if (errors.Count > 0)
            throw new InvalidOperationException($"Invalid beatmap '{path}':\n- {string.Join("\n- ", errors)}");

        map.Events = map.Events.OrderBy(item => item.TimeSeconds).ToList();
        return map;
    }

    public static IReadOnlyList<string> Validate(BeatmapDocument map)
    {
        var errors = new List<string>();

        if (map.Version != 1)
            errors.Add($"Unsupported beatmap version {map.Version}; expected 1.");
        if (string.IsNullOrWhiteSpace(map.Id))
            errors.Add("id is required.");
        if (string.IsNullOrWhiteSpace(map.Title))
            errors.Add("title is required.");
        if (map.Bpm < 40.0 || map.Bpm > 300.0)
            errors.Add("bpm must be between 40 and 300.");
        if (map.BeatsPerBar < 1 || map.BeatsPerBar > 12)
            errors.Add("beatsPerBar must be between 1 and 12.");
        if (map.DurationSeconds <= 0.0)
            errors.Add("duration must be greater than zero.");

        double lastTime = -1.0;
        for (int i = 0; i < map.Events.Count; i++)
        {
            BeatmapEvent item = map.Events[i];
            if (item.TimeSeconds < 0.0)
                errors.Add($"events[{i}].time cannot be negative.");
            if (item.TimeSeconds > map.DurationSeconds + 0.001)
                errors.Add($"events[{i}].time exceeds duration.");
            if (string.IsNullOrWhiteSpace(item.Type))
                errors.Add($"events[{i}].type is required.");
            else if (!BeatmapEventTypes.Known.Contains(item.Type))
                errors.Add($"events[{i}].type '{item.Type}' is unknown.");
            if (item.TimeSeconds + 0.000001 < lastTime)
                errors.Add("events must be ordered by time.");

            lastTime = item.TimeSeconds;
        }

        return errors;
    }
}
