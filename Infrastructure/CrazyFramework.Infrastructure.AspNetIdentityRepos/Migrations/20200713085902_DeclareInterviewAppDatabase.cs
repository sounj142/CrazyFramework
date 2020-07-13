using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Migrations
{
    public partial class DeclareInterviewAppDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Products",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Products",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Orders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Orders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "OrderItems",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "OrderItems",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Multiplier = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Cc = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    JobTitleId = table.Column<Guid>(nullable: false),
                    JobscoreUrl = table.Column<string>(maxLength: 500, nullable: true),
                    GitHubAccount = table.Column<string>(maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    JobTitleId = table.Column<Guid>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false, defaultValue: ""),
                    Template = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    CandidateId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Score = table.Column<double>(nullable: true),
                    SentDate = table.Column<DateTimeOffset>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: true),
                    AccessCode = table.Column<string>(maxLength: 80, nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: true),
                    GitHubRepositoryName = table.Column<string>(maxLength: 400, nullable: true),
                    GitHubInvitationId = table.Column<string>(maxLength: 100, nullable: true),
                    GitHubLink = table.Column<string>(maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTimeOffset>(nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    ExamId = table.Column<Guid>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 5000, nullable: false),
                    FieldType = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationNotes_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationNotes_EvaluationSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "EvaluationSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobTitleId",
                table: "Candidates",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationNotes_ExamId",
                table: "EvaluationNotes",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationNotes_SectionId",
                table: "EvaluationNotes",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CandidateId",
                table: "Exams",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_TestId",
                table: "Exams",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_JobTitleId",
                table: "Tests",
                column: "JobTitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationNotes");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "EvaluationSections");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
