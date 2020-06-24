using BotCsgo.Controller.Steam;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Chrome;

namespace BotCsgo.Controller
{
    class SteamBot
    {
        private IBotSettings botSettings;
        private IWebDriver Browser;
        public SteamBot(IBotSettings botSettings)
        {
            this.botSettings = botSettings;
            Browser = new ChromeDriver();
            Browser.Manage().Window.Size = new System.Drawing.Size(1005, 975);
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        public void Start()
        {
            Browser.Navigate().GoToUrl(botSettings.url);
            Browser.FindElement(By.ClassName("global_action_link")).Click();
            Browser.FindElement(By.ClassName("text_input")).SendKeys(botSettings.Login);
            var pass = Browser.FindElement(By.Id("input_password"));
            if (pass != null)
            {
                pass.SendKeys(botSettings.Pass);
            }
            Browser.FindElement(By.CssSelector(".btnv6_blue_hoverfade.btn_medium")).Click();
            var items = ReaderCodsSteam.GetSteamCode();
            for (int i = 0; i < items.Count; i++)
            {
                try
                {
                    Browser.FindElement(By.CssSelector(".twofactorauthcode_entry_input.authcode_placeholder")).SendKeys(items[i]);
                    var elements = Browser.FindElements(By.CssSelector(".auth_button.leftbtn")).ToList();
                    for (int j = 0; j < elements.Count; j++)
                    {
                        if (elements[j].Text == "Подтвердить\r\nкод аутентификатора")
                        {
                            elements[j].Click();
                            ReaderCodsSteam.DeleteCode();
                            break;
                        }
                        if (elements[j].Text == "Повторить\r\nКод аутентификатора введен верно")
                        {
                            elements[j].Click();
                            ReaderCodsSteam.DeleteCode();
                            break;
                        }

                    }
                }
                catch
                {
                    break;
                }
            }
        }

        public void CloseBot()
        {
            this.Browser.Quit();
        }

        public bool CheckStatusBot()
        {
            try
            {
                Browser.FindElement(By.Id("header_notification_area"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GetTrade(string token)
        {
            string trade = $"https://steamcommunity.com/tradeoffer/{token}";
            Browser.Navigate().GoToUrl(trade);
            IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
            jse.ExecuteScript("jQuery('#you_notready').trigger('click')");
            jse.ExecuteScript("jQuery('.btn_green_white_innerfade.btn_medium').trigger('click')");
            jse.ExecuteScript("jQuery('.btn_grey_white_innerfade.btn_medium').trigger('click')");
            jse.ExecuteScript("jQuery('#trade_confirmbtn').trigger('click')");
            var elements = Browser.FindElements(By.CssSelector(".received_items_header"));
            if (elements != null)
            {
                jse.ExecuteScript("jQuery('.black_square_btn').trigger('click')");
            }

        }
    }
}
