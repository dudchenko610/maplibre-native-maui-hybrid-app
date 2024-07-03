using System;
using FarmApp.Mobile.Services;
using Foundation;
using PassKit;
using UIKit;

namespace FarmApp.Mobile;

public class IOSPaymentService : PKPaymentAuthorizationControllerDelegate, IPaymentService
{
    public event EventHandler<string> AuthorizationComplete;
    public event EventHandler CanMakePaymentsUpdated;  // handled behind the scenes in iOS

    // protected IApiConfiguration ApiConfiguration { get; }

    // public PaymentService(IApiConfiguration apiConfiguration)
    public IOSPaymentService()
    {
    }

    // public bool CanMakePayments => StripeSdk.DeviceSupportsApplePay;
    public bool CanMakePayments => true;

    public void AuthorizePayment(decimal total)
    {
        var request = new PKPaymentRequest
        {
            PaymentSummaryItems = new[]
            {
                new PKPaymentSummaryItem
                {
                    Label = "AppleGooglePay", 
                    Amount = new NSDecimalNumber(total), 
                    Type = PKPaymentSummaryItemType.Final
                }
            },
            CountryCode = "US",
            CurrencyCode = "USD",
            // MerchantIdentifier = ApiConfiguration.MerchantId,
            MerchantIdentifier = "merchantId",
            MerchantCapabilities = PKMerchantCapability.ThreeDS,
            SupportedNetworks = new[] { PKPaymentNetwork.Amex, PKPaymentNetwork.MasterCard, PKPaymentNetwork.Visa }
        };

        var authorization = new PKPaymentAuthorizationController(request)
        {
            Delegate = (IPKPaymentAuthorizationControllerDelegate)Self
        };

        authorization.Present(null);
    }

    public override void DidAuthorizePayment(PKPaymentAuthorizationController controller, PKPayment payment, Action<PKPaymentAuthorizationStatus> completion)
    {
        // ApiClient.SharedClient.CreateToken(payment, TokenComplete);
        

        completion(PKPaymentAuthorizationStatus.Success);
    }

    // protected void TokenComplete(UIGestureRecognizer.Token token, NSError arg1) => AuthorizationComplete.Invoke(this, token.TokenId);
    protected void TokenComplete(UIGestureRecognizer.Token token, NSError arg1) => AuthorizationComplete.Invoke(this, "");

    public override void DidFinish(PKPaymentAuthorizationController controller) => controller.Dismiss(null);
}