using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace POM
{
    public class DriverManager
    {
        public static IWebDriver Driver { get; private set; }

        public static void InitializeDriver()
        {
            Driver = new ChromeDriver();
        }

        public static void TearDown()
        {
            Driver.Close();
        }
    }
}