using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace POM
{
    public class MyAccountPage
    {
        private IWebDriver driver;
        public MyAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement LoginTitle => driver.FindElement(By.XPath("//div/h2[contains(text(),'Login')]"));
        public IWebElement UsernameOrEmailAddressLabel => driver.FindElement(By.XPath("//form//p//label[@for='username']"));
        public IWebElement UsernameInput => driver.FindElement(By.XPath("//form//p//input[@id='username']"));
        public IWebElement LoginPasswordLabel => driver.FindElement(By.XPath("//form//p//label[@for='password']"));
        public IWebElement PasswordInput => driver.FindElement(By.XPath("// form//p//input[@id='password']"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//input[@value='Login']"));
        public IWebElement RememberMe => driver.FindElement(By.XPath("//label[@class = 'inline']"));
        public IWebElement LostYourPassword => driver.FindElement(By.XPath("//p//a[@href = 'https://practice.automationtesting.in/my-account/lost-password/']"));

        public IWebElement LoginError => driver.FindElement(By.CssSelector("ul.woocommerce-error"));

        public IWebElement RegisterTitle => driver.FindElement(By.XPath("//div/h2[contains(text(),'Register')]"));
        public IWebElement EmailAddressLabel => driver.FindElement(By.XPath("//form//p//label[@for='reg_email']"));
        public IWebElement EmailAddressInput => driver.FindElement(By.XPath("//form//p//input[@id='reg_email']"));
        public IWebElement RegisterPasswordLabel => driver.FindElement(By.XPath("//form//p//label[@for='reg_password']"));
        public IWebElement RegisterPasswordInput => driver.FindElement(By.XPath("//form//p//input[@id='reg_password']"));
        public IWebElement RegisterButton => driver.FindElement(By.XPath("//input[@value='Register']"));


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
        public IWebElement WaitForSuccessMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(driver => driver.FindElement(By.CssSelector($"div.woocommerce-MyAccount-content")));
        }
    }
}
