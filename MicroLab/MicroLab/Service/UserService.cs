using MicroLab.Data;
using MicroLab.Dto;
using MicroLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Service
{
    public static class UserService
    {
        static readonly Context db = new Context();
        public static BasiliskResponse<UserDto> Login(UserDto userDto)
        {

            try
            {
                var u = db.Users.Where(s => !s.Deleted && s.Username==userDto.Username).ToList();
                if (u.Count!=1)
                {
                    return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="کاربری با این نام پیدا نشد!!" };
                }
                if (u.First().Password!=userDto.Password)
                {
                    return new BasiliskResponse<UserDto> { Result = null, Success = false, Text = "رمز عبور اشتباه است!!" };
                }
                var res = u.First();
                var r = new UserDto
                {
                    Id=res.Id,
                    Credit=res.Credit,
                    IsAdmin=res.IsAdmin,
                    Password=res.Password,
                    Username=res.Username,
                };
                return new BasiliskResponse<UserDto> { Text="کاربر با موفقیت وارد شد!!",Success=true,Result=r};
            }
            catch (Exception)
            {
                return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="خطایی ایجاد شد با پشتیبانی تماس بگیرید" };
            }
        }
        public static BasiliskResponse<UserDto> SignUp(UserDto userDto)
        {

            try
            {
                var u = db.Users.Where(s => !s.Deleted && s.Username==userDto.Username).ToList();
                if (u.Count!=0)
                {
                    return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="کاربر دیگری با این نام وجود دارد!!" };
                }
                var r = new User
                {
                    Credit=0,
                    IsAdmin=false,
                    Password=userDto.Password,
                    Username=userDto.Username,
                    CreateDate=DateTime.Now,
                    Deleted=false,
                };
                db.Users.Add(r);
                db.SaveChanges();

                var res = new UserDto
                {
                    Id = r.Id,
                    Credit = r.Credit,
                    IsAdmin = r.IsAdmin,
                    Password = r.Password,
                    Username = r.Username,
                };
                return new BasiliskResponse<UserDto> { Text="کاربر با موفقیت ثبت نام کرد!!",Success=true,Result=res};
            }
            catch (Exception)
            {
                return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="خطایی ایجاد شد با پشتیبانی تماس بگیرید" };
            }
        }
        public static BasiliskResponse<UserDto> GetUser(object userIds)
        {

            try
            {

                if (userIds == null)
                {
                    return new BasiliskResponse<UserDto> { Text = "کاربر احراز نشد!!", Success = true, Result = null };
                }
                int userId = Convert.ToInt32(userIds);
                var u = db.Users.Where(s => !s.Deleted && s.Id==userId).ToList();
                if (u.Count!=1)
                {
                    return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="خطا. با پشتیبانی تماس بگیرید" };
                }
                var user = u.First();
                var userdto = new UserDto
                {
                    CreateDate = user.CreateDate,
                    Credit = user.Credit,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Username = user.Username,
                    Password = user.Password,
                };
                return new BasiliskResponse<UserDto> { Text= "کاربر با موفقیت احراز شد!!", Success=true,Result=userdto};
            }
            catch (Exception)
            {
                return new BasiliskResponse<UserDto> {Result=null,Success=false,Text="خطایی ایجاد شد با پشتیبانی تماس بگیرید" };
            }
        }
        public static BasiliskResponse<bool?> AddCredit(object userIds, int value)
        {

            try
            {

                if (userIds == null)
                {
                    return new BasiliskResponse<bool?> { Text = "کاربر احراز نشد!!", Success = true, Result = null };
                }
                int userId = Convert.ToInt32(userIds);
                var u = db.Users.Where(s => !s.Deleted && s.Id==userId).ToList();
                if (u.Count!=1)
                {
                    return new BasiliskResponse<bool?> {Result=null,Success=false,Text="خطا. با پشتیبانی تماس بگیرید" };
                }
                var user = u.First();
                user.Credit += value;
                db.SaveChanges();

                return new BasiliskResponse<bool?> { Text= "کاربر با موفقیت احراز شد!!", Success=true,Result=true};
            }
            catch (Exception)
            {
                return new BasiliskResponse<bool?> {Result=null,Success=false,Text="خطایی ایجاد شد با پشتیبانی تماس بگیرید" };
            }
        }

        public static BasiliskResponse<int?> Authenticate(object userIds)
        {

            try
            {

                if (userIds == null)
                {
                    return new BasiliskResponse<int?> { Text = "کاربر احراز نشد!!", Success = true, Result = 0 };
                }
                int userId = Convert.ToInt32(userIds);
                var u = db.Users.Where(s => !s.Deleted && s.Id==userId).ToList();
                if (u.Count!=1)
                {
                    return new BasiliskResponse<int?> {Result=null,Success=false,Text="خطا. با پشتیبانی تماس بگیرید" };
                }
                if (u.First().IsAdmin)
                {
                    return new BasiliskResponse<int?> { Text = "کاربر با موفقیت احراز شد!!", Success = true, Result = 2 };

                }
                return new BasiliskResponse<int?> { Text= "کاربر با موفقیت احراز شد!!", Success=true,Result=1};
            }
            catch (Exception)
            {
                return new BasiliskResponse<int?> {Result=null,Success=false,Text="خطایی ایجاد شد با پشتیبانی تماس بگیرید" };
            }
        }
    }
}