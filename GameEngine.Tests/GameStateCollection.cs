using Xunit;
//configure Xunit to Share Context Across Test Classes :
//step 1 : implement Xunit's ICollectionFixture interface
namespace GameEngine.Tests
{
    //we define the collection name
    [CollectionDefinition("GameState collection")]

    //GameState collection class doesn't have any implementation!
    public class GameStateCollection : ICollectionFixture<GameStateFixture> { }
}
//Step 2 : decorate TestClass1 & 2 with [CollectionDefinition("GameState collection")] --> look at TestClass1 & 2