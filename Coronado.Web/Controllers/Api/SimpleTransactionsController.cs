﻿using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronado.Web.Data;
using Coronado.Web.Domain;
using Coronado.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coronado.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleTransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimpleTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSimpleTransaction() {
            return Ok("moo");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimpleTransaction([FromRoute] Guid id, 
                [FromBody] SimpleTransaction simpleTransaction) {

            if (id != simpleTransaction.TransactionId)
            {
                return BadRequest();
            }


            var transaction = await _context.Transactions.FindAsync(simpleTransaction.TransactionId);
            try
            {
                transaction.Vendor = simpleTransaction.Vendor;
                transaction.Description = simpleTransaction.Description;
                transaction.Date = simpleTransaction.TransactionDate;
                transaction.Category = await _context.Categories.FindAsync(simpleTransaction.CategoryId);
                transaction.Amount = simpleTransaction.Amount;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> PostSimpleTransaction([FromBody] SimpleTransaction transaction)
        {
            if (transaction.TransactionId == null) transaction.TransactionId = Guid.NewGuid();
            Account account;
            if (transaction.AccountId != null) {
                account = await _context.Accounts.FindAsync(transaction.AccountId);
            } else {
                account = _context.Accounts.FirstOrDefault(a => a.Name.Equals(transaction.AccountName, StringComparison.CurrentCultureIgnoreCase));
            }
            await _context.Entry(account).Collection(a => a.Transactions).LoadAsync();
            Category category;
            if (transaction.CategoryId == null) {
                category = _context.Categories.FirstOrDefault(c => (c.Name.Equals(transaction.CategoryName, StringComparison.CurrentCultureIgnoreCase)));
            } else {
                category = await _context.Categories.FindAsync(transaction.CategoryId);
            }

            var newTransaction = new Transaction {
                TransactionId = transaction.TransactionId,
                Date = transaction.TransactionDate,
                Vendor = transaction.Vendor,
                Description = transaction.Description,
                Account = account,
                Category = category,
                Amount = transaction.Amount
            };

            var bankFeeTransactions = GetBankFeeTransactions(transaction.Description, account, transaction.TransactionDate);
            var transactions = new List<Transaction>();
            transactions.Add(newTransaction);
            transactions.AddRange(bankFeeTransactions);
            _context.Transactions.AddRange(transactions);
            await _context.SaveChangesAsync();
            newTransaction.Account = null;

            return CreatedAtAction("GetTransaction", new { id = newTransaction.TransactionId }, transactions);
        }

        private IEnumerable<Transaction> GetBankFeeTransactions(string description, Account account, DateTime transactionDate) {
            var transactions = new List<Transaction>();

            var category = _context.Categories.First(c => c.Name.Equals("bank fees", StringComparison.CurrentCultureIgnoreCase));
            if (description.Contains("bf:", StringComparison.CurrentCultureIgnoreCase)) {
                var parsed = description.Substring(description.IndexOf("bf:", 0, StringComparison.CurrentCultureIgnoreCase));
                while (parsed.StartsWith("bf:", StringComparison.CurrentCultureIgnoreCase)) {
                    var next = parsed.IndexOf("bf:", 1, StringComparison.CurrentCultureIgnoreCase);
                    if (next == -1) next = parsed.Length;
                    var transactionData = (parsed.Substring(3, next - 3)).Trim().Split(" ");
                    Decimal amount;
                    if (decimal.TryParse(transactionData[0], out amount)) {
                        var transaction = new Transaction {
                            TransactionId = Guid.NewGuid(),
                            Date = transactionDate,
                            Account = account,
                            Category = category,
                            Amount = 0 - amount
                        };
                        transactions.Add(transaction);
                    }
                    parsed = parsed.Substring(next);
                } 
            }
            return transactions;
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }

}