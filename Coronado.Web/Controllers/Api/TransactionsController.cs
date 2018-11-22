﻿using System;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IInvoiceRepository _invoiceRepo;

        public TransactionsController(ApplicationDbContext context, ITransactionRepository transactionRepo,
            IAccountRepository accountRepo, ICategoryRepository categoryRepo, IInvoiceRepository invoiceRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
            _categoryRepo = categoryRepo;
            _invoiceRepo = invoiceRepo;
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<TransactionForDisplay> GetTransactions([FromQuery] UrlQuery query )
        {
            return _transactionRepo.GetByAccount(query.AccountId);
        }
        
        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = _transactionRepo.Get(id);
            _transactionRepo.Delete(id);
            InvoiceForPosting invoice = null;
            if (transaction.InvoiceId.HasValue) {
                invoice = _invoiceRepo.Get(transaction.InvoiceId.Value);
            }

            return Ok(new {transaction, accountBalances = _accountRepo.GetAccountBalances(), invoice});
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction([FromRoute] Guid id, 
                [FromBody] TransactionForDisplay transaction) {

            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            var originalAmount = _transactionRepo.Get(transaction.TransactionId).Amount;
            transaction.SetAmount();
            _transactionRepo.Update(transaction);
            InvoiceForPosting invoice = null;
            if (transaction.InvoiceId.HasValue) {
                invoice = _invoiceRepo.Get(transaction.InvoiceId.Value);
            }

            return Ok(new {transaction, originalAmount, accountBalances = _accountRepo.GetAccountBalances(), invoice});
        }

        [HttpPost]
        public IActionResult PostTransaction([FromBody] TransactionForDisplay transaction)
        {
            var transactions = new List<TransactionForDisplay>();
            if (transaction.TransactionId == null || transaction.TransactionId == Guid.Empty) transaction.TransactionId = Guid.NewGuid();
            if (transaction.AccountId == null) {
                transaction.AccountId = _accountRepo.GetAll().Single(a => a.Name.Equals(transaction.AccountName, StringComparison.CurrentCultureIgnoreCase)).AccountId;
            }
            transaction.SetAmount();
            transaction.EnteredDate = DateTime.Now;
            if (transaction.CategoryId == null && !string.IsNullOrWhiteSpace(transaction.CategoryName)) {
                transaction.CategoryId = _categoryRepo.GetAll().Single(c => (c.Name.Equals(transaction.CategoryName, StringComparison.CurrentCultureIgnoreCase))).CategoryId;
            }

            var bankFeeTransactions = TransactionHelpers.GetBankFeeTransactions(transaction, _categoryRepo, _accountRepo);
            transactions.Add(transaction);
            transactions.AddRange(bankFeeTransactions);
            foreach(var trx in transactions) {
                _transactionRepo.Insert(trx);
            }

            var accountBalances = _accountRepo.GetAccountBalances().Select(a => new {a.AccountId, a.CurrentBalance});
            InvoiceForPosting invoice = null;
            if (transaction.InvoiceId.HasValue) {
                invoice = _invoiceRepo.Get(transaction.InvoiceId.Value);
            }

            return CreatedAtAction("PostTransaction", new { id = transaction.TransactionId }, new {transactions, accountBalances, invoice});
        }

    }

    public class UrlQuery
    {
        public Guid AccountId { get; set; }
    }
}