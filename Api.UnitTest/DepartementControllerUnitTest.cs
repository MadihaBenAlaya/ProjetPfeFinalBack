using AppRecrutement.Controllers;
using AppRecrutement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTest
{
    public class DepartementControllerUnitTest
    {
        private readonly Departement _departement;
        private readonly AuthenticationContext _context;
        public Guid Id;

        #region public Methods
        [Fact]
        public void ShouldReturnListOfExemple()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new DepartementController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetAllDepartements();
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple1()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new DepartementController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetDepartement(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple2()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new DepartementController(_context);


            //Act// //execute notre controlleur//

            var result = controller.AddDepartement(_departement);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple3()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new DepartementController(_context);


            //Act// //execute notre controlleur//

            var result = controller.PutDepartement(Id, _departement);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple4()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new DepartementController(_context);


            //Act// //execute notre controlleur//

            var result = controller.DeleteDepartement(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }


        #endregion
    }
}
