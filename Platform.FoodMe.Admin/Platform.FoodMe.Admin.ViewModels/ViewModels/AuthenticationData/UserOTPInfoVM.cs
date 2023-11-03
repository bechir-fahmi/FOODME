namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;

public class UserOTPInfoVM
{
    //TO DO : change PhonrNumber type from String to PhoneNumber
    public string PhoneNumber { get; set; }

    public string PhoneNumberWithoutCountryKey(string phoneNumber)
    {
        string CountryCode = "+966";
        if (phoneNumber.StartsWith(CountryCode))
            return PhoneNumber.Remove(0, 4);
        else
            return phoneNumber;
    }
}
