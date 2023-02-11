using Microsoft.Extensions.Logging;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Repositories;
using Xunit;

namespace WebReferenceSite.Mvc.Tests.Regression_Selenium
{
    public class FolderContentsPageTests
    {
        [Fact]
        public void Startup_NavToFolderLandingPage()
        {
            IWebDriver webdriver = new ChromeDriver();
            try
            {
                webdriver.Navigate().GoToUrl("https://localhost:44373/FolderContents/GetFolderContents");
            }
            catch (InvalidOperationException ioe)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
