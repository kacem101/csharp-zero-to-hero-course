using System.Collections.Generic;
using Xunit;

public class SignatureEngineTests
{
    [Theory]
    [InlineData("username=' OR '1'='1")]
    [InlineData("comment=<script>alert(1)</script>")]
    [InlineData("file=../../etc/passwd")]
    public void Check_MaliciousLookingLine_RaisesAtLeastOneAlert(string line)
    {
        var engine = new SignatureEngine();
        var raised = new List<Alert>();
        engine.AlertRaised += alert => raised.Add(alert);

        engine.Check(line);

        Assert.NotEmpty(raised);
    }

    [Fact]
    public void Check_OrdinaryLogLine_RaisesNoAlert()
    {
        var engine = new SignatureEngine();
        var raised = new List<Alert>();
        engine.AlertRaised += alert => raised.Add(alert);

        engine.Check("2026-07-18 10:00:01 LOGIN_SUCCESS user=bob ip=198.51.100.9");

        Assert.Empty(raised);
    }
}
