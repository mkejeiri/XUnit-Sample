using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
        
        //add custom test output by creating a test class constructor and adding a parameter of type ITestOutputHelper
        private readonly ITestOutputHelper _output;

        //reduce duplicated Arrange phase code by moving it into the test class constructor
        private readonly PlayerCharacter _sut;


        public PlayerCharacterShould(ITestOutputHelper output)
        {
            _output = output;
            _sut = new PlayerCharacter();
            //Sometime is handy to put a comment...
            //Note: if we use Console.WriteLine we can't see the message
            _output.WriteLine("Creating a PlayerCharacter");
        }


        [Fact]
        public void Inexperienced_Should_Be_Noob_When_New()
        {
            //Assert.False(_sut.IsNoob);
            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void FullName_Should_Be_FirstName_Followed_By_LastName()
        {

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";
            Assert.Equal("Sarah Smith", _sut.FullName);
        }


        [Fact]
        public void FullName_Should_Start_With_FirstName()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";
            Assert.StartsWith("Sarah", _sut.FullName);
        }

        [Fact]
        public void FullName_Should_End_With_LastName()
        {

            _sut.FirstName = "sarah";
            _sut.LastName = "Smith";
            Assert.EndsWith("Smith", _sut.FullName);
        }


        [Fact]
        public void FullName_Should_Be_FirstName_Followed_By_LastName_Ingore_Case()
        {
            _sut.FirstName = "sarah";
            _sut.LastName = "smith";
            Assert.Equal("SARAH SMITH", _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void FullName_Should_Contains_Substring_FullName()
        {
            _sut.FirstName = "sarah";
            _sut.LastName = "smith";
            Assert.Contains("ah sm", _sut.FullName);
        }

        [Fact]
        public void FullName_Should_Be_Title_Case()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
        }

        [Fact]
        public void Player_Should_Start_With_Default_Health()
        {
            Assert.Equal(100, _sut.Health);
            //Assert.NotEqual(0, _sut.Health);
        }


        [Fact]
        public void Player_Health_Should_Increase_From_1_To_100_After_Sleep()
        {
            _sut.Sleep(); //Expect increase between 1 to 100 inclusive
            //Assert.True(_sut.Health>=101 || _sut.Health <=200);
            Assert.InRange<int>(_sut.Health, 101, 200);
        }

        [Fact]
        public void Should_Not_Have_NickName_By_Default()
        {
            Assert.Null(_sut.Nickname);
        }

        [Fact]
        public void Should_Have_Specific_Weapon()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
        }
        [Fact]
        public void Should_Not_Have_Specific_StaffOfWonder_Weapon()
        {
            Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
        }


        [Fact]
        public void Should_Have_At_Least_One_Kind_Of_Sword_Weapon()
        {
            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void Should_Have_All_Expected_Weapons()
        {

            var expectedWeapons = new List<string>
            {
                "Long Bow",
                "Short Bow",
                "Short Sword",
                //"Staff Of Wonder",
            };
            Assert.Equal(expectedWeapons, _sut.Weapons);
        }

        [Fact]
        public void Should_Have_All_Expected_Weapons_Not_Empty()
        {
            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void Should_Raise_An_Event_When_Sleep()
        {
            //test doesn't pass if OnPlayerSlept(EventArgs.Empty) is commented out
            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep());
        }

        //in our case PlayerCharacter implement INotifyPropertyChanged interface
        [Fact]
        public void Should_Raise_A_PropertyChangedEvent()
        {
            //no longer pass if the call to OnPropertyChanged is commented out
            Assert.PropertyChanged(
                /*Object which implements INPC*/_sut,
                /*PropertyName*/"Health",
                /*action cause the event to fire*/() => _sut.TakeDamage(10));
        }

        //centralize any cleanup code by implementing IDisposable and adding common cleanup code to the Dispose method
        public void Dispose()
        {
            //To perform any clean up code we use IDisposable
            //interface and the cleanup should go to dispose method
            //_sut.Dispose();
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");
        }
    }
}
