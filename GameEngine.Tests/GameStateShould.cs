using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    //we're telling Xunit that we wanted to supply an instance of the GameStateFixture before the first test executes,
    //and then dispose off the GameStateFixture instance after all of the tests have executed

    //CAREFUL when using class fixtures, because we're sharing the instance among multiple tests, 
    //we need to make sure the actions we perform against the shared instance don't have side effects 
    //and potentially break other tests.Each individual test should be able to be executed in any order
    //and not break any other tests.
    public class GameStateShould : IClassFixture<GameStateFixture>
    {

        [Fact(Skip = "Deprecated: use shared context via GameStateFixture GameState wrapper, " +
                     "GameState is expensive to create multiple time!")]
        public void Damage_All_Players_When_Earthquake_Wrong()
        {
            var sut = new GameState();
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            sut.Players.Add(player1);
            sut.Players.Add(player2);


            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;
            sut.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact(Skip = "Deprecated: use shared context via GameStateFixture GameState wrapper, " +
                     "GameState is expensive to create multiple time!")]
        public void Reset_Wrong()
        {

            var sut = new GameState();
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            sut.Players.Add(player1);
            sut.Players.Add(player2);
            sut.Reset();
        }


        private readonly ITestOutputHelper _output;
        private readonly GameStateFixture _gameStateFixture;
        public GameStateShould(GameStateFixture gameStateFixture,
            ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }

        [Fact]
        public void Damage_All_Players_When_Earthquake()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            _gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {


            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            _gameStateFixture.State.Reset();

            Assert.Empty(_gameStateFixture.State.Players);
        }
    }
}