using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace POM
{
    public class AutoTests
    {
        private IWebDriver driver;
        private MyAccountPage myAccountPage;

        [SetUp]
        public void Setup()
        {
            DriverManager.InitializeDriver();
            driver = DriverManager.Driver;
            driver.Navigate().GoToUrl("https://practice.automationtesting.in/my-account/");
            myAccountPage = new MyAccountPage(driver);
        }

        [Test]
        public void TestLostPasswordText()
        {
            var lostPasswordText = myAccountPage.GetLostPasswordText(myAccountPage.LostYourPassword);
            Assert.AreEqual("Lost your password?", lostPasswordText);
        }

        [Test]
        public void TestRememberMeText()
        {
            var rememberMeText = myAccountPage.GetRememberMeText(myAccountPage.RememberMe);
            Assert.AreEqual("Remember me", rememberMeText);

        }

        [Test]
        public void TestRegisterButtonText()
        {
            var registerButtonText = myAccountPage.GetRegisterButtonText(myAccountPage.RegisterButton);
            Assert.AreEqual("Register", registerButtonText);
        }

        public void TestLoginNotRegistered()
        {
            var username = Constants.UnregisteredUsername;
            var password = Constants.UnregisteredPassword;
            myAccountPage.Login(username, password, myAccountPage.UsernameInput, myAccountPage.PasswordInput, myAccountPage.LoginButton);

            var ErrorMessage = myAccountPage.LoginError.Text;
            var expectedErrorMessage = $"Error: The username {username} is not registered on this site. If you are unsure of your username, try your email address instead.";

            Assert.AreEqual(expectedErrorMessage, ErrorMessage, "Error message does not match the expected text");
        }

        [Test]
        public void TestSuccessfulLogin()
        {
            var username = Constants.RegisteredUsername;
            var password = Constants.RegisteredPassword;

            myAccountPage.Login(username, password, myAccountPage.UsernameInput, myAccountPage.PasswordInput, myAccountPage.LoginButton);

            var successMessage = myAccountPage.WaitForSuccessMessage();

            Assert.IsTrue(successMessage.Displayed, "Success message after login is not displayed.");
        }


        [TearDown]
        public void TearDown()
        {
            DriverManager.TearDown();
        }
    }
}







