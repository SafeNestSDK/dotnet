using System.Text.Json.Serialization;

namespace SafeNest;

// =============================================================================
// Configuration
// =============================================================================

/// <summary>
/// Configuration options for the SafeNest client.
/// </summary>
public class SafeNestOptions
{
    /// <summary>Request timeout in milliseconds (default: 30000, range: 1000-120000).</summary>
    public int Timeout { get; set; } = 30_000;

    /// <summary>Number of retry attempts for transient failures (default: 3, range: 0-10).</summary>
    public int Retries { get; set; } = 3;

    /// <summary>Initial retry delay in milliseconds (default: 1000).</summary>
    public int RetryDelay { get; set; } = 1_000;

    /// <summary>API base URL (default: https://api.safenest.dev).</summary>
    public string BaseUrl { get; set; } = "https://api.safenest.dev";
}

// =============================================================================
// Context
// =============================================================================

/// <summary>
/// Optional context for analysis requests.
/// </summary>
public class AnalysisContext
{
    /// <summary>Language code (e.g. "en").</summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>Age group (e.g. "7-10", "11-13", "14-17").</summary>
    [JsonPropertyName("age_group")]
    public string? AgeGroup { get; set; }

    /// <summary>Relationship between participants (e.g. "classmates").</summary>
    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    /// <summary>Platform name (e.g. "Discord", "chat").</summary>
    [JsonPropertyName("platform")]
    public string? Platform { get; set; }
}

// =============================================================================
// Messages
// =============================================================================

/// <summary>
/// A message in a grooming detection conversation.
/// </summary>
public class GroomingMessage
{
    /// <summary>Role of the sender (adult, child, or unknown).</summary>
    public MessageRole Role { get; set; }

    /// <summary>Message content.</summary>
    public string Content { get; set; } = "";

    public GroomingMessage() { }

    public GroomingMessage(MessageRole role, string content)
    {
        Role = role;
        Content = content;
    }
}

/// <summary>
/// A message for emotion analysis.
/// </summary>
public class EmotionMessage
{
    public string Sender { get; set; } = "";
    public string Content { get; set; } = "";

    public EmotionMessage() { }

    public EmotionMessage(string sender, string content)
    {
        Sender = sender;
        Content = content;
    }
}

/// <summary>
/// A message for incident reports.
/// </summary>
public class ReportMessage
{
    public string Sender { get; set; } = "";
    public string Content { get; set; } = "";

    public ReportMessage() { }

    public ReportMessage(string sender, string content)
    {
        Sender = sender;
        Content = content;
    }
}

// =============================================================================
// Input Types
// =============================================================================

