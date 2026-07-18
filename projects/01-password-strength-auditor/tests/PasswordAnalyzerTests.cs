using System;
using Xunit;

// Contract-level tests: these check the BEHAVIOR required by the
// project brief, not any specific internal implementation choice (exact
// entropy formula constants, exact wording of pattern messages, etc.).
// A passing test suite here means your implementation satisfies the
// requirements — it does not mean your code looks like the reference
// solution, and it shouldn't need to.
public class PasswordAnalyzerTests
{
    private readonly PasswordAnalyzer _analyzer = new();

    [Fact]
    public void CalculateEntropyBits_LongerPassword_HasHigherEntropyThanShorter()
    {
        double shortEntropy = _analyzer.CalculateEntropyBits("abc123");
        double longEntropy = _analyzer.CalculateEntropyBits("abc123abc123abc123");

        Assert.True(longEntropy > shortEntropy);
    }

    [Fact]
    public void CalculateEntropyBits_EmptyPassword_ReturnsZero()
    {
        Assert.Equal(0, _analyzer.CalculateEntropyBits(""));
    }

    [Fact]
    public void DetectPatterns_ObviouslyWeakPassword_ReturnsAtLeastOneIssue()
    {
        var issues = _analyzer.DetectPatterns("aaaa1234");
        Assert.NotEmpty(issues);
    }

    [Fact]
    public void DetectPatterns_CommonPassword_IsFlagged()
    {
        var issues = _analyzer.DetectPatterns("password");
        Assert.NotEmpty(issues);
    }

    [Fact]
    public void ClassifyStrength_ObviouslyWeakPassword_IsNotRatedStrong()
    {
        string rating = _analyzer.ClassifyStrength("aaaa");
        Assert.DoesNotContain("Strong", rating); // catches both "Strong" and "Very Strong"
    }

    [Fact]
    public void ClassifyStrength_LongRandomLookingPassword_IsNotRatedWeak()
    {
        string rating = _analyzer.ClassifyStrength("xQ7#mK9$pL2@vR5!wZ8&");
        Assert.DoesNotContain("Weak", rating); // catches both "Weak" and "Very Weak"
    }
}
