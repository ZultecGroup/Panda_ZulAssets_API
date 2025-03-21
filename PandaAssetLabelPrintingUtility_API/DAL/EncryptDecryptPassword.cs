﻿using System.Security.Cryptography;
using System.Text;

namespace PandaAssetLabelPrintingUtility_API.DAL
{
    public class EncryptDecryptPassword
    {

        #region "DecryptQueryString"

        internal static string DecryptQueryString(string strQueryString)
        {
            EncryptDecryptPassword objEDQueryString = new EncryptDecryptPassword();
            return Decrypt(strQueryString, "r0b1nr0y");
        }

        #endregion

        #region "EncryptQueryString"

        internal static string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptPassword objEDQueryString = new EncryptDecryptPassword();
            return Encrypt(strQueryString, "r0b1nr0y");
        }

        #endregion

        #region "Encrypt"
        public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #endregion

        #region "Decrypt"
        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #endregion

        #region Keys

        public static byte[] key = { };

        public static byte[] IV = {
            0x12,
            0x34,
            0x56,
            0x78,
            0x90,
            0xab,
            0xcd,
            0xef

        };

        #endregion

    }
}
