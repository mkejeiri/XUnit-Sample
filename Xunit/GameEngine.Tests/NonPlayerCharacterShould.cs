using Xunit;

namespace GameEngine.Tests
{
    public class NonPlayerCharacterShould
    {
        [Theory(Skip = "Deprecated: Been replaced by a shared data coming from InternalHealthDamageData class")]
        [InlineData(0, 100)]
        [InlineData(1, 99)]
        [InlineData(50, 50)]
        [InlineData(101, 1)]
        public void Take_Damage_InLineData(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }


        [Theory]
        [MemberData(nameof(InternalHealthDamageData.TestData), MemberType = typeof(InternalHealthDamageData))]
        public void Take_Damage_InternalClassData(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }

        [Theory]
        [MemberData(nameof(ExternalHealthDamageData.TestData), MemberType = typeof(ExternalHealthDamageData))]
        public void Take_Damage_ExternalClassData(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [HealthDamageDataInternal]
        public void Take_Damage_InternalClassData_CustomAttribute(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }

        //The theory attribute tells xUnit that this test method should be executed multiple times,
        //and for each execution, this test method needs to be provided with some test data.
        [Theory]
        [HealthDamageDataExternal("TestData.csv")]
        public void Take_Damage_ExternalClassData_CustomAttribute(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
    }
}
