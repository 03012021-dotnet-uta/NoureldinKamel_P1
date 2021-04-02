using System;
using Microsoft.EntityFrameworkCore;
using ToyStore.Business.Authentication;
using ToyStore.Business.Logic;
using ToyStore.Models.ControllerModels;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;
using Xunit;

namespace ToyStore.Tests
{
    public class UnitTest1
    {

        DbContextOptions<DbContextClass> testOptions = new DbContextOptionsBuilder<DbContextClass>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;



        [Theory]
        [InlineData("James", "Foster", "user1", "abcd1234")]
        [InlineData("James", "Foster", "user2", "qwerqwer")]
        [InlineData("James", "Foster", "user3", "abcd1234")]
        [InlineData("James", "Foster", "user4", "qwerqewr")]
        public void TestCustomer(string fname, string lname, string username, string password)
        {
            // ARRANGE - create the data to insert into the Db
            //create the new Person seed
            AuthModel authModel = new AuthModel()
            {
                FirstName = fname,
                LastName = lname,
                Username = username,
                Password = password
            };
            Customer outCustomer = null;
            bool success = false;
            using (var context1 = new DbContextClass(testOptions))
            {
                context1.Database.EnsureDeleted();//do this ONCE at hte beginning of each test
                context1.Database.EnsureCreated();// this creates the new-for-this-test database

                //create the MemeSaverRepo instance
                ToyRepository msr = new ToyRepository(context1);

                success = msr.RegisterTest(authModel, out outCustomer);
            }

            Assert.True(success);
            Assert.Equal(outCustomer.FirstName, authModel.FirstName);
            Assert.Equal(outCustomer.LastName, authModel.LastName);
            Assert.Equal(outCustomer.CustomerUName, authModel.Username);

            // ACT - call the method that inserts into the Db
            Customer outCustomer2 = new Customer();
            using (var context2 = new DbContextClass(testOptions))
            {
                context2.Database.EnsureCreated();
                ToyRepository msr = new ToyRepository(context2);
                success = msr.LoginTest(authModel, out outCustomer2);
                // success = context2.Persons.Where(x => x.PasswordHash == testPerson.PasswordHash).FirstOrDefault();
            }

            // ASSERT - verify the the data state is as expected
            Assert.Equal(outCustomer2.FirstName, authModel.FirstName);
            Assert.Equal(outCustomer2.LastName, authModel.LastName);
            Assert.Equal(outCustomer2.CustomerUName, authModel.Username);
            Assert.True(success);
        }




        // [Theory]
        // [InlineData("James", "Foster", "user1", "abcd1234")]
        // [InlineData("James", "Foster", "user2", "qwerqwer")]
        // [InlineData("James", "Foster", "user3", "abcd1234")]
        // [InlineData("James", "Foster", "user4", "qwerqewr")]
        // [InlineData("James", "Foster", "user6", "qwerqewr")]
        // [InlineData("James", "Foster", "user5", "qwerqewr")]
        // public void LoginAuthenticate(string fname, string lname, string username, string password)
        // {
        //     // ARRANGE - create the data to insert into the Db
        //     //create the new Person seed
        //     AuthModel authModel = new AuthModel()
        //     {
        //         FirstName = fname,
        //         LastName = lname,
        //         Username = username,
        //         Password = password
        //     };
        //     Customer outCustomer = null;
        //     bool success = false;
        //     using (var context1 = new DbContextClass(testOptions))
        //     {
        //         context1.Database.EnsureDeleted();//do this ONCE at hte beginning of each test
        //         context1.Database.EnsureCreated();// this creates the new-for-this-test database
        //         ToyRepository msr = new ToyRepository(context1);
        //         Authenticator authenticator = new Authenticator(msr);

        //         //create the MemeSaverRepo instance
        //         success = authenticator.CreateNewCustomer(authModel, out outCustomer);

        //         // success = msr.RegisterTest(authModel, out outCustomer);

        //         // Assert.True(success);
        //         Assert.Equal(outCustomer.FirstName, authModel.FirstName);
        //         Assert.Equal(outCustomer.LastName, authModel.LastName);
        //         Assert.Equal(outCustomer.CustomerUName, authModel.Username);

        //         Customer outCustomer2 = new Customer();
        //         success = authenticator.Authenticate(authModel, out outCustomer);

        //         // ASSERT - verify the the data state is as expected
        //         Assert.Equal(outCustomer2.FirstName, authModel.FirstName);
        //         Assert.Equal(outCustomer2.LastName, authModel.LastName);
        //         Assert.Equal(outCustomer2.CustomerUName, authModel.Username);
        //         Assert.True(success);


        //         Customer outCustomer3 = new Customer();
        //         success = authenticator.ValidateToken(outCustomer.CustomerToken, out outCustomer);

        //         // ASSERT - verify the the data state is as expected
        //         Assert.Equal(outCustomer2.FirstName, authModel.FirstName);
        //         Assert.Equal(outCustomer2.LastName, authModel.LastName);
        //         Assert.Equal(outCustomer2.CustomerUName, authModel.Username);
        //         Assert.True(success);
        //     }
        // }

        [Fact]
        public void Test1()
        {
            // // arrange
            // SellableLogic logic = new SellableLogic(new ToyRepository());
            // var expected = 6;
            // // act
            // var i = logic.TestMethod(2);

            // // assert
            // Assert.Equal(expected, i);
        }
    }
}
