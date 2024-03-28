Feature: AddCustomer
	In order to add customer
	As a user
	I want to be able add customer

@Customers
Scenario Outline: Add new customer
	Given I have entered the customer details
		| FirstName   | LastName   | BirthOfDate   | PhoneNumber   | Email   | BankAccount   |
		| <FirstName> | <LastName> | <BirthOfDate> | <PhoneNumber> | <Email> | <BankAccount> |
	When I save the customer
	Then the customer details should be <Expectation>

Examples:
	| FirstName | LastName  | BirthOfDate | PhoneNumber   | Email                        | BankAccount | Expectation |
	| Mahyar    | Hoorbakht | 1993/08/03  | +989365386860 | Mahyar.hoorbakht@outlook.com | 1234567890  | Saved       |
	| Mahyar    | Hoorbakht | 1993/08/03  | +9896860      | Mahyar.hoorbakht@outlook.com | 1234567890  | Not Saved   |
	| Mahyar    | Hoorbakht | 1993/08/03  | +989365386860 | Mahyar.hoorbakht.com         | 1234567890  | Not Saved   |
	| Mahyar    | Hoorbakht | 1893/08/03  | +989365386860 | Mahyar.hoorbakht@outlook.com | 1234567890  | Not Saved   |
	| Mahyar    | Hoorbakht | 1993/08/03  | +989365386860 | Mahyar.hoorbakht@outlook.com | 127890      | Not Saved   |
