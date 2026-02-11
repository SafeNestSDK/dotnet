namespace SafeNest;

/// <summary>
/// Severity levels for safety detections.
/// </summary>
public enum Severity
{
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Grooming risk assessment levels.
/// </summary>
public enum GroomingRisk
{
    None,
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Overall risk level for combined analysis.
/// </summary>
public enum RiskLevel
{
    Safe,
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Categories of safety risks.
/// </summary>
public enum RiskCategory
{
    Bullying,
    Grooming,
    Unsafe,
    SelfHarm,
    Other
}

/// <summary>
/// Types of safety analysis.
/// </summary>
public enum AnalysisType
{
    Bullying,
    Grooming,
    Unsafe,
    Emotions
}

/// <summary>
/// Emotional trend direction.
/// </summary>
public enum EmotionTrend
{
    Improving,
    Stable,
    Worsening
}

/// <summary>
/// Target audience for action plans.
/// </summary>
public enum Audience
{
    Child,
    Parent,
    Educator,
    Platform
}

/// <summary>
/// Incident tracking status.
/// </summary>
public enum IncidentStatus
{
    New,
    Reviewed,
    Resolved
}

/// <summary>
/// Role of a message sender in grooming detection.
/// </summary>
public enum MessageRole
{
    Adult,
    Child,
    Unknown
}

/// <summary>
/// Webhook event types.
/// </summary>
public enum WebhookEventType
{
    IncidentCritical,
    IncidentHigh,
    GroomingDetected,
    SelfHarmDetected,
    BullyingSevere
}

internal static class EnumExtensions
{
    public static string ToApiString(this Severity value) => value switch
    {
        Severity.Low => "low",
        Severity.Medium => "medium",
        Severity.High => "high",
        Severity.Critical => "critical",
        _ => "low"
    };

    public static string ToApiString(this GroomingRisk value) => value switch
    {
        GroomingRisk.None => "none",
        GroomingRisk.Low => "low",
        GroomingRisk.Medium => "medium",
        GroomingRisk.High => "high",
        GroomingRisk.Critical => "critical",
        _ => "none"
    };

    public static string ToApiString(this RiskLevel value) => value switch
    {
        RiskLevel.Safe => "safe",
        RiskLevel.Low => "low",
        RiskLevel.Medium => "medium",
        RiskLevel.High => "high",
        RiskLevel.Critical => "critical",
        _ => "safe"
    };

    public static string ToApiString(this Audience value) => value switch
    {
        Audience.Child => "child",
        Audience.Parent => "parent",
        Audience.Educator => "educator",
        Audience.Platform => "platform",
        _ => "parent"
    };

    public static string ToApiString(this MessageRole value) => value switch
    {
        MessageRole.Adult => "adult",
        MessageRole.Child => "child",
        MessageRole.Unknown => "unknown",
        _ => "unknown"
    };

    public static string ToApiString(this WebhookEventType value) => value switch
    {
        WebhookEventType.IncidentCritical => "incident.critical",
        WebhookEventType.IncidentHigh => "incident.high",
        WebhookEventType.GroomingDetected => "grooming.detected",
        WebhookEventType.SelfHarmDetected => "self_harm.detected",
        WebhookEventType.BullyingSevere => "bullying.severe",
        _ => "incident.critical"
    };

    public static Severity ParseSeverity(string? value) => value?.ToLowerInvariant() switch
    {
        "low" => Severity.Low,
        "medium" => Severity.Medium,
        "high" => Severity.High,
        "critical" => Severity.Critical,
        _ => Severity.Low
    };

    public static GroomingRisk ParseGroomingRisk(string? value) => value?.ToLowerInvariant() switch
    {
        "none" => GroomingRisk.None,
        "low" => GroomingRisk.Low,
        "medium" => GroomingRisk.Medium,
        "high" => GroomingRisk.High,
        "critical" => GroomingRisk.Critical,
        _ => GroomingRisk.None
    };

    public static RiskLevel ParseRiskLevel(string? value) => value?.ToLowerInvariant() switch
    {
        "safe" => RiskLevel.Safe,
        "low" => RiskLevel.Low,
        "medium" or "moderate" => RiskLevel.Medium,
        "high" => RiskLevel.High,
        "critical" => RiskLevel.Critical,
        _ => RiskLevel.Safe
    };

    public static EmotionTrend ParseEmotionTrend(string? value) => value?.ToLowerInvariant() switch
    {
        "improving" => EmotionTrend.Improving,
        "stable" => EmotionTrend.Stable,
        "worsening" => EmotionTrend.Worsening,
        _ => EmotionTrend.Stable
    };
}
