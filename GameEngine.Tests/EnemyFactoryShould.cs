using System;
using Xunit;

namespace GameEngine.Tests
{
    //dotnet test --filter "Category=Boss|Category=Enemy"
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        private readonly EnemyFactory _sut;
        public EnemyFactoryShould()
        {
            _sut = new EnemyFactory();
        }
        [Fact]
        //[Trait("Category", "Enemy")]
        public void Should_Create_NormalEnemy_ByDefault()
        {

            Enemy enemy = _sut.Create("Zombie");
            Assert.IsType<NormalEnemy>(enemy);
        }
        [Fact(Skip = "This is not a relevant test, it's used for educational purpose only")]
        public void Should_Create_NormalEnemy_ByDefault_UnRelevant()
        {

            Enemy enemy = _sut.Create("Zombie");
            Assert.IsNotType<DateTime>(enemy);
        }

        [Fact]
        public void Should_Not_Create_BossEnemy_ByDefault()
        {

            Enemy enemy = _sut.Create("Zombie");
            Assert.IsNotType<BossEnemy>(enemy);
        }

        [Fact]
        public void Should_Create_BossEnemy_When_IsBoss_True()
        {

            Enemy enemy = _sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void Should_Be_BossEnemy_When_IsBoss_True_And_Return_Correct_Name()
        {

            Enemy enemy = _sut.Create("Zombie King", true);
            BossEnemy bossEnemy = Assert.IsType<BossEnemy>(enemy);
            Assert.Equal("Zombie King", bossEnemy.Name);
        }

        [Fact]
        public void Should_Create_BossEnemy_When_IsBoss_True_Assignable_To_Enemy()
        {

            Enemy enemy = _sut.Create("Zombie King", true);

            //operates in a strict fashion, BossEnemy is not a type of enemy even if it inherited from it
            //Assert.IsType<Enemy>(enemy); //this will fail
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void Should_Two_Separate_Instances_Be_Different()
        {

            Enemy enemy1 = _sut.Create("Zombie");
            Enemy enemy2 = _sut.Create("Zombie");
            //Assert.Same(enemy1, enemy2); //This will fail
            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void Should_Not_Allowed_Enemy_With_Null_Name()
        {

            //Assert.Throws<ArgumentNullException>(() => _sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => _sut.Create(null));

            //This will fail : ArgumentNullException contains nameof(name) rather nameof(isBoss)
            //Assert.Throws<ArgumentNullException>("isBoss", () => _sut.Create(null));

        }

        [Fact]
        public void Should_Not_Allowed_BossEnemy_With_Name_Not_Ending_With_King_Or_Queen()
        {


            EnemyCreationException ex =
                Assert.Throws<EnemyCreationException>(() => _sut.Create("Zombie", true));
        }
    }
}
