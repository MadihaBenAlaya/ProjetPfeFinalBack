using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AppRecrutement.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

          

           

            



           

            

           

            migrationBuilder.CreateTable(
                name: "Candidatures",
                columns: table => new
                {
                    CandidatureID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date_postulation = table.Column<string>(type: "text", nullable: true),
                    Etat = table.Column<string>(type: "text", nullable: true),
                    Curriculum_Vitae = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    nb_annee_exp_candidat = table.Column<string>(type: "text", nullable: true),
                    OffreFK = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidatFK = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatures", x => x.CandidatureID);
                    table.ForeignKey(
                        name: "FK_Candidatures_AspNetUsers_CandidatFK",
                        column: x => x.CandidatFK,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidatures_Offres_OffreFK",
                        column: x => x.OffreFK,
                        principalTable: "Offres",
                        principalColumn: "OffreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntretienRHs",
                columns: table => new
                {
                    EntretienID = table.Column<Guid>(type: "uuid", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: true),
                    Equipe_recrutement = table.Column<string>(type: "text", nullable: true),
                    Type_entretien = table.Column<string>(type: "text", nullable: true),
                    Localisation = table.Column<string>(type: "text", nullable: false),
                    Duree = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    Heure = table.Column<string>(type: "text", nullable: false),
                    lien_entretien = table.Column<string>(type: "text", nullable: true),
                    candidatureFk = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntretienRHs", x => x.EntretienID);
                    table.ForeignKey(
                        name: "FK_EntretienRHs_Candidatures_candidatureFk",
                        column: x => x.candidatureFk,
                        principalTable: "Candidatures",
                        principalColumn: "CandidatureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTechniques",
                columns: table => new
                {
                    TestID = table.Column<Guid>(type: "uuid", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: true),
                    Date_depot = table.Column<string>(type: "text", nullable: true),
                    Duree = table.Column<string>(type: "text", nullable: false),
                    lien_test = table.Column<string>(type: "text", nullable: false),
                    candidatureFk = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTechniques", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_TestTechniques_Candidatures_candidatureFk",
                        column: x => x.candidatureFk,
                        principalTable: "Candidatures",
                        principalColumn: "CandidatureID",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_EntretienRHs_candidatureFk",
                table: "EntretienRHs",
                column: "candidatureFk");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_DepartementId",
                table: "Personnel",
                column: "DepartementId");

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "EntretienRHs");

           

            migrationBuilder.DropTable(
                name: "TestTechniques");

           

            migrationBuilder.DropTable(
                name: "Candidatures");

           
        }
    }
}