/// <summary>
/// Input for bullying detection.
/// </summary>
public class DetectBullyingInput
{
    /// <summary>Text content to analyze (max 50KB).</summary>
    public string Content { get; set; } = "";
    public AnalysisContext? Context { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for grooming detection.
/// </summary>
public class DetectGroomingInput
{
    /// <summary>Conversation messages to analyze (max 100).</summary>
    public List<GroomingMessage> Messages { get; set; } = new();
    /// <summary>Age of the child in the conversation.</summary>
    public int? ChildAge { get; set; }
    public AnalysisContext? Context { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for unsafe content detection.
/// </summary>
public class DetectUnsafeInput
{
    /// <summary>Text content to analyze (max 50KB).</summary>
    public string Content { get; set; } = "";
    public AnalysisContext? Context { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for combined safety analysis.
/// </summary>
public class AnalyzeInput
{
    /// <summary>Text content to analyze (max 50KB).</summary>
    public string Content { get; set; } = "";
    public AnalysisContext? Context { get; set; }
    /// <summary>Which checks to run: "bullying", "unsafe", "grooming" (default: bullying + unsafe).</summary>
    public List<string>? Include { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for emotion analysis.
/// </summary>
public class AnalyzeEmotionsInput
{
    /// <summary>Single text content to analyze.</summary>
    public string? Content { get; set; }
    /// <summary>Conversation messages to analyze.</summary>
    public List<EmotionMessage>? Messages { get; set; }
    public AnalysisContext? Context { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for action plan generation.
/// </summary>
public class GetActionPlanInput
{
    /// <summary>Description of the safety situation.</summary>
    public string Situation { get; set; } = "";
    /// <summary>Age of the child involved.</summary>
    public int? ChildAge { get; set; }
    /// <summary>Target audience for the plan (default: Parent).</summary>
    public Audience Audience { get; set; } = Audience.Parent;
    /// <summary>Severity of the situation.</summary>
    public Severity? Severity { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Input for incident report generation.
/// </summary>
public class GenerateReportInput
{
    /// <summary>Messages involved in the incident (max 100).</summary>
    public List<ReportMessage> Messages { get; set; } = new();
    /// <summary>Age of the child involved.</summary>
    public int? ChildAge { get; set; }
    /// <summary>Type of incident (e.g. "harassment").</summary>
    public string? IncidentType { get; set; }
    /// <summary>Your unique identifier for correlation with your systems (max 255 chars).</summary>
    public string? ExternalId { get; set; }
    /// <summary>Your end-customer identifier for multi-tenant / B2B2C routing (max 255 chars).</summary>
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

// =============================================================================
// Result Types
// =============================================================================

/// <summary>
/// Result of bullying detection.
/// </summary>
public class BullyingResult
{
    [JsonPropertyName("is_bullying")]
    public bool IsBullying { get; set; }

    [JsonPropertyName("bullying_type")]
    public List<string> BullyingType { get; set; } = new();

    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }

    [JsonPropertyName("severity")]
    public string SeverityRaw { get; set; } = "low";

    [JsonIgnore]
    public Severity Severity => EnumExtensions.ParseSeverity(SeverityRaw);

    [JsonPropertyName("rationale")]
    public string Rationale { get; set; } = "";

    [JsonPropertyName("recommended_action")]
    public string RecommendedAction { get; set; } = "";

    [JsonPropertyName("risk_score")]
    public double RiskScore { get; set; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of grooming detection.
/// </summary>
public class GroomingResult
{
    [JsonPropertyName("grooming_risk")]
    public string GroomingRiskRaw { get; set; } = "none";

    [JsonIgnore]
    public GroomingRisk GroomingRisk => EnumExtensions.ParseGroomingRisk(GroomingRiskRaw);

    [JsonPropertyName("flags")]
    public List<string> Flags { get; set; } = new();

    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }

    [JsonPropertyName("rationale")]
    public string Rationale { get; set; } = "";

    [JsonPropertyName("risk_score")]
    public double RiskScore { get; set; }

    [JsonPropertyName("recommended_action")]
    public string RecommendedAction { get; set; } = "";

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of unsafe content detection.
/// </summary>
public class UnsafeResult
{
    [JsonPropertyName("unsafe")]
    public bool Unsafe { get; set; }

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = new();

    [JsonPropertyName("severity")]
    public string SeverityRaw { get; set; } = "low";

    [JsonIgnore]
    public Severity Severity => EnumExtensions.ParseSeverity(SeverityRaw);

    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }

    [JsonPropertyName("risk_score")]
    public double RiskScore { get; set; }

    [JsonPropertyName("rationale")]
    public string Rationale { get; set; } = "";

    [JsonPropertyName("recommended_action")]
    public string RecommendedAction { get; set; } = "";

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of combined safety analysis.
/// </summary>
public class AnalyzeResult
{
    public RiskLevel RiskLevel { get; set; }
    public double RiskScore { get; set; }
    public string Summary { get; set; } = "";
    public BullyingResult? Bullying { get; set; }
    public UnsafeResult? Unsafe { get; set; }
    public string RecommendedAction { get; set; } = "";
    public string? ExternalId { get; set; }
    public string? CustomerId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of emotion analysis.
/// </summary>
public class EmotionsResult
{
    [JsonPropertyName("dominant_emotions")]
    public List<string> DominantEmotions { get; set; } = new();

    [JsonPropertyName("emotion_scores")]
    public Dictionary<string, double>? EmotionScores { get; set; }

    [JsonPropertyName("trend")]
    public string TrendRaw { get; set; } = "stable";

    [JsonIgnore]
    public EmotionTrend Trend => EnumExtensions.ParseEmotionTrend(TrendRaw);

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = "";

    [JsonPropertyName("recommended_followup")]
    public string RecommendedFollowup { get; set; } = "";

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of action plan generation.
/// </summary>
public class ActionPlanResult
{
    [JsonPropertyName("audience")]
    public string AudienceRaw { get; set; } = "";

    [JsonPropertyName("steps")]
    public List<string> Steps { get; set; } = new();

    [JsonPropertyName("tone")]
    public string Tone { get; set; } = "";

    [JsonPropertyName("approx_reading_level")]
    public string? ReadingLevel { get; set; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Result of incident report generation.
/// </summary>
public class ReportResult
{
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = "";

    [JsonPropertyName("risk_level")]
    public string RiskLevelRaw { get; set; } = "low";

    [JsonIgnore]
    public RiskLevel RiskLevel => EnumExtensions.ParseRiskLevel(RiskLevelRaw);

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = new();

    [JsonPropertyName("recommended_next_steps")]
    public List<string> RecommendedNextSteps { get; set; } = new();

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

// =============================================================================
// Account Management (GDPR)
// =============================================================================

/// <summary>
/// Result of account data deletion (GDPR Article 17 — Right to Erasure).
/// </summary>
public class AccountDeletionResult
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("deleted_count")]
    public int DeletedCount { get; set; }
}

/// <summary>
/// Result of account data export (GDPR Article 20 — Right to Data Portability).
/// </summary>
public class AccountExportResult
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = "";

    [JsonPropertyName("exportedAt")]
    public string ExportedAt { get; set; } = "";

    [JsonPropertyName("data")]
    public Dictionary<string, object>? Data { get; set; }
}

// =============================================================================
// Usage & Rate Limit
// =============================================================================

/// <summary>
/// Monthly API usage information.
/// </summary>
public class Usage
{
    public int Limit { get; set; }
    public int Used { get; set; }
    public int Remaining { get; set; }
}

/// <summary>
/// Rate limit information for the current minute.
/// </summary>
public class RateLimitInfo
{
    public int Limit { get; set; }
    public int Remaining { get; set; }
    public long? Reset { get; set; }
}
