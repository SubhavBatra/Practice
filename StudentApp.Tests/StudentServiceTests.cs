using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StudentApp;

namespace StudentApp.Tests;

[TestClass]
public class StudentServiceTests
{
    [TestMethod]
    public void Add_ValidStudent_IncreasesCount()
    {
        var svc = new StudentService();
        svc.Add("Alice", 85);
        Assert.AreEqual(1, svc.All.Count);
        Assert.AreEqual("Alice", svc.All[0].Name);
        Assert.AreEqual(85, svc.All[0].Mark);
    }

    [TestMethod]
    public void Add_InvalidInputs_Throw()
    {
        var svc = new StudentService();

        Assert.ThrowsException<ArgumentException>(() => svc.Add("   ", 70));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => svc.Add("A", -1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => svc.Add("A", 101));

        svc.Add("Bob", 60);
        Assert.ThrowsException<InvalidOperationException>(() => svc.Add("bob", 70)); // duplicate (case-insensitive)
    }

    [TestMethod]
    public void ComputeStats_CorrectNumbers()
    {
        var svc = new StudentService();
        svc.Add("A", 50);
        svc.Add("B", 100);
        svc.Add("C", 80);

        var s = svc.ComputeStats();

        Assert.AreEqual(3, s.Count);
        Assert.AreEqual(76.666, s.Average, 1e-3);
        Assert.AreEqual("B", s.MaxName);
        Assert.AreEqual(100, s.MaxMark);
        Assert.AreEqual("A", s.MinName);
        Assert.AreEqual(50, s.MinMark);
    }

    [TestMethod]
    public void SearchByName_Substring_CaseInsensitive()
    {
        var svc = new StudentService();
        svc.Add("Alice", 90);
        svc.Add("Alicia", 70);
        svc.Add("Bob", 60);

        var r = svc.SearchByName("LIC");
        Assert.AreEqual(2, r.Count);
        Assert.AreEqual("Alice", r[0].Name);
        Assert.AreEqual("Alicia", r[1].Name);
    }

    [TestMethod]
    public void ComputeStats_Empty_Throws()
    {
        var svc = new StudentService();
        Assert.ThrowsException<InvalidOperationException>(() => svc.ComputeStats());
    }
}
