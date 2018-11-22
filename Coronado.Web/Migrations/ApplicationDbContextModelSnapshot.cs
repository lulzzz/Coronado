﻿// <auto-generated />
using System;
using Coronado.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coronado.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        new { CategoryId = new Guid("426dcfe2-24b3-43c0-b4e4-97d3b27860f1"), Name = "Starting Balance", Type = "Income" },
                        new { CategoryId = new Guid("5fed3e42-e6c9-4881-863a-f7942fb4f461"), Name = "Bank Fees", Type = "Expense" }
                    );
                });

            modelBuilder.Entity("Coronado.Web.Domain.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("customer_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("CustomerId")
                        .HasName("pk_customers");

                    b.ToTable("customers");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("normalized_name")
                        .HasMaxLength(256);

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("role_name_index");

                    b.ToTable("asp_net_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasName("ix_asp_net_role_claims_role_id");

                    b.ToTable("asp_net_role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("normalized_email")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("normalized_user_name")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasMaxLength(256);

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("user_name_index");

                    b.ToTable("asp_net_users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasName("ix_asp_net_user_claims_user_id");

                    b.ToTable("asp_net_user_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnName("login_provider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnName("provider_key")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasName("ix_asp_net_user_logins_user_id");

                    b.ToTable("asp_net_user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasName("ix_asp_net_user_roles_role_id");

                    b.ToTable("asp_net_user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnName("login_provider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("asp_net_user_tokens");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
