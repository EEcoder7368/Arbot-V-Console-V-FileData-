using System.Collections.Concurrent;

namespace Arbot__V_Console___V_FileData_;

public static class ArbotWebHost
{
    private static readonly ConcurrentDictionary<string, bool> Sessions = new();

    public static void Run(string[] args)
    {
        var app = StartAsync(args).GetAwaiter().GetResult();
        app.WaitForShutdown();
    }

    public static async Task<WebApplication> StartAsync(string[] args)
    {
        Settings.settings(File.ReadAllText(Settings.Settings_path).Split(":"));
        ReloadTimetable();
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://127.0.0.1:5080");
        var app = builder.Build();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapPost("/api/login", (LoginRequest request) =>
        {
            if (request.Password != File.ReadAllText(Info.password_path)) return Results.Unauthorized();
            var token = Guid.NewGuid().ToString("N");
            Sessions[token] = true;
            return Results.Ok(new { token });
        });

        app.MapPost("/api/password", (HttpRequest request, ChangePasswordRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (data.NewPassword.Length < 4) return Results.BadRequest(new { message = "Use at least 4 characters." });
            if (data.NewPassword != data.ConfirmPassword) return Results.BadRequest(new { message = "The new passwords do not match." });
            if (data.CurrentPassword != File.ReadAllText(Info.password_path)) return Results.BadRequest(new { message = "The current password is incorrect." });
            File.WriteAllText(Info.password_path, data.NewPassword);
            Info.Password = data.NewPassword;
            return Results.Ok(new { message = "Password changed on this device." });
        });

        app.MapPost("/api/reset", (ResetRequest request) =>
        {
            if (request.Token != File.ReadAllText(Info.password_reset_path)) return Results.BadRequest(new { message = "That reset token is not valid." });
            var password = Window.Generate();
            File.WriteAllText(Info.password_path, password);
            return Results.Ok(new { password });
        });

        app.MapGet("/api/state", (HttpRequest request) => !IsAuthenticated(request) ? Results.Unauthorized() : Results.Ok(BuildState()));

        app.MapPost("/api/credits", (HttpRequest request, CounterRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (data.Amount <= 0) return Results.BadRequest(new { message = "Enter a positive number." });
            Info.Positives = ReadInt(Info.positives_path) + data.Amount;
            File.WriteAllText(Info.positives_path, Info.Positives.ToString());
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/negatives", (HttpRequest request, CounterRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (data.Amount <= 0) return Results.BadRequest(new { message = "Enter a positive number." });
            Info.Negatives = ReadInt(Info.negatives_path) + data.Amount;
            File.WriteAllText(Info.negatives_path, Info.Negatives.ToString());
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/profile", (HttpRequest request, ProfileRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (string.IsNullOrWhiteSpace(data.Name)) return Results.BadRequest(new { message = "Name cannot be empty." });
            Info.Name = data.Name.Trim();
            File.WriteAllText(Info.name_path, Info.Name);
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/settings", (HttpRequest request, SettingsRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            Settings.Fast_load = data.FastLoad;
            Settings.Name_or_master = data.UseName;
            Settings.Dark_mode = data.DarkMode;
            File.WriteAllText(Settings.Settings_path, $"{Settings.Fast_load}:{Settings.Name_or_master}:{Settings.Dark_mode}");
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/timetable", (HttpRequest request, TimetableRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (string.IsNullOrWhiteSpace(data.Day) || string.IsNullOrWhiteSpace(data.Lesson) || data.Period is < 1 or > 6)
                return Results.BadRequest(new { message = "Choose a weekday, a period from 1 to 6, and enter a lesson." });
            var path = data.Day.ToLowerInvariant() switch
            {
                "monday" => Timetable.monday_path,
                "tuesday" => Timetable.tuesday_path,
                "wednesday" => Timetable.wednesday_path,
                "thursday" => Timetable.thursday_path,
                "friday" => Timetable.friday_path,
                _ => string.Empty
            };
            if (path.Length == 0) return Results.BadRequest(new { message = "Choose a weekday." });
            var lessons = File.ReadAllText(path).Split(':');
            var lessonIndex = data.Period - 1;
            if (data.Period >= 5) lessonIndex++;
            lessons[lessonIndex] = data.Lesson.Trim();
            File.WriteAllText(path, string.Join(':', lessons));
            ReloadTimetable();
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/form", (HttpRequest request, FormRequest data) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            if (string.IsNullOrWhiteSpace(data.Room)) return Results.BadRequest(new { message = "Enter a form room." });
            File.WriteAllText(Timetable.form_path, data.Room.Trim());
            ReloadTimetable();
            return Results.Ok(BuildState());
        });

        app.MapPost("/api/timetable/clear", (HttpRequest request) =>
        {
            if (!IsAuthenticated(request)) return Results.Unauthorized();
            var emptyDay = "::::lunch time:::home time";
            foreach (var path in TimetablePaths()) File.WriteAllText(path, emptyDay);
            ReloadTimetable();
            return Results.Ok(BuildState());
        });

        await app.StartAsync();
        return app;
    }

    private static bool IsAuthenticated(HttpRequest request) => request.Headers.TryGetValue("X-Arbot-Session", out var token) && Sessions.ContainsKey(token.ToString());

