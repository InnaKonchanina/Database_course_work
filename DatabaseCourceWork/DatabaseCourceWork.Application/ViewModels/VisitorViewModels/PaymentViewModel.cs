using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels
{
    internal partial class PaymentViewModel : ObservableValidator
    {
        public PaymentViewModel(string eventName, decimal price)
        {
            EventName = eventName;
            Price = price;
        }

        public string EventName { get; }
        public decimal Price { get; }

        [ObservableProperty]
        [Required]
        private string? cardNumber;

        [ObservableProperty]
        [Required]
        private string? expiryDate;

        [ObservableProperty]
        [Required]
        private string? cvv;

        [ObservableProperty]
        private string? errorMessage;

        [RelayCommand]
        private void Pay(Window window)
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                ErrorMessage = "Please fill in all fields correctly.";
                return;
            }

            if (string.IsNullOrWhiteSpace(CardNumber) || CardNumber.Length < 12)
            {
                ErrorMessage = "Invalid card number. Mmust be 12 numbers.";
                return;
            }

            if (string.IsNullOrWhiteSpace(ExpiryDate) || !System.Text.RegularExpressions.Regex.IsMatch(ExpiryDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                ErrorMessage = "Invalid expiry date format. Format is MM/YY.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Cvv) || Cvv.Length != 3 || !Cvv.All(char.IsDigit))
            {
                ErrorMessage = "Invalid CVV. Must be exactly 3 digits.";
                return;
            }

            MessageBox.Show($"Payment of {Price:C} for \"{EventName}\" successful!");
            window.DialogResult = true;
            window.Close();
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

}
