using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Campussen",
                columns: new[] { "CampusId", "Gemeente", "Huisnummer", "CampusNaam", "Postcode", "Straat" },
                values: new object[,]
                {
                    { 1, "Antwerpen", "22", "Andros", "2018", "Somersstraat" },
                    { 2, "Dendermonde", "17", "Delos", "9200", "Oude Vest" },
                    { 3, "Genk", "37", "Gavdos", "3600", "Europalaan" },
                    { 4, "Heverlee", "2", "Hydra", "3001", "Interleuvenlaan" },
                    { 5, "Wevelgem", "10", "Ikaria", "8560", "Vlamingstraat" },
                    { 6, "Oostende", "4", "Oinouses", "8400", "Archimedesstraat" }
                });

            migrationBuilder.InsertData(
                table: "Landen",
                columns: new[] { "LandCode", "Naam" },
                values: new object[,]
                {
                    { "BE", "België" },
                    { "NL", "Nederland" },
                    { "DE", "Duitsland" },
                    { "FR", "Frankrijk" },
                    { "IT", "Italië" },
                    { "LU", "Luxemburg" }
                });

            migrationBuilder.InsertData(
                table: "Docenten",
                columns: new[] { "DocentId", "CampusId", "Familienaam", "HeeftRijbewijs", "InDienst", "LandCode", "Voornaam", "Maandwedde" },
                values: new object[,]
                {
                    { 5, 1, "Adriaense ns", true, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jan", 2100m },
                    { 42, 5, "Belvaux", false, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean", 1000m },
                    { 37, 5, "Beeckman", null, new DateTime(2019, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Koen", 1500m },
                    { 33, 5, "Bauwens", false, new DateTime(2019, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Henri", 1500m },
                    { 32, 5, "Bauwens", true, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Arsène", 2000m },
                    { 21, 5, "Baert", false, new DateTime(2019, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dirk", 1000m },
                    { 16, 5, "Anseeuw", null, new DateTime(2019, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Urbain", 1500m },
                    { 12, 5, "Aerts", false, new DateTime(2019, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Stefan", 1500m },
                    { 11, 5, "Aerts", true, new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Paul", 2000m },
                    { 127, 4, "De Wolf", null, new DateTime(2019, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfons", 1400m },
                    { 123, 4, "De Vlaeminck", false, new DateTime(2019, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eric", 1400m },
                    { 106, 4, "Deltour", null, new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hubert", 1400m },
                    { 102, 4, "De Jonghe", false, new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 1400m },
                    { 85, 4, "Cerami", null, new DateTime(2019, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pino", 1400m },
                    { 53, 5, "Blavier", true, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marcel", 2000m },
                    { 81, 4, "Buysse", false, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lucien", 1400m },
                    { 60, 4, "Bogaert", false, new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jan", 1400m },
                    { 43, 4, "Benoit", null, new DateTime(2019, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Adelin", 1400m },
                    { 39, 4, "Beeckman", false, new DateTime(2019, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Theophile", 1400m },
                    { 22, 4, "Baert", null, new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hubert", 1400m },
                    { 18, 4, "Arlet", false, new DateTime(2019, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jacques", 1400m },
                    { 4, 4, "Adam ", null, new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "François", 1700m },
                    { 1, 4, "Abbeloo s", null, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Willy", 1400m },
                    { 122, 3, "Despontin", true, new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Léon", 1900m },
                    { 118, 3, "Deruyter", null, new DateTime(2019, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Charles", 1900m },
                    { 112, 3, "Depoorter", null, new DateTime(2019, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jules", 1300m },
                    { 108, 3, "Demeyer", false, new DateTime(2019, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marc", 1300m },
                    { 101, 3, "Defraeye", true, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Odiel", 1900m },
                    { 97, 3, "Decabooter", null, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Arthur", 1900m },
                    { 64, 4, " Boons", null, new DateTime(2019, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jozef", 1400m },
                    { 91, 3, "Couvreur", null, new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hilaire", 1300m },
                    { 54, 5, " Blockx", false, new DateTime(2019, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roger", 1500m },
                    { 63, 5, "Boonen", false, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tom", 1000m },
                    { 111, 6, "De muysere", false, new DateTime(2019, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jef", 1600m },
                    { 98, 6, "De Clerq", true, new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hans", 1600m },
                    { 94, 6, "Daems", null, new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emile", 1600m },
                    { 90, 6, "Corbusier", false, new DateTime(2019, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Yvan", 1600m },
                    { 77, 6, "Bruyère", true, new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean-Marie", 1600m },
                    { 73, 6, "Brankart", null, new DateTime(2019, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean", 1600m },
                    { 69, 6, "Bracke", false, new DateTime(2019, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ferdinand", 1600m },
                    { 56, 6, "Bocklandt", true, new DateTime(2019, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Willy", 1600m },
                    { 52, 6, "Billiet", null, new DateTime(2019, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hector", 1600m },
                    { 48, 6, "Beths", false, new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frans ", 1600m },
                    { 35, 6, "Beckaert", true, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 1600m },
                    { 31, 6, "Baumans", null, new DateTime(2019, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auguste", 1600m },
                    { 27, 6, "Baguet", false, new DateTime(2019, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Serge", 1600m },
                    { 58, 5, "Boekaerts", null, new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfons", 1500m },
                    { 14, 6, " Allard", true, new DateTime(2019, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Henri", 1600m },
                    { 6, 6, "Adriaens ens", false, new DateTime(2019, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "René", 1600m },
                    { 126, 5, "Dewaele", false, new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maurice", 1000m },
                    { 121, 5, "Desplenter", null, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1500m },
                    { 117, 5, "Dernies", false, new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Michel", 1500m },
                    { 116, 5, "Derijcke", true, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Germain", 2000m },
                    { 105, 5, "Deloor", false, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gustaaf", 1000m },
                    { 100, 5, "Decraeye", null, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1500m },
                    { 96, 5, "De Bruyne", false, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fred", 1500m },
                    { 95, 5, "Danneels", true, new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gustave", 2000m },
                    { 84, 5, "Capiot", false, new DateTime(2019, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Johan ", 1000m },
                    { 79, 5, "Bruylandts", null, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dave", 1500m },
                    { 75, 5, "Brosteaux", false, new DateTime(2019, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1500m },
                    { 74, 5, "Brichard", true, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emile", 2000m },
                    { 10, 6, "Aerts", null, new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mario", 1600m },
                    { 87, 3, "Claes", false, new DateTime(2019, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1300m },
                    { 80, 3, "Bruyneel", true, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Johan", 1900m },
                    { 76, 3, "Bruneau", null, new DateTime(2019, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emile", 1900m },
                    { 30, 2, "Barras", false, new DateTime(2019, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1200m },
                    { 23, 2, "Baert", true, new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean-Pierre", 1800m },
                    { 20, 2, "Baens", true, new DateTime(2019, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roger", 2200m },
                    { 19, 2, "Arras", null, new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wim", 1800m },
                    { 9, 2, "Aerts", false, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean", 1200m },
                    { 2, 2, "Abelsh ausen", true, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", 1800m },
                    { 120, 1, "Desmet", false, new DateTime(2019, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gilbert", 1100m },
                    { 113, 1, "Depoorter", true, new DateTime(2019, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Richard", 1700m },
                    { 110, 1, "De Muynck", true, new DateTime(2019, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Johan", 2100m },
                    { 109, 1, "De Mulder", null, new DateTime(2019, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frans", 1700m },
                    { 99, 1, "Decock", false, new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roger", 1100m },
                    { 92, 1, "Cretskens", true, new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wilfried", 1700m },
                    { 89, 1, "Close", true, new DateTime(2019, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alex", 2100m },
                    { 40, 2, "Beheyt", null, new DateTime(2019, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Benoni", 1800m },
                    { 88, 1, "Clerckx", null, new DateTime(2019, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Karel", 1700m },
                    { 71, 1, "Braekevelt", true, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Omer", 1700m },
                    { 68, 1, "Boumon", true, new DateTime(2019, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marcel", 2100m },
                    { 67, 1, "Boucquet", null, new DateTime(2019, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Walter", 1700m },
                    { 57, 1, "Bodart", false, new DateTime(2019, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emile", 1100m },
                    { 50, 1, "Beyssens", true, new DateTime(2019, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Herman", 1700m },
                    { 47, 1, "Berton", true, new DateTime(2019, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 2100m },
                    { 46, 1, "Berckmans", null, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean-Pierre", 1700m },
                    { 36, 1, "Beckaert", false, new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Marcel", 1100m },
                    { 29, 1, "Barbé", true, new DateTime(2019, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Koen", 1700m },
                    { 26, 1, "Baguet", true, new DateTime(2019, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roger", 2100m },
                    { 25, 1, "Baeyens", null, new DateTime(2019, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jan", 1700m },
                    { 15, 1, "Anciaux", false, new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 1100m },
                    { 8, 1, "Aerts", true, new DateTime(2019, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emile", 1700m },
                    { 78, 1, "Bruyere", false, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", 1100m },
                    { 41, 2, "Beirnaert", true, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 2200m },
                    { 44, 2, "Benoit", true, new DateTime(2019, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auguste", 1800m },
                    { 51, 2, "Billiet", false, new DateTime(2019, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Albert", 1200m },
                    { 70, 3, "Braeckeveldt", null, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Adolphe", 1300m },
                    { 66, 3, "Bosmans", false, new DateTime(2019, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", 1300m },
                    { 59, 3, "Bogaert", true, new DateTime(2019, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cesar", 1900m },
                    { 55, 3, "Blomme", null, new DateTime(2019, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maurice", 1900m },
                    { 49, 3, "Beyens", null, new DateTime(2019, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "René", 1300m },
                    { 45, 3, "Berben", false, new DateTime(2019, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jef", 1300m },
                    { 38, 3, "Beeckman", true, new DateTime(2019, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kamiel", 1900m },
                    { 34, 3, "Bayens", null, new DateTime(2019, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jules", 1900m },
                    { 28, 3, "Balducq", null, new DateTime(2019, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gérard", 1300m },
                    { 24, 3, "Baeyens", false, new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Armand", 1300m },
                    { 17, 3, "Antheunis", true, new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Etienne", 1900m },
                    { 13, 3, "Alexander", null, new DateTime(2019, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "François", 1900m },
                    { 7, 3, "Aerenho uts", null, new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frans", 1300m },
                    { 3, 3, "Achten ", false, new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", 1300m },
                    { 128, 2, "Dhaenens", true, new DateTime(2019, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rudy", 1800m },
                    { 125, 2, "Devolder", true, new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Stijn", 2200m },
                    { 124, 2, "De Vlaeminck", null, new DateTime(2019, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Roger", 1800m },
                    { 114, 2, "Depredomme", false, new DateTime(2019, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Prosper", 1200m },
                    { 107, 2, "Deman", true, new DateTime(2019, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Paul", 1800m },
                    { 104, 2, "Deloor", true, new DateTime(2019, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alfons", 2200m },
                    { 103, 2, "Delbecque", null, new DateTime(2019, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Julien", 1800m },
                    { 93, 2, "Criquielion", false, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Claude", 1200m },
                    { 86, 2, "Christiaens", true, new DateTime(2019, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Georges", 1800m },
                    { 83, 2, "Callens", true, new DateTime(2019, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Norbert", 2200m },
                    { 82, 2, "Brandt", null, new DateTime(2019, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Christophe", 1800m },
                    { 72, 2, "Brands", false, new DateTime(2019, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frans", 1200m },
                    { 65, 2, "Borra", true, new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gabriel", 1800m },
                    { 62, 2, "Bonduel", true, new DateTime(2019, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Frans", 2200m },
                    { 61, 2, "Bogaerts", null, new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jean", 1800m },
                    { 115, 6, "Derboven", null, new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Willy", 1600m },
                    { 119, 6, "Desimpelaere", true, new DateTime(2019, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maurice", 1600m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "BE");

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "DE");

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "FR");

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "IT");

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "LU");

            migrationBuilder.DeleteData(
                table: "Landen",
                keyColumn: "LandCode",
                keyValue: "NL");

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Campussen",
                keyColumn: "CampusId",
                keyValue: 6);
        }
    }
}
