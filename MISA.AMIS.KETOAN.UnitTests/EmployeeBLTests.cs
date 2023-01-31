using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MISA.AMIS.KETOAN.DL;
using NSubstitute;

namespace MISA.AMIS.KETOAN.UnitTests
{
    public class EmployeeBLTests
    {
        /// <summary>
        /// Hàm test dữ liệu đầu vào không chính xác - EmployeeCode rỗng
        /// Author: PVLONG(16/01/2023)
        /// </summary>
        [Test]
        public void InsertRecord_EmptyEmpoyeeCode_ReturnsInputDataInvalidStatus()
        {
            //Arrange - Chuẩn bị tất cả tham số đầu vào
            Employee employee = new Employee();
            employee.EmployeeCode = "";
            employee.EmployeeName = "pvlong";
            employee.Email = "pvlong@gmail.com";
            employee.DepartmentID = new Guid("11452b0c-768e-5ff7-0d63-eeb1d8ed8cef");

            //Fake dữ liệu dựa trên NSAttribute 
            var expectedResult = new ServiceResponse()
            {
                Status = ServiceResponseStatus.InputDataInvalid,
            };
            var fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            var employeeBL = new EmployeeBL(fakeEmployeeDL);
   
            //Act - Gọi hàm cần test
            var actualResult = employeeBL.InsertRecord(employee);

            //Assert - Kiểm tra kết quả có đúng mong đợi không
            Assert.AreEqual(expectedResult.Status, actualResult.Status);
        }

        /// <summary>
        /// Hàm test dữ liệu đầu vào không chính xác - EmployeeName rỗng
        /// Author: PVLONG(16/01/2023)
        /// </summary>
        [Test]
        public void InsertRecord_EmptyEmployeeName_ReturnsInputDataInvalidStatus()
        {
            //Arrange - Chuẩn bị tất cả tham số đầu vào
            Employee employee = new Employee();
            employee.EmployeeCode = "NV00000";
            employee.EmployeeName = "";
            employee.Email = "pvlong@gmail.com";
            employee.DepartmentID = new Guid("11452b0c-768e-5ff7-0d63-eeb1d8ed8cef");

            //Fake dữ liệu dựa trên NSAttribute 
            var expectedResult = new ServiceResponse()
            {
                Status = ServiceResponseStatus.InputDataInvalid,
            };
            var fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            var employeeBL = new EmployeeBL(fakeEmployeeDL);

            //Act - Gọi hàm cần test
            var actualResult = employeeBL.InsertRecord(employee);

            //Assert - Kiểm tra kết quả có đúng mong đợi không
            Assert.AreEqual(expectedResult.Status, actualResult.Status);
        }

        /// <summary>
        /// Hàm test dữ liệu đầu vào không chính xác - DepartmentID rỗng
        /// Author: PVLONG(16/01/2023)
        /// </summary>
        [Test]
        public void InsertRecord_EmptyDepartmentID_ReturnsInputDataInvalidStatus()
        {
            //Arrange - Chuẩn bị tất cả tham số đầu vào
            Employee employee = new Employee();
            employee.EmployeeCode = "NV00000";
            employee.EmployeeName = "pvlong";
            employee.Email = "pvlong@gmail.com";

            //Fake dữ liệu dựa trên NSAttribute 
            var expectedResult = new ServiceResponse()
            {
                Status = ServiceResponseStatus.InputDataInvalid,
            };
            var fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            var employeeBL = new EmployeeBL(fakeEmployeeDL);

            //Act - Gọi hàm cần test
            var actualResult = employeeBL.InsertRecord(employee);

            //Assert - Kiểm tra kết quả có đúng mong đợi không
            Assert.AreEqual(expectedResult.Status, actualResult.Status);
        }

        /// <summary>
        /// Hàm test dữ liệu đầu vào không chính xác - EmployeeCode rỗng
        /// Author: PVLONG(16/01/2023)
        /// </summary>
        [Test]
        public void InsertRecord_DuplicateEmpoyeeCode_ReturnsDuplicateCodeStatus()
        {
            //Arrange - Chuẩn bị tất cả tham số đầu vào
            Employee employee = new Employee();
            employee.EmployeeCode = "NV00000";
            employee.EmployeeName = "pvlong";
            employee.Email = "pvlong@gmail.com";
            employee.DepartmentID = new Guid("11452b0c-768e-5ff7-0d63-eeb1d8ed8cef");

            //Fake dữ liệu dựa trên NSAttribute 
            var expectedResult = new ServiceResponse()
            {
                Status = ServiceResponseStatus.DuplicateCode,
            };
            var fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            var employeeBL = new EmployeeBL(fakeEmployeeDL);

            //Act - Gọi hàm cần test
            var actualResult = employeeBL.InsertRecord(employee);

            //Assert - Kiểm tra kết quả có đúng mong đợi không
            Assert.AreEqual(expectedResult.Status, actualResult.Status);
        }
    }
}