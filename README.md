# partial date

This project is a .NET Standard 2.0 class library.

This project provides a class which can be used to model a date which allows certain components of the date (day, month, year) to be null.

##Usage

Add the package to your project(s):

`Install-Package PartialDate`

```
bool isCompleteDate;
DateTime dateTime;

//model a day and month when the year is not known
var partialDate = new PartialDate(null, 3, 15);
isCompleteDate = partialDate.IsCompleteDate;    //isCompleteDate = false
dateTime = partialDate.ToDateTime();            //throws InvalidOperationException

//model just the year
var partialDate2 = new PartialDate(2016, null, null);
isCompleteDate = partialDate2.IsCompleteDate;    //isCompleteDate = false
dateTime = partialDate2.ToDateTime();            //throws InvalidOperationException

//model a date when all components are knwon and call ToDateTime to retrieve a native .NET DateTime object
var partialDate3 = new PartialDate(2015, 2, 28);
isCompleteDate = partialDate3.IsCompleteDate;    //isCompleteDate = true
dateTime = partialDate3.ToDateTime();            //dateTime = DateTime(2015, 2, 28)

//exception is thrown if an invalid date time is provided
var partialDate4 = new PartialDate(2015, 2, 31); //Argument Exception
```
