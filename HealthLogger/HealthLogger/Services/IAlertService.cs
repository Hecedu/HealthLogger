using System;
using System.Threading.Tasks;

public interface IAlertService
{
    Task ShowErrorAsync(string message, string title, string buttonText);
    Task ShowErrorAsync(string message, string title, string buttonText, Action CallBackAferHide);
}