using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.User;
using Common;
namespace Service.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        StoreUserService userService = new StoreUserService();

        [TestMethod()]
        public void AddTest()
         {
            UserModifyDTO dto = new UserModifyDTO();
            dto.Address= string.Join(",", RandomUtils.GenerateChineseWords(50).ToArray()); 
            dto.Image= string.Join(",", RandomUtils.GenerateChineseWords(50).ToArray());
            dto.Email = RandomUtils.GetRandomEmail();
         
            dto.Mobile = RandomUtils.GetRandomTel();
            dto.PWD = "123456";
            dto.NickName= RandomUtils.GetRandomTel();
            dto.TrueName = string.Join(",", RandomUtils.GetNames(1));
            try
            {
                userService.Add(dto,out string Meg);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}