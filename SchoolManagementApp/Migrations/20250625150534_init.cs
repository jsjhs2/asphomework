using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagementApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseCode);
                });

            migrationBuilder.CreateTable(
                name: "OperationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassCode", "ClassName", "CreatedDate", "Description", "LastUpdatedDate", "TeacherName" },
                values: new object[,]
                {
                    { 1, "CS1", "计算机一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(90), null, null, "张老师" },
                    { 2, "MATH1", "数学一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8093), null, null, "李老师" },
                    { 3, "ENG1", "英语一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8106), null, null, "王老师" },
                    { 4, "PHY1", "物理一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8108), null, null, "赵老师" },
                    { 5, "CHEM1", "化学一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8110), null, null, "钱老师" },
                    { 6, "BIO1", "生物一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8111), null, null, "孙老师" },
                    { 7, "HIST1", "历史一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8112), null, null, "周老师" },
                    { 8, "LAW1", "法学一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8114), null, null, "吴老师" },
                    { 9, "ART1", "艺术一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8115), null, null, "郑老师" },
                    { 10, "PE1", "体育一班", new DateTime(2025, 6, 25, 23, 5, 34, 580, DateTimeKind.Local).AddTicks(8116), null, null, "王五" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseCode", "CourseName", "Credit", "Department" },
                values: new object[,]
                {
                    { "ART101", "艺术鉴赏", 1.5, "艺术学院" },
                    { "LAW101", "法学导论", 2.0, "法学院" },
                    { "HIST101", "中国历史", 2.0, "历史学院" },
                    { "BIO101", "生物科学", 2.5, "生命科学学院" },
                    { "CS101", "计算机基础", 3.0, "计算机学院" },
                    { "PHY101", "大学物理", 3.5, "物理学院" },
                    { "ENG301", "大学英语", 2.0, "外国语学院" },
                    { "MATH201", "高等数学", 4.0, "数学学院" },
                    { "PE101", "体育", 1.0, "体育学院" },
                    { "CHEM101", "基础化学", 3.0, "化学学院" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DisplayName", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, "系统管理员", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", "Admin", "sys" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ClassId", "DateOfBirth", "Email", "Gender", "Name", "Password", "PhoneNumber", "RollNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "男", "小明", null, null, "2023001" },
                    { 2, 2, new DateTime(2005, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "女", "小红", null, null, "2023002" },
                    { 3, 3, new DateTime(2005, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "男", "小刚", null, null, "2023003" },
                    { 4, 4, new DateTime(2005, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "女", "小丽", null, null, "2023004" },
                    { 5, 5, new DateTime(2005, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "男", "小强", null, null, "2023005" },
                    { 6, 6, new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "女", "小芳", null, null, "2023006" },
                    { 7, 7, new DateTime(2005, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "男", "小军", null, null, "2023007" },
                    { 8, 8, new DateTime(2005, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "女", "小燕", null, null, "2023008" },
                    { 9, 9, new DateTime(2005, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "男", "小鹏", null, null, "2023009" },
                    { 10, 10, new DateTime(2005, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "女", "小玉", null, null, "2023010" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "Score", "StudentId", "Subject" },
                values: new object[,]
                {
                    { 1, 85m, 1, "计算机基础" },
                    { 28, 91m, 6, "大学英语" },
                    { 29, 89m, 6, "基础化学" },
                    { 30, 85m, 6, "体育" },
                    { 31, 78m, 7, "中国历史" },
                    { 32, 72m, 7, "高等数学" },
                    { 33, 85m, 7, "大学英语" },
                    { 34, 68m, 7, "大学物理" },
                    { 35, 88m, 7, "体育" },
                    { 36, 91m, 8, "法学导论" },
                    { 37, 86m, 8, "高等数学" },
                    { 38, 89m, 8, "大学英语" },
                    { 39, 94m, 8, "中国历史" },
                    { 40, 82m, 8, "体育" },
                    { 41, 95m, 9, "艺术鉴赏" },
                    { 42, 79m, 9, "高等数学" },
                    { 43, 87m, 9, "大学英语" },
                    { 44, 92m, 9, "中国历史" },
                    { 45, 89m, 9, "体育" },
                    { 46, 98m, 10, "体育" },
                    { 47, 84m, 10, "高等数学" },
                    { 48, 86m, 10, "大学英语" },
                    { 27, 83m, 6, "高等数学" },
                    { 26, 87m, 6, "生物科学" },
                    { 25, 90m, 5, "体育" },
                    { 24, 82m, 5, "大学物理" },
                    { 2, 78m, 1, "高等数学" },
                    { 3, 92m, 1, "大学英语" },
                    { 4, 88m, 1, "大学物理" },
                    { 5, 95m, 1, "体育" },
                    { 6, 90m, 2, "高等数学" },
                    { 7, 85m, 2, "大学英语" },
                    { 8, 87m, 2, "大学物理" },
                    { 9, 93m, 2, "基础化学" },
                    { 10, 88m, 2, "体育" },
                    { 11, 88m, 3, "大学英语" },
                    { 49, 80m, 10, "大学物理" },
                    { 12, 82m, 3, "高等数学" },
                    { 14, 89m, 3, "基础化学" },
                    { 15, 92m, 3, "体育" },
                    { 16, 92m, 4, "大学物理" },
                    { 17, 88m, 4, "高等数学" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "Score", "StudentId", "Subject" },
                values: new object[,]
                {
                    { 18, 85m, 4, "大学英语" },
                    { 19, 90m, 4, "基础化学" },
                    { 20, 87m, 4, "体育" },
                    { 21, 80m, 5, "基础化学" },
                    { 22, 85m, 5, "高等数学" },
                    { 23, 78m, 5, "大学英语" },
                    { 13, 75m, 3, "大学物理" },
                    { 50, 88m, 10, "基础化学" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "OperationLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
