using Controller.simulation.util;

namespace ControllerTests.simulation.util;

public class RandomGeneratorTests
{
    [Test]
    public void GetRandomBoolReturnsBool()
    {
        Assert.That(RandomGenerator.GetRandomBool().GetType(), Is.EqualTo(true.GetType()));
    }
    
    [Test]
    public void GetRandomIntReturnsIntWithinRange()
    {
        for (int i = 0; i < 10; i++) // run 10 times to verify 
        {
            int randomInt = RandomGenerator.GetRandomInt(0, 9);
            Assert.True(randomInt is <= 9 and >= 0);
        }
    }
    
    [Test]
    public void GetRandomChoiceReturnsSameDatatypePassedIn()
    {
        List<string> stringList = new List<string>();
        stringList.Add("Apple");
        stringList.Add("Banana");
        stringList.Add("Orange");
        string testString = "test";
        
        Assert.That(RandomGenerator.GetRandomChoice(stringList).GetType(), Is.EqualTo(testString.GetType()));

        List<int> intList = new List<int>();
        intList.Add(1);
        intList.Add(2);
        intList.Add(3);
        int testInt = 1;
        
        Assert.That(RandomGenerator.GetRandomChoice(intList).GetType(), Is.EqualTo(testInt.GetType()));
    }
    
    [Test]
    public void GetRandomChoiceReturnsItemFromListPassedIn()
    {
        List<string> stringList = new List<string>();
        stringList.Add("Apple");
        stringList.Add("Banana");
        stringList.Add("Orange");

        Assert.True(stringList.Contains(RandomGenerator.GetRandomChoice(stringList)));
    }
}