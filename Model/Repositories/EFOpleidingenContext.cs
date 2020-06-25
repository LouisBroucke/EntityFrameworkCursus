using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.Repositories
{
    public class EFOpleidingenContext : DbContext
    {
        //const string Server = @".\SQLEXPRESS";
        //const string DatabaseNaam = "EFOpleidingen";

        public static IConfigurationRoot configuration;

        public DbSet<Campus> Campussen { get; set; }
        public DbSet<Docent> Docenten { get; set; }
        public DbSet<Land> Landen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    $@"Server={Server};Database={DatabaseNaam};Trusted_Connection=true;"
            //    , options => options.MaxBatchSize(150)); //Max aantal SQL commands

            //Zoek de naam in de connectionStrings section van appsettings.json
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var connectionString = configuration.GetConnectionString("efopleidingen");

            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(
                    connectionString
                    , options => options.MaxBatchSize(150));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Docent>().ToTable("Docenten");
            modelBuilder.Entity<Docent>().HasKey(c => c.DocentId);
            modelBuilder.Entity<Docent>().Property(b => b.DocentId)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Docent>().Property(b => b.Voornaam)
            .IsRequired()
            .HasMaxLength(20);
            modelBuilder.Entity<Docent>().Property(b => b.Familienaam)
            .IsRequired()
            .HasMaxLength(30);
            modelBuilder.Entity<Docent>().Property(b => b.Wedde)
            .HasColumnName("Maandwedde")
            .HasColumnType("decimal(18, 4)");
            modelBuilder.Entity<Docent>().Property(b => b.InDienst)
            .HasColumnType("date");
            modelBuilder.Entity<Docent>()
            .HasOne(b => b.Campus)
            .WithMany(c => c.Docenten)
            .HasForeignKey(b => b.CampusId);
            modelBuilder.Entity<Docent>()
            .HasOne(b => b.Land)
            .WithMany(c => c.Docenten)
            .HasForeignKey(b => b.LandCode);

            modelBuilder.Entity<Campus>().ToTable("Campussen");
            modelBuilder.Entity<Campus>().HasKey(c => c.CampusId);
            modelBuilder.Entity<Campus>().Property(b => b.CampusId)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Campus>().Property(b => b.Naam)
            .HasColumnName("CampusNaam")
            .IsRequired();
            modelBuilder.Entity<Campus>().Property(b => b.Gemeente)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Campus>().Ignore(c => c.Commentaar);

            modelBuilder.Entity<Land>().ToTable("Landen");
            modelBuilder.Entity<Land>().HasKey(c => c.LandCode);
            modelBuilder.Entity<Land>().Property(b => b.LandCode)
            .ValueGeneratedNever();

            // --------------------------
            // Campus objecten definiëren
            // --------------------------
            var andros = new Campus // nieuwe Campus (Strongly typed)
            {
                CampusId = 1,             // Primary Key
                Naam = "Andros",
                Straat = "Somersstraat",
                Huisnummer = "22",
                Postcode = "2018",
                Gemeente = "Antwerpen"
            };
            var delos = new Campus
            {
                CampusId = 2,
                Naam = "Delos",
                Straat = "Oude Vest",
                Huisnummer = "17",
                Postcode = "9200",
                Gemeente = "Dendermonde"
            };
            var gavdos = new Campus
            {
                CampusId = 3,
                Naam = "Gavdos",
                Straat = "Europalaan",
                Huisnummer = "37",
                Postcode = "3600",
                Gemeente = "Genk"
            };
            var hydra = new Campus
            {
                CampusId = 4,
                Naam = "Hydra",
                Straat = "Interleuvenlaan",
                Huisnummer = "2",
                Postcode = "3001",
                Gemeente = "Heverlee"
            };
            var ikaria = new Campus
            {
                CampusId = 5,
                Naam = "Ikaria",
                Straat = "Vlamingstraat",
                Huisnummer = "10",
                Postcode = "8560",
                Gemeente = "Wevelgem"
            };
            var oinouses = new Campus
            {
                CampusId = 6,
                Naam = "Oinouses",
                Straat = "Archimedesstraat",
                Huisnummer = "4",
                Postcode = "8400",
                Gemeente = "Oostende"
            };

            //Seeding Campus
            modelBuilder.Entity<Campus>().HasData(
                andros, delos, gavdos, hydra, ikaria, oinouses);

            //Seeding Land (Anonymous types)
            modelBuilder.Entity<Land>().HasData(
            new // Toevoegen nieuw Land (Anonymous type)
            {
                LandCode = "BE", // Primary Key
                Naam = "België"
            },
            new
            {
                LandCode = "NL",
                Naam = "Nederland"
            },
            new
            {
                LandCode = "DE",
                Naam = "Duitsland"
            },
            new
            {
                LandCode = "FR",
                Naam = "Frankrijk"
            },
            new
            {
                LandCode = "IT",
                Naam = "Italië"
            },
            new
            {
                LandCode = "LU",
                Naam = "Luxemburg"
            }
            );

            modelBuilder.Entity<Docent>().HasData(
            new { DocentId = 001, Voornaam = "Willy", Familienaam = "Abbeloos", Wedde = 1500m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 1), CampusId = hydra.CampusId },
            new { DocentId = 002, Voornaam = "Joseph", Familienaam = "Abelshausen", Wedde = 1800m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 2), CampusId = delos.CampusId },
            new { DocentId = 003, Voornaam = "Joseph", Familienaam = "Achten", Wedde = 1300m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 3), CampusId = gavdos.CampusId },
            new { DocentId = 004, Voornaam = "François", Familienaam = "Adam", Wedde = 1700m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 4), CampusId = hydra.CampusId },
            new { DocentId = 005, Voornaam = "Jan", Familienaam = "Adriaensens", Wedde = 2100m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 5), CampusId = andros.CampusId },
            new { DocentId = 006, Voornaam = "René", Familienaam = "Adriaensens", Wedde = 1600m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 6), CampusId = oinouses.CampusId },
            new { DocentId = 007, Voornaam = "Frans", Familienaam = "Aerenhouts", Wedde = 1300m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 7), CampusId = gavdos.CampusId },
            new { DocentId = 008, Voornaam = "Emile", Familienaam = "Aerts", Wedde = 1700m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 8), CampusId = andros.CampusId },
            new { DocentId = 009, Voornaam = "Jean", Familienaam = "Aerts", Wedde = 1200m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 9), CampusId = delos.CampusId },
            new { DocentId = 010, Voornaam = "Mario", Familienaam = "Aerts", Wedde = 1600m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 10), CampusId = oinouses.CampusId },
            // Toevoegen nieuwe Docent met een Docent clas s
            new Docent { DocentId = 011, Voornaam = "Paul", Familienaam = "Aerts", Wedde = 2000m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 11), CampusId = ikaria.CampusId },
            new Docent { DocentId = 012, Voornaam = "Stefan", Familienaam = "Aerts", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 12), CampusId = ikaria.CampusId },
            new Docent { DocentId = 013, Voornaam = "François", Familienaam = "Alexander", Wedde = 1900m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 13), CampusId = gavdos.CampusId },
            new Docent { DocentId = 014, Voornaam = "Henri", Familienaam = " Allard", Wedde = 1600m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 14), CampusId = oinouses.CampusId },
            new Docent { DocentId = 015, Voornaam = "Albert", Familienaam = "Anciaux", Wedde = 1100m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 15), CampusId = 1 },
            new Docent { DocentId = 016, Voornaam = "Urbain", Familienaam = "Anseeuw", Wedde = 1500m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 16), CampusId = ikaria.CampusId },
            new Docent { DocentId = 017, Voornaam = "Etienne", Familienaam = "Antheunis", Wedde = 1900m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 17), CampusId = gavdos.CampusId },
            new Docent { DocentId = 018, Voornaam = "Jacques", Familienaam = "Arlet", Wedde = 1400m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 18), CampusId = hydra.CampusId },
            new Docent { DocentId = 019, Voornaam = "Wim", Familienaam = "Arras", Wedde = 1800m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 19), CampusId = delos.CampusId },
            new Docent { DocentId = 020, Voornaam = "Roger", Familienaam = "Baens", Wedde = 2200m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 20), CampusId = delos.CampusId },
            new Docent { DocentId = 021, Voornaam = "Dirk", Familienaam = "Baert", Wedde = 1000m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 21), CampusId = ikaria.CampusId },
            new Docent { DocentId = 022, Voornaam = "Hubert", Familienaam = "Baert", Wedde = 1400m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 22), CampusId = hydra.CampusId },
            new Docent { DocentId = 023, Voornaam = "Jean-Pierre", Familienaam = "Baert", Wedde = 1800m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 23), CampusId = delos.CampusId },
            new Docent { DocentId = 024, Voornaam = "Armand", Familienaam = "Baeyens", Wedde = 1300m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 24), CampusId = gavdos.CampusId },
            new Docent { DocentId = 025, Voornaam = "Jan", Familienaam = "Baeyens", Wedde = 1700m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 25), CampusId = andros.CampusId },
            new Docent { DocentId = 026, Voornaam = "Roger", Familienaam = "Baguet", Wedde = 2100m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 26), CampusId = andros.CampusId },
            new Docent { DocentId = 027, Voornaam = "Serge", Familienaam = "Baguet", Wedde = 1600m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 27), CampusId = oinouses.CampusId },
            new Docent { DocentId = 028, Voornaam = "Gérard", Familienaam = "Balducq", Wedde = 1300m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 28), CampusId = gavdos.CampusId },
            new Docent { DocentId = 029, Voornaam = "Koen", Familienaam = "Barbé", Wedde = 1700m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 29), CampusId = andros.CampusId },
            new Docent { DocentId = 030, Voornaam = "Georges", Familienaam = "Barras", Wedde = 1200m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 30), CampusId = delos.CampusId },
            new Docent { DocentId = 031, Voornaam = "Auguste", Familienaam = "Baumans", Wedde = 1600m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 31), CampusId = oinouses.CampusId },
            new Docent { DocentId = 032, Voornaam = "Arsène", Familienaam = "Bauwens", Wedde = 2000m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 1), CampusId = ikaria.CampusId },
            new Docent { DocentId = 033, Voornaam = "Henri", Familienaam = "Bauwens", Wedde = 1500m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 2), CampusId = ikaria.CampusId },
            new Docent { DocentId = 034, Voornaam = "Jules", Familienaam = "Bayens", Wedde = 1900m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 3), CampusId = gavdos.CampusId },
            new Docent { DocentId = 035, Voornaam = "Albert", Familienaam = "Beckaert", Wedde = 1600m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 4), CampusId = oinouses.CampusId },
            new Docent { DocentId = 036, Voornaam = "Marcel", Familienaam = "Beckaert", Wedde = 1100m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 5), CampusId = andros.CampusId },
            new Docent { DocentId = 037, Voornaam = "Koen", Familienaam = "Beeckman", Wedde = 1500m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 6), CampusId = ikaria.CampusId },
            new Docent { DocentId = 038, Voornaam = "Kamiel", Familienaam = "Beeckman", Wedde = 1900m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 7), CampusId = gavdos.CampusId },
            new Docent { DocentId = 039, Voornaam = "Theophile", Familienaam = "Beeckman", Wedde = 1400m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 8), CampusId = hydra.CampusId },
            new Docent { DocentId = 040, Voornaam = "Benoni", Familienaam = "Beheyt", Wedde = 1800m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 9), CampusId = delos.CampusId },
            new Docent { DocentId = 041, Voornaam = "Albert", Familienaam = "Beirnaert", Wedde = 2200m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 10), CampusId = delos.CampusId },
            new Docent { DocentId = 042, Voornaam = "Jean", Familienaam = "Belvaux", Wedde = 1000m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 11), CampusId = ikaria.CampusId },
            new Docent { DocentId = 043, Voornaam = "Adelin", Familienaam = "Benoit", Wedde = 1400m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 12), CampusId = hydra.CampusId},
            new Docent { DocentId = 044, Voornaam = "Auguste", Familienaam = "Benoit", Wedde = 1800m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 13), CampusId = delos.CampusId },
            new Docent { DocentId = 045, Voornaam = "Jef", Familienaam = "Berben", Wedde = 1300m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 14), CampusId = gavdos.CampusId },
            new Docent { DocentId = 046, Voornaam = "Jean-Pierre", Familienaam = "Berckmans", Wedde = 1700m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 15), CampusId = andros.CampusId },
            new Docent { DocentId = 047, Voornaam = "Albert", Familienaam = "Berton", Wedde = 2100m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 16), CampusId = andros.CampusId },
            new Docent { DocentId = 048, Voornaam = "Frans ", Familienaam = "Beths", Wedde = 1600m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 17), CampusId = oinouses.CampusId },
            new Docent { DocentId = 049, Voornaam = "René", Familienaam = "Beyens", Wedde = 1300m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 18), CampusId = gavdos.CampusId },
            new Docent { DocentId = 050, Voornaam = "Herman", Familienaam = "Beyssens", Wedde = 1700m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 19), CampusId = andros.CampusId },
            new Docent { DocentId = 051, Voornaam = "Albert", Familienaam = "Billiet", Wedde = 1200m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 20), CampusId = delos.CampusId },
            new Docent { DocentId = 052, Voornaam = "Hector", Familienaam = "Billiet", Wedde = 1600m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 21), CampusId = oinouses.CampusId },
            new Docent { DocentId = 053, Voornaam = "Marcel", Familienaam = "Blavier", Wedde = 2000m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 22), CampusId = ikaria.CampusId },
            new Docent { DocentId = 054, Voornaam = "Roger", Familienaam = " Blockx", Wedde = 1500m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 23), CampusId = ikaria.CampusId },
            new Docent { DocentId = 055, Voornaam = "Maurice", Familienaam = "Blomme", Wedde = 1900m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 24), CampusId = gavdos.CampusId },
            new Docent { DocentId = 056, Voornaam = "Willy", Familienaam = "Bocklandt", Wedde = 1600m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 25), CampusId = oinouses.CampusId },
            new Docent { DocentId = 057, Voornaam = "Emile", Familienaam = "Bodart", Wedde = 1100m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 26), CampusId = andros.CampusId },
            new Docent { DocentId = 058, Voornaam = "Alfons", Familienaam = "Boekaerts", Wedde = 1500m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 27), CampusId = ikaria.CampusId },
            new Docent { DocentId = 059, Voornaam = "Cesar", Familienaam = "Bogaert", Wedde = 1900m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 2, 28), CampusId = gavdos.CampusId },
            new Docent { DocentId = 060, Voornaam = "Jan", Familienaam = "Bogaert", Wedde = 1400m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 1), CampusId = hydra.CampusId },
            new Docent { DocentId = 061, Voornaam = "Jean", Familienaam = "Bogaerts", Wedde = 1800m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 2), CampusId = delos.CampusId },
            new Docent { DocentId = 062, Voornaam = "Frans", Familienaam = "Bonduel", Wedde = 2200m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 3), CampusId = delos.CampusId },
            new Docent { DocentId = 063, Voornaam = "Tom", Familienaam = "Boonen", Wedde = 1000m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 4), CampusId = ikaria.CampusId },
            new Docent { DocentId = 064, Voornaam = "Jozef", Familienaam = " Boons", Wedde = 1400m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 5), CampusId = hydra.CampusId },
            new Docent { DocentId = 065, Voornaam = "Gabriel", Familienaam = "Borra", Wedde = 1800m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 6), CampusId = delos.CampusId },
            new Docent { DocentId = 066, Voornaam = "Joseph", Familienaam = "Bosmans", Wedde = 1300m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 7), CampusId = gavdos.CampusId },
            new Docent { DocentId = 067, Voornaam = "Walter", Familienaam = "Boucquet", Wedde = 1700m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 8), CampusId = andros.CampusId },
            new Docent { DocentId = 068, Voornaam = "Marcel", Familienaam = "Boumon", Wedde = 2100m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 9), CampusId = andros.CampusId },
            new Docent { DocentId = 069, Voornaam = "Ferdinand", Familienaam = "Bracke", Wedde = 1600m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 10), CampusId = oinouses.CampusId },
            new Docent { DocentId = 070, Voornaam = "Adolphe", Familienaam = "Braeckeveldt", Wedde = 1300m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 11), CampusId = gavdos.CampusId },
            new Docent { DocentId = 071, Voornaam = "Omer", Familienaam = "Braekevelt", Wedde = 1700m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 12), CampusId = andros.CampusId },
            new Docent { DocentId = 072, Voornaam = "Frans", Familienaam = "Brands", Wedde = 1200, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 13), CampusId = delos.CampusId },
            new Docent { DocentId = 073, Voornaam = "Jean", Familienaam = "Brankart", Wedde = 1600, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 14), CampusId = oinouses.CampusId },
            new Docent { DocentId = 074, Voornaam = "Emile", Familienaam = "Brichard", Wedde = 2000, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 15), CampusId = ikaria.CampusId },
            new Docent { DocentId = 075, Voornaam = "Georges", Familienaam = "Brosteaux", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 16), CampusId = ikaria.CampusId },
            new Docent { DocentId = 076, Voornaam = "Emile", Familienaam = "Bruneau", Wedde = 1900, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 17), CampusId = gavdos.CampusId },
            new Docent { DocentId = 077, Voornaam = "Jean-Marie", Familienaam = "Bruyère", Wedde = 1600, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 18), CampusId = oinouses.CampusId },
            new Docent { DocentId = 078, Voornaam = "Joseph", Familienaam = "Bruyere", Wedde = 1100, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 19), CampusId = andros.CampusId },
            new Docent { DocentId = 079, Voornaam = "Dave", Familienaam = "Bruylandts", Wedde = 1500, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 20), CampusId = ikaria.CampusId },
            new Docent { DocentId = 080, Voornaam = "Johan", Familienaam = "Bruyneel", Wedde = 1900, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 21), CampusId = gavdos.CampusId },
            new Docent { DocentId = 081, Voornaam = "Lucien", Familienaam = "Buysse", Wedde = 1400, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 22), CampusId = hydra.CampusId },
            new Docent { DocentId = 082, Voornaam = "Christophe", Familienaam = "Brandt", Wedde = 1800, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 23), CampusId = delos.CampusId },
            new Docent { DocentId = 083, Voornaam = "Norbert", Familienaam = "Callens", Wedde = 2200, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 24), CampusId = delos.CampusId },
            new Docent { DocentId = 084, Voornaam = "Johan ", Familienaam = "Capiot", Wedde = 1000, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 25), CampusId = ikaria.CampusId },
            new Docent { DocentId = 085, Voornaam = "Pino", Familienaam = "Cerami", Wedde = 1400, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 26), CampusId = hydra.CampusId },
            new Docent { DocentId = 086, Voornaam = "Georges", Familienaam = "Christiaens", Wedde = 1800, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 27), CampusId = delos.CampusId },
            new Docent { DocentId = 087, Voornaam = "Georges", Familienaam = "Claes", Wedde = 1300, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 28), CampusId = gavdos.CampusId },
            new Docent { DocentId = 088, Voornaam = "Karel", Familienaam = "Clerckx", Wedde = 1700, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 29), CampusId = andros.CampusId },
            new Docent { DocentId = 089, Voornaam = "Alex", Familienaam = "Close", Wedde = 2100, HeeftRijbewijs = true, InDienst = new DateTime(2019, 3, 30), CampusId = andros.CampusId },
            new Docent { DocentId = 090, Voornaam = "Yvan", Familienaam = "Corbusier", Wedde = 1600, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 31), CampusId = oinouses.CampusId },
            new Docent { DocentId = 091, Voornaam = "Hilaire", Familienaam = "Couvreur", Wedde = 1300, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 1), CampusId = gavdos.CampusId },
            new Docent { DocentId = 092, Voornaam = "Wilfried", Familienaam = "Cretskens", Wedde = 1700, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 2), CampusId = andros.CampusId },
            new Docent { DocentId = 093, Voornaam = "Claude", Familienaam = "Criquielion", Wedde = 1200, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 3), CampusId = delos.CampusId },
            new Docent { DocentId = 094, Voornaam = "Emile", Familienaam = "Daems", Wedde = 1600, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 4), CampusId = oinouses.CampusId },
            new Docent { DocentId = 095, Voornaam = "Gustave", Familienaam = "Danneels", Wedde = 2000, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 5), CampusId = ikaria.CampusId },
            new Docent { DocentId = 096, Voornaam = "Fred", Familienaam = "De Bruyne", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 6), CampusId = ikaria.CampusId },
            new Docent { DocentId = 097, Voornaam = "Arthur", Familienaam = "Decabooter", Wedde = 1900, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 7), CampusId = gavdos.CampusId },
            new Docent { DocentId = 098, Voornaam = "Hans", Familienaam = "De Clerq", Wedde = 1600, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 8), CampusId = oinouses.CampusId },
            new Docent { DocentId = 099, Voornaam = "Roger", Familienaam = "Decock", Wedde = 1100, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 9), CampusId = andros.CampusId },
            new Docent { DocentId = 100, Voornaam = "Georges", Familienaam = "Decraeye", Wedde = 1500, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 10), CampusId = ikaria.CampusId },
            new Docent { DocentId = 101, Voornaam = "Odiel", Familienaam = "Defraeye", Wedde = 1900, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 11), CampusId = gavdos.CampusId },
            new Docent { DocentId = 102, Voornaam = "Albert", Familienaam = "De Jonghe", Wedde = 1400, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 12), CampusId = hydra.CampusId },
            new Docent { DocentId = 103, Voornaam = "Julien", Familienaam = "Delbecque", Wedde = 1800, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 13), CampusId = delos.CampusId },
            new Docent { DocentId = 104, Voornaam = "Alfons", Familienaam = "Deloor", Wedde = 2200, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 14), CampusId = delos.CampusId },
            new Docent { DocentId = 105, Voornaam = "Gustaaf", Familienaam = "Deloor", Wedde = 1000, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 15), CampusId = ikaria.CampusId },
            new Docent { DocentId = 106, Voornaam = "Hubert", Familienaam = "Deltour", Wedde = 1400, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 16), CampusId = hydra.CampusId },
            new Docent { DocentId = 107, Voornaam = "Paul", Familienaam = "Deman", Wedde = 1800, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 17), CampusId = delos.CampusId },
            new Docent { DocentId = 108, Voornaam = "Marc", Familienaam = "Demeyer", Wedde = 1300, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 18), CampusId = gavdos.CampusId },
            new Docent { DocentId = 109, Voornaam = "Frans", Familienaam = "De Mulder", Wedde = 1700, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 19), CampusId = andros.CampusId },
            new Docent { DocentId = 110, Voornaam = "Johan", Familienaam = "De Muynck", Wedde = 2100, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 20), CampusId = andros.CampusId },
            new Docent { DocentId = 111, Voornaam = "Jef", Familienaam = "De muysere", Wedde = 1600, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 21), CampusId = oinouses.CampusId },
            new Docent { DocentId = 112, Voornaam = "Jules", Familienaam = "Depoorter", Wedde = 1300, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 22), CampusId = gavdos.CampusId },
            new Docent { DocentId = 113, Voornaam = "Richard", Familienaam = "Depoorter", Wedde = 1700, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 23), CampusId = andros.CampusId },
            new Docent { DocentId = 114, Voornaam = "Prosper", Familienaam = "Depredomme", Wedde = 1200, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 24), CampusId = delos.CampusId },
            new Docent { DocentId = 115, Voornaam = "Willy", Familienaam = "Derboven", Wedde = 1600, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 25), CampusId = oinouses.CampusId },
            new Docent { DocentId = 116, Voornaam = "Germain", Familienaam = "Derijcke", Wedde = 2000, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 26), CampusId = ikaria.CampusId },
            new Docent { DocentId = 117, Voornaam = "Michel", Familienaam = "Dernies", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 27), CampusId = ikaria.CampusId },
            new Docent { DocentId = 118, Voornaam = "Charles", Familienaam = "Deruyter", Wedde = 1900, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 28), CampusId = gavdos.CampusId },
            new Docent { DocentId = 119, Voornaam = "Maurice", Familienaam = "Desimpelaere", Wedde = 1600, HeeftRijbewijs = true, InDienst = new DateTime(2019, 4, 29), CampusId = oinouses.CampusId },
            new Docent { DocentId = 120, Voornaam = "Gilbert", Familienaam = "Desmet", Wedde = 1100, HeeftRijbewijs = false, InDienst = new DateTime(2019, 4, 30), CampusId = andros.CampusId },
            new Docent { DocentId = 121, Voornaam = "Georges", Familienaam = "Desplenter", Wedde = 1500, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 1), CampusId = ikaria.CampusId },
            new Docent { DocentId = 122, Voornaam = "Léon", Familienaam = "Despontin", Wedde = 1900, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 2), CampusId = gavdos.CampusId },
            new Docent { DocentId = 123, Voornaam = "Eric", Familienaam = "De Vlaeminck", Wedde = 1400, HeeftRijbewijs = false, InDienst = new DateTime(2019, 5, 3), CampusId = hydra.CampusId },
            new Docent { DocentId = 124, Voornaam = "Roger", Familienaam = "De Vlaeminck", Wedde = 1800, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 4), CampusId = delos.CampusId },
            new Docent { DocentId = 125, Voornaam = "Stijn", Familienaam = "Devolder", Wedde = 2200, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 5), CampusId = delos.CampusId },
            new Docent { DocentId = 126, Voornaam = "Maurice", Familienaam = "Dewaele", Wedde = 1000, HeeftRijbewijs = false, InDienst = new DateTime(2019, 5, 6), CampusId = ikaria.CampusId },
            new Docent { DocentId = 127, Voornaam = "Alfons", Familienaam = "De Wolf", Wedde = 1400, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 7), CampusId = hydra.CampusId },
            new Docent { DocentId = 128, Voornaam = "Rudy", Familienaam = "Dhaenens", Wedde = 1800, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 8), CampusId = delos.CampusId }/*,*/
            //new Docent { DocentId = 129, Voornaam = "Michel", Familienaam = "D''Hooghe", Wedde = 1300, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 9), Campu sId = gavdos.Campu sId },
            //new Docent { DocentId = 130, Voornaam = "Ludo", Familienaam = "D ierckxsens", Wedde = 1700, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 10), Campu sId = andros.Campu sId },
            //new Docent { DocentId = 131, Voornaam = "Frans", Familienaam = " Dictus", Wedde = 21 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 11), CampusId = andros.CampusId },
            //new Docent { DocentId = 132, Voornaam = "Lomme", Familienaam = " Driessens", Wedde = 1600, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 5, 12), Campu sId = oinouses.Cam pusId },
            //new Docent { DocentId = 133, Voornaam = "Gustave", Familienaam = "Drioul", Wedde = 1300, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 5, 13), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 134, Voornaam = "Marcel", Familienaam = "Dupont", Wedde = 1 700, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 5, 14), CampusId = andros.CampusId },
            //new Docent { DocentId = 135, Voornaam = "Niko", Familienaam = "E eckhout", Wedde = 1 200, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 5, 15), CampusI d = delos.CampusId },
            //new Docent { DocentId = 136, Voornaam = "Nico", Familienaam = "E monds", Wedde = 160 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 16), CampusId = oinouses.CampusId },
            //new Docent { DocentId = 137, Voornaam = "Peter", Familienaam = " Farazijn", Wedde = 2000, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 17), CampusI d = ikaria.CampusI d },
            //new Docent { DocentId = 138, Voornaam = "Herman", Familienaam = "Frison", Wedde = 1 500, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 5, 18), CampusI d = ikaria.CampusI d },
            //new Docent { DocentId = 139, Voornaam = "Henri", Familienaam = " Garnier", Wedde = 1 900, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 5, 19), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 140, Voornaam = "Frans", Familienaam = " Gielen", Wedde = 16 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 20), CampusId = oinouses.CampusI d },
            //new Docent { DocentId = 141, Voornaam = "Romain", Familienaam = "Gijssels", Wedde = 1100, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 5, 21), Campu sId = andros.Campu sId },
            //new Docent { DocentId = 142, Voornaam = "Walter", Familienaam = "Godefroot", Wedde = 1500, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 22), Campu sId = ikaria.Campu sId },
            //new Docent { DocentId = 143, Voornaam = "Dries", Familienaam = " Govaerts", Wedde = 1900, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 23), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 144, Voornaam = "Sylvain", Familienaam = "Grysolle", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 24), Camp usId = hydra.Campu sId },
            //new Docent { DocentId = 145, Voornaam = "Roger", Familienaam = " Gyselinck", Wedde = 1800, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 5, 25), Campus Id = delos.CampusI d },
            //new Docent { DocentId = 146, Voornaam = "Paul", Familienaam = "H aghedooren", Wedde = 2200, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 26), Campu sId = delos.Campus Id },
            //new Docent { DocentId = 147, Voornaam = "Alfred", Familienaam = "Hamerlinck", Wedde = 1000, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 5, 27), Camp usId = ikaria.Camp usId },
            //new Docent { DocentId = 148, Voornaam = "Louis", Familienaam = " Hardiquest", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 28), Camp usId = hydra.Campu sId },
            //new Docent { DocentId = 149, Voornaam = "Emile", Familienaam = " Hardy", Wedde = 180 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 29), CampusId = delos.CampusId },
            //new Docent { DocentId = 150, Voornaam = "Marcel", Familienaam = "Hendrikx", Wedde = 1300, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 5, 30), Campus Id = gavdos.Campus Id },
            //new Docent { DocentId = 151, Voornaam = "Joseph", Familienaam = "Hoevenaers", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 5, 31), Cam pusId = andros.Cam pusId },
            //new Docent { DocentId = 152, Voornaam = "Kevin", Familienaam = " Hulsmans", Wedde = 2100, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 6, 1), CampusId = andros.CampusId },
            //new Docent { DocentId = 153, Voornaam = "Raymond", Familienaam = "Impanis", Wedde = 1600, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 2), CampusI d = oinouses.Campu sId },
            //new Docent { DocentId = 154, Voornaam = "Paul", Familienaam = "I n''t", Wedde = 1300, HeeftRijbewijs = false, InDienst = new DateTime(2019, 6, 3), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 155, Voornaam = "Willy", Familienaam = " In''t", Wedde = 170 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 6, 4), CampusId = andros.CampusId },
            //new Docent { DocentId = 156, Voornaam = "Marcel", Familienaam = "Janssens", Wedde = 1200, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 5), CampusI d = delos.CampusId },
            //new Docent { DocentId = 157, Voornaam = "Benjamin", Familienaam = "Javaux", Wedde = 1600, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 6, 6), Campus Id = oinouses.Camp usId },
            //new Docent { DocentId = 158, Voornaam = "Karel", Familienaam = " Kaers", Wedde = 200 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 6, 7), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 159, Voornaam = "Francis", Familienaam = "Kemplaire", Wedde = 1500, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 6, 8), Campu sId = ikaria.Campu sId },
            //new Docent { DocentId = 160, Voornaam = "Norbert", Familienaam = "Kerckhove", Wedde = 1900, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 6, 9), Camp usId = gavdos.Camp usId },
            //new Docent { DocentId = 161, Voornaam = "Désir é", Familienaam = "Keteleer", Wedde = 1600, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 10), Campus Id = oinouses.Camp usId },
            //new Docent { DocentId = 162, Voornaam = "Marce l", Familienaam = "Kint", Wedde = 110 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 11), CampusId = andros.CampusId },
            //new Docent { DocentId = 163, Voornaam = "Firmi n", Familienaam = "Lambot", Wedde = 1 500, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 12), CampusI d = ikaria.CampusI d },
            //new Docent { DocentId = 164, Voornaam = "Roger ", Familienaam = " Lambrecht", Wedde = 1900, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 13), Campus Id = gavdos.Campus Id },
            //new Docent { DocentId = 165, Voornaam = "Eric", Familienaam = "L eman", Wedde = 1400, HeeftRijbewijs = true, InDienst = n ew DateTime(2019, 6, 14), CampusId = hydra.CampusId },
            //new Docent { DocentId = 166, Voornaam = "Camil le", Familienaam = "Leroy", Wedde = 1 800, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 15), CampusI d = delos.CampusId },
            //new Docent { DocentId = 167, Voornaam = "Rolan d", Familienaam = "Liboton", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 6, 16), CampusI d = delos.CampusId },
            //new Docent { DocentId = 168, Voornaam = "Jules ", Familienaam = " Lowie", Wedde = 100 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 17), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 169, Voornaam = "André ", Familienaam = " Lurquin", Wedde = 1 400, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 18), CampusI d = hydra.CampusId },
            //new Docent { DocentId = 170, Voornaam = "Henri ", Familienaam = " Rik", Wedde = 1800, HeeftRijbewijs = null, InDienst = ne w DateTime(2019, 6, 19), CampusId = d elos.CampusId },
            //new Docent { DocentId = 171, Voornaam = "Pierr ot", Familienaam = "Machiels", Wedde = 1300, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 6, 20), Campu sId = gavdos.Campu sId },
            //new Docent { DocentId = 172, Voornaam = "André ", Familienaam = " Maelbrancke", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 6, 21), Cam pusId = andros.Cam pusId },
            //new Docent { DocentId = 173, Voornaam = "Fredd y", Familienaam = "Maertens", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 22), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 174, Voornaam = "Romai n", Familienaam = "Maes", Wedde = 160 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 23), CampusId = oinouses.CampusId },
            //new Docent { DocentId = 175, Voornaam = "Sylvè re", Familienaam = "Maes", Wedde = 13 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 6, 24), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 176, Voornaam = "Josep h", Familienaam = "Marchand", Wedde = 1700, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 25), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 177, Voornaam = "René", Familienaam = "M artens", Wedde = 12 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 26), CampusId = delos.CampusId },
            //new Docent { DocentId = 178, Voornaam = "Jacqu es", Familienaam = "Martin", Wedde = 1600, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 6, 27), Campus Id = oinouses.Camp usId },
            //new Docent { DocentId = 179, Voornaam = "Emile ", Familienaam = " père", Wedde = 2000, HeeftRijbewijs = null, InDienst = n ew DateTime(2019, 6, 28), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 180, Voornaam = "Flore nt", Familienaam = "Mathieu", Wedde = 1500, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 29), Campus Id = ikaria.Campus Id },
            //new Docent { DocentId = 181, Voornaam = "Nico", Familienaam = "M attan", Wedde = 190 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 6, 30), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 182, Voornaam = "Filip ", Familienaam = " Meirhaeghe", Wedde = 1600, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 7, 1), Campus Id = oinouses.Camp usId },
            //new Docent { DocentId = 183, Voornaam = "Axel", Familienaam = "M erckx", Wedde = 110 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 7, 2), CampusId = andros.CampusId },
            //new Docent { DocentId = 184, Voornaam = "Eddy", Familienaam = "M erckx", Wedde = 150 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 3), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 185, Voornaam = "André ", Familienaam = " Messelis", Wedde = 1900, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 4), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 186, Voornaam = "Mauri ce", Familienaam = "Meuleman", Wedde = 1400, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 7, 5), Campus Id = hydra.CampusI d },
            //new Docent { DocentId = 187, Voornaam = "Eloi", Familienaam = "M eulenberg", Wedde = 1800, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 7, 6), Campus Id = delos.CampusI d },
            //new Docent { DocentId = 188, Voornaam = "Frans ", Familienaam = " Mintjens", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 7), CampusId = delos.CampusId },
            //new Docent { DocentId = 189, Voornaam = "Yvo", Familienaam = "Mo lenaers", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 8), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 190, Voornaam = "Mauri ce", Familienaam = "Mollin", Wedde = 1400, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 7, 9), CampusI d = hydra.CampusId },
            //new Docent { DocentId = 191, Voornaam = "Arthu r", Familienaam = "Mommerency", Wedde = 1800, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 7, 10), Camp usId = delos.Campu sId },
            //new Docent { DocentId = 192, Voornaam = "Jean-Pierre", Familiena am = "Monséré", Wed de = 1300, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 7, 11), Ca mpusId = gavdos.Ca mpusId },
            //new Docent { DocentId = 193, Voornaam = "Willy ", Familienaam = " Monty", Wedde = 170 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 12), CampusId = andros.CampusId },
            //new Docent { DocentId = 194, Voornaam = "Sammi e", Familienaam = "Moreels", Wedde = 2100, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 13), CampusI d = andros.CampusI d },
            //new Docent { DocentId = 195, Voornaam = "Alfre d", Familienaam = "Mottard", Wedde = 1600, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 7, 14), CampusI d = oinouses.Campu sId },
            //new Docent { DocentId = 196, Voornaam = "Ernes t", Familienaam = "Mottart", Wedde = 1300, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 7, 15), Campus Id = gavdos.Campus Id },
            //new Docent { DocentId = 197, Voornaam = "Louis ", Familienaam = " Mottiat", Wedde = 1 700, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 7, 16), CampusId = andros.CampusId },
            //new Docent { DocentId = 198, Voornaam = "Johan ", Familienaam = " Museeuw", Wedde = 1 200, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 17), CampusId = delos.CampusId },
            //new Docent { DocentId = 199, Voornaam = "Wilfr ied", Familienaam = "Nelissen", Wedde = 1600, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 7, 18), Cam pusId = oinouses.C ampusId },
            //new Docent { DocentId = 200, Voornaam = "Franç ois", Familienaam = "Neuville", Wedde = 2000, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 7, 19), Camp usId = ikaria.Camp usId },
            //new Docent { DocentId = 201, Voornaam = "André ", Familienaam = " Noyelle", Wedde = 1 500, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 20), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 202, Voornaam = "Guy", Familienaam = "Nu lens", Wedde = 1900, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 21), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 203, Voornaam = "Nick", Familienaam = "N uyens", Wedde = 160 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 7, 22), CampusId = oinouses.CampusId },
            //new Docent { DocentId = 204, Voornaam = "Sven", Familienaam = "N ys", Wedde = 1100, HeeftRijbewijs = t rue, InDienst = new DateTime(2019, 7, 23), CampusId = an dros.CampusId },
            //new Docent { DocentId = 205, Voornaam = "Stan", Familienaam = "O ckers", Wedde = 150 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 24), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 206, Voornaam = "Petru s", Familienaam = "Oellibrandt", Wedd e = 1900, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 7, 25), Cam pusId = gavdos.Cam pusId },
            //new Docent { DocentId = 207, Voornaam = "Valèr e", Familienaam = "Ollivier", Wedde = 1400, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 7, 26), Campus Id = hydra.CampusI d },
            //new Docent { DocentId = 208, Voornaam = "Eddy", Familienaam = "P eelman", Wedde = 18 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 7, 27), CampusId = delos.CampusId },
            //new Docent { DocentId = 209, Voornaam = "Edwar d", Familienaam = "Peeters", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 28), CampusI d = delos.CampusId },
            //new Docent { DocentId = 210, Voornaam = "Luc", Familienaam = "Pe titjean", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 29), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 211, Voornaam = "Victo r", Familienaam = "Louis", Wedde = 14 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 7, 30), CampusId = hydra.CampusId },
            //new Docent { DocentId = 212, Voornaam = "Georg es", Familienaam = "Pintens", Wedde = 1800, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 7, 31), Campus Id = delos.CampusI d },
            //new Docent { DocentId = 213, Voornaam = "Théod ore", Familienaam = "Pirmez", Wedde = 1300, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 8, 1), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 214, Voornaam = "Eddy", Familienaam = "P lanckaert", Wedde = 1700, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 2), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 215, Voornaam = "Jo", Familienaam = "Pla nckaert", Wedde = 2 100, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 8, 3), CampusId = andros.CampusId },
            //new Docent { DocentId = 216, Voornaam = "Walte r", Familienaam = "Planckaert", Wedde = 1600, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 8, 4), Campu sId = oinouses.Cam pusId },
            //new Docent { DocentId = 217, Voornaam = "Willy ", Familienaam = " Planckaert", Wedde = 1300, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 8, 5), Campu sId = gavdos.Campu sId },
            //new Docent { DocentId = 218, Voornaam = "Miche l", Familienaam = "Pollentier", Wedde = 1700, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 8, 6), Campu sId = andros.Campu sId },
            //new Docent { DocentId = 219, Voornaam = "Léon", Familienaam = "P oncelet", Wedde = 1 200, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 7), CampusId = delos.CampusId },
            //new Docent { DocentId = 220, Voornaam = "Louis ", Familienaam = " Proost", Wedde = 16 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 8, 8), CampusId = oinouses.CampusI d },
            //new Docent { DocentId = 221, Voornaam = "Rober t", Familienaam = "Protin", Wedde = 2 000, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 8, 9), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 222, Voornaam = "Alber t", Familienaam = "Ramon", Wedde = 15 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 10), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 223, Voornaam = "Gasto n", Familienaam = "Rebry", Wedde = 19 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 8, 11), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 224, Voornaam = "Jens", Familienaam = "R enders", Wedde = 16 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 8, 12), CampusId = oinouses.CampusI d },
            //new Docent { DocentId = 225, Voornaam = "Guido ", Familienaam = " Reybrouck", Wedde = 1100, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 8, 13), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 226, Voornaam = "Marce l", Familienaam = "Rijckaert", Wedde = 1500, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 8, 14), Camp usId = ikaria.Camp usId },
            //new Docent { DocentId = 227, Voornaam = "Alber t", Familienaam = "Ritserveldt", Wedd e = 1900, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 8, 15), Cam pusId = gavdos.Cam pusId },
            //new Docent { DocentId = 228, Voornaam = "Bert", Familienaam = "R oesems", Wedde = 14 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 16), CampusId = hydra.CampusId },
            //new Docent { DocentId = 229, Voornaam = "Louis ", Familienaam = " Rolus", Wedde = 180 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 8, 17), CampusId = delos.CampusId },
            //new Docent { DocentId = 230, Voornaam = "Georg es", Familienaam = "Ronsse", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 8, 18), CampusI d = delos.CampusId },
            //new Docent { DocentId = 231, Voornaam = "André ", Familienaam = " Rosseel", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 19), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 232, Voornaam = "Félic ien", Familienaam = "Salmon", Wedde = 1400, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 20), Campu sId = hydra.Campus Id },
            //new Docent { DocentId = 233, Voornaam = "Léopo ld", Familienaam = "Schaeken", Wedde = 1800, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 8, 21), Campu sId = delos.Campus Id },
            //new Docent { DocentId = 234, Voornaam = "Willy ", Familienaam = " Scheers", Wedde = 1 300, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 22), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 235, Voornaam = "Alfon s", Familienaam = "Schepers", Wedde = 1700, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 23), Campu sId = andros.Campu sId },
            //new Docent { DocentId = 236, Voornaam = "Josep h", Familienaam = "Scherens", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 8, 24), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 237, Voornaam = "Jef", Familienaam = "Sc herens", Wedde = 1600, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 25), CampusId = oinouses.CampusI d },
            //new Docent { DocentId = 238, Voornaam = "Briek ", Familienaam = " Schotte", Wedde = 1 300, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 8, 26), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 239, Voornaam = "Frans ", Familienaam = " Schoubben", Wedde = 1700, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 8, 27), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 240, Voornaam = "Léon", Familienaam = "S cieur", Wedde = 120 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 28), CampusId = delos.CampusId },
            //new Docent { DocentId = 241, Voornaam = "Félix ", Familienaam = " Sellier", Wedde = 1 600, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 8, 29), CampusI d = oinouses.Campu sId },
            //new Docent { DocentId = 242, Voornaam = "Edwar d", Familienaam = "Sels", Wedde = 200 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 8, 30), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 243, Voornaam = "Alber t", Familienaam = "Sercu", Wedde = 15 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 31), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 244, Voornaam = "Patri ck", Familienaam = "Sercu", Wedde = 1 900, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 9, 1), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 245, Voornaam = "Andy", Familienaam = "d e Smet", Wedde = 16 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 2), CampusId = oinouses.CampusId },
            //new Docent { DocentId = 246, Voornaam = "Josep h", Familienaam = "Somers", Wedde = 1 100, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 3), CampusId = andros.CampusId },
            //new Docent { DocentId = 247, Voornaam = "Tom", Familienaam = "St eels", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 9, 4), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 248, Voornaam = "Ernes t", Familienaam = "Sterckx", Wedde = 1900, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 9, 5), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 249, Voornaam = "Lucie n", Familienaam = "Storme", Wedde = 1 400, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 6), CampusId = hydra.CampusId },
            //new Docent { DocentId = 250, Voornaam = "Tom", Familienaam = "St ubbe", Wedde = 1800, HeeftRijbewijs = false, InDienst = new DateTime(2019, 9, 7), CampusId = delos.CampusId },
            //new Docent { DocentId = 251, Voornaam = "Roger ", Familienaam = " Swerts", Wedde = 22 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 8), CampusId = delos.CampusId },
            //new Docent { DocentId = 252, Voornaam = "Arthu r", Familienaam = "Targez", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 10), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 253, Voornaam = "Andre i", Familienaam = "Tchmil", Wedde = 1 400, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 9, 11), CampusI d = hydra.CampusId },
            //new Docent { DocentId = 254, Voornaam = "Emman uel", Familienaam = "Thoma", Wedde = 1800, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 9, 12), CampusI d = delos.CampusId },
            //new Docent { DocentId = 255, Voornaam = "Phili ppe", Familienaam = "Thys", Wedde = 1 300, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 13), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 256, Voornaam = "Hecto r", Familienaam = "Tiberghien", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 9, 14), Cam pusId = andros.Cam pusId },
            //new Docent { DocentId = 257, Voornaam = "Léon", Familienaam = "T ommies", Wedde = 21 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 15), CampusId = andros.CampusId },
            //new Docent { DocentId = 258, Voornaam = "Lode", Familienaam = "T roonbeeckx", Wedde = 1600, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 9, 16), Campu sId = oinouses.Cam pusId },
            //new Docent { DocentId = 259, Voornaam = "Greg", Familienaam = "V an Avermaet", Wedde = 1300, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 9, 17), Cam pusId = gavdos.Cam pusId },
            //new Docent { DocentId = 260, Voornaam = "Arman d", Familienaam = "Van Bruaene", Wedd e = 1700, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 9, 18), Cam pusId = andros.Cam pusId },
            //new Docent { DocentId = 261, Voornaam = "Georg es", Familienaam = "Vanconingsloo", W edde = 1200, Heeft Rijbewijs = true, I nDienst = new Date Time(2019, 9, 19), CampusId = delos.C ampusId },
            //new Docent { DocentId = 262, Voornaam = "Léon", Familienaam = "V an Daele", Wedde = 1600, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 9, 20), Campus Id = oinouses.Camp usId },
            //new Docent { DocentId = 263, Voornaam = "Charl es", Familienaam = "Van Den Born", We dde = 2000, HeeftR ijbewijs = null, In Dienst = new DateT ime(2019, 9, 21), C ampusId = ikaria.C ampusId },
            //new Docent { DocentId = 264, Voornaam = "Frank ", Familienaam = " Vandenbroucke", Wed de = 1500, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 9, 22), Ca mpusId = ikaria.Ca mpusId },
            //new Docent { DocentId = 265, Voornaam = "Odiel ", Familienaam = " Vanden Meerschaut", Wedde = 1900, Hee ftRijbewijs = false, InDienst = new D ateTime(2019, 9, 23), CampusId = gavd os.CampusId },
            //new Docent { DocentId = 266, Voornaam = "Eric", Familienaam = "V anderaerden", Wedde = 1600, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 9, 24), Camp usId = oinouses.Ca mpusId },
            //new Docent { DocentId = 267, Voornaam = "Kurt", Familienaam = "V an de Wouwer", Wedd e = 1100, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 9, 25), Cam pusId = andros.Cam pusId },
            //new Docent { DocentId = 268, Voornaam = "Richa rd", Familienaam = "Van Genechten", W edde = 1500, Heeft Rijbewijs = false, InDienst = new Dat eTime(2019, 9, 26), CampusId = ikaria.CampusId },
            //new Docent { DocentId = 269, Voornaam = "Marti n", Familienaam = "Van Geneugden", We dde = 1900, HeeftR ijbewijs = null, In Dienst = new DateT ime(2019, 9, 27), C ampusId = gavdos.C ampusId },
            //new Docent { DocentId = 270, Voornaam = "Cyril le", Familienaam = "Van Hauwaert", We dde = 1400, HeeftR ijbewijs = true, In Dienst = new DateT ime(2019, 9, 28), C ampusId = hydra.Ca mpusId },
            //new Docent { DocentId = 271, Voornaam = "Mauri ce", Familienaam = "Van Herzele", Wed de = 1800, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 9, 30), C ampusId = delos.Ca mpusId },
            //new Docent { DocentId = 272, Voornaam = "Jules ", Familienaam = " Van Hevel", Wedde = 2200, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 1), Campus Id = delos.CampusI d },
            //new Docent { DocentId = 273, Voornaam = "Edwig ", Familienaam = " Van Hooydonck", Wed de = 1000, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 10, 2), Ca mpusId = ikaria.Ca mpusId },
            //new Docent { DocentId = 274, Voornaam = "Lucie n", Familienaam = "Van Impe", Wedde = 1400, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 10, 3), Campu sId = hydra.Campus Id },
            //new Docent { DocentId = 275, Voornaam = "Henri ", Familienaam = " Van Kerkhove", Wedd e = 1800, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 10, 4), Cam pusId = delos.Camp usId },
            //new Docent { DocentId = 276, Voornaam = "Rik", Familienaam = "Va n Linden", Wedde = 1300, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 5), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 277, Voornaam = "Rik", Familienaam = "Va n Looy", Wedde = 17 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 10, 6), CampusId = andros.CampusId },
            //new Docent { DocentId = 278, Voornaam = "Willy ", Familienaam = " Vannitsen", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 7), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 279, Voornaam = "Peter ", Familienaam = " Van Petegem", Wedde = 1600, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 10, 8), Camp usId = oinouses.Ca mpusId },
            //new Docent { DocentId = 280, Voornaam = "Peter ", Familienaam = " Van Santvliet", Wed de = 1300, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 10, 9), C ampusId = gavdos.C ampusId },
            //new Docent { DocentId = 281, Voornaam = "Victo r", Familienaam = "Van Schil", Wedde = 1700, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 10, 10), Camp usId = andros.Camp usId },
            //new Docent { DocentId = 282, Voornaam = "Herma n", Familienaam = "van Springel", Wed de = 1200, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 10, 11), C ampusId = delos.Ca mpusId },
            //new Docent { DocentId = 283, Voornaam = "Rik", Familienaam = "Va n Steenbergen", Wed de = 1600, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 10, 12), CampusId = oinouse s.CampusId },
            //new Docent { DocentId = 284, Voornaam = "Guill aume", Familienaam = "Van Tongerloo", Wedde = 2000, Hee ftRijbewijs = null, InDienst = new Da teTime(2019, 10, 13), CampusId = ikar ia.CampusId },
            //new Docent { DocentId = 285, Voornaam = "Noël", Familienaam = "V antyghem", Wedde = 1500, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 14), Campus Id = ikaria.Campus Id },
            //new Docent { DocentId = 286, Voornaam = "Rik", Familienaam = "Ve rbrugghe", Wedde = 1900, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 10, 15), Campu sId = gavdos.Campu sId },
            //new Docent { DocentId = 287, Voornaam = "Augus te", Familienaam = "Verdyck", Wedde = 1600, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 16), Campu sId = oinouses.Cam pusId },
            //new Docent { DocentId = 288, Voornaam = "Jozef ", Familienaam = " Verhaert", Wedde = 1100, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 17), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 289, Voornaam = "René", Familienaam = "V ermandel", Wedde = 1500, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 10, 18), Campu sId = ikaria.Campu sId },
            //new Docent { DocentId = 290, Voornaam = "Stive ", Familienaam = " Vermaut", Wedde = 1 900, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 10, 19), CampusI d = gavdos.CampusI d },
            //new Docent { DocentId = 291, Voornaam = "Adolf ", Familienaam = " Verschueren", Wedde = 1400, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 10, 20), Cam pusId = hydra.Camp usId },
            //new Docent { DocentId = 292, Voornaam = "Const ant", Familienaam = "Verschueren", We dde = 1800, HeeftR ijbewijs = false, I nDienst = new Date Time(2019, 10, 21), CampusId = delos.CampusId },
            //new Docent { DocentId = 293, Voornaam = "Johan ", Familienaam = " Verstrepen", Wedde = 2200, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 10, 22), Camp usId = delos.Campu sId },
            //new Docent { DocentId = 294, Voornaam = "Félic ien", Familienaam = "Vervaecke", Wedd e = 1000, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 10, 23), Ca mpusId = ikaria.Ca mpusId },
            //new Docent { DocentId = 295, Voornaam = "Julie n", Familienaam = "Vervaecke", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 10, 24), Cam pusId = hydra.Camp usId },
            //new Docent { DocentId = 296, Voornaam = "Edwar d", Familienaam = "Vissers", Wedde = 1800, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 10, 25), Campus Id = delos.CampusI d },
            //new Docent { DocentId = 297, Voornaam = "Lucie n", Familienaam = "Vlaemynck", Wedde = 1300, HeeftRijbe wijs = true, InDienst = new DateTime(2019, 10, 26), Camp usId = gavdos.Camp usId },
            //new Docent { DocentId = 298, Voornaam = "André ", Familienaam = " Vlaeyen", Wedde = 1 700, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 10, 27), Campus Id = andros.Campus Id },
            //new Docent { DocentId = 299, Voornaam = "Jean", Familienaam = "V liegen", Wedde = 21 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 10, 28), CampusId = andros.CampusId },
            //new Docent { DocentId = 300, Voornaam = "Luc", Familienaam = "Wa llays", Wedde = 160 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 10, 29), CampusId = oinouses.CampusI d },
            //new Docent { DocentId = 301, Voornaam = "René", Familienaam = "W alschot", Wedde = 1 300, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 10, 30), Campus Id = gavdos.Campus Id },
            //new Docent { DocentId = 302, Voornaam = "Jean-Marie", Familienaa m = "Wampers", Wedd e = 1700, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 10, 31), Ca mpusId = andros.Ca mpusId },
            //new Docent { DocentId = 303, Voornaam = "Rober t", Familienaam = "Wancour", Wedde = 1200, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 11, 1), CampusI d = delos.CampusId },
            //new Docent { DocentId = 304, Voornaam = "Bart", Familienaam = "W ellens", Wedde = 16 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 11, 2), CampusId = oinouses.Campus Id },
            //new Docent { DocentId = 305, Voornaam = "Wilfr ied", Familienaam = "Wesemael", Wedde = 2000, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 11, 3), Camp usId = ikaria.Camp usId },
            //new Docent { DocentId = 306, Voornaam = "Woute r", Familienaam = "Weylandt", Wedde = 1500, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 11, 4), Campus Id = ikaria.Campus Id },
            //new Docent { DocentId = 307, Voornaam = "Marc", Familienaam = "W auters", Wedde = 19 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 11, 5), CampusId = gavdos.CampusId },
            //new Docent { DocentId = 308, Voornaam = "Danie l", Familienaam = "Willems", Wedde = 1600, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 11, 6), CampusI d = oinouses.Campu sId },
            //new Docent { DocentId = 309, Voornaam = "Jozef ", Familienaam = " Wouters", Wedde = 1 100, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 11, 7), CampusId = andros.CampusId }
            );
        }
    }
}
