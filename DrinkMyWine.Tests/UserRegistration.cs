using System;
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
            User registeringUser = User.Create("s@qqmail.com", "abaaa");

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(true);

            mockRepo.Object.AddUser(registeringUser);

            mockRepo.Verify(s => s.AddUser(registeringUser), Times.Once());
        }

        [Test]
        public void Repository_AddExistingNewValidUser_Fails()
        {
            User registeringUser = User.Create("s@qqmail.com", "abaaa");

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(true);
            mockRepo.Setup(rep => rep.AddUser(registeringUser)).Returns(false);

            bool succees = mockRepo.Object.AddUser(registeringUser);

            Assert.IsFalse(succees);
        }

        [Test]
        [TestCase("a@a", "aaa")]
        [TestCase("a@a.", "aaa")]
        [TestCase(".@", "pass")]
        public void EmailValidator_InvalidEmail_GetsInvalidUser(string email, string pass)
        {
            User registeringUser = User.Create(email, pass);
            Assert.IsInstanceOf(typeof(InvalidUser), registeringUser);
        }

        [Test]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void UserCreate_EmptyEmail_Throws(string email, string pass)
        {
            Assert.That(() => User.Create(email, pass),
                        Throws.Exception.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("email"));
        }
    }
}
