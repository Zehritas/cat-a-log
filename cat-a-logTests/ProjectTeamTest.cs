using cat_a_logB;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ProjectTeamTest
{
    [Test]
    public void MembersSetter_NullInput_ShouldThrowArgumentException()
    {

        var team = new ProjectTeam();

        NUnit.Framework.Assert.Throws<ArgumentException>(() => team.Members = null);
    }

}