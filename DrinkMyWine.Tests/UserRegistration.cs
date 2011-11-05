using System;
using System.Collections;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace DrinkMyWine.Tests
{
    [TestFixture]
    public class UserRegistrationTest
    {
        [Test]
        public void Repository_AddNewValidUser_Succeeds()
        {
            User registeringUser = User.Create("sese@gmail.com", "sese");

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(true);

            mockRepo.Object.AddUser(registeringUser);

            mockRepo.Verify(s => s.AddUser(registeringUser), Times.Once());
        }

        [Test]
        public void Repository_AddExistingNewValidUser_Fails()
        {
            User registeringUser = User.Create("sese@gmail.com", "sese");

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(true);
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(false);

            bool succees = mockRepo.Object.AddUser(registeringUser);

            Assert.IsFalse(succees);
        }

        [Test, TestCaseSource("DivideCases")]
        public void EmailValidator_InvalidEmail_GetsInvalidUser(string email, string pass)
        {
            User registeringUser = User.Create(email, pass);
            Assert.IsInstanceOf(typeof(InvalidUser), registeringUser);
        }

        static object[] DivideCases =
        {
            new string[] { "One@a", "Two" },
            new string[] { "@Three", "Four" },
            new string[] { "Five.@", "Six" } 
        };

        [Test]
        public void UserCreate_EmptyEmail_Throws()
        {
            Assert.That(() => User.Create(null, null),
                        Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("email"));
        }
    }
}
