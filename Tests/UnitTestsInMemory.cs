using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities;
using Model.Repositories;
using Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class UnitTestsInMemory
    {
        [TestMethod]
        public void GetDocentenVoorCampus_Docenten_AantalIsZesDocenten()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<EFOpleidingenContext>()
                                        .UseInMemoryDatabase("InMemoryDatabase")
                                        .Options;
            using var context = new EFOpleidingenContext(options);
            
            // Toevoegen campussen
            context.Campussen.Add(new Campus()
            {
                CampusId = 1,
                Naam = "Andros",
                Straat = "Somersstraat",
                Huisnummer = "22",
                Postcode = "2018",
                Gemeente = "Antwerpen"
            });
            context.Campussen.Add(new Campus()
            {
                CampusId = 2,
                Naam = "Delos",
                Straat = "Oude Vest",
                Huisnummer = "17",
                Postcode = "9200",
                Gemeente = "Dendermonde"
            });
            // Toevoegen docenten
            context.Docenten.Add(new Docent()
            {
                DocentId = 001,
                Voornaam = "Willy",
                Familienaam = "Abbeloos",
                Wedde = 1500m,
                HeeftRijbewijs = new Nullable<bool>(),
                InDienst = new DateTime(2019, 1, 1),
                CampusId = 1
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 002,
                Voornaam = "Joseph",
                Familienaam = "Abelshausen",
                Wedde = 1800m,
                HeeftRijbewijs = true,
                InDienst = new DateTime(2019, 1, 2),
                CampusId = 2
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 003,
                Voornaam = "Joseph",
                Familienaam = "Achten",
                Wedde = 1300m,
                HeeftRijbewijs = false,
                InDienst = new DateTime(2019, 1, 3),
                CampusId = 1
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 004,
                Voornaam = "François",
                Familienaam = "Adam",
                Wedde = 1700m,
                HeeftRijbewijs = new Nullable<bool>(),
                InDienst = new DateTime(2019, 1, 4),
                CampusId = 2
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 005,
                Voornaam = "Jan",
                Familienaam = "Adriaensens",
                Wedde = 2100m,
                HeeftRijbewijs = true,
                InDienst = new DateTime(2019, 1, 5),
                CampusId = 1
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 006,
                Voornaam = "René",
                Familienaam = "Adriaensens",
                Wedde = 1600m,
                HeeftRijbewijs = false,
                InDienst = new DateTime(2019, 1, 6),
                CampusId = 2
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 007,
                Voornaam = "Frans",
                Familienaam = "Aerenhouts",
                Wedde = 1300m,
                HeeftRijbewijs = new Nullable<bool>(),
                InDienst = new DateTime(2019, 1, 7),
                CampusId = 1
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 008,
                Voornaam = "Emile",
                Familienaam = "Aerts",
                Wedde = 1700m,
                HeeftRijbewijs = true,
                InDienst = new DateTime(2019, 1, 8),
                CampusId = 1
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 009,
                Voornaam = "Jean",
                Familienaam = "Aerts",
                Wedde = 1200m,
                HeeftRijbewijs = false,
                InDienst = new DateTime(2019, 1, 9),
                CampusId = 3 // Onbekende campus !
            });
            context.Docenten.Add(new Docent()
            {
                DocentId = 010,
                Voornaam = "Mario",
                Familienaam = "Aerts",
                Wedde = 1600m,
                HeeftRijbewijs = new Nullable<bool>(),
                InDienst = new DateTime(2019, 1, 10),
                CampusId = 1
            });
            context.SaveChanges();
            var docentService = new DocentService(context);

            //Act
            var docenten = docentService.GetDocentenVoorCampus(1);

            //Assert
            Assert.AreEqual(6, docenten.Count());
        }
    }
}
