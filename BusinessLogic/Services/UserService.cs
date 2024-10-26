//using DAL.Data;
//using DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogic.Services
//{
//    public class UserService
//    {
//        private readonly BudgetBaeContext _context;
        
//        public UserService(BudgetBaeContext context)
//        {
//            _context = context;
//        }

//        public async Task<bool> RegisterUserAsync(string email, string name, string password)
//        {
//            if (await _context.Users.AnyAsync(u => u.Email == email))
//            {
//                throw new Exception("Користувач з такою електронною поштою вже існує!");
//            }

//            var user = new User
//            {
//                Email = email,
//                Name = name,
//                Password = password
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            return true;
//        }
//    }
//}
