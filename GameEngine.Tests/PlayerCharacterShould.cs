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

        [Fact(Skip = "Ignored due to duplication, Theory and InlineData")]
        public void Should_Take_Zero_Damage()
        {
            _sut.TakeDamage(0);
            Assert.Equal(100,_sut.Health);
        }

        [Fact(Skip = "Ignored due to duplication, Replaced by Theory and InlineData")]
        public void Should_Take_Small_Damage()
        {
            _sut.TakeDamage(1);
            Assert.Equal(99, _sut.Health);
        }

        [Fact(Skip = "Ignored due to duplication, Replaced by Theory and InlineData")]
        public void Should_Take_Medium_Damage()
        {
            _sut.TakeDamage(50);
            Assert.Equal(50, _sut.Health);
        }

        [Fact(Skip = "Ignored due to duplication, Replaced by Theory and InlineData")]
        public void Should_Take_Mininum_Damage()
        {
            _sut.TakeDamage(101);
            Assert.Equal(1, _sut.Health);
        }

        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        //drawbacks: we can't share test case data across multiple test methods or test classes
        [InlineData(0,100)]
        [InlineData(1, 99)]
        [InlineData(50, 50)]
        [InlineData(101, 1)]
        public void Should_Take_Damage_Test_Inline_Data_Driven(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);
            Assert.Equal(expectedHealth, _sut.Health);
        }


        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [MemberData(nameof(InternalHealthDamageData.TestData), MemberType = typeof(InternalHealthDamageData))]
        public void Should_Take_Damage_Test_InternalDataClass_Driven(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);
            Assert.Equal(expectedHealth, _sut.Health);
        }


        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [MemberData(nameof(ExternalHealthDamageData.TestData), MemberType = typeof(ExternalHealthDamageData))]
        public void Should_Take_Damage_Test_ExternalDataClass_Driven(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);
            Assert.Equal(expectedHealth, _sut.Health);
        }



        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [HealthDamageDataInternal]
        public void Should_Take_Damage_Test_InternalDataClass_CustomAttribute(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);
            Assert.Equal(expectedHealth, _sut.Health);
        }

        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [HealthDamageDataExternal("TestData.csv")]
        public void Should_Take_Damage_Test_ExternalDataClass_CustomAttribute(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);
            Assert.Equal(expectedHealth, _sut.Health);
        }


        //Centralize any cleanup code by implementing IDisposable and adding common cleanup code to the Dispose method
        public void Dispose()
        {
            //To perform any clean up code we use IDisposable
            //interface and the cleanup should go to dispose method
            //_sut.Dispose();
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");
        }
    }
}