    private static object BuildState()
    {
        Info.info(File.ReadAllText(Info.name_path), File.ReadAllText(Info.password_path), ReadInt(Info.positives_path), ReadInt(Info.negatives_path), File.ReadAllText(Info.password_reset_path));
        var now = DateTime.Now;
        var weekday = now.DayOfWeek;
        var lessons = weekday switch
        {
            DayOfWeek.Monday => WebDay(Timetable.Form, Timetable.Mon_p1, Timetable.Mon_p2, Timetable.Mon_p3, Timetable.Mon_p4, Timetable.Mon_p5, Timetable.Mon_p6),
            DayOfWeek.Tuesday => WebDay(Timetable.Form, Timetable.Tue_p1, Timetable.Tue_p2, Timetable.Tue_p3, Timetable.Tue_p4, Timetable.Tue_p5, Timetable.Tue_p6),
            DayOfWeek.Wednesday => WebDay(Timetable.Form, Timetable.Wed_p1, Timetable.Wed_p2, Timetable.Wed_p3, Timetable.Wed_p4, Timetable.Wed_p5, Timetable.Wed_p6),
            DayOfWeek.Thursday => WebDay(Timetable.Form, Timetable.Thu_p1, Timetable.Thu_p2, Timetable.Thu_p3, Timetable.Thu_p4, Timetable.Thu_p5, Timetable.Thu_p6),
            DayOfWeek.Friday => WebDay(Timetable.Form, Timetable.Fri_p1, Timetable.Fri_p2, Timetable.Fri_p3, Timetable.Fri_p4, Timetable.Fri_p5, Timetable.Fri_p6),
            _ => Array.Empty<string>()
        };
        var period = GetPeriod(now.TimeOfDay);
        var timetable = new
        {
            form = Timetable.Form,
            monday = WebDay(Timetable.Form, Timetable.Mon_p1, Timetable.Mon_p2, Timetable.Mon_p3, Timetable.Mon_p4, Timetable.Mon_p5, Timetable.Mon_p6),
            tuesday = WebDay(Timetable.Form, Timetable.Tue_p1, Timetable.Tue_p2, Timetable.Tue_p3, Timetable.Tue_p4, Timetable.Tue_p5, Timetable.Tue_p6),
            wednesday = WebDay(Timetable.Form, Timetable.Wed_p1, Timetable.Wed_p2, Timetable.Wed_p3, Timetable.Wed_p4, Timetable.Wed_p5, Timetable.Wed_p6),
            thursday = WebDay(Timetable.Form, Timetable.Thu_p1, Timetable.Thu_p2, Timetable.Thu_p3, Timetable.Thu_p4, Timetable.Thu_p5, Timetable.Thu_p6),
            friday = WebDay(Timetable.Form, Timetable.Fri_p1, Timetable.Fri_p2, Timetable.Fri_p3, Timetable.Fri_p4, Timetable.Fri_p5, Timetable.Fri_p6)
        };
        var settings = new { fastLoad = Settings.Fast_load, useName = Settings.Name_or_master, darkMode = Settings.Dark_mode };
        return new { name = Settings.Name_or_master ? Info.Name : "Master", positives = Info.Positives, negatives = Info.Negatives, day = weekday.ToString(), period, currentLesson = period > 0 && lessons.Length > period ? lessons[period] : "No lesson", timetable, settings };
    }

    private static int GetPeriod(TimeSpan time)
    {
        if (time >= TimeSpan.Parse("09:10") && time < TimeSpan.Parse("10:00")) return 1;
        if (time >= TimeSpan.Parse("10:00") && time < TimeSpan.Parse("10:50")) return 2;
        if (time >= TimeSpan.Parse("11:10") && time < TimeSpan.Parse("12:00")) return 3;
        if (time >= TimeSpan.Parse("12:00") && time < TimeSpan.Parse("12:50")) return 4;
        if (time >= TimeSpan.Parse("13:30") && time < TimeSpan.Parse("14:20")) return 5;
        if (time >= TimeSpan.Parse("14:20") && time < TimeSpan.Parse("15:10")) return 6;
        return 0;
    }

    private static string[] Day(params string[] lessons) => lessons;
    private static string[] WebDay(string form, string p1, string p2, string p3, string p4, string p5, string p6) => new[] { form, p1, p2, "Breaktime", p3, p4, p5, p6 };
    private static string[] TimetablePaths() => new[]
    {
        Timetable.monday_path,
        Timetable.tuesday_path,
        Timetable.wednesday_path,
        Timetable.thursday_path,
        Timetable.friday_path
    };
    private static int ReadInt(string path) => int.TryParse(File.ReadAllText(path), out var value) ? value : 0;
    private static void ReloadTimetable() => Timetable.timetable(File.ReadAllText(Timetable.form_path), File.ReadAllText(Timetable.monday_path).Split(":"), File.ReadAllText(Timetable.tuesday_path).Split(":"), File.ReadAllText(Timetable.wednesday_path).Split(":"), File.ReadAllText(Timetable.thursday_path).Split(":"), File.ReadAllText(Timetable.friday_path).Split(":"));

    public record LoginRequest(string Password);
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword, string ConfirmPassword);
    public record ResetRequest(string Token);
    public record CounterRequest(int Amount);
    public record ProfileRequest(string Name);
    public record SettingsRequest(bool FastLoad, bool UseName, bool DarkMode);
    public record TimetableRequest(string Day, int Period, string Lesson);
    public record FormRequest(string Room);
}