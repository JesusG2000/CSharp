using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.dto;
using Music.dao;
using System.Security.Cryptography;
using System.IO;
using Music.db;

namespace Music.service.impl
{
    class UserService : IUserService
    {
        private IUserDao userDao = DaoFactory.getInstance().GetUserDao();
        private const string strPermutation = "ouiveyxaqtd";
        private const int bytePermutation1 = 0x19;
        private const int bytePermutation2 = 0x59;
        private const int bytePermutation3 = 0x17;
        private const int bytePermutation4 = 0x41;
        public void create(DUser t)
        {

            t.Password = Encrypt(t.Password);
            userDao.create(t);
        }



        public void delete(DUser t)
        {
            userDao.delete(t);
        }



        public List<DUser> getAllUser()
        {
            throw new NotImplementedException();
        }

        public bool isExist(DUser user)
        {
            user.Password = Encrypt(user.Password);
            return userDao.isExist(user);
        }

        public bool isRegistered(DUser user)
        {

            return userDao.isRegistered(user);
        }

        public DUser readById(int id)
        {
            return userDao.readById(id);
        }

        public DUser update(int id, DUser t)
        {
            return userDao.update(id, t);
        }



        public DUser readByName(string name)
        {
            return userDao.readByLogin(name);
        }

        public List<DUser> getAllRegisterUser()
        {
            return userDao.getAllRegisterUser();
        }

        private string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));


        }


        // decoding
        private string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));


        }

        // encrypt
        private byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] {bytePermutation1,
                         bytePermutation2,
                        bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        private byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { bytePermutation1,
                         bytePermutation2,
                         bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
