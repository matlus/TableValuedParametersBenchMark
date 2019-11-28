﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TableValuedParameterExample
{
    internal static class Randomizer
    {
        private static int[] s_punctuationACIICodes = { 33, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 58, 59, 60, 61, 62, 63, 64, 91, 92, 93, 94, 95, 123, 124, 125, 126 };


        private static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
        private static int GetRandomNumber()
        {
            var buffer = new byte[4];
            rngCrypto.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        private static uint GetRandomUintNumber()
        {
            var buffer = new byte[4];
            rngCrypto.GetBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static int[] GetRandomUniqueInts(int numberOfInts)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var hashset = new HashSet<int>();
                var randomInts = new int[numberOfInts];

                var generated = 0;
                do
                {
                    var number = GetRandomNumber();
                    if (!hashset.Contains(number))
                    {
                        randomInts[generated] = number;
                        hashset.Add(number);
                        generated++;
                    }
                }
                while (generated < numberOfInts);

                return randomInts;
            }
        }

        public static int GetRandomNumberBetween(int minValue, int maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }

            int diff = maxValue - minValue;
            while (true)
            {
                UInt32 rand = GetRandomUintNumber();

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public static T[] ShuffleArray<T>(T[] source)
        {
            var keyValuePairs = new List<KeyValuePair<int, T>>();
            var randomUniqueInts = GetRandomUniqueInts(source.Length);

            for (int i = 0; i < source.Length; i++)
            {
                keyValuePairs.Add(new KeyValuePair<int, T>(randomUniqueInts[i], source[i]));
            }

            return keyValuePairs.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "length-4")]
        public static string GetRandomAciiString(int length)
        {
            const int NUMERIC = 0;
            const int LOWERCASE = 1;
            const int UPPERCASE = 2;
            const int PUNCT = 3;
            var random = new Random(GetRandomNumber());

            StringBuilder sb = new StringBuilder();

            //ensure at least one of each type occurs 
            sb.Append(Convert.ToChar(random.Next(97, 122))); //lowercase
            sb.Append(Convert.ToChar(random.Next(65, 90))); //uppercase
            sb.Append(Convert.ToChar(random.Next(48, 57))); //numeric
            sb.Append(Convert.ToChar(s_punctuationACIICodes[random.Next(0, s_punctuationACIICodes.Length)])); //punctuation

            char ch;
            for (int i = 0; i < length - 4; i++)
            {
                int rnd = random.Next(0, 3);
                switch (rnd)
                {
                    case LOWERCASE: ch = Convert.ToChar(random.Next(97, 122)); break;
                    case UPPERCASE: ch = Convert.ToChar(random.Next(65, 90)); break;
                    case NUMERIC: ch = Convert.ToChar(random.Next(48, 57)); break;
                    case PUNCT: ch = Convert.ToChar(s_punctuationACIICodes[random.Next(0, s_punctuationACIICodes.Length)]); break;
                    default:
                        ch = Convert.ToChar(random.Next(97, 122)); break;
                }
                sb.Append(ch);
            }
            return sb.ToString();
        }

        public static string[] GenerateUniqueAsciiStrings(int length)
        {
            var count = 0;
            var hashSet = new HashSet<string>();
            do
            {
                if (hashSet.Add(GetRandomAciiString(20)))
                {
                    count++;
                }

            } while (count < length);

            return hashSet.ToArray();
        }

        public static string GetRandomUnicodeString(int length)
        {
            var random = new Random(GetRandomNumber());
            length = length * 2;

            byte[] str = new byte[length];

            for (int i = 0; i < length; i += 2)
            {
                int chr = random.Next(0xD7FF);
                str[i + 1] = (byte)((chr & 0xFF00) >> 8);
                str[i] = (byte)(chr & 0xFF);
            }

            return Encoding.Unicode.GetString(str);
        }
    }
}
