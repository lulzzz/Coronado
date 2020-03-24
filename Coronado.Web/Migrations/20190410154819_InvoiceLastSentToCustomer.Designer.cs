﻿// <auto-generated />
using System;
using Coronado.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coronado.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190410154819_InvoiceLastSentToCustomer")]
    partial class InvoiceLastSentToCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Coronado.Web.Domain.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("account_id");

                    b.Property<string>("AccountType")
                        .HasColumnName("account_type");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnName("currency");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnName("current_balance");

                    b.Property<int>("DisplayOrder")
                        .HasColumnName("display_order");

                    b.Property<decimal?>("MortgagePayment")
                        .HasColumnName("mortgage_payment");

                    b.Property<string>("MortgageType")
                        .HasColumnName("mortgage_type");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Vendor")
                        .HasColumnName("vendor");

                    b.HasKey("AccountId")
                        .HasName("pk_accounts");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnName("parent_category_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type");

                    b.HasKey("CategoryId")
                        .HasName("pk_categories");

                    b.ToTable("categories");

                    b.HasData(
                        new { CategoryId = new Guid("4de79a2e-ef21-4879-acdb-0b18a77a79ab"), Name = "Starting Balance", Type = "Income" },
                        new { CategoryId = new Guid("2368d581-c012-4948-8ca3-cf021c798656"), Name = "Bank Fees", Type = "Expense" }
                    );
                });

            modelBuilder.Entity("Coronado.Web.Domain.Configuration", b =>
                {
                    b.Property<Guid>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("configuration_id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnName("value");

                    b.HasKey("ConfigurationId")
                        .HasName("pk_configuration");

                    b.ToTable("configuration");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Currency", b =>
                {
                    b.Property<string>("Symbol")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("symbol");

                    b.Property<DateTime>("LastRetrieved")
                        .HasColumnName("last_retrieved");

                    b.Property<decimal>("PriceInUsd")
                        .HasColumnName("price_in_usd");

                    b.HasKey("Symbol")
                        .HasName("pk_currencies");

                    b.ToTable("currencies");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("customer_id");

                    b.Property<string>("City")
                        .HasColumnName("city");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("Region")
                        .HasColumnName("region");

                    b.Property<string>("StreetAddress")
                        .HasColumnName("street_address");

                    b.HasKey("CustomerId")
                        .HasName("pk_customers");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Investment", b =>
                {
                    b.Property<Guid>("InvestmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("investment_id");

                    b.Property<string>("Currency")
                        .HasColumnName("currency");

                    b.Property<DateTime>("LastRetrieved")
                        .HasColumnName("last_retrieved");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnName("price");

                    b.Property<decimal>("Shares")
                        .HasColumnName("shares");

                    b.Property<string>("Symbol")
                        .HasColumnName("symbol");

                    b.Property<string>("Url")
                        .HasColumnName("url");

                    b.HasKey("InvestmentId")
                        .HasName("pk_investments");

                    b.ToTable("investments");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("invoice_id");

                    b.Property<decimal>("Balance")
                        .HasColumnName("balance");

                    b.Property<Guid>("CustomerId")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnName("invoice_number");

                    b.Property<bool>("IsPaidInFull")
                        .HasColumnName("is_paid_in_full");

                    b.Property<DateTime?>("LastSentToCustomer")
                        .HasColumnName("last_sent_to_customer");

                    b.HasKey("InvoiceId")
                        .HasName("pk_invoices");

                    b.HasIndex("CustomerId")
                        .HasName("ix_invoices_customer_id");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("Coronado.Web.Domain.InvoiceLineItem", b =>
                {
                    b.Property<Guid>("InvoiceLineItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("invoice_line_item_id");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnName("invoice_id");

                    b.Property<decimal>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<decimal>("UnitAmount")
                        .HasColumnName("unit_amount");

                    b.HasKey("InvoiceLineItemId")
                        .HasName("pk_invoice_line_items");

                    b.HasIndex("InvoiceId")
                        .HasName("ix_invoice_line_items_invoice_id");

                    b.ToTable("invoice_line_items");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("transaction_id");

                    b.Property<Guid>("AccountId")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<DateTime>("EnteredDate")
                        .HasColumnName("entered_date");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnName("invoice_id");

                    b.Property<bool>("IsReconciled")
                        .HasColumnName("is_reconciled");

                    b.Property<Guid?>("RelatedTransactionId")
                        .HasColumnName("related_transaction_id");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnName("transaction_date");

                    b.Property<string>("Vendor")
                        .HasColumnName("vendor");

                    b.HasKey("TransactionId")
                        .HasName("pk_transactions");

                    b.HasIndex("AccountId")
                        .HasName("ix_transactions_account_id");

                    b.HasIndex("CategoryId")
                        .HasName("ix_transactions_category_id");

                    b.HasIndex("InvoiceId")
                        .HasName("ix_transactions_invoice_id");

                    b.HasIndex("RelatedTransactionId")
                        .HasName("ix_transactions_related_transaction_id");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("Coronado.Web.Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Vendor", b =>
                {
                    b.Property<Guid>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("vendor_id");

                    b.Property<Guid>("LastTransactionCategoryId")
                        .HasColumnName("last_transaction_category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("VendorId")
                        .HasName("pk_vendors");

                    b.ToTable("vendors");
                });

            modelBuilder.Entity("Coronado.Web.Domain.Invoice", b =>
                {
                    b.HasOne("Coronado.Web.Domain.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_invoices_customers_customer_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Coronado.Web.Domain.InvoiceLineItem", b =>
                {
                    b.HasOne("Coronado.Web.Domain.Invoice", "Invoice")
                        .WithMany("LineItems")
                        .HasForeignKey("InvoiceId")
                        .HasConstraintName("fk_invoice_line_items_invoices_invoice_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Coronado.Web.Domain.Transaction", b =>
                {
                    b.HasOne("Coronado.Web.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .HasConstraintName("fk_transactions_accounts_account_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Coronado.Web.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fk_transactions_categories_category_id");

                    b.HasOne("Coronado.Web.Domain.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .HasConstraintName("fk_transactions_invoices_invoice_id");

                    b.HasOne("Coronado.Web.Domain.Transaction", "RelatedTransaction")
                        .WithMany()
                        .HasForeignKey("RelatedTransactionId")
                        .HasConstraintName("fk_transactions_transactions_related_transaction_id");
                });
#pragma warning restore 612, 618
        }
    }
}