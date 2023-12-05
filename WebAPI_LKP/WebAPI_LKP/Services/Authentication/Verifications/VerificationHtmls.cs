namespace WebAPI_LKP.Services.Authentication.Verifications
{
    public class VerificationHtmls
    {
        public static string htmlSuccessVerification = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Email Verification</title>
            </head>
            <body>
                <h1>Email Verified Successfully!</h1>
                <p>Your email has been successfully verified. Thank you for using our service.</p>
            </body>
            </html>
        ";
        public static string htmlFailVerification = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Email Verification</title>
            </head>
            <body>
                <h1>Email Verification Failed</h1>
                <p>Something went wrong while verifying your email. Please try again later.</p>
            </body>
            </html>
        ";
    }
}
