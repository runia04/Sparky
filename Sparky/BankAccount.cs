﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        public int balance { get; set; }
        private readonly ILogBook _logBook;
        public BankAccount(ILogBook logBook)
        {
            _logBook = logBook;
            balance = 0;
        }
        public bool Deposit(int amount)
        {
            _logBook.Message("Deposit invoked");//true
            _logBook.Message("Test");
            _logBook.LogSeverity = 101;
            var temp = _logBook.LogSeverity;
            balance += amount;
            return true;
        }
        public bool Withdraw(int amount)
        {
            if (amount <= balance)
            {
                _logBook.LogToDb("Withdrawal Amount: "+amount.ToString());
                balance -= amount;
                return _logBook.LogBalanceAfterWithdraw(balance);
            }
            return _logBook.LogBalanceAfterWithdraw(balance-amount); ;
        }
        public int GetBalance() 
        {
            return balance;
        }
    }
}
