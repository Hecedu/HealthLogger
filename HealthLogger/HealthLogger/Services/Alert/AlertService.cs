using HealthLogger;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class AlertService : IAlertService
{
    public async Task ShowErrorAsync(string message, string title, string buttonText)
    {
        await App.Current.MainPage.DisplayAlert(title, message, buttonText);
    }

    public async Task ShowErrorAsync(string message, string title, string buttonText, Action CallBackAferHide)
    {
        await App.Current.MainPage.DisplayAlert(title, message, buttonText);
        CallBackAferHide?.Invoke();
    }
}