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
                    { 1, "CS1", "计算机一班", new DateTime(2025, 6, 25, 0, 3, 32, 453, DateTimeKind.Local).AddTicks(8359), null, null, "张老师" },
                    { 2, "MATH1", "数学一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1925), null, null, "李老师" },
                    { 3, "ENG1", "英语一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1945), null, null, "王老师" },
                    { 4, "PHY1", "物理一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1949), null, null, "赵老师" },
                    { 5, "CHEM1", "化学一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1951), null, null, "钱老师" },
                    { 6, "BIO1", "生物一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1953), null, null, "孙老师" },
                    { 7, "HIST1", "历史一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1954), null, null, "周老师" },
                    { 8, "LAW1", "法学一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1957), null, null, "吴老师" },
                    { 9, "ART1", "艺术一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1960), null, null, "郑老师" },
                    { 10, "PE1", "体育一班", new DateTime(2025, 6, 25, 0, 3, 32, 455, DateTimeKind.Local).AddTicks(1970), null, null, "王五" }
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
                    { 2, 90m, 2, "高等数学" },
                    { 3, 88m, 3, "大学英语" },
                    { 4, 92m, 4, "大学物理" },
                    { 5, 80m, 5, "基础化学" },
                    { 6, 87m, 6, "生物科学" },
                    { 7, 78m, 7, "中国历史" },
                    { 8, 91m, 8, "法学导论" },
                    { 9, 95m, 9, "艺术鉴赏" },
                    { 10, 98m, 10, "体育" }
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
                name: "Users");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
