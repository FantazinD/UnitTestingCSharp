using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests;

[TestFixture]
public sealed class MathTests
{
    private Math _math;

    [SetUp]
    public void Setup()
    {
        _math = new Math();
    }

    [Test]
    public void Add_WhenCalled_ReturnsSumOfArguments()
    {
        var result = _math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    [TestCase(1,2,2)]
    [TestCase(2,1,2)]
    [TestCase(2,2,2)]
    public void Max_WhenCalled_ReturnsArgumentWithGreaterValue(int a, int b, int expectedResult)
    {
        var result = _math.Max(a, b);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
    {
        var result = _math.GetOddNumbers(5);

        /* General Way */
        //Assert.That(result, Is.Not.Empty);
        //Assert.That(result.Count(), Is.EqualTo(3));

        Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

        //Assert.That(result, Is.Ordered);
        //Assert.That(result, Is.Unique);
    }
}
