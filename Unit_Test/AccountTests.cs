using Store_App.Models.Classes;

namespace Unit_Test;

[TestClass]
public class AccountTests
{
    
    [TestMethod]
    public void TestAccessAccountByLogin_isValid()
    {
        string email = "email1";
        string password = "password1";  
        string name = "name";   
        Account newAccount = new Account(1, email, name);
        newAccount.email = email;
        newAccount.accountPassword = password;
        Account returnAccount = newAccount.accessAccountByLogin(email, password);
        Assert.AreEqual(returnAccount, newAccount);
    }

    [TestMethod]
    public void TestAccessAccountById_isValid()
    {
        int id = 1;
        string email = "email1";  
        string name = "name";        
        Account newAccount = new Account(id, email, name);
        Account returnAccount = newAccount.accessAccountById(1);
        Assert.AreEqual(returnAccount, newAccount);

        id = 2;      
        Account newAccount = new Account(id, email, name);
        returnAccount = newAccount.accessAccountById(2);
        Assert.AreEqual(returnAccount, newAccount);

        int id = 3;      
        Account newAccount = new Account(id, email, name);
        returnAccount = newAccount.accessAccountById(3);
        Assert.AreEqual(returnAccount, newAccount);
    }

    [TestMethod]
    public void TestAccessAccountByEmail_isValid()
    {
        int id = 1;
        string email = "email1";  
        string name = "name";     
        Account newAccount = new Account(id, email, name);
        newAccount.email = email;
        Account returnAccount = newAccount.accessAccountByEmail("email1");
        Assert.AreEqual(returnAccount, newAccount);

        email = "email2";      
        Account newAccount = new Account(id, email, name);
        newAccount.email = email;
        returnAccount = newAccount.accessAccountByEmail("email2");
        Assert.AreEqual(returnAccount, newAccount);

        email = "email3";       
        Account newAccount = new Account(id, email, name);
        newAccount.email = email;
        returnAccount = newAccount.accessAccountByEmail("email3");
        Assert.AreEqual(returnAccount, newAccount);
    }
}