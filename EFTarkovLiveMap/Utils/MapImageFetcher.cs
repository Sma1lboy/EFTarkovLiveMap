using System;
using System.Drawing;
using System.Drawing.Imaging;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EFTarkovLiveMap.ViewModels;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EFTarkovLiveMap.Utils;


public static class MapImageFetcher
{
private static string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Escape from Tarkov", "Screenshots");


private static string _lastFileName = "";

public static string GetPosition()
{
    var files = Directory.GetFiles(imgPath);
    if (files.Length == 0)
    {
        return _lastFileName;
    }

    _lastFileName = Path.GetFileName(files[0]);
    File.Delete(Path.Combine(imgPath, _lastFileName));

    return _lastFileName;
}

private static void deleteSomeUseLessDiv(ChromeDriver driver)
{
    var leftPane = driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div/div/div[2]"));
    var rightPane = driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div/div/div[3]"));
    var header = driver.FindElement(By.XPath("/html/body/div/div/div/header"));
    var superHeader = driver.FindElement(By.XPath("/html/body/div/div/div/div[1]"));
    var footer = driver.FindElement(By.XPath("/html/body/div/div/div/div[3]"));
    // 创建一个 JavaScript 执行器
    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

    // 移除元素
    if (leftPane != null) {
        js.ExecuteScript("arguments[0].remove();", leftPane);
    }

    if (rightPane != null) {
        js.ExecuteScript("arguments[0].remove();", rightPane);
    }

    if (header != null) {
        js.ExecuteScript("arguments[0].remove();", header);
    }
    if (superHeader != null) {
        js.ExecuteScript("arguments[0].remove();", superHeader);
    }
    if (footer != null) {
        js.ExecuteScript("arguments[0].remove();", footer);
    }

}

public static Avalonia.Media.Imaging.Bitmap FetchMapImage(string mapName)
{
    try
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://tarkov-market.com/maps/" + mapName);
        // Assuming there is an input element with id 'inputId'
        var button = driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div/div/div[1]/div/button"));
        Thread.Sleep(1000);
        button.Click();
        var input = driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div/div/div[1]/div/input"));
        Thread.Sleep(1000);
        input.SendKeys(GetPosition());
        deleteSomeUseLessDiv(driver);
        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        using (var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray)))
        {
            Rectangle cropArea = new Rectangle(0, 0, bmpScreen.Width, bmpScreen.Height - 200);
            using (Bitmap croppedImage = bmpScreen.Clone(cropArea, bmpScreen.PixelFormat))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    croppedImage.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    driver.Quit();
                    return new Avalonia.Media.Imaging.Bitmap(memory);
                }
            }
        }
    }
    catch (Exception e)
    {
        return null;
    }
}
}
