namespace ArchitectureSample.Tests.Integration.Helpers;

internal static class Constants
{
	internal static Guid SampleId = Guid.Parse("e9abba80-f92b-49ec-871a-ee5c7a30e4b0");

	internal const string ValidSampleFirstName = "Mahyar";

	internal const string ValidSampleLastName = "Hoorbakht";
	
	internal const string SampleUpdatedLastName = "***UPDATED***";

	internal static DateTime ValidSampleBirthOfDate = DateTime.Now.AddYears(-30);

	internal const string ValidSamplePhoneNumber = "+989365386860";

	internal const string ValidSampleEmail = "Mahyar.Hoorbakht@outlook.com";

	internal const string ValidSampleBankAccount = "1234567890";




	internal const string InvalidSamplePhoneNumber = "123355234";

	internal const string InvalidSampleEmail = "hoorbakht.com";

	internal static DateTime InvalidSampleBirthOfDate = DateTime.Now.AddYears(-130);
}