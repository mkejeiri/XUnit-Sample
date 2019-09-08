using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    //To perform any clean up code we use IDisposable
    //interface and the cleanup should go to dispose method
    public class BossEnemyShould
    {
        //reduce duplicated Arrange phase code by moving it into the test class constructor
        private readonly BossEnemy _sut;

        //add custom test output by creating a test class constructor and adding a parameter of type ITestOutputHelper
        //if we use Console.WriteLine we can't see the message
        private readonly ITestOutputHelper _output;

        public BossEnemyShould(ITestOutputHelper output)
        {
            _output = output;
            _sut = new BossEnemy();
            //Sometime is handy to put a comment...
            //Note: if we use Console.WriteLine we can't see the message
            _output.WriteLine("Creating a Boss Enemy");
        }

        [Fact]
        [Trait("Category","Boss")]
        public void Should_Have_The_Correct_Power()
        {
            //  Assert.Equal(166.667, _sut.TotalSpecialAttackPower, 3); // will fail due to the precision
            Assert.Equal(166.667, _sut.TotalSpecialAttackPower, 3);
        }
    }
}
