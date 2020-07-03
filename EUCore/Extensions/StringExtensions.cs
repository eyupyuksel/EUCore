using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EUCore.Extensions
{
    public static class StringExtensions
    {
        public static string MaskEmail(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            return Regex.Replace(text, pattern, m => new string('*', m.Length));
        }

        public static string MaskPhone(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var pattern = @"[0-9]{4}$";
            var regex = Regex.Replace(text, pattern, m => new string('*', m.Length));
            return regex.Remove(regex.Length - 1, 1) + text.Last();
        }

        public static string MaskName(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1})";
            return Regex.Replace(text, pattern, m => new string('*', m.Length));
        }

        private static string ClearDecimal(this decimal total)
        {
            var result = total.ToString();
            var sp = result.Split(",");
            if (sp.LastOrDefault()?.Length > 2)
                return $"{sp.FirstOrDefault()},{sp.LastOrDefault()?.Substring(0, 2)}";
            return result;
        }
        public static string ToCurrencyText(this string total, string kur, bool isKurussuz = false)
        {
            if (decimal.Parse(total) <= 0m)
                return "";
            if (isKurussuz)
            {
                total = total.Replace(".", "");
                total = total.Replace(",", ".");
                total = (Convert.ToDecimal(total.Replace(".", "")) / 100).ToString("0,0.00", CultureInfo.CurrentCulture);
            }
            string krtanim = kur == "TL" ? "KURUŞ" : "CENT";
            string[] birler = { " ", "BİR ", "İKİ ", "ÜÇ ", "DÖRT ", "BEŞ ", "ALTI ", "YEDİ ", "SEKİZ ", "DOKUZ " };
            string[] onlar = { " ", "ON ", "YİRMİ ", "OTUZ ", "KIRK ", "ELLİ ", "ALTMIŞ ", "YETMİŞ ", "SEKSEN ", "DOKSAN " };
            string[] binler = { "KATRİLYON ", "TRİLYON ", "MİLYAR ", "MİLYON ", "BİN ", " " };
            var grupSayisi = 6;
            var sonuc = "";
            var payda = total.Substring(total.Length - 2, 2);
            if (total.IndexOf("-", StringComparison.Ordinal) >= 0)
            {
                total = total.Replace("-", "");
            }

            if (total.Length > 3)
            {
                total = total.Substring(0, total.Length - 3).Replace(".", "");
            }
            total = total.PadLeft(grupSayisi * 3, '0');
            for (int i = 0; i < grupSayisi * 3; i += 3)
            {
                var grupDegeri = "";
                if (total.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(total.Substring(i, 1))] + "YÜZ "; //yüzler                
                if (grupDegeri.Trim() == "BİR YÜZ" || grupDegeri.Trim() == "BİR  YÜZ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ ";
                grupDegeri += onlar[Convert.ToInt32(total.Substring(i + 1, 1))]; //onlar
                grupDegeri += birler[Convert.ToInt32(total.Substring(i + 2, 1))]; //birler  
                if (grupDegeri.Trim() != "") //binler
                    grupDegeri += binler[i / 3];
                if (grupDegeri.Trim() == "BİR BİN" || grupDegeri.Trim() == "BİR  BİN") //birbin düzeltiliyor.
                    grupDegeri = "BİN ";
                sonuc += grupDegeri;
            }

            if (sonuc.Trim() != "")
                sonuc += $" {kur} ";
            var yaziUzunlugu = sonuc.Length;
            if (isKurussuz)
                return sonuc.Trim();
            if (payda.Substring(0, 1) != "0") //kuruş onlar
                sonuc += onlar[Convert.ToInt32(payda.Substring(0, 1))];
            if (payda.Substring(1, 1) != "0") //kuruş birler
                sonuc += birler[Convert.ToInt32(payda.Substring(1, 1))];
            if (sonuc.Length > yaziUzunlugu)
                sonuc += (" " + krtanim + " ");

            return sonuc.Trim();
        }
    }
}
