﻿using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_demam { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public CovidConfig()
    {
        satuan_suhu = "celcius";
        batas_hari_demam = 14;
        pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
    }

    public void ReadFile(string path)
    {
        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            CovidConfig config = JsonSerializer.Deserialize<CovidConfig>(jsonText);
            this.satuan_suhu = config.satuan_suhu;
            this.batas_hari_demam = config.batas_hari_demam;
            this.pesan_ditolak = config.pesan_ditolak;
            this.pesan_diterima = config.pesan_diterima;
        }
        else
        {
            Console.WriteLine("File config tidak ditemukan.");
        }
    }

    public void WriteFIle(string path)
    {
        string jsonText = JsonSerializer.Serialize(this);
        File.WriteAllText(path, jsonText);
        Console.WriteLine("File config berhasil ditulis.");
    }

    public void ReadAndWriteConfigFile(string path)
    {
        if (File.Exists(path))
        {
            ReadFile(path);
        }
        else
        {
            WriteFIle(path);
            ReadFile(path);
        }
    }

    public void UbahSatuan()
    {
        switch (this.satuan_suhu)
        {
            case "celcius":
                this.satuan_suhu = "fahrenheit";
                break;
            case "fahrenheit":
                this.satuan_suhu = "celcius";
                break;
            default:
                break;
        }
    }

    public bool kondisiSuhu(double suhuBadan)
    {
        return (this.satuan_suhu == "celcius" && suhuBadan >= 36.5 && suhuBadan <= 37.5) ||
               (this.satuan_suhu == "fahrenheit" && suhuBadan >= 97.7 && suhuBadan <= 99.5);
    }

    public bool kondisiHariDemam(int hariDemam)
    {
        return hariDemam < this.batas_hari_demam;
    }

}