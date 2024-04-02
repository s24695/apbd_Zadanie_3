using System.ComponentModel.DataAnnotations;
using LegacyApp;
using Validator = LegacyApp.Validator;

namespace LegacyAppTests;
public class UnitTest1
{
    [Fact]
    public void Validate_Name_Should_Return_False_If_FName_Or_LName_Empty()
    {
        //Arrange
        string fname = "";
        string lname = null;
        //Act
        bool result = new Validator().ValidateName(fname, lname);
        //Assert
        Assert.False(result);
    }
    [Fact]
    public void Validate_Name_Should_Return_True_If_FName_Or_LName_Not_Empty_OR_Null()
    {
        //Arrange
        string fname = "Jason";
        string lname = "Doe";
        //Act
        bool result = new Validator().ValidateName(fname, lname);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_Email_Should_Return_True_If_Correct_Structure()
    {
        string email = "abc@somehting.com";

        bool result = new Validator().ValidateEmail(email);
        
        Assert.True(result);
    }
    [Fact]
    public void Validate_Email_Should_Return_False_If_Wrong_Structure()
    {
        string email = "abcsomehting.com";

        bool result = new Validator().ValidateEmail(email);
        
        Assert.False(result);
    }
    [Fact]
    public void Validate_DateOfBirth_Should_Return_False_IF_Younger_Than_21()
    {
        DateTime dateOfBirth = new DateTime(2004, 12, 01);

        bool result = new Validator().ValidateAge(dateOfBirth);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Validate_DateOfBirth_Should_Return_True_IF_Older_Than_21()
    {
        DateTime dateOfBirth = new DateTime(2000, 12, 01);

        bool result = new Validator().ValidateAge(dateOfBirth);
        
        Assert.True(result);
    }

    [Fact]
    public void ValidateUserCredit()
    {
        var user = new User();
        user.CreditLimit = 300;
        user.HasCreditLimit = true;

        bool result = new Validator().ValidateUserCredit(user);
        
        Assert.False(result);
    }
}