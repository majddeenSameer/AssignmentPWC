using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currency_Currency_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclarationReason",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentReasonId = table.Column<long>(type: "bigint", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeclarationReason_DeclarationReason_ParentReasonId",
                        column: x => x.ParentReasonId,
                        principalTable: "DeclarationReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisclosureType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisclosureType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisclosureType_DisclosureType_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DisclosureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemplateBodyArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateBodyEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateSubjectArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateSubjectEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emirate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emirate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobDescription",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficerTransactionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficerTransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassengerType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestSource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResidencyStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidencyStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelingType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDomainsTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomainsTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDomainsTransactions_DomainTransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "DomainTransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentGroupId = table.Column<long>(type: "bigint", nullable: false),
                    ContentSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtentionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_AttachmentGroup_AttachmentGroupId",
                        column: x => x.AttachmentGroupId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalField",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeclarationReasonId = table.Column<long>(type: "bigint", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalField_DeclarationReason_DeclarationReasonId",
                        column: x => x.DeclarationReasonId,
                        principalTable: "DeclarationReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyDisclosureType",
                columns: table => new
                {
                    CurrenciesId = table.Column<long>(type: "bigint", nullable: false),
                    DisclosureTypesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDisclosureType", x => new { x.CurrenciesId, x.DisclosureTypesId });
                    table.ForeignKey(
                        name: "FK_CurrencyDisclosureType_Currency_CurrenciesId",
                        column: x => x.CurrenciesId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyDisclosureType_DisclosureType_DisclosureTypesId",
                        column: x => x.DisclosureTypesId,
                        principalTable: "DisclosureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Port",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmirateId = table.Column<long>(type: "bigint", nullable: true),
                    PortNumber = table.Column<long>(type: "bigint", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    portTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Port", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Port_Emirate_EmirateId",
                        column: x => x.EmirateId,
                        principalTable: "Emirate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Port_PortType_portTypeId",
                        column: x => x.portTypeId,
                        principalTable: "PortType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransporterType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    portTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporterType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransporterType_PortType_portTypeId",
                        column: x => x.portTypeId,
                        principalTable: "PortType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Officer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedPortId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfficePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserStatusId = table.Column<long>(type: "bigint", nullable: true),
                    UserTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Officer_Port_AssignedPortId",
                        column: x => x.AssignedPortId,
                        principalTable: "Port",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Officer_UserStatus_UserStatusId",
                        column: x => x.UserStatusId,
                        principalTable: "UserStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Officer_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalAttatchmentId = table.Column<long>(type: "bigint", nullable: true),
                    ArrivingFromId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBirthHijri = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartureToId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmirateId = table.Column<long>(type: "bigint", nullable: true),
                    EmiratesIDCopyBackSideId = table.Column<long>(type: "bigint", nullable: true),
                    EmiratesIDCopyFrontsideId = table.Column<long>(type: "bigint", nullable: true),
                    EmiratesIDN = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsTravellerHaveAccompanyingPerson = table.Column<bool>(type: "bit", nullable: false),
                    JobDescriptionId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true),
                    OtherJobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherNationalityId = table.Column<long>(type: "bigint", nullable: true),
                    OtherNationalityIdPassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerTypeId = table.Column<long>(type: "bigint", nullable: true),
                    PassportCopyAttatchmentId = table.Column<long>(type: "bigint", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBirthId = table.Column<long>(type: "bigint", nullable: true),
                    PortId = table.Column<long>(type: "bigint", nullable: true),
                    RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestSourceId = table.Column<long>(type: "bigint", nullable: true),
                    ResidenceyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidencyStatusId = table.Column<long>(type: "bigint", nullable: true),
                    StayingPeriod = table.Column<long>(type: "bigint", nullable: true),
                    TransportStatusId = table.Column<long>(type: "bigint", nullable: true),
                    TransporterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporterTypeId = table.Column<long>(type: "bigint", nullable: true),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelerSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelingTypeId = table.Column<long>(type: "bigint", nullable: true),
                    UAEPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnifiedNumber = table.Column<long>(type: "bigint", nullable: true),
                    requestStatusId = table.Column<long>(type: "bigint", nullable: true),
                    ResidenceyAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceyAddress_CountryId = table.Column<long>(type: "bigint", nullable: true),
                    ResidenceyAddress_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceyAddress_EmirateId = table.Column<long>(type: "bigint", nullable: true),
                    ResidenceyAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceyAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceyAddress_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAEAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAEAddress_CountryId = table.Column<long>(type: "bigint", nullable: true),
                    UAEAddress_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAEAddress_EmirateId = table.Column<long>(type: "bigint", nullable: true),
                    UAEAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAEAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UAEAddress_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_ArabicValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_EnglishValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_FamilyNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_FamilyNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_FirstNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_FirstNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_SecondNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName_SecondNameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AttachmentGroup_AdditionalAttatchmentId",
                        column: x => x.AdditionalAttatchmentId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_AttachmentGroup_EmiratesIDCopyBackSideId",
                        column: x => x.EmiratesIDCopyBackSideId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_AttachmentGroup_EmiratesIDCopyFrontsideId",
                        column: x => x.EmiratesIDCopyFrontsideId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_AttachmentGroup_PassportCopyAttatchmentId",
                        column: x => x.PassportCopyAttatchmentId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_ArrivingFromId",
                        column: x => x.ArrivingFromId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_DepartureToId",
                        column: x => x.DepartureToId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_OtherNationalityId",
                        column: x => x.OtherNationalityId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_PlaceOfBirthId",
                        column: x => x.PlaceOfBirthId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_ResidenceyAddress_CountryId",
                        column: x => x.ResidenceyAddress_CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Country_UAEAddress_CountryId",
                        column: x => x.UAEAddress_CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Emirate_EmirateId",
                        column: x => x.EmirateId,
                        principalTable: "Emirate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Emirate_ResidenceyAddress_EmirateId",
                        column: x => x.ResidenceyAddress_EmirateId,
                        principalTable: "Emirate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Emirate_UAEAddress_EmirateId",
                        column: x => x.UAEAddress_EmirateId,
                        principalTable: "Emirate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_JobDescription_JobDescriptionId",
                        column: x => x.JobDescriptionId,
                        principalTable: "JobDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_PassengerType_PassengerTypeId",
                        column: x => x.PassengerTypeId,
                        principalTable: "PassengerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Port_PortId",
                        column: x => x.PortId,
                        principalTable: "Port",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_RequestSource_RequestSourceId",
                        column: x => x.RequestSourceId,
                        principalTable: "RequestSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_RequestStatus_requestStatusId",
                        column: x => x.requestStatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_ResidencyStatus_ResidencyStatusId",
                        column: x => x.ResidencyStatusId,
                        principalTable: "ResidencyStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_TransporterType_TransporterTypeId",
                        column: x => x.TransporterTypeId,
                        principalTable: "TransporterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_TransportStatus_TransportStatusId",
                        column: x => x.TransportStatusId,
                        principalTable: "TransportStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_TravelingType_TravelingTypeId",
                        column: x => x.TravelingTypeId,
                        principalTable: "TravelingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficerTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfficerId = table.Column<long>(type: "bigint", nullable: true),
                    OfficerTransactionTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficerTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficerTransaction_Officer_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfficerTransaction_OfficerTransactionType_OfficerTransactionTypeId",
                        column: x => x.OfficerTransactionTypeId,
                        principalTable: "OfficerTransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeclarationDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeclarationReasonId = table.Column<long>(type: "bigint", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisclosureTypeId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_CurrencyDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount_CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_ValueInLocalCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeclarationDetail_Currency_Amount_CurrencyId",
                        column: x => x.Amount_CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeclarationDetail_DeclarationReason_DeclarationReasonId",
                        column: x => x.DeclarationReasonId,
                        principalTable: "DeclarationReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeclarationDetail_DisclosureType_DisclosureTypeId",
                        column: x => x.DisclosureTypeId,
                        principalTable: "DisclosureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeclarationDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DomainTransactionType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1L, null, "نطاق جديد", "New Domain", null, null },
                    { 2L, null, "تعديل بيانات", "Updating information", null, null },
                    { 3L, null, "حذف", "Deletaion", null, null }
                });

            migrationBuilder.InsertData(
                table: "Emirate",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 7L, null, "الفجيرة", "Fujairah", null, null },
                    { 5L, null, "الشارقة", "Sharjah", null, null },
                    { 4L, null, "عجمان", "Ajman", null, null },
                    { 6L, null, "أم القيوين", "Umm Al Quwain", null, null },
                    { 2L, null, "دبي", "Dubai", null, null },
                    { 1L, null, "ابوظبي", " Abu Dhabi", null, null },
                    { 3L, null, "رأس الخيمة", "RAK", null, null }
                });

            migrationBuilder.InsertData(
                table: "OfficerTransactionType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1L, null, "إضافة", "Add", null, null },
                    { 2L, null, "تعديل", "Update", null, null },
                    { 3L, null, "حذف", "Delete", null, null }
                });

            migrationBuilder.InsertData(
                table: "PassengerType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 3L, null, "زائر", "Visitor", null, null },
                    { 1L, null, "مواطن", " Citizen", null, null },
                    { 2L, null, "مقيم", "Resident", null, null }
                });

            migrationBuilder.InsertData(
                table: "PortType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 7L, null, "منطقة حرة", "Free zone", null, null },
                    { 6L, null, "منفذ جوي", "Airport ", null, null },
                    { 4L, null, "منفذ بحري", "Seaport", null, null },
                    { 5L, null, "منفذ بري", "Land port", null, null }
                });

            migrationBuilder.InsertData(
                table: "RequestSource",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1L, null, "النظام المحلي", "Local System", null, null },
                    { 2L, null, "النظام الفيدرالي", "Federal system", null, null }
                });

            migrationBuilder.InsertData(
                table: "TransactionType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1L, null, " إفصاح صحيح", "Right Disclosure", null, null },
                    { 2L, null, "إفصاح خاطيء", " False Disclosure", null, null }
                });

            migrationBuilder.InsertData(
                table: "TransporterType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo", "portTypeId" },
                values: new object[,]
                {
                    { 4L, null, "سيارة", "Car", null, null, null },
                    { 9L, null, "سفينة", "Ship", null, null, null },
                    { 8L, null, "شاحنة", "Truck", null, null, null },
                    { 7L, null, "ناقلة", "Tanker", null, null, null },
                    { 6L, null, "طائرة", "Plane", null, null, null },
                    { 3L, null, "حافلة", "Bus", null, null, null },
                    { 10L, null, "حاوية", "Vessel", null, null, null },
                    { 2L, null, "دراجة نارية", "Motorcycle", null, null, null },
                    { 1L, null, "دراجة", "Bicycle", null, null, null },
                    { 5L, null, "طرد", "Mail", null, null, null },
                    { 11L, null, "المشي", "Walking", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "TravelingType",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1L, null, "وصول", " Arrival", null, null },
                    { 2L, null, "مغادرة", "Departure", null, null }
                });

            migrationBuilder.InsertData(
                table: "Port",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "EmirateId", "PortNumber", "ValidFrom", "ValidTo", "portTypeId" },
                values: new object[,]
                {
                    { 18L, null, "مركز جمارك مدينة دبي للإنترنت", " Dubai Internet City Customs Center", 2L, 15L, null, null, 8L },
                    { 53L, null, " جمرك الدارة الحدودي ", " Al Dara Custom ", 3L, 50L, null, null, 5L },
                    { 66L, null, " بريد الإمارات - قسم تفتيش جمارك البريد ", "Emirates Post - Fujairah ", 7L, 62L, null, null, 5L },
                    { 9L, null, " جمرك بريد أبوظبي", "Abu Dhabi Post Office Customs ", 1L, 6L, null, null, 6L },
                    { 10L, null, " مطار أبوظبي", "Abu Dhabi Airport ", 1L, 7L, null, null, 6L },
                    { 11L, null, " مطار العين", "Al Ain Airport ", 1L, 8L, null, null, 6L },
                    { 14L, null, " جمرك بريد العين", "Al Ain Post- Office Customs", 1L, 11L, null, null, 6L },
                    { 15L, null, "جمرك مطار أبوظبي- البطين", " Abu Dhabi Airport-Al Bateen TER", 1L, 12L, null, null, 6L },
                    { 25L, null, "  مركز جمارك البريد - دبي", "Post Customs Center ", 2L, 22L, null, null, 6L },
                    { 26L, null, "مركز جمارك السلع و المعادن المتعددة", "Multi Commiddities Customs Center ", 2L, 23L, null, null, 6L },
                    { 27L, null, "مركز جمارك قرية دبي للشحن", " Dubai Cargo Village Customs Center", 2L, 24L, null, null, 6L },
                    { 28L, null, " مركز جمارك مطار المنطقة الحرة بالمطار ", " Airport Free Zone Customs Center", 2L, 25L, null, null, 6L },
                    { 32L, null, " مطار آل مكتوم ", " Al Maktoum International Airport", 2L, 29L, null, null, 6L },
                    { 33L, null, "  مركز جمرك مطار دبي الدولي 1", " Dubai International Airport - Terminal 1", 2L, 30L, null, null, 6L },
                    { 44L, null, "مركز جمارك الحاويات ", " Container Center", 5L, 41L, null, null, 5L },
                    { 34L, null, "  مركز جمرك مطار دبي الدولي 2", " Dubai International Airport - Terminal 2", 2L, 31L, null, null, 6L },
                    { 40L, null, "مركز جمارك الشحن الجوي / مطار الشارقه ", " Air Freight Customs Center", 5L, 37L, null, null, 6L },
                    { 42L, null, "  مركز جمارك الركاب / مطار الشارقه", " Sharjah Airport", 5L, 39L, null, null, 6L },
                    { 51L, null, "  جمرك بريد الإمارات/رأس الخيمة", "  Emirates Post Custom/RAK", 3L, 48L, null, null, 6L },
                    { 52L, null, "  جمرك مطار رأس الخيمة الدولي", " RAK international Airport Custom", 3L, 49L, null, null, 6L },
                    { 62L, null, " جمارك مطار الفجيرة الدولي ", "Fujairah Airport - Customs Section ", 7L, 58L, null, null, 6L },
                    { 63L, null, " جمارك الشحن بمطار الفجيرة الدولي ", "Fujairah Airport Cargo - Customs Section ", 7L, 59L, null, null, 6L },
                    { 16L, null, " مركز جمارك جبل علي ", " Jebel Ali Port Customs Center", 2L, 13L, null, null, 7L },
                    { 21L, null, "مركز جمارك مدينة دبي اللوجستية ", "Dubai Logistics City Customs Center ", 2L, 18L, null, null, 7L },
                    { 41L, null, " مركز جمارك المنطقه الحره بالمطار", " Free Zone Airport Port", 5L, 38L, null, null, 7L },
                    { 43L, null, " مركز جمارك المنطقة الحرة بالحمرية - الشارقة ", " Al Hamriyyah Free Zone - Sharjah", 5L, 40L, null, null, 7L },
                    { 49L, null, "  منطقة عجمان الحرة", " Ajman Free Zone", 4L, 46L, null, null, 7L },
                    { 57L, null, " جمرك المنطقة الحرة بالغيل ", "Al Ghail Free Zone Custom ", 3L, 54L, null, null, 7L },
                    { 58L, null, " جمرك المناطق الحرة الشمالية ", "North Free Zone Customs ", 3L, 55L, null, null, 7L },
                    { 35L, null, "  مركز جمرك مطار دبي الدولي 3", " Dubai International Airport - Terminal 3", 2L, 32L, null, null, 6L },
                    { 59L, null, " جمرك المنطقة الحرة الجنوبية ", "South Free Zone Customs ", 3L, 56L, null, null, 7L },
                    { 38L, null, "مركز جمارك خطم الملاحة ", " Khatam Al Malaha", 5L, 35L, null, null, 5L },
                    { 24L, null, " مركز جمرك دوكامز - منطقة حرة ", "Ducamz Customs Center ", 2L, 21L, null, null, 5L },
                    { 19L, null, " مركز جمارك مدينة دبي للإعلام ", " Dubai Media City Customs Center", 2L, 16L, null, null, 8L },
                    { 20L, null, "  مركز مدينة دبي الطبية", "Dubai Health Care City Customs Center ", 2L, 17L, null, null, 8L },
                    { 47L, null, " ميناء وجمارك عجمان", " Ajman Port", 4L, 44L, null, null, 8L },
                    { 48L, null, "  مركز جمارك البريد", " Post Office Center - Ajman", 4L, 45L, null, null, 8L },
                    { 50L, null, "  جمرك ميناء أحمد بن راشد", " Umm Al Qaiwain", 6L, 47L, null, null, 8L },
                    { 4L, null, " جمرك ميناء زايد", "Mina Zayed Port", 1L, 1L, null, null, 4L },
                    { 12L, null, " مركز جمرك  ميناء خليفه ", " Khalifa Port Customs Centre", 1L, 9L, null, null, 4L },
                    { 17L, null, "مركز جمرك ميناء راشد", "Port Rashid Customs ", 2L, 14L, null, null, 4L },
                    { 29L, null, "مركز جمارك ميناء الحمرية - دبي", " Hamriyah Port Customs Center - Dubai", 2L, 26L, null, null, 4L }
                });

            migrationBuilder.InsertData(
                table: "Port",
                columns: new[] { "Id", "Code", "DescriptionArabic", "DescriptionEnglish", "EmirateId", "PortNumber", "ValidFrom", "ValidTo", "portTypeId" },
                values: new object[,]
                {
                    { 30L, null, "مركز جمارك الخور - دبي مرفأ ديرة ", " Dubai Creek Customs Center", 2L, 27L, null, null, 4L },
                    { 31L, null, " عمليات الخور البحرية", " creek marina operation", 2L, 28L, null, null, 4L },
                    { 37L, null, "  مركز جمارك ميناء خالد", " Khalid Port", 5L, 34L, null, null, 4L },
                    { 39L, null, "مركز جمارك خورفكان ", " Khor Fakkan Port", 5L, 36L, null, null, 4L },
                    { 36L, null, " مركز جمارك البريد - الشارقة ", " Sharjah Post", 5L, 33L, null, null, 5L },
                    { 45L, null, " مركز جمارك الخور - الشارقة ", " Al Khor Port", 5L, 42L, null, null, 4L },
                    { 54L, null, "  جمرك ميناء الجير", "Al Jeer Port Custom ", 3L, 51L, null, null, 4L },
                    { 55L, null, " جمرك ميناء راس الخيمة ", "Ras Al Khaimah Port Custom ", 3L, 52L, null, null, 4L },
                    { 56L, null, "  جمرك ميناء الجزيرة الحمراء", "Al Jazeera Al Hamra Port Custom", 3L, 53L, null, null, 4L },
                    { 60L, null, "  جمرك ميناء صقر", "Saqr Port Custom ", 3L, 57L, null, null, 4L },
                    { 61L, null, " جمرك ميناء الجير  ", "Aljeer Port Custom ", 3L, 63L, null, null, 4L },
                    { 65L, null, " جمرك ميناء الفجيرة ", "Fujairah SeaPort ", 7L, 61L, null, null, 4L },
                    { 5L, null, " جمرك خطم الشكلة", "khtam Al-shakleh ", 1L, 2L, null, null, 5L },
                    { 6L, null, " مركز جمرك مزيد", "MZYED ", 1L, 3L, null, null, 5L },
                    { 7L, null, "منفذ المضيف ", "Al Madheef", 1L, 4L, null, null, 5L },
                    { 8L, null, " مركز الهيلي", "Al Hily", 1L, 5L, null, null, 5L },
                    { 13L, null, " مركز جمرك الغويفات", "Guwaifat ", 1L, 10L, null, null, 5L },
                    { 22L, null, "  مركز جمارك حتا", "Hatta customs Center ", 2L, 19L, null, null, 5L },
                    { 23L, null, " مركز جمارك الميناء الجاف ", "Dry Port Customs Center ", 2L, 20L, null, null, 5L },
                    { 46L, null, "  ميناء دبا الحصن", " Dibba Al Hisn Marina", 5L, 43L, null, null, 4L },
                    { 64L, null, " جمرك المنطقة الحرة ", "Fujairah Free Zone ", 7L, 60L, null, null, 7L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalField_DeclarationReasonId",
                table: "AdditionalField",
                column: "DeclarationReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentGroupId",
                table: "Attachment",
                column: "AttachmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_ParentId",
                table: "Currency",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDisclosureType_DisclosureTypesId",
                table: "CurrencyDisclosureType",
                column: "DisclosureTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationDetail_Amount_CurrencyId",
                table: "DeclarationDetail",
                column: "Amount_CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationDetail_DeclarationReasonId",
                table: "DeclarationDetail",
                column: "DeclarationReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationDetail_DisclosureTypeId",
                table: "DeclarationDetail",
                column: "DisclosureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationDetail_UserId",
                table: "DeclarationDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationReason_ParentReasonId",
                table: "DeclarationReason",
                column: "ParentReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DisclosureType_ParentId",
                table: "DisclosureType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Officer_AssignedPortId",
                table: "Officer",
                column: "AssignedPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Officer_UserStatusId",
                table: "Officer",
                column: "UserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Officer_UserTypeId",
                table: "Officer",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficerTransaction_OfficerId",
                table: "OfficerTransaction",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficerTransaction_OfficerTransactionTypeId",
                table: "OfficerTransaction",
                column: "OfficerTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Port_EmirateId",
                table: "Port",
                column: "EmirateId");

            migrationBuilder.CreateIndex(
                name: "IX_Port_portTypeId",
                table: "Port",
                column: "portTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeId",
                table: "Transaction",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransporterType_portTypeId",
                table: "TransporterType",
                column: "portTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AdditionalAttatchmentId",
                table: "User",
                column: "AdditionalAttatchmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ArrivingFromId",
                table: "User",
                column: "ArrivingFromId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartureToId",
                table: "User",
                column: "DepartureToId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmirateId",
                table: "User",
                column: "EmirateId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmiratesIDCopyBackSideId",
                table: "User",
                column: "EmiratesIDCopyBackSideId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmiratesIDCopyFrontsideId",
                table: "User",
                column: "EmiratesIDCopyFrontsideId");

            migrationBuilder.CreateIndex(
                name: "IX_User_JobDescriptionId",
                table: "User",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_NationalityId",
                table: "User",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OtherNationalityId",
                table: "User",
                column: "OtherNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PassengerTypeId",
                table: "User",
                column: "PassengerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PassportCopyAttatchmentId",
                table: "User",
                column: "PassportCopyAttatchmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PlaceOfBirthId",
                table: "User",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PortId",
                table: "User",
                column: "PortId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RequestSourceId",
                table: "User",
                column: "RequestSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_requestStatusId",
                table: "User",
                column: "requestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ResidenceyAddress_CountryId",
                table: "User",
                column: "ResidenceyAddress_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ResidenceyAddress_EmirateId",
                table: "User",
                column: "ResidenceyAddress_EmirateId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ResidencyStatusId",
                table: "User",
                column: "ResidencyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TransporterTypeId",
                table: "User",
                column: "TransporterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TransportStatusId",
                table: "User",
                column: "TransportStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TravelingTypeId",
                table: "User",
                column: "TravelingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UAEAddress_CountryId",
                table: "User",
                column: "UAEAddress_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UAEAddress_EmirateId",
                table: "User",
                column: "UAEAddress_EmirateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDomainsTransactions_TransactionTypeId",
                table: "UserDomainsTransactions",
                column: "TransactionTypeId");
        }
    }
}
