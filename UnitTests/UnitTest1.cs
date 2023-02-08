using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineFinanicalApplicaton.Core;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {        

        #region Global Variables        
        string Email = "newemail9@test.com";
        string Password = "abc";
        string budgetId = "0";
        string budgetuserId = "0";
        #endregion
        [TestMethod]
        /// <summary>
        /// This Test will create a new user in the system
        /// </summary>
        /// 
        public void SignupTest()
        {
            string Forename = "Test";
            string Surname = "Test";            
            string message = "";
            string userId = "";

            UserProcess userProcess = new UserProcess();
            int userStatus = userProcess.CreateUser(ConfigurationManager.ConnectionStrings["dbconnecttest"].ConnectionString,Forename, Surname, Email, Password, out message, out userId);
            //UserId = userId;
            budgetuserId = userId;
            Assert.IsTrue(userStatus == 1);            
        }

        [TestMethod]
        /// <summary>
        /// This test will signin the newly created user
        /// </summary>

        public void LoginTest()
        {
            string UserId = "0";
            UserProcess userProcess = new UserProcess();
            int userStatus = userProcess.LoginUser(ConfigurationManager.ConnectionStrings["dbconnecttest"].ConnectionString,Email, Password , out UserId);
            Assert.IsTrue(userStatus == 1);
        }

        [TestMethod]
        /// <summary>
        /// This test will Create Budget for the new user
        /// </summary>

        public void CreateBudget()
        {
            string UserId = budgetuserId;
            string UserEmail = Email;
            string BudgetName = "Test Budget";
            string BudgetAmount = "100";
            string BudgetDescription = "Budget Description";
            string CategoryId = "1";
            DateTime SelectedDate = DateTime.Now.AddDays(1);
            string message = "";
            string BudgetId = "0";
            BudgetProcess budgetProcess = new BudgetProcess();
            int budgetStatus = budgetProcess.CreateBudget(ConfigurationManager.ConnectionStrings["dbconnecttest"].ConnectionString, BudgetName,
                BudgetAmount, BudgetDescription, CategoryId, UserEmail, UserId, SelectedDate,out BudgetId, out message);
            budgetId = BudgetId;

            Assert.IsTrue(budgetStatus == 1);
        }

        [TestMethod]
        /// <summary>
        /// This Test Will Create Transaction
        /// </summary>
        public void CreateTransaction()
        {
            string TransactionDescription = "Desc";
            string TransactionAmount = "100";
            string TransactionType = "1";
            DateTime TransactionDate = DateTime.Now.AddDays(1);
            string TransactionNote = "Unit Test Note";
            string CategoryId = "1";
            string BudgetId = budgetId;
            string message = "";

            TransactionProcess transactionProcess = new TransactionProcess();
            int transactionStatus = transactionProcess.CreateTransaction(ConfigurationManager.ConnectionStrings["dbconnecttest"].ConnectionString, TransactionDescription, TransactionAmount,
                TransactionType, BudgetId, TransactionNote, CategoryId, TransactionDate, out message);
            Assert.IsTrue(transactionStatus == 1);

        }
    }
}
