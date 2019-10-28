using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlausDaryklosGamybosValdymoSistema.Migrations
{
    public partial class AlausDarykla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Vardas = table.Column<string>(nullable: true),
                    Pavarde = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klientas",
                columns: table => new
                {
                    KlientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KlientoPavadinimas = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klientas", x => x.KlientoID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "LaisviResursai",
                columns: table => new
                {
                    FormosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LaisvuResursuKodas = table.Column<string>(type: "char(1)", nullable: false),
                    ResursoPavadinimas = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaisviResursai", x => x.FormosID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Tiekejas",
                columns: table => new
                {
                    TiekejoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TiekejoPavadinimas = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    TiekejoTtipas = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiekejas", x => x.TiekejoID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlientoUzsakymoAplankas",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KlientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    Terminas = table.Column<DateTime>(type: "datetime", nullable: false),
                    Busena = table.Column<string>(type: "char(1)", nullable: false),
                    Archyvavimo_Komentaras = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlientoUzsakymoAplankas", x => x.KlientoUzsakymoID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_KlientoUzsakymoAplankas_Klientas_KlientoID",
                        column: x => x.KlientoID,
                        principalTable: "Klientas",
                        principalColumn: "KlientoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientas",
                columns: table => new
                {
                    IngredientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: true),
                    Ingredientopavadinimas = table.Column<string>(name: "Ingrediento pavadinimas", unicode: false, maxLength: 50, nullable: false),
                    LaisvasKiekis = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientas", x => x.IngredientoID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_INGREDIE_RELATIONS_LAISVIRE",
                        column: x => x.FormosID,
                        principalTable: "LaisviResursai",
                        principalColumn: "FormosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnikosPrietaisas",
                columns: table => new
                {
                    TechnikosPrietaisoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: true),
                    Prietaisopavadinimas = table.Column<string>(name: "Prietaiso pavadinimas", unicode: false, maxLength: 50, nullable: false),
                    LaisvasKiekis = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnikosPrietaisas", x => x.TechnikosPrietaisoID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_TECHNIKO_TURI55_LAISVIRE",
                        column: x => x.FormosID,
                        principalTable: "LaisviResursai",
                        principalColumn: "FormosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamybosInstrukcija",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    InstrukcijosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Busena = table.Column<string>(type: "char(1)", nullable: false),
                    RecepturaParuosta = table.Column<string>(type: "char(1)", nullable: false),
                    TechnikosPatarimaiParuosti = table.Column<string>(type: "char(1)", nullable: false),
                    GamybosTerminas = table.Column<DateTime>(type: "datetime", nullable: false),
                    Brandinimolaikas = table.Column<string>(name: "Brandinimo laikas", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamybosInstrukcija", x => new { x.KlientoUzsakymoID, x.InstrukcijosID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_GAMYBOSI_TURI_KLIENTOU",
                        column: x => x.KlientoUzsakymoID,
                        principalTable: "KlientoUzsakymoAplankas",
                        principalColumn: "KlientoUzsakymoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LaisviResursaiUzsakymui",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    FormosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    LaisvuResursuTipas = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaisviResursaiUzsakymui", x => new { x.KlientoUzsakymoID, x.FormosID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK__LaisviRes__Formo__13BCEBC1",
                        column: x => x.FormosID,
                        principalTable: "LaisviResursai",
                        principalColumn: "FormosID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaisviResursaiUzsakymui_KlientoUzsakymoAplankas_KlientoUzsakymoID",
                        column: x => x.KlientoUzsakymoID,
                        principalTable: "KlientoUzsakymoAplankas",
                        principalColumn: "KlientoUzsakymoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sutartis",
                columns: table => new
                {
                    SutartiesID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: true),
                    TiekejoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: true),
                    KlientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: true),
                    SutartiesPasirasymoData = table.Column<DateTime>(type: "datetime", nullable: false),
                    SutartiesTerminas = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sutartis", x => x.SutartiesID);
                    table.ForeignKey(
                        name: "FK_SUTARTIS_TURI13_KLIENTAS",
                        column: x => x.KlientoID,
                        principalTable: "Klientas",
                        principalColumn: "KlientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SUTARTIS_TURI14_KLIENTOU",
                        column: x => x.KlientoUzsakymoID,
                        principalTable: "KlientoUzsakymoAplankas",
                        principalColumn: "KlientoUzsakymoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SUTARTIS_TURI12_TIEKEJAS",
                        column: x => x.TiekejoID,
                        principalTable: "Tiekejas",
                        principalColumn: "TiekejoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientoPrasymas",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    IngredientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    Ingredientokiekis = table.Column<double>(name: "Ingrediento kiekis", nullable: false),
                    Busena = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientoPrasymas", x => new { x.KlientoUzsakymoID, x.IngredientoID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_INGREDIE_TURI9_INGREDIE",
                        column: x => x.IngredientoID,
                        principalTable: "Ingredientas",
                        principalColumn: "IngredientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INGREDIE_TURI8_KLIENTOU",
                        column: x => x.KlientoUzsakymoID,
                        principalTable: "KlientoUzsakymoAplankas",
                        principalColumn: "KlientoUzsakymoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Technikos prietaiso prasymas",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    TechnikosPrietaisoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    Prietaisokiekis = table.Column<double>(name: "Prietaiso kiekis", nullable: false),
                    Busena = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technikos prietaiso prasymas", x => new { x.KlientoUzsakymoID, x.TechnikosPrietaisoID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_TECHNIKO_TURI11_KLIENTOU",
                        column: x => x.KlientoUzsakymoID,
                        principalTable: "KlientoUzsakymoAplankas",
                        principalColumn: "KlientoUzsakymoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TECHNIKO_TURI10_TECHNIKO",
                        column: x => x.TechnikosPrietaisoID,
                        principalTable: "TechnikosPrietaisas",
                        principalColumn: "TechnikosPrietaisoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientoReceptas",
                columns: table => new
                {
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    InstrukcijosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    IngredientoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    Ingredientokiekis = table.Column<double>(name: "Ingrediento kiekis", nullable: false),
                    Virimolaikas = table.Column<string>(name: "Virimo laikas", maxLength: 50, nullable: true),
                    Fermentacijoslaikas = table.Column<string>(name: "Fermentacijos laikas", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientoReceptas", x => new { x.KlientoUzsakymoID, x.InstrukcijosID, x.IngredientoID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_INGREDIE_TURI6_INGREDIE",
                        column: x => x.IngredientoID,
                        principalTable: "Ingredientas",
                        principalColumn: "IngredientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INGREDIE_TURI7_GAMYBOSI",
                        columns: x => new { x.KlientoUzsakymoID, x.InstrukcijosID },
                        principalTable: "GamybosInstrukcija",
                        principalColumns: new[] { "KlientoUzsakymoID", "InstrukcijosID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Technikos prietaiso patarimas",
                columns: table => new
                {
                    TechnikosPrietaisoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    KlientoUzsakymoID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    InstrukcijosID = table.Column<decimal>(type: "numeric(6, 0)", nullable: false),
                    Prietaisokomentaras = table.Column<string>(name: "Prietaiso komentaras", unicode: false, maxLength: 1000, nullable: true),
                    PrietaisoKiekis = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technikos prietaiso patarimas", x => new { x.TechnikosPrietaisoID, x.KlientoUzsakymoID, x.InstrukcijosID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_TECHNIKO_TURI5_TECHNIKO",
                        column: x => x.TechnikosPrietaisoID,
                        principalTable: "TechnikosPrietaisas",
                        principalColumn: "TechnikosPrietaisoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TECHNIKO_TURI4_GAMYBOSI",
                        columns: x => new { x.KlientoUzsakymoID, x.InstrukcijosID },
                        principalTable: "GamybosInstrukcija",
                        principalColumns: new[] { "KlientoUzsakymoID", "InstrukcijosID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "TURI_FK",
                table: "GamybosInstrukcija",
                column: "KlientoUzsakymoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RELATIONSHIP_24_FK",
                table: "Ingredientas",
                column: "FormosID");

            migrationBuilder.CreateIndex(
                name: "TURI9_FK",
                table: "IngredientoPrasymas",
                column: "IngredientoID");

            migrationBuilder.CreateIndex(
                name: "TURI8_FK",
                table: "IngredientoPrasymas",
                column: "KlientoUzsakymoID");

            migrationBuilder.CreateIndex(
                name: "TURI6_FK",
                table: "IngredientoReceptas",
                column: "IngredientoID");

            migrationBuilder.CreateIndex(
                name: "TURI7_FK",
                table: "IngredientoReceptas",
                columns: new[] { "KlientoUzsakymoID", "InstrukcijosID" });

            migrationBuilder.CreateIndex(
                name: "TURI15_FK",
                table: "KlientoUzsakymoAplankas",
                column: "KlientoID");

            migrationBuilder.CreateIndex(
                name: "index_FormosTuri",
                table: "LaisviResursaiUzsakymui",
                column: "FormosID");

            migrationBuilder.CreateIndex(
                name: "index_KlientoUzsakymoTuri",
                table: "LaisviResursaiUzsakymui",
                column: "KlientoUzsakymoID");

            migrationBuilder.CreateIndex(
                name: "TURI13_FK",
                table: "Sutartis",
                column: "KlientoID");

            migrationBuilder.CreateIndex(
                name: "TURI14_FK",
                table: "Sutartis",
                column: "KlientoUzsakymoID",
                unique: true,
                filter: "[KlientoUzsakymoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "TURI12_FK",
                table: "Sutartis",
                column: "TiekejoID");

            migrationBuilder.CreateIndex(
                name: "TURI5_FK",
                table: "Technikos prietaiso patarimas",
                column: "TechnikosPrietaisoID");

            migrationBuilder.CreateIndex(
                name: "TURI4_FK",
                table: "Technikos prietaiso patarimas",
                columns: new[] { "KlientoUzsakymoID", "InstrukcijosID" });

            migrationBuilder.CreateIndex(
                name: "TURI11_FK",
                table: "Technikos prietaiso prasymas",
                column: "KlientoUzsakymoID");

            migrationBuilder.CreateIndex(
                name: "TURI10_FK",
                table: "Technikos prietaiso prasymas",
                column: "TechnikosPrietaisoID");

            migrationBuilder.CreateIndex(
                name: "TURI55_FK",
                table: "TechnikosPrietaisas",
                column: "FormosID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "IngredientoPrasymas");

            migrationBuilder.DropTable(
                name: "IngredientoReceptas");

            migrationBuilder.DropTable(
                name: "LaisviResursaiUzsakymui");

            migrationBuilder.DropTable(
                name: "Sutartis");

            migrationBuilder.DropTable(
                name: "Technikos prietaiso patarimas");

            migrationBuilder.DropTable(
                name: "Technikos prietaiso prasymas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ingredientas");

            migrationBuilder.DropTable(
                name: "Tiekejas");

            migrationBuilder.DropTable(
                name: "GamybosInstrukcija");

            migrationBuilder.DropTable(
                name: "TechnikosPrietaisas");

            migrationBuilder.DropTable(
                name: "KlientoUzsakymoAplankas");

            migrationBuilder.DropTable(
                name: "LaisviResursai");

            migrationBuilder.DropTable(
                name: "Klientas");
        }
    }
}
