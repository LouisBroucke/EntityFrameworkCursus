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
                //CampusId = 1,             // Primary Key
                Naam = "Andros",
                Straat = "Somersstraat",
                Huisnummer = "22",
                Postcode = "2018",
                Gemeente = "Antwerpen"
            };
            var delos = new Campus
            {
                //CampusId = 2,
                Naam = "Delos",
                Straat = "Oude Vest",
                Huisnummer = "17",
                Postcode = "9200",
                Gemeente = "Dendermonde"
            };
            var gavdos = new Campus
            {
                //CampusId = 3,
                Naam = "Gavdos",
                Straat = "Europalaan",
                Huisnummer = "37",
                Postcode = "3600",
                Gemeente = "Genk"
            };
            var hydra = new Campus
            {
                //CampusId = 4,
                Naam = "Hydra",
                Straat = "Interleuvenlaan",
                Huisnummer = "2",
                Postcode = "3001",
                Gemeente = "Heverlee"
            };
            var ikaria = new Campus
            {
                //CampusId = 5,
                Naam = "Ikaria",
                Straat = "Vlamingstraat",
                Huisnummer = "10",
                Postcode = "8560",
                Gemeente = "Wevelgem"
            };
            var oinouses = new Campus
            {
                //CampusId = 6,
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
            new { DocentId = 001, Voornaam = "Willy", Familienaam = "Abbeloo s", Wedde = 1400m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 1), CampusId = hydra.CampusId },
            new { DocentId = 002, Voornaam = "Joseph", Familienaam = "Abelsh ausen", Wedde = 1800m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 2), CampusId = delos.CampusId },
            new { DocentId = 003, Voornaam = "Joseph", Familienaam = "Achten ", Wedde = 1300m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 3), CampusId = gavdos.CampusId },
            new { DocentId = 004, Voornaam = "François", Familienaam = "Adam ", Wedde = 1700m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 4), CampusId = hydra.CampusId },
            new { DocentId = 005, Voornaam = "Jan", Familienaam = "Adriaense ns", Wedde = 2100m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 5), CampusId = andros.CampusId },
            new { DocentId = 006, Voornaam = "René", Familienaam = "Adriaens ens", Wedde = 1600m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 6), CampusId = oinouses.CampusId },
            new { DocentId = 007, Voorn aam = "Frans", Fami lienaam = "Aerenho uts", Wedde = 1300m, HeeftRijbewijs = new Nullable<bool>(), InDienst = new DateTime(2019, 1, 7), CampusId = gav dos.CampusId },
            new { DocentId = 008, Voorn aam = "Emile", Fami lienaam = "Aerts", Wedde = 1700m, Hee ftRijbewijs = true, InDienst = new Da teTime(2019, 1, 8), CampusId = andros.CampusId },
            new { DocentId = 009, Voorn aam = "Jean", Famil ienaam = "Aerts", Wedde = 1200m, Heef tRijbewijs = false, InDienst = new Da teTime(2019, 1, 9), CampusId = delos.CampusId },
            new { DocentId = 010, Voorn aam = "Mario", Fami lienaam = "Aerts", Wedde = 1600m, Hee ftRijbewijs = new Nullable<bool>(), I nDienst = new Date Time(2019, 1, 10), CampusId = oinouse s.CampusId },
            // Toevoegen nieuwe Docent met een Docent clas s
            new Docent { DocentId = 011, Voornaam = "Paul", Familienaam = "A erts", Wedde = 2000 m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 11), CampusId = ikaria.CampusId },
            new Docent { DocentId = 012, Voornaam = "Stefa n", Familienaam = "Aerts", Wedde = 15 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 1, 12), CampusId = ikaria.CampusId },
            new Docent { DocentId = 013, Voornaam = "Franç ois", Familienaam = "Alexander", Wedd e = 1900m, HeeftRi jbewijs = null, InD ienst = new DateTi me(2019, 1, 13), Ca mpusId = gavdos.Ca mpusId },
            new Docent { DocentId = 014, Voornaam = "Henri ", Familienaam = " Allard", Wedde = 16 00m, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 1, 14), CampusId = oinouses.Campus Id },
            new Docent { DocentId = 015, Voornaam = "Alber t", Familienaam = "Anciaux", Wedde = 1100m, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 1, 15), Campu sId = 1 },
            new Docent { DocentId = 016, Voornaam = "Urbai n", Familienaam = "Anseeuw", Wedde = 1500m, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 1, 16), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 017, Voornaam = "Etien ne", Familienaam = "Antheunis", Wedde = 1900m, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 1, 17), Cam pusId = gavdos.Cam pusId },
            new Docent { DocentId = 018, Voornaam = "Jacqu es", Familienaam = "Arlet", Wedde = 1 400m, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 1, 18), Campus Id = hydra.CampusI d },
            new Docent { DocentId = 019, Voornaam = "Wim", Familienaam = "Ar ras", Wedde = 1800m, HeeftRijbewijs = null, InDienst = n ew DateTime(2019, 1, 19), CampusId = delos.CampusId },
            new Docent { DocentId = 020, Voornaam = "Roger ", Familienaam = " Baens", Wedde = 220 0m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 20), CampusId = delos.CampusId },
            new Docent { DocentId = 021, Voornaam = "Dirk", Familienaam = "B aert", Wedde = 1000 m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 1, 21), CampusId = ikaria.CampusId },
            new Docent { DocentId = 022, Voornaam = "Huber t", Familienaam = "Baert", Wedde = 14 00m, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 1, 22), CampusId = hydra.CampusId },
            new Docent { DocentId = 023, Voornaam = "Jean-Pierre", Familiena am = "Baert", Wedde = 1800m, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 1, 23), Cam pusId = delos.Camp usId },
            new Docent { DocentId = 024, Voornaam = "Arman d", Familienaam = "Baeyens", Wedde = 1300m, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 1, 24), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 025, Voornaam = "Jan", Familienaam = "Ba eyens", Wedde = 170 0m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 1, 25), CampusId = andros.CampusId },
            new Docent { DocentId = 026, Voornaam = "Roger ", Familienaam = " Baguet", Wedde = 21 00m, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 1, 26), CampusId = andros.CampusId },
            new Docent { DocentId = 027, Voornaam = "Serge ", Familienaam = " Baguet", Wedde = 16 00m, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 1, 27), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 028, Voornaam = "Gérar d", Familienaam = "Balducq", Wedde = 1300m, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 1, 28), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 029, Voornaam = "Koen", Familienaam = "B arbé", Wedde = 1700 m, HeeftRijbewijs = true, InDienst = new DateTime(2019, 1, 29), CampusId = andros.CampusId },
            new Docent { DocentId = 030, Voornaam = "Georg es", Familienaam = "Barras", Wedde = 1200m, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 1, 30), Campu sId = delos.Campus Id },
            new Docent { DocentId = 031, Voornaam = "Augus te", Familienaam = "Baumans", Wedde = 1600m, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 1, 31), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 032, Voornaam = "Arsèn e", Familienaam = "Bauwens", Wedde = 2000m, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 2, 1), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 033, Voornaam = "Henri ", Familienaam = " Bauwens", Wedde = 1 500m, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 2, 2), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 034, Voornaam = "Jules ", Familienaam = " Bayens", Wedde = 19 00m, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 2, 3), CampusId = gavdos.CampusId },
            new Docent { DocentId = 035, Voornaam = "Alber t", Familienaam = "Beckaert", Wedde = 1600m, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 2, 4), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 036, Voornaam = "Marce l", Familienaam = "Beckaert", Wedde = 1100m, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 2, 5), Campu sId = andros.Campu sId },
            new Docent { DocentId = 037, Voornaam = "Koen", Familienaam = "B eeckman", Wedde = 1 500m, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 2, 6), CampusId = ikaria.CampusId },
            new Docent { DocentId = 038, Voornaam = "Kamie l", Familienaam = "Beeckman", Wedde = 1900m, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 2, 7), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 039, Voornaam = "Theop hile", Familienaam = "Beeckman", Wedd e = 1400m, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 2, 8), Ca mpusId = hydra.Cam pusId },
            new Docent { DocentId = 040, Voornaam = "Benon i", Familienaam = "Beheyt", Wedde = 1 800m, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 2, 9), CampusId = delos.CampusId },
            new Docent { DocentId = 041, Voornaam = "Alber t", Familienaam = "Beirnaert", Wedde = 2200m, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 2, 10), Camp usId = delos.Campu sId },
            new Docent { DocentId = 042, Voornaam = "Jean", Familienaam = "B elvaux", Wedde = 10 00m, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 2, 11), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 043, Voornaam = "Adeli n", Familienaam = "Benoit", Wedde = 1 400m, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 2, 12), CampusI d = hydra.CampusId },
            new Docent { DocentId = 044, Voornaam = "Augus te", Familienaam = "Benoit", Wedde = 1800m, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 2, 13), Campus Id = delos.CampusI d },
            new Docent { DocentId = 045, Voornaam = "Jef", Familienaam = "Be rben", Wedde = 1300 m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 2, 14), CampusId = gavdos.CampusId },
            new Docent { DocentId = 046, Voornaam = "Jean-Pierre", Familiena am = "Berckmans", W edde = 1700m, Heef tRijbewijs = null, InDienst = new Dat eTime(2019, 2, 15), CampusId = andros.CampusId },
            new Docent { DocentId = 047, Voornaam = "Alber t", Familienaam = "Berton", Wedde = 2 100m, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 2, 16), CampusI d = andros.CampusI d },
            new Docent { DocentId = 048, Voornaam = "Frans ", Familienaam = " Beths", Wedde = 160 0m, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 2, 17), CampusId = oinouses.Campus Id },
            new Docent { DocentId = 049, Voornaam = "René", Familienaam = "B eyens", Wedde = 130 0m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 2, 18), CampusId = gavdos.CampusId },
            new Docent { DocentId = 050, Voornaam = "Herma n", Familienaam = "Beyssens", Wedde = 1700m, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 2, 19), Campu sId = andros.Campu sId },
            new Docent { DocentId = 051, Voornaam = "Alber t", Familienaam = "Billiet", Wedde = 1200m, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 2, 20), Campu sId = delos.Campus Id },
            new Docent { DocentId = 052, Voornaam = "Hecto r", Familienaam = "Billiet", Wedde = 1600m, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 2, 21), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 053, Voornaam = "Marce l", Familienaam = "Blavier", Wedde = 2000m, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 2, 22), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 054, Voornaam = "Roger ", Familienaam = " Blockx", Wedde = 15 00m, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 2, 23), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 055, Voornaam = "Mauri ce", Familienaam = "Blomme", Wedde = 1900m, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 2, 24), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 056, Voornaam = "Willy ", Familienaam = " Bocklandt", Wedde = 1600m, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 2, 25), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 057, Voornaam = "Emile ", Familienaam = " Bodart", Wedde = 11 00m, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 2, 26), CampusI d = andros.CampusI d },
            new Docent { DocentId = 058, Voornaam = "Alfon s", Familienaam = "Boekaerts", Wedde = 1500m, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 2, 27), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 059, Voornaam = "Cesar ", Familienaam = " Bogaert", Wedde = 1 900m, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 2, 28), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 060, Voornaam = "Jan", Familienaam = "Bo gaert", Wedde = 140 0m, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 3, 1), CampusId = hydra.CampusId },
            new Docent { DocentId = 061, Voornaam = "Jean", Familienaam = "B ogaerts", Wedde = 1 800m, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 3, 2), CampusId = delos.CampusId },
            new Docent { DocentId = 062, Voornaam = "Frans ", Familienaam = " Bonduel", Wedde = 2 200m, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 3, 3), CampusId = delos.CampusId },
            new Docent { DocentId = 063, Voornaam = "Tom", Familienaam = "Bo onen", Wedde = 1000 m, HeeftRijbewijs = false, InDienst = new DateTime(2019, 3, 4), CampusId = ikaria.CampusId },
            new Docent { DocentId = 064, Voornaam = "Jozef ", Familienaam = " Boons", Wedde = 140 0m, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 5), CampusId = hydra.CampusId },
            new Docent { DocentId = 065, Voornaam = "Gabri el", Familienaam = "Borra", Wedde = 1 800m, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 3, 6), CampusId = delos.CampusId },
            new Docent { DocentId = 066, Voornaam = "Josep h", Familienaam = "Bosmans", Wedde = 1300m, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 3, 7), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 067, Voornaam = "Walte r", Familienaam = "Boucquet", Wedde = 1700m, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 3, 8), Campus Id = andros.Campus Id },
            new Docent { DocentId = 068, Voornaam = "Marce l", Familienaam = "Boumon", Wedde = 2 100m, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 3, 9), CampusId = andros.CampusId },
            new Docent { DocentId = 069, Voornaam = "Ferdi nand", Familienaam = "Bracke", Wedde = 1600m, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 3, 10), Cam pusId = oinouses.C ampusId },
            new Docent { DocentId = 070, Voornaam = "Adolp he", Familienaam = "Braeckeveldt", We dde = 1300m, Heeft Rijbewijs = null, I nDienst = new Date Time(2019, 3, 11), CampusId = gavdos.CampusId },
            new Docent { DocentId = 071, Voornaam = "Omer", Familienaam = "B raekevelt", Wedde = 1700m, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 3, 12), Campu sId = andros.Campu sId },
            new Docent { DocentId = 072, Voornaam = "Frans ", Familienaam = " Brands", Wedde = 12 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 3, 13), CampusId = delos.CampusId },
            new Docent { DocentId = 073, Voornaam = "Jean", Familienaam = "B rankart", Wedde = 1 600, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 3, 14), CampusId = oinouses.Campus Id },
            new Docent { DocentId = 074, Voornaam = "Emile ", Familienaam = " Brichard", Wedde = 2000, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 3, 15), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 075, Voornaam = "Georg es", Familienaam = "Brosteaux", Wedde = 1500, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 3, 16), Cam pusId = ikaria.Cam pusId },
            new Docent { DocentId = 076, Voornaam = "Emile ", Familienaam = " Bruneau", Wedde = 1 900, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 3, 17), CampusId = gavdos.CampusId },
            new Docent { DocentId = 077, Voornaam = "Jean-Marie", Familienaa m = "Bruyère", Wedd e = 1600, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 3, 18), Cam pusId = oinouses.C ampusId },
            new Docent { DocentId = 078, Voornaam = "Josep h", Familienaam = "Bruyere", Wedde = 1100, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 3, 19), Campus Id = andros.Campus Id },
            new Docent { DocentId = 079, Voornaam = "Dave", Familienaam = "B ruylandts", Wedde = 1500, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 3, 20), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 080, Voornaam = "Johan ", Familienaam = " Bruyneel", Wedde = 1900, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 3, 21), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 081, Voornaam = "Lucie n", Familienaam = "Buysse", Wedde = 1 400, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 3, 22), CampusI d = hydra.CampusId },
            new Docent { DocentId = 082, Voornaam = "Chris tophe", Familienaa m = "Brandt", Wedde = 1800, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 3, 23), Camp usId = delos.Campu sId },
            new Docent { DocentId = 083, Voornaam = "Norbe rt", Familienaam = "Callens", Wedde = 2200, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 3, 24), Campus Id = delos.CampusI d },
            new Docent { DocentId = 084, Voornaam = "Johan ", Familienaam = " Capiot", Wedde = 10 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 3, 25), CampusId = ikaria.CampusId },
            new Docent { DocentId = 085, Voornaam = "Pino", Familienaam = "C erami", Wedde = 140 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 3, 26), CampusId = hydra.CampusId },
            new Docent { DocentId = 086, Voornaam = "Georg es", Familienaam = "Christiaens", Wed de = 1800, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 3, 27), Ca mpusId = delos.Cam pusId },
            new Docent { DocentId = 087, Voornaam = "Georg es", Familienaam = "Claes", Wedde = 1 300, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 3, 28), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 088, Voornaam = "Karel ", Familienaam = " Clerckx", Wedde = 1 700, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 3, 29), CampusId = andros.CampusId },
            new Docent { DocentId = 089, Voornaam = "Alex", Familienaam = "C lose", Wedde = 2100, HeeftRijbewijs = true, InDienst = n ew DateTime(2019, 3, 30), CampusId = andros.CampusId },
            new Docent { DocentId = 090, Voornaam = "Yvan", Familienaam = "C orbusier", Wedde = 1600, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 3, 31), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 091, Voornaam = "Hilai re", Familienaam = "Couvreur", Wedde = 1300, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 4, 1), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 092, Voornaam = "Wilfr ied", Familienaam = "Cretskens", Wedd e = 1700, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 4, 2), Camp usId = andros.Camp usId },
            new Docent { DocentId = 093, Voornaam = "Claud e", Familienaam = "Criquielion", Wedd e = 1200, HeeftRij bewijs = false, InD ienst = new DateTi me(2019, 4, 3), Cam pusId = delos.Camp usId },
            new Docent { DocentId = 094, Voornaam = "Emile ", Familienaam = " Daems", Wedde = 160 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 4, 4), CampusId = oinouses.CampusId },
            new Docent { DocentId = 095, Voornaam = "Gusta ve", Familienaam = "Danneels", Wedde = 2000, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 4, 5), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 096, Voornaam = "Fred", Familienaam = "D e Bruyne", Wedde = 1500, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 4, 6), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 097, Voornaam = "Arthu r", Familienaam = "Decabooter", Wedde = 1900, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 4, 7), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 098, Voornaam = "Hans", Familienaam = "D e Clerq", Wedde = 1 600, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 4, 8), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 099, Voornaam = "Roger ", Familienaam = " Decock", Wedde = 11 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 4, 9), CampusId = andros.CampusId },
            new Docent { DocentId = 100, Voornaam = "Georg es", Familienaam = "Decraeye", Wedde = 1500, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 4, 10), Campu sId = ikaria.Campu sId },
            new Docent { DocentId = 101, Voornaam = "Odiel ", Familienaam = " Defraeye", Wedde = 1900, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 4, 11), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 102, Voornaam = "Alber t", Familienaam = "De Jonghe", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 4, 12), Camp usId = hydra.Campu sId },
            new Docent { DocentId = 103, Voornaam = "Julie n", Familienaam = "Delbecque", Wedde = 1800, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 4, 13), Campu sId = delos.Campus Id },
            new Docent { DocentId = 104, Voornaam = "Alfon s", Familienaam = "Deloor", Wedde = 2 200, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 4, 14), CampusId = delos.CampusId },
            new Docent { DocentId = 105, Voornaam = "Gusta af", Familienaam = "Deloor", Wedde = 1000, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 4, 15), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 106, Voornaam = "Huber t", Familienaam = "Deltour", Wedde = 1400, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 4, 16), CampusI d = hydra.CampusId },
            new Docent { DocentId = 107, Voornaam = "Paul", Familienaam = "D eman", Wedde = 1800, HeeftRijbewijs = true, InDienst = n ew DateTime(2019, 4, 17), CampusId = delos.CampusId },
            new Docent { DocentId = 108, Voornaam = "Marc", Familienaam = "D emeyer", Wedde = 13 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 4, 18), CampusId = gavdos.CampusId },
            new Docent { DocentId = 109, Voornaam = "Frans ", Familienaam = " De Mulder", Wedde = 1700, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 4, 19), Campus Id = andros.Campus Id },
            new Docent { DocentId = 110, Voornaam = "Johan ", Familienaam = " De Muynck", Wedde = 2100, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 4, 20), Campus Id = andros.Campus Id },
            new Docent { DocentId = 111, Voornaam = "Jef", Familienaam = "De muysere", Wedde = 1 600, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 4, 21), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 112, Voornaam = "Jules ", Familienaam = " Depoorter", Wedde = 1300, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 4, 22), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 113, Voornaam = "Richa rd", Familienaam = "Depoorter", Wedde = 1700, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 4, 23), Camp usId = andros.Camp usId },
            new Docent { DocentId = 114, Voornaam = "Prosp er", Familienaam = "Depredomme", Wedd e = 1200, HeeftRij bewijs = false, InD ienst = new DateTi me(2019, 4, 24), Ca mpusId = delos.Cam pusId },
            new Docent { DocentId = 115, Voornaam = "Willy ", Familienaam = " Derboven", Wedde = 1600, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 4, 25), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 116, Voornaam = "Germa in", Familienaam = "Derijcke", Wedde = 2000, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 4, 26), Campu sId = ikaria.Campu sId },
            new Docent { DocentId = 117, Voornaam = "Miche l", Familienaam = "Dernies", Wedde = 1500, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 4, 27), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 118, Voornaam = "Charl es", Familienaam = "Deruyter", Wedde = 1900, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 4, 28), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 119, Voornaam = "Mauri ce", Familienaam = "Desimpelaere", We dde = 1600, HeeftR ijbewijs = true, In Dienst = new DateT ime(2019, 4, 29), C ampusId = oinouses.CampusId },
            new Docent { DocentId = 120, Voornaam = "Gilbe rt", Familienaam = "Desmet", Wedde = 1100, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 4, 30), Campus Id = andros.Campus Id },
            new Docent { DocentId = 121, Voornaam = "Georg es", Familienaam = "Desplenter", Wedd e = 1500, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 5, 1), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 122, Voornaam = "Léon", Familienaam = "D espontin", Wedde = 1900, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 2), CampusId = gavdos.CampusId },
            new Docent { DocentId = 123, Voornaam = "Eric", Familienaam = "D e Vlaeminck", Wedde = 1400, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 5, 3), Camp usId = hydra.Campu sId },
            new Docent { DocentId = 124, Voornaam = "Roger ", Familienaam = " De Vlaeminck", Wedd e = 1800, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 5, 4), Camp usId = delos.Campu sId },
            new Docent { DocentId = 125, Voornaam = "Stijn ", Familienaam = " Devolder", Wedde = 2200, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 5), CampusId = delos.CampusId },
            new Docent { DocentId = 126, Voornaam = "Mauri ce", Familienaam = "Dewaele", Wedde = 1000, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 5, 6), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 127, Voornaam = "Alfon s", Familienaam = "De Wolf", Wedde = 1400, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 5, 7), CampusId = hydra.CampusId },
            new Docent { DocentId = 128, Voornaam = "Rudy", Familienaam = "D haenens", Wedde = 1 800, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 5, 8), CampusId = delos.CampusId },
            new Docent { DocentId = 129, Voornaam = "Miche l", Familienaam = "D''Hooghe", Wedde = 1300, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 9), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 130, Voornaam = "Ludo", Familienaam = "D ierckxsens", Wedde = 1700, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 10), Campu sId = andros.Campu sId },
            new Docent { DocentId = 131, Voornaam = "Frans ", Familienaam = " Dictus", Wedde = 21 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 11), CampusId = andros.CampusId },
            new Docent { DocentId = 132, Voornaam = "Lomme ", Familienaam = " Driessens", Wedde = 1600, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 5, 12), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 133, Voornaam = "Gusta ve", Familienaam = "Drioul", Wedde = 1300, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 5, 13), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 134, Voornaam = "Marce l", Familienaam = "Dupont", Wedde = 1 700, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 5, 14), CampusId = andros.CampusId },
            new Docent { DocentId = 135, Voornaam = "Niko", Familienaam = "E eckhout", Wedde = 1 200, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 5, 15), CampusI d = delos.CampusId },
            new Docent { DocentId = 136, Voornaam = "Nico", Familienaam = "E monds", Wedde = 160 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 16), CampusId = oinouses.CampusId },
            new Docent { DocentId = 137, Voornaam = "Peter ", Familienaam = " Farazijn", Wedde = 2000, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 17), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 138, Voornaam = "Herma n", Familienaam = "Frison", Wedde = 1 500, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 5, 18), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 139, Voornaam = "Henri ", Familienaam = " Garnier", Wedde = 1 900, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 5, 19), CampusId = gavdos.CampusId },
            new Docent { DocentId = 140, Voornaam = "Frans ", Familienaam = " Gielen", Wedde = 16 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 5, 20), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 141, Voornaam = "Romai n", Familienaam = "Gijssels", Wedde = 1100, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 5, 21), Campu sId = andros.Campu sId },
            new Docent { DocentId = 142, Voornaam = "Walte r", Familienaam = "Godefroot", Wedde = 1500, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 22), Campu sId = ikaria.Campu sId },
            new Docent { DocentId = 143, Voornaam = "Dries ", Familienaam = " Govaerts", Wedde = 1900, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 5, 23), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 144, Voornaam = "Sylva in", Familienaam = "Grysolle", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 24), Camp usId = hydra.Campu sId },
            new Docent { DocentId = 145, Voornaam = "Roger ", Familienaam = " Gyselinck", Wedde = 1800, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 5, 25), Campus Id = delos.CampusI d },
            new Docent { DocentId = 146, Voornaam = "Paul", Familienaam = "H aghedooren", Wedde = 2200, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 5, 26), Campu sId = delos.Campus Id },
            new Docent { DocentId = 147, Voornaam = "Alfre d", Familienaam = "Hamerlinck", Wedde = 1000, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 5, 27), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 148, Voornaam = "Louis ", Familienaam = " Hardiquest", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 5, 28), Camp usId = hydra.Campu sId },
            new Docent { DocentId = 149, Voornaam = "Emile ", Familienaam = " Hardy", Wedde = 180 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 5, 29), CampusId = delos.CampusId },
            new Docent { DocentId = 150, Voornaam = "Marce l", Familienaam = "Hendrikx", Wedde = 1300, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 5, 30), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 151, Voornaam = "Josep h", Familienaam = "Hoevenaers", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 5, 31), Cam pusId = andros.Cam pusId },
            new Docent { DocentId = 152, Voornaam = "Kevin ", Familienaam = " Hulsmans", Wedde = 2100, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 6, 1), CampusId = andros.CampusId },
            new Docent { DocentId = 153, Voornaam = "Raymo nd", Familienaam = "Impanis", Wedde = 1600, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 2), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 154, Voornaam = "Paul", Familienaam = "I n''t", Wedde = 1300, HeeftRijbewijs = false, InDienst = new DateTime(2019, 6, 3), CampusId = gavdos.CampusId },
            new Docent { DocentId = 155, Voornaam = "Willy ", Familienaam = " In''t", Wedde = 170 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 6, 4), CampusId = andros.CampusId },
            new Docent { DocentId = 156, Voornaam = "Marce l", Familienaam = "Janssens", Wedde = 1200, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 5), CampusI d = delos.CampusId },
            new Docent { DocentId = 157, Voornaam = "Benja min", Familienaam = "Javaux", Wedde = 1600, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 6, 6), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 158, Voornaam = "Karel ", Familienaam = " Kaers", Wedde = 200 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 6, 7), CampusId = ikaria.CampusId },
            new Docent { DocentId = 159, Voornaam = "Franc is", Familienaam = "Kemplaire", Wedde = 1500, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 6, 8), Campu sId = ikaria.Campu sId },
            new Docent { DocentId = 160, Voornaam = "Norbe rt", Familienaam = "Kerckhove", Wedde = 1900, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 6, 9), Camp usId = gavdos.Camp usId },
            new Docent { DocentId = 161, Voornaam = "Désir é", Familienaam = "Keteleer", Wedde = 1600, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 10), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 162, Voornaam = "Marce l", Familienaam = "Kint", Wedde = 110 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 11), CampusId = andros.CampusId },
            new Docent { DocentId = 163, Voornaam = "Firmi n", Familienaam = "Lambot", Wedde = 1 500, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 12), CampusI d = ikaria.CampusI d },
            new Docent { DocentId = 164, Voornaam = "Roger ", Familienaam = " Lambrecht", Wedde = 1900, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 13), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 165, Voornaam = "Eric", Familienaam = "L eman", Wedde = 1400, HeeftRijbewijs = true, InDienst = n ew DateTime(2019, 6, 14), CampusId = hydra.CampusId },
            new Docent { DocentId = 166, Voornaam = "Camil le", Familienaam = "Leroy", Wedde = 1 800, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 15), CampusI d = delos.CampusId },
            new Docent { DocentId = 167, Voornaam = "Rolan d", Familienaam = "Liboton", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 6, 16), CampusI d = delos.CampusId },
            new Docent { DocentId = 168, Voornaam = "Jules ", Familienaam = " Lowie", Wedde = 100 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 17), CampusId = ikaria.CampusId },
            new Docent { DocentId = 169, Voornaam = "André ", Familienaam = " Lurquin", Wedde = 1 400, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 6, 18), CampusI d = hydra.CampusId },
            new Docent { DocentId = 170, Voornaam = "Henri ", Familienaam = " Rik", Wedde = 1800, HeeftRijbewijs = null, InDienst = ne w DateTime(2019, 6, 19), CampusId = d elos.CampusId },
            new Docent { DocentId = 171, Voornaam = "Pierr ot", Familienaam = "Machiels", Wedde = 1300, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 6, 20), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 172, Voornaam = "André ", Familienaam = " Maelbrancke", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 6, 21), Cam pusId = andros.Cam pusId },
            new Docent { DocentId = 173, Voornaam = "Fredd y", Familienaam = "Maertens", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 22), Campus Id = andros.Campus Id },
            new Docent { DocentId = 174, Voornaam = "Romai n", Familienaam = "Maes", Wedde = 160 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 23), CampusId = oinouses.CampusId },
            new Docent { DocentId = 175, Voornaam = "Sylvè re", Familienaam = "Maes", Wedde = 13 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 6, 24), CampusId = gavdos.CampusId },
            new Docent { DocentId = 176, Voornaam = "Josep h", Familienaam = "Marchand", Wedde = 1700, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 6, 25), Campus Id = andros.Campus Id },
            new Docent { DocentId = 177, Voornaam = "René", Familienaam = "M artens", Wedde = 12 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 6, 26), CampusId = delos.CampusId },
            new Docent { DocentId = 178, Voornaam = "Jacqu es", Familienaam = "Martin", Wedde = 1600, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 6, 27), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 179, Voornaam = "Emile ", Familienaam = " père", Wedde = 2000, HeeftRijbewijs = null, InDienst = n ew DateTime(2019, 6, 28), CampusId = ikaria.CampusId },
            new Docent { DocentId = 180, Voornaam = "Flore nt", Familienaam = "Mathieu", Wedde = 1500, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 6, 29), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 181, Voornaam = "Nico", Familienaam = "M attan", Wedde = 190 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 6, 30), CampusId = gavdos.CampusId },
            new Docent { DocentId = 182, Voornaam = "Filip ", Familienaam = " Meirhaeghe", Wedde = 1600, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 7, 1), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 183, Voornaam = "Axel", Familienaam = "M erckx", Wedde = 110 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 7, 2), CampusId = andros.CampusId },
            new Docent { DocentId = 184, Voornaam = "Eddy", Familienaam = "M erckx", Wedde = 150 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 3), CampusId = ikaria.CampusId },
            new Docent { DocentId = 185, Voornaam = "André ", Familienaam = " Messelis", Wedde = 1900, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 4), CampusId = gavdos.CampusId },
            new Docent { DocentId = 186, Voornaam = "Mauri ce", Familienaam = "Meuleman", Wedde = 1400, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 7, 5), Campus Id = hydra.CampusI d },
            new Docent { DocentId = 187, Voornaam = "Eloi", Familienaam = "M eulenberg", Wedde = 1800, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 7, 6), Campus Id = delos.CampusI d },
            new Docent { DocentId = 188, Voornaam = "Frans ", Familienaam = " Mintjens", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 7), CampusId = delos.CampusId },
            new Docent { DocentId = 189, Voornaam = "Yvo", Familienaam = "Mo lenaers", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 8), CampusId = ikaria.CampusId },
            new Docent { DocentId = 190, Voornaam = "Mauri ce", Familienaam = "Mollin", Wedde = 1400, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 7, 9), CampusI d = hydra.CampusId },
            new Docent { DocentId = 191, Voornaam = "Arthu r", Familienaam = "Mommerency", Wedde = 1800, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 7, 10), Camp usId = delos.Campu sId },
            new Docent { DocentId = 192, Voornaam = "Jean-Pierre", Familiena am = "Monséré", Wed de = 1300, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 7, 11), Ca mpusId = gavdos.Ca mpusId },
            new Docent { DocentId = 193, Voornaam = "Willy ", Familienaam = " Monty", Wedde = 170 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 12), CampusId = andros.CampusId },
            new Docent { DocentId = 194, Voornaam = "Sammi e", Familienaam = "Moreels", Wedde = 2100, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 13), CampusI d = andros.CampusI d },
            new Docent { DocentId = 195, Voornaam = "Alfre d", Familienaam = "Mottard", Wedde = 1600, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 7, 14), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 196, Voornaam = "Ernes t", Familienaam = "Mottart", Wedde = 1300, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 7, 15), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 197, Voornaam = "Louis ", Familienaam = " Mottiat", Wedde = 1 700, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 7, 16), CampusId = andros.CampusId },
            new Docent { DocentId = 198, Voornaam = "Johan ", Familienaam = " Museeuw", Wedde = 1 200, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 17), CampusId = delos.CampusId },
            new Docent { DocentId = 199, Voornaam = "Wilfr ied", Familienaam = "Nelissen", Wedde = 1600, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 7, 18), Cam pusId = oinouses.C ampusId },
            new Docent { DocentId = 200, Voornaam = "Franç ois", Familienaam = "Neuville", Wedde = 2000, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 7, 19), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 201, Voornaam = "André ", Familienaam = " Noyelle", Wedde = 1 500, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 20), CampusId = ikaria.CampusId },
            new Docent { DocentId = 202, Voornaam = "Guy", Familienaam = "Nu lens", Wedde = 1900, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 21), CampusId = gavdos.CampusId },
            new Docent { DocentId = 203, Voornaam = "Nick", Familienaam = "N uyens", Wedde = 160 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 7, 22), CampusId = oinouses.CampusId },
            new Docent { DocentId = 204, Voornaam = "Sven", Familienaam = "N ys", Wedde = 1100, HeeftRijbewijs = t rue, InDienst = new DateTime(2019, 7, 23), CampusId = an dros.CampusId },
            new Docent { DocentId = 205, Voornaam = "Stan", Familienaam = "O ckers", Wedde = 150 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 7, 24), CampusId = ikaria.CampusId },
            new Docent { DocentId = 206, Voornaam = "Petru s", Familienaam = "Oellibrandt", Wedd e = 1900, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 7, 25), Cam pusId = gavdos.Cam pusId },
            new Docent { DocentId = 207, Voornaam = "Valèr e", Familienaam = "Ollivier", Wedde = 1400, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 7, 26), Campus Id = hydra.CampusI d },
            new Docent { DocentId = 208, Voornaam = "Eddy", Familienaam = "P eelman", Wedde = 18 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 7, 27), CampusId = delos.CampusId },
            new Docent { DocentId = 209, Voornaam = "Edwar d", Familienaam = "Peeters", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 7, 28), CampusI d = delos.CampusId },
            new Docent { DocentId = 210, Voornaam = "Luc", Familienaam = "Pe titjean", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 7, 29), CampusId = ikaria.CampusId },
            new Docent { DocentId = 211, Voornaam = "Victo r", Familienaam = "Louis", Wedde = 14 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 7, 30), CampusId = hydra.CampusId },
            new Docent { DocentId = 212, Voornaam = "Georg es", Familienaam = "Pintens", Wedde = 1800, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 7, 31), Campus Id = delos.CampusI d },
            new Docent { DocentId = 213, Voornaam = "Théod ore", Familienaam = "Pirmez", Wedde = 1300, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 8, 1), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 214, Voornaam = "Eddy", Familienaam = "P lanckaert", Wedde = 1700, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 2), Campus Id = andros.Campus Id },
            new Docent { DocentId = 215, Voornaam = "Jo", Familienaam = "Pla nckaert", Wedde = 2 100, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 8, 3), CampusId = andros.CampusId },
            new Docent { DocentId = 216, Voornaam = "Walte r", Familienaam = "Planckaert", Wedde = 1600, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 8, 4), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 217, Voornaam = "Willy ", Familienaam = " Planckaert", Wedde = 1300, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 8, 5), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 218, Voornaam = "Miche l", Familienaam = "Pollentier", Wedde = 1700, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 8, 6), Campu sId = andros.Campu sId },
            new Docent { DocentId = 219, Voornaam = "Léon", Familienaam = "P oncelet", Wedde = 1 200, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 7), CampusId = delos.CampusId },
            new Docent { DocentId = 220, Voornaam = "Louis ", Familienaam = " Proost", Wedde = 16 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 8, 8), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 221, Voornaam = "Rober t", Familienaam = "Protin", Wedde = 2 000, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 8, 9), CampusId = ikaria.CampusId },
            new Docent { DocentId = 222, Voornaam = "Alber t", Familienaam = "Ramon", Wedde = 15 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 10), CampusId = ikaria.CampusId },
            new Docent { DocentId = 223, Voornaam = "Gasto n", Familienaam = "Rebry", Wedde = 19 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 8, 11), CampusId = gavdos.CampusId },
            new Docent { DocentId = 224, Voornaam = "Jens", Familienaam = "R enders", Wedde = 16 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 8, 12), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 225, Voornaam = "Guido ", Familienaam = " Reybrouck", Wedde = 1100, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 8, 13), Campus Id = andros.Campus Id },
            new Docent { DocentId = 226, Voornaam = "Marce l", Familienaam = "Rijckaert", Wedde = 1500, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 8, 14), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 227, Voornaam = "Alber t", Familienaam = "Ritserveldt", Wedd e = 1900, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 8, 15), Cam pusId = gavdos.Cam pusId },
            new Docent { DocentId = 228, Voornaam = "Bert", Familienaam = "R oesems", Wedde = 14 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 16), CampusId = hydra.CampusId },
            new Docent { DocentId = 229, Voornaam = "Louis ", Familienaam = " Rolus", Wedde = 180 0, HeeftRijbewijs = false, InDienst = new DateTime(2019, 8, 17), CampusId = delos.CampusId },
            new Docent { DocentId = 230, Voornaam = "Georg es", Familienaam = "Ronsse", Wedde = 2200, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 8, 18), CampusI d = delos.CampusId },
            new Docent { DocentId = 231, Voornaam = "André ", Familienaam = " Rosseel", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 19), CampusId = ikaria.CampusId },
            new Docent { DocentId = 232, Voornaam = "Félic ien", Familienaam = "Salmon", Wedde = 1400, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 20), Campu sId = hydra.Campus Id },
            new Docent { DocentId = 233, Voornaam = "Léopo ld", Familienaam = "Schaeken", Wedde = 1800, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 8, 21), Campu sId = delos.Campus Id },
            new Docent { DocentId = 234, Voornaam = "Willy ", Familienaam = " Scheers", Wedde = 1 300, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 8, 22), CampusId = gavdos.CampusId },
            new Docent { DocentId = 235, Voornaam = "Alfon s", Familienaam = "Schepers", Wedde = 1700, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 8, 23), Campu sId = andros.Campu sId },
            new Docent { DocentId = 236, Voornaam = "Josep h", Familienaam = "Scherens", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 8, 24), Campus Id = andros.Campus Id },
            new Docent { DocentId = 237, Voornaam = "Jef", Familienaam = "Sc herens", Wedde = 1600, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 25), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 238, Voornaam = "Briek ", Familienaam = " Schotte", Wedde = 1 300, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 8, 26), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 239, Voornaam = "Frans ", Familienaam = " Schoubben", Wedde = 1700, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 8, 27), Campus Id = andros.Campus Id },
            new Docent { DocentId = 240, Voornaam = "Léon", Familienaam = "S cieur", Wedde = 120 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 28), CampusId = delos.CampusId },
            new Docent { DocentId = 241, Voornaam = "Félix ", Familienaam = " Sellier", Wedde = 1 600, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 8, 29), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 242, Voornaam = "Edwar d", Familienaam = "Sels", Wedde = 200 0, HeeftRijbewijs = null, InDienst = new DateTime(2019, 8, 30), CampusId = ikaria.CampusId },
            new Docent { DocentId = 243, Voornaam = "Alber t", Familienaam = "Sercu", Wedde = 15 00, HeeftRijbewijs = true, InDienst = new DateTime(2019, 8, 31), CampusId = ikaria.CampusId },
            new Docent { DocentId = 244, Voornaam = "Patri ck", Familienaam = "Sercu", Wedde = 1 900, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 9, 1), CampusId = gavdos.CampusId },
            new Docent { DocentId = 245, Voornaam = "Andy", Familienaam = "d e Smet", Wedde = 16 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 2), CampusId = oinouses.CampusId },
            new Docent { DocentId = 246, Voornaam = "Josep h", Familienaam = "Somers", Wedde = 1 100, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 3), CampusId = andros.CampusId },
            new Docent { DocentId = 247, Voornaam = "Tom", Familienaam = "St eels", Wedde = 1500, HeeftRijbewijs = false, InDienst = new DateTime(2019, 9, 4), CampusId = ikaria.CampusId },
            new Docent { DocentId = 248, Voornaam = "Ernes t", Familienaam = "Sterckx", Wedde = 1900, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 9, 5), CampusId = gavdos.CampusId },
            new Docent { DocentId = 249, Voornaam = "Lucie n", Familienaam = "Storme", Wedde = 1 400, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 6), CampusId = hydra.CampusId },
            new Docent { DocentId = 250, Voornaam = "Tom", Familienaam = "St ubbe", Wedde = 1800, HeeftRijbewijs = false, InDienst = new DateTime(2019, 9, 7), CampusId = delos.CampusId },
            new Docent { DocentId = 251, Voornaam = "Roger ", Familienaam = " Swerts", Wedde = 22 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 8), CampusId = delos.CampusId },
            new Docent { DocentId = 252, Voornaam = "Arthu r", Familienaam = "Targez", Wedde = 1 000, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 10), CampusId = ikaria.CampusId },
            new Docent { DocentId = 253, Voornaam = "Andre i", Familienaam = "Tchmil", Wedde = 1 400, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 9, 11), CampusI d = hydra.CampusId },
            new Docent { DocentId = 254, Voornaam = "Emman uel", Familienaam = "Thoma", Wedde = 1800, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 9, 12), CampusI d = delos.CampusId },
            new Docent { DocentId = 255, Voornaam = "Phili ppe", Familienaam = "Thys", Wedde = 1 300, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 9, 13), CampusId = gavdos.CampusId },
            new Docent { DocentId = 256, Voornaam = "Hecto r", Familienaam = "Tiberghien", Wedde = 1700, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 9, 14), Cam pusId = andros.Cam pusId },
            new Docent { DocentId = 257, Voornaam = "Léon", Familienaam = "T ommies", Wedde = 21 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 9, 15), CampusId = andros.CampusId },
            new Docent { DocentId = 258, Voornaam = "Lode", Familienaam = "T roonbeeckx", Wedde = 1600, HeeftRijbe wijs = true, InDien st = new DateTime(2019, 9, 16), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 259, Voornaam = "Greg", Familienaam = "V an Avermaet", Wedde = 1300, HeeftRijb ewijs = false, InDi enst = new DateTim e(2019, 9, 17), Cam pusId = gavdos.Cam pusId },
            new Docent { DocentId = 260, Voornaam = "Arman d", Familienaam = "Van Bruaene", Wedd e = 1700, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 9, 18), Cam pusId = andros.Cam pusId },
            new Docent { DocentId = 261, Voornaam = "Georg es", Familienaam = "Vanconingsloo", W edde = 1200, Heeft Rijbewijs = true, I nDienst = new Date Time(2019, 9, 19), CampusId = delos.C ampusId },
            new Docent { DocentId = 262, Voornaam = "Léon", Familienaam = "V an Daele", Wedde = 1600, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 9, 20), Campus Id = oinouses.Camp usId },
            new Docent { DocentId = 263, Voornaam = "Charl es", Familienaam = "Van Den Born", We dde = 2000, HeeftR ijbewijs = null, In Dienst = new DateT ime(2019, 9, 21), C ampusId = ikaria.C ampusId },
            new Docent { DocentId = 264, Voornaam = "Frank ", Familienaam = " Vandenbroucke", Wed de = 1500, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 9, 22), Ca mpusId = ikaria.Ca mpusId },
            new Docent { DocentId = 265, Voornaam = "Odiel ", Familienaam = " Vanden Meerschaut", Wedde = 1900, Hee ftRijbewijs = false, InDienst = new D ateTime(2019, 9, 23), CampusId = gavd os.CampusId },
            new Docent { DocentId = 266, Voornaam = "Eric", Familienaam = "V anderaerden", Wedde = 1600, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 9, 24), Camp usId = oinouses.Ca mpusId },
            new Docent { DocentId = 267, Voornaam = "Kurt", Familienaam = "V an de Wouwer", Wedd e = 1100, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 9, 25), Cam pusId = andros.Cam pusId },
            new Docent { DocentId = 268, Voornaam = "Richa rd", Familienaam = "Van Genechten", W edde = 1500, Heeft Rijbewijs = false, InDienst = new Dat eTime(2019, 9, 26), CampusId = ikaria.CampusId },
            new Docent { DocentId = 269, Voornaam = "Marti n", Familienaam = "Van Geneugden", We dde = 1900, HeeftR ijbewijs = null, In Dienst = new DateT ime(2019, 9, 27), C ampusId = gavdos.C ampusId },
            new Docent { DocentId = 270, Voornaam = "Cyril le", Familienaam = "Van Hauwaert", We dde = 1400, HeeftR ijbewijs = true, In Dienst = new DateT ime(2019, 9, 28), C ampusId = hydra.Ca mpusId },
            new Docent { DocentId = 271, Voornaam = "Mauri ce", Familienaam = "Van Herzele", Wed de = 1800, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 9, 30), C ampusId = delos.Ca mpusId },
            new Docent { DocentId = 272, Voornaam = "Jules ", Familienaam = " Van Hevel", Wedde = 2200, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 1), Campus Id = delos.CampusI d },
            new Docent { DocentId = 273, Voornaam = "Edwig ", Familienaam = " Van Hooydonck", Wed de = 1000, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 10, 2), Ca mpusId = ikaria.Ca mpusId },
            new Docent { DocentId = 274, Voornaam = "Lucie n", Familienaam = "Van Impe", Wedde = 1400, HeeftRijbew ijs = false, InDien st = new DateTime(2019, 10, 3), Campu sId = hydra.Campus Id },
            new Docent { DocentId = 275, Voornaam = "Henri ", Familienaam = " Van Kerkhove", Wedd e = 1800, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 10, 4), Cam pusId = delos.Camp usId },
             new Docent { DocentId = 276, Voornaam = "Rik", Familienaam = "Va n Linden", Wedde = 1300, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 5), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 277, Voornaam = "Rik", Familienaam = "Va n Looy", Wedde = 17 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 10, 6), CampusId = andros.CampusId },
            new Docent { DocentId = 278, Voornaam = "Willy ", Familienaam = " Vannitsen", Wedde = 2100, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 7), Campus Id = andros.Campus Id },
            new Docent { DocentId = 279, Voornaam = "Peter ", Familienaam = " Van Petegem", Wedde = 1600, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 10, 8), Camp usId = oinouses.Ca mpusId },
            new Docent { DocentId = 280, Voornaam = "Peter ", Familienaam = " Van Santvliet", Wed de = 1300, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 10, 9), C ampusId = gavdos.C ampusId },
            new Docent { DocentId = 281, Voornaam = "Victo r", Familienaam = "Van Schil", Wedde = 1700, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 10, 10), Camp usId = andros.Camp usId },
            new Docent { DocentId = 282, Voornaam = "Herma n", Familienaam = "van Springel", Wed de = 1200, HeeftRi jbewijs = true, InD ienst = new DateTi me(2019, 10, 11), C ampusId = delos.Ca mpusId },
            new Docent { DocentId = 283, Voornaam = "Rik", Familienaam = "Va n Steenbergen", Wed de = 1600, HeeftRi jbewijs = false, In Dienst = new DateT ime(2019, 10, 12), CampusId = oinouse s.CampusId },
            new Docent { DocentId = 284, Voornaam = "Guill aume", Familienaam = "Van Tongerloo", Wedde = 2000, Hee ftRijbewijs = null, InDienst = new Da teTime(2019, 10, 13), CampusId = ikar ia.CampusId },
            new Docent { DocentId = 285, Voornaam = "Noël", Familienaam = "V antyghem", Wedde = 1500, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 14), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 286, Voornaam = "Rik", Familienaam = "Ve rbrugghe", Wedde = 1900, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 10, 15), Campu sId = gavdos.Campu sId },
            new Docent { DocentId = 287, Voornaam = "Augus te", Familienaam = "Verdyck", Wedde = 1600, HeeftRijbew ijs = null, InDiens t = new DateTime(2 019, 10, 16), Campu sId = oinouses.Cam pusId },
            new Docent { DocentId = 288, Voornaam = "Jozef ", Familienaam = " Verhaert", Wedde = 1100, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 10, 17), Campus Id = andros.Campus Id },
            new Docent { DocentId = 289, Voornaam = "René", Familienaam = "V ermandel", Wedde = 1500, HeeftRijbewi js = false, InDiens t = new DateTime(2 019, 10, 18), Campu sId = ikaria.Campu sId },
            new Docent { DocentId = 290, Voornaam = "Stive ", Familienaam = " Vermaut", Wedde = 1 900, HeeftRijbewij s = null, InDienst = new DateTime(201 9, 10, 19), CampusI d = gavdos.CampusI d },
            new Docent { DocentId = 291, Voornaam = "Adolf ", Familienaam = " Verschueren", Wedde = 1400, HeeftRijb ewijs = true, InDie nst = new DateTime(2019, 10, 20), Cam pusId = hydra.Camp usId },
            new Docent { DocentId = 292, Voornaam = "Const ant", Familienaam = "Verschueren", We dde = 1800, HeeftR ijbewijs = false, I nDienst = new Date Time(2019, 10, 21), CampusId = delos.CampusId },
            new Docent { DocentId = 293, Voornaam = "Johan ", Familienaam = " Verstrepen", Wedde = 2200, HeeftRijbe wijs = null, InDien st = new DateTime(2019, 10, 22), Camp usId = delos.Campu sId },
            new Docent { DocentId = 294, Voornaam = "Félic ien", Familienaam = "Vervaecke", Wedd e = 1000, HeeftRij bewijs = true, InDi enst = new DateTim e(2019, 10, 23), Ca mpusId = ikaria.Ca mpusId },
            new Docent { DocentId = 295, Voornaam = "Julie n", Familienaam = "Vervaecke", Wedde = 1400, HeeftRijbe wijs = false, InDie nst = new DateTime(2019, 10, 24), Cam pusId = hydra.Camp usId },
            new Docent { DocentId = 296, Voornaam = "Edwar d", Familienaam = "Vissers", Wedde = 1800, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 10, 25), Campus Id = delos.CampusI d },
            new Docent { DocentId = 297, Voornaam = "Lucie n", Familienaam = "Vlaemynck", Wedde = 1300, HeeftRijbe wijs = true, InDienst = new DateTime(2019, 10, 26), Camp usId = gavdos.Camp usId },
            new Docent { DocentId = 298, Voornaam = "André ", Familienaam = " Vlaeyen", Wedde = 1 700, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 10, 27), Campus Id = andros.Campus Id },
            new Docent { DocentId = 299, Voornaam = "Jean", Familienaam = "V liegen", Wedde = 21 00, HeeftRijbewijs = null, InDienst = new DateTime(2019, 10, 28), CampusId = andros.CampusId },
            new Docent { DocentId = 300, Voornaam = "Luc", Familienaam = "Wa llays", Wedde = 160 0, HeeftRijbewijs = true, InDienst = new DateTime(2019, 10, 29), CampusId = oinouses.CampusI d },
            new Docent { DocentId = 301, Voornaam = "René", Familienaam = "W alschot", Wedde = 1 300, HeeftRijbewij s = false, InDienst = new DateTime(20 19, 10, 30), Campus Id = gavdos.Campus Id },
            new Docent { DocentId = 302, Voornaam = "Jean-Marie", Familienaa m = "Wampers", Wedd e = 1700, HeeftRij bewijs = null, InDi enst = new DateTim e(2019, 10, 31), Ca mpusId = andros.Ca mpusId },
            new Docent { DocentId = 303, Voornaam = "Rober t", Familienaam = "Wancour", Wedde = 1200, HeeftRijbewi js = true, InDienst = new DateTime(20 19, 11, 1), CampusI d = delos.CampusId },
            new Docent { DocentId = 304, Voornaam = "Bart", Familienaam = "W ellens", Wedde = 16 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 11, 2), CampusId = oinouses.Campus Id },
            new Docent { DocentId = 305, Voornaam = "Wilfr ied", Familienaam = "Wesemael", Wedde = 2000, HeeftRijb ewijs = null, InDie nst = new DateTime(2019, 11, 3), Camp usId = ikaria.Camp usId },
            new Docent { DocentId = 306, Voornaam = "Woute r", Familienaam = "Weylandt", Wedde = 1500, HeeftRijbew ijs = true, InDiens t = new DateTime(2 019, 11, 4), Campus Id = ikaria.Campus Id },
            new Docent { DocentId = 307, Voornaam = "Marc", Familienaam = "W auters", Wedde = 19 00, HeeftRijbewijs = false, InDienst = new DateTime(201 9, 11, 5), CampusId = gavdos.CampusId },
            new Docent { DocentId = 308, Voornaam = "Danie l", Familienaam = "Willems", Wedde = 1600, HeeftRijbewi js = null, InDienst = new DateTime(20 19, 11, 6), CampusI d = oinouses.Campu sId },
            new Docent { DocentId = 309, Voornaam = "Jozef ", Familienaam = " Wouters", Wedde = 1 100, HeeftRijbewij s = true, InDienst = new DateTime(201 9, 11, 7), CampusId = andros.CampusId }
            );
        }
    }
}
