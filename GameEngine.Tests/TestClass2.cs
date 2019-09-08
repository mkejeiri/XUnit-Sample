using Xunit;
using Xunit.Abstractions;
//Step 2 : look at TestClass1 & 2
namespace GameEngine.Tests
{
    //We've used the same collection name : step 2
    //unlike in the GameStateShould.cs where we implemented the IClassFixture interface,
    //when we're using the Collection attribute, we don't need to implement a specific interface in our test class
    //JUST apply the Collection attribute
    //Xunit. net creates an instance of GameStateFixture, supply this same instance to TestClass1 and TestClass2
    // consequently the same instance GameStateFixture is used in TestClass1 and TestClass2
    [Collection("GameState collection")]
    public class TestClass2
    {
        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public TestClass2(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;

            _output = output;
        }

        [Fact]
        public void Test3()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");
        }

        [Fact]
        public void Test4()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");
        }
    }
}
