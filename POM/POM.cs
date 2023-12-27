using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace POM
{
    public class MyAccountPage
    {
        IWebDriver webDriver;

        public MyAccountPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public IWebElement LoginTitle => webDriver.FindElement(By.XPath("//div/h2[contains(text(),'Login')]"));
        public IWebElement UsernameOrEmailAddressLabel => webDriver.FindElement(By.XPath("//form//p//label[@for='username']"));
        public IWebElement UsernameInput => webDriver.FindElement(By.XPath("//form//p//input[@id='username']"));
        public IWebElement LoginPasswordLabel => webDriver.FindElement(By.XPath("//form//p//label[@for='password']"));
        public IWebElement PasswordInput => webDriver.FindElement(By.XPath("// form//p//input[@id='password']"));
        public IWebElement LoginButton => webDriver.FindElement(By.XPath("//input[@value='Login']"));
        public IWebElement RememberMe => webDriver.FindElement(By.XPath("//label[@class = 'inline']"));
        public IWebElement LostYourPassword => webDriver.FindElement(By.XPath("//p//a[@href = 'https://practice.automationtesting.in/my-account/lost-password/']"));

        public IWebElement LoginError => webDriver.FindElement(By.CssSelector("ul.woocommerce-error"));

        public IWebElement RegisterTitle => webDriver.FindElement(By.XPath("//div/h2[contains(text(),'Register')]"));
        public IWebElement EmailAddressLabel => webDriver.FindElement(By.XPath("//form//p//label[@for='reg_email']"));
        public IWebElement EmailAddressInput => webDriver.FindElement(By.XPath("//form//p//input[@id='reg_email']"));
        public IWebElement RegisterPasswordLabel => webDriver.FindElement(By.XPath("//form//p//label[@for='reg_password']"));
        public IWebElement RegisterPasswordInput => webDriver.FindElement(By.XPath("//form//p//input[@id='reg_password']"));
        public IWebElement RegisterButton => webDriver.FindElement(By.XPath("//input[@value='Register']"));


        public string GetLostPasswordText(IWebElement LostYourPassword)
        {
            return LostYourPassword.Text;
        }

        public string GetRememberMeText(IWebElement RememberMe)
        {
            return RememberMe.Text;
        }

        public string GetRegisterButtonText(IWebElement RegisterButton)
        {
            return RegisterButton.GetAttribute("value");
        }

        public void Login(string username, string password, IWebElement UsernameInput, IWebElement PasswordInput, IWebElement LoginButton)
        {
            UsernameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
    }

    public class AutoTests
    {
        IWebDriver driver;
        MyAccountPage myAccountPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.automationtesting.in/my-account/");
            myAccountPage = new MyAccountPage(driver);
        }


        [Test]
        public void TestLostPasswordText()
        {
            string lostPasswordText = myAccountPage.GetLostPasswordText(myAccountPage.LostYourPassword);
            Assert.AreEqual("Lost your password?", lostPasswordText);
        }

        [Test]
        public void TestRememberMeText()
        {
            string rememberMeText = myAccountPage.GetRememberMeText(myAccountPage.RememberMe);
            Assert.AreEqual("Remember me", rememberMeText);

        }

        [Test]
        public void TestRegisterButtonText()
        {
            string registerButtonText = myAccountPage.GetRegisterButtonText(myAccountPage.RegisterButton);
            Assert.AreEqual("Register", registerButtonText);
        }

        [Test]
        public void TestLoginNotRegistered()
        {
            string username = "my_username";
            string password = "my_password";
            myAccountPage.Login(username, password, myAccountPage.UsernameInput, myAccountPage.PasswordInput, myAccountPage.LoginButton);

            string ErrorMessage = myAccountPage.LoginError.Text;
            string expectedErrorMessage = $"Error: The username {username} is not registered on this site. If you are unsure of your username, try your email address instead.";

            Assert.AreEqual(expectedErrorMessage, ErrorMessage, "Error message does not match the expected text");
        }

        [Test]
        public void TestSuccessfulLogin()
        {
            string username = "inna.chykova.99";
            string password = "Marshall313???";
            myAccountPage.Login(username, password, myAccountPage.UsernameInput, myAccountPage.PasswordInput, myAccountPage.LoginButton);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement successMessage = wait.Until(driver => driver.FindElement(By.CssSelector($"div.woocommerce-MyAccount-content")));

            Assert.IsTrue(successMessage.Displayed, "Success message after login is not displayed.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}







