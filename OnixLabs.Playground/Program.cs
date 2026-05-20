// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using OnixLabs.Numerics;
using OnixLabs.Units;

namespace OnixLabs.Playground;

internal static class Program
{
    private static readonly (string Name, Action Demo)[] Demos =
    [
        ("AmountOfSubstance",   DemoAmountOfSubstance),
        ("Angle",               DemoAngle),
        ("Area",                DemoArea),
        ("Current",             DemoCurrent),
        ("DataSize",            DemoDataSize),
        ("Distance",            DemoDistance),
        ("Frequency",           DemoFrequency),
        ("LuminousIntensity",   DemoLuminousIntensity),
        ("Mass",                DemoMass),
        ("SolidAngle",          DemoSolidAngle),
        ("Temperature",         DemoTemperature),
        ("Time",                DemoTime),
        ("Volume",              DemoVolume),
    ];

    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;
    private static readonly CultureInfo German = new("de-DE");

    private static void Main(string[] args)
    {
        string? outPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (string.Equals(args[i], "--out", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                outPath = args[i + 1];
                i++;
            }
        }

        if (outPath is not null)
        {
            RunAllToFile(outPath);
            return;
        }

        if (args.Any(a => string.Equals(a, "--all", StringComparison.OrdinalIgnoreCase)))
        {
            RunAll();
            return;
        }

        InteractiveMenu();
    }

    private static void RunAllToFile(string path)
    {
        TextWriter originalOut = Console.Out;
        using StreamWriter writer = new(path) { AutoFlush = false };
        Console.SetOut(writer);
        try
        {
            RunAll();
        }
        finally
        {
            writer.Flush();
            Console.SetOut(originalOut);
        }
        Console.WriteLine($"Wrote playground output to {Path.GetFullPath(path)}");
    }

    private static void InteractiveMenu()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("Choose (number, name, 'all', or 'q' to quit): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input)) continue;
            if (input is "q" or "Q" or "quit" or "exit") return;

            if (string.Equals(input, "all", StringComparison.OrdinalIgnoreCase))
            {
                RunAll();
                continue;
            }

            (string Name, Action Demo)? entry = null;

            if (int.TryParse(input, NumberStyles.Integer, Inv, out int idx) && idx >= 1 && idx <= Demos.Length)
            {
                entry = Demos[idx - 1];
            }
            else
            {
                (string Name, Action Demo) match = Demos.FirstOrDefault(d => string.Equals(d.Name, input, StringComparison.OrdinalIgnoreCase));
                if (match.Name is not null) entry = match;
            }

            if (entry is null)
            {
                Console.WriteLine($"  Unknown selection: '{input}'\n");
                continue;
            }

            Console.WriteLine();
            RunDemo(entry.Value.Name, entry.Value.Demo);
            Console.WriteLine();
            Console.Write("Press Enter to return to menu...");
            Console.ReadLine();
        }
    }

    private static void RunAll()
    {
        foreach ((string name, Action demo) in Demos)
        {
            RunDemo(name, demo);
            Console.WriteLine();
        }
    }

    private static void RunDemo(string name, Action demo)
    {
        try
        {
            demo();
        }
        catch (Exception ex)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  !! {name} threw {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"  {ex.StackTrace?.Split('\n').FirstOrDefault()?.Trim()}");
            Console.ForegroundColor = previous;
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== OnixLabs.Units Playground (Float128) =====");
        for (int i = 0; i < Demos.Length; i++)
        {
            Console.Write($"  {i + 1,2}. {Demos[i].Name,-22}");
            if ((i + 1) % 3 == 0) Console.WriteLine();
        }
        if (Demos.Length % 3 != 0) Console.WriteLine();
        Console.WriteLine();
    }

    // --- Display helpers ---

    private static void Heading(string title)
    {
        ConsoleColor previous = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"== {title} ==");
        Console.ForegroundColor = previous;
    }

    private static void Sub(string title)
    {
        ConsoleColor previous = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"  -- {title}");
        Console.ForegroundColor = previous;
    }

    private static void Row(string label, object? value) =>
        Console.WriteLine($"    {label,-32} : {value}");

    private static Float128 F(string value) => Float128.Parse(value, Inv);
    private static Float128 F(int value) => (Float128)value;

    // ============================================================================================
    // Canonical units
    // ============================================================================================

    private static void DemoAmountOfSubstance()
    {
        Heading("AmountOfSubstance");
        AmountOfSubstance<Float128> n = AmountOfSubstance<Float128>.FromMoles(F("2.5"));

        Sub("Scales (FromMoles(2.5))");
        Row("Moles",        n.Moles);
        Row("Millimoles",   n.MilliMoles);
        Row("Kilomoles",    n.KiloMoles);
        Row("Micromoles",   n.MicroMoles);

        Sub("Format");
        Row("\"mol3\"",     n.ToString("mol3", Inv));
        Row("\"mmol2\"",    n.ToString("mmol2", Inv));
        Row("\"kmol6\"",    n.ToString("kmol6", Inv));
        Row("de-DE mol2",   n.ToString("mol2", German));

        Sub("Arithmetic");
        AmountOfSubstance<Float128> a = AmountOfSubstance<Float128>.FromMoles(F("1"));
        AmountOfSubstance<Float128> b = AmountOfSubstance<Float128>.FromMoles(F("3"));
        Row("1 + 3 mol",    a.Add(b).ToString("mol2", Inv));
        Row("3 - 1 mol",    b.Subtract(a).ToString("mol2", Inv));

        Sub("Equality / Comparison");
        AmountOfSubstance<Float128> a2 = AmountOfSubstance<Float128>.FromMillimoles(F("1000"));
        Row("1 mol == 1000 mmol",   a.Equals(a2));
        Row("Compare(a, b)",        AmountOfSubstance<Float128>.Compare(a, b));
    }

    private static void DemoAngle()
    {
        Heading("Angle");
        Angle<Float128> half = Angle<Float128>.FromRadians(Float128.Pi);

        Sub("Scales (FromRadians(pi) = 180 deg)");
        Row("Radians",      half.Radians);
        Row("Degrees",      half.Degrees);
        Row("Gradians",     half.Gradians);
        Row("Turns",        half.Turns);
        Row("Arcminutes",   half.Arcminutes);
        Row("Arcseconds",   half.Arcseconds);

        Sub("Format");
        Row("\"rad6\"",     half.ToString("rad6", Inv));
        Row("\"deg3\"",     half.ToString("deg3", Inv));
        Row("\"gon3\"",     half.ToString("gon3", Inv));
        Row("\"tr4\"",      half.ToString("tr4", Inv));
        Row("\"arcmin1\"",  half.ToString("arcmin1", Inv));
        Row("\"urad3\"",    half.ToString("urad3", Inv)); // symbol differs (mu)
        Row("de-DE deg3",   half.ToString("deg3", German));

        Sub("Arithmetic");
        Angle<Float128> a = Angle<Float128>.FromDegrees(F(30));
        Angle<Float128> b = Angle<Float128>.FromDegrees(F(60));
        Row("30deg + 60deg",        a.Add(b).ToString("deg3", Inv));
        Row("60deg - 30deg",        b.Subtract(a).ToString("deg3", Inv));

        Sub("Equality / Comparison");
        Angle<Float128> twoRad = Angle<Float128>.FromRadians(F(2));
        Angle<Float128> mrad   = Angle<Float128>.FromMilliradians(F(2000));
        Row("2 rad == 2000 mrad",   twoRad.Equals(mrad));
        Row("Compare(30deg, 60deg)", Angle<Float128>.Compare(a, b));
    }

    private static void DemoArea()
    {
        Heading("Area");
        Area<Float128> a = Area<Float128>.FromSquareMeters(F("100"));

        Sub("Scales (FromSquareMeters(100))");
        Row("SquareMeters",      a.SquareMeters);
        Row("SquareCentimeters", a.SquareCentiMeters);
        Row("SquareKilometers",  a.SquareKiloMeters);
        Row("SquareFeet",        a.SquareFeet);
        Row("SquareInches",      a.SquareInches);
        Row("SquareYards",       a.SquareYards);

        Sub("Format");
        Row("\"sqm3\"",     a.ToString("sqm3", Inv));
        Row("\"sqcm0\"",    a.ToString("sqcm0", Inv));
        Row("\"sqft4\"",    a.ToString("sqft4", Inv));
        Row("\"sqyd4\"",    a.ToString("sqyd4", Inv));
        Row("de-DE sqm3",   a.ToString("sqm3", German));

        Sub("Arithmetic");
        Area<Float128> b = Area<Float128>.FromSquareMeters(F("50"));
        Row("100 + 50 sqm", a.Add(b).ToString("sqm2", Inv));
        Row("100 - 50 sqm", a.Subtract(b).ToString("sqm2", Inv));

        Sub("Equality / Comparison");
        Area<Float128> aClone = Area<Float128>.FromSquareMeters(F("100"));
        Row("100 sqm == 100 sqm",   a.Equals(aClone));
        Row("Compare(a, b)",        Area<Float128>.Compare(a, b));
    }

    private static void DemoCurrent()
    {
        Heading("Current");
        Current<Float128> i = Current<Float128>.FromAmperes(F("2.5"));

        Sub("Scales (FromAmperes(2.5))");
        Row("Amperes",      i.Amperes);
        Row("Milliamperes", i.MilliAmperes);
        Row("Microamperes", i.MicroAmperes);
        Row("Kiloamperes",  i.KiloAmperes);

        Sub("Format");
        Row("\"A3\"",       i.ToString("A3", Inv));
        Row("\"mA1\"",      i.ToString("mA1", Inv));
        Row("\"uA1\"",      i.ToString("uA1", Inv));
        Row("de-DE A3",     i.ToString("A3", German));

        Sub("Arithmetic");
        Current<Float128> a = Current<Float128>.FromAmperes(F(2));
        Current<Float128> b = Current<Float128>.FromAmperes(F(3));
        Row("2 + 3 A",      a.Add(b).ToString("A2", Inv));
        Row("3 - 2 A",      b.Subtract(a).ToString("A2", Inv));

        Sub("Equality / Comparison");
        Current<Float128> aClone = Current<Float128>.FromMilliamperes(F(2000));
        Row("2 A == 2000 mA",   a.Equals(aClone));
        Row("Compare(2A, 3A)",  Current<Float128>.Compare(a, b));
    }

    private static void DemoDataSize()
    {
        Heading("DataSize");
        DataSize<Float128> size = DataSize<Float128>.FromMebiBytes(F("512"));

        Sub("Scales (FromMebiBytes(512))");
        Row("Bits",        size.Bits);
        Row("Bytes",       size.Bytes);
        Row("KibiBytes",   size.KibiBytes);
        Row("MebiBytes",   size.MebiBytes);
        Row("GibiBytes",   size.GibiBytes);
        Row("KiloBytes",   size.KiloBytes);   // 1000-based
        Row("MegaBytes",   size.MegaBytes);   // 1000-based

        Sub("Format");
        Row("\"B0\"",       size.ToString("B0", Inv));
        Row("\"KiB3\"",     size.ToString("KiB3", Inv));
        Row("\"MiB3\"",     size.ToString("MiB3", Inv));
        Row("\"GiB6\"",     size.ToString("GiB6", Inv));
        Row("\"MB3\"",      size.ToString("MB3", Inv));    // decimal MB
        Row("de-DE MiB3",   size.ToString("MiB3", German));

        Sub("Arithmetic");
        DataSize<Float128> a = DataSize<Float128>.FromMebiBytes(F(100));
        DataSize<Float128> b = DataSize<Float128>.FromMebiBytes(F(28));
        Row("100 + 28 MiB", a.Add(b).ToString("MiB2", Inv));
        Row("100 - 28 MiB", a.Subtract(b).ToString("MiB2", Inv));

        Sub("Equality / Comparison");
        DataSize<Float128> oneKi = DataSize<Float128>.FromKibiBytes(F(1));
        DataSize<Float128> b1024 = DataSize<Float128>.FromBytes(F(1024));
        Row("1 KiB == 1024 B",  oneKi.Equals(b1024));
        Row("Compare(a, b)",    DataSize<Float128>.Compare(a, b));
    }

    private static void DemoDistance()
    {
        Heading("Distance");
        Distance<Float128> marathon = Distance<Float128>.FromKilometers(F("42.195"));

        Sub("Scales (FromKilometers(42.195))");
        Row("Meters",         marathon.Meters);
        Row("Kilometers",     marathon.KiloMeters);
        Row("Centimeters",    marathon.CentiMeters);
        Row("Miles",          marathon.Miles);
        Row("Feet",           marathon.Feet);
        Row("NauticalMiles",  marathon.NauticalMiles);
        Row("AstronomicalUnits", marathon.AstronomicalUnits);
        Row("LightYears",     marathon.LightYears);

        Sub("Format");
        Row("\"m3\"",   marathon.ToString("m3", Inv));
        Row("\"km4\"",  marathon.ToString("km4", Inv));
        Row("\"mi5\"",  marathon.ToString("mi5", Inv));
        Row("\"ft2\"",  marathon.ToString("ft2", Inv));
        Row("de-DE km4", marathon.ToString("km4", German));

        Sub("Arithmetic");
        Distance<Float128> a = Distance<Float128>.FromMeters(F(100));
        Distance<Float128> b = Distance<Float128>.FromMeters(F(250));
        Row("100 + 250 m",      a.Add(b).ToString("m3", Inv));
        Row("250 - 100 m",      b.Subtract(a).ToString("m3", Inv));

        Sub("Equality / Comparison");
        Distance<Float128> oneKm = Distance<Float128>.FromKilometers(F("0.1"));
        Row("100 m == 0.1 km",      a.Equals(oneKm));
        Row("Compare(100m, 250m)",  Distance<Float128>.Compare(a, b));
    }

    private static void DemoFrequency()
    {
        Heading("Frequency");
        Frequency<Float128> f = Frequency<Float128>.FromKilohertz(F("2.4"));

        Sub("Scales (FromKilohertz(2.4) = WiFi-ish)");
        Row("Hertz",            f.Hertz);
        Row("Kilohertz",        f.KiloHertz);
        Row("Megahertz",        f.MegaHertz);
        Row("Gigahertz",        f.GigaHertz);
        Row("RevolutionsPerMinute", f.RevolutionsPerMinute);
        Row("BeatsPerMinute",   f.BeatsPerMinute);
        Row("RadiansPerSecond", f.RadiansPerSecond);

        Sub("Format");
        Row("\"Hz3\"",      f.ToString("Hz3", Inv));
        Row("\"kHz3\"",     f.ToString("kHz3", Inv));
        Row("\"MHz6\"",     f.ToString("MHz6", Inv));
        Row("\"rpm2\"",     f.ToString("rpm2", Inv));
        Row("de-DE kHz3",   f.ToString("kHz3", German));

        Sub("Arithmetic");
        Frequency<Float128> a = Frequency<Float128>.FromHertz(F(50));
        Frequency<Float128> b = Frequency<Float128>.FromHertz(F(60));
        Row("50 + 60 Hz",   a.Add(b).ToString("Hz2", Inv));
        Row("60 - 50 Hz",   b.Subtract(a).ToString("Hz2", Inv));

        Sub("Equality / Comparison");
        Frequency<Float128> oneKHz = Frequency<Float128>.FromKilohertz(F(1));
        Frequency<Float128> thousand = Frequency<Float128>.FromHertz(F(1000));
        Row("1 kHz == 1000 Hz", oneKHz.Equals(thousand));
        Row("Compare(50, 60)",  Frequency<Float128>.Compare(a, b));
    }

    private static void DemoLuminousIntensity()
    {
        Heading("LuminousIntensity");
        LuminousIntensity<Float128> l = LuminousIntensity<Float128>.FromCandelas(F("100"));

        Sub("Scales (FromCandelas(100))");
        Row("Candelas",       l.Candelas);
        Row("Millicandelas",  l.MilliCandelas);
        Row("Kilocandelas",   l.KiloCandelas);
        Row("Microcandelas",  l.MicroCandelas);

        Sub("Format");
        Row("\"cd3\"",      l.ToString("cd3", Inv));
        Row("\"mcd1\"",     l.ToString("mcd1", Inv));
        Row("\"kcd6\"",     l.ToString("kcd6", Inv));
        Row("de-DE cd3",    l.ToString("cd3", German));

        Sub("Arithmetic");
        LuminousIntensity<Float128> a = LuminousIntensity<Float128>.FromCandelas(F(40));
        LuminousIntensity<Float128> b = LuminousIntensity<Float128>.FromCandelas(F(60));
        Row("40 + 60 cd",   a.Add(b).ToString("cd2", Inv));
        Row("60 - 40 cd",   b.Subtract(a).ToString("cd2", Inv));

        Sub("Equality / Comparison");
        LuminousIntensity<Float128> kilo = LuminousIntensity<Float128>.FromKilocandelas(F("0.1"));
        Row("100 cd == 0.1 kcd",    l.Equals(kilo));
        Row("Compare(40, 60)",      LuminousIntensity<Float128>.Compare(a, b));
    }

    private static void DemoMass()
    {
        Heading("Mass");
        Mass<Float128> m = Mass<Float128>.FromKilograms(F("75"));

        Sub("Scales (FromKilograms(75))");
        Row("Kilograms",    m.KiloGrams);
        Row("Grams",        m.Grams);
        Row("Milligrams",   m.MilliGrams);
        Row("Pounds",       m.Pounds);
        Row("Ounces",       m.Ounces);
        Row("Stones",       m.Stones);
        Row("Tonnes",       m.Tonnes);
        Row("Carats",       m.Carats);
        Row("Daltons",      m.Daltons);

        Sub("Format");
        Row("\"kg3\"",      m.ToString("kg3", Inv));
        Row("\"g2\"",       m.ToString("g2", Inv));
        Row("\"lb3\"",      m.ToString("lb3", Inv));
        Row("\"oz2\"",      m.ToString("oz2", Inv));
        Row("\"st3\"",      m.ToString("st3", Inv));
        Row("de-DE kg3",    m.ToString("kg3", German));

        Sub("Arithmetic");
        Mass<Float128> a = Mass<Float128>.FromKilograms(F(10));
        Mass<Float128> b = Mass<Float128>.FromKilograms(F(15));
        Row("10 + 15 kg",   a.Add(b).ToString("kg2", Inv));
        Row("15 - 10 kg",   b.Subtract(a).ToString("kg2", Inv));

        Sub("Equality / Comparison");
        Mass<Float128> oneT = Mass<Float128>.FromTonnes(F(1));
        Mass<Float128> thouKg = Mass<Float128>.FromKilograms(F(1000));
        Row("1 t == 1000 kg",   oneT.Equals(thouKg));
        Row("Compare(10, 15)",  Mass<Float128>.Compare(a, b));
    }

    private static void DemoTemperature()
    {
        Heading("Temperature");
        Temperature<Float128> t = Temperature<Float128>.FromCelsius(F("100"));

        Sub("Scales (FromCelsius(100) = boiling water)");
        Row("Kelvin",       t.Kelvin);
        Row("Celsius",      t.Celsius);
        Row("Fahrenheit",   t.Fahrenheit);
        Row("Rankine",      t.Rankine);
        Row("Reaumur",      t.Reaumur);
        Row("Romer",        t.Romer);
        Row("Newton",       t.Newton);
        Row("Delisle",      t.Delisle);

        Sub("Format");
        Row("\"K3\"",       t.ToString("K3", Inv));
        Row("\"C3\"",       t.ToString("C3", Inv));
        Row("\"F3\"",       t.ToString("F3", Inv));
        Row("\"R3\"",       t.ToString("R3", Inv));
        Row("de-DE C3",     t.ToString("C3", German));

        Sub("Affine round-trips (non-trivial for temperature)");
        Temperature<Float128> body = Temperature<Float128>.FromFahrenheit(F("98.6"));
        Row("98.6 F as C",  body.ToString("C3", Inv));
        Row("98.6 F as K",  body.ToString("K3", Inv));

        Sub("Arithmetic (on Kelvin scale)");
        Temperature<Float128> a = Temperature<Float128>.FromKelvin(F(100));
        Temperature<Float128> b = Temperature<Float128>.FromKelvin(F(50));
        Row("100K + 50K",   a.Add(b).ToString("K3", Inv));
        Row("100K - 50K",   a.Subtract(b).ToString("K3", Inv));

        Sub("Equality / Comparison");
        Temperature<Float128> zeroC = Temperature<Float128>.FromCelsius(F(0));
        Temperature<Float128> k273  = Temperature<Float128>.FromKelvin(F("273.15"));
        Row("0 C == 273.15 K",  zeroC.Equals(k273));
        Row("Compare(100K, 50K)", Temperature<Float128>.Compare(a, b));
    }

    private static void DemoTime()
    {
        Heading("Time");
        Time<Float128> day = Time<Float128>.FromDays(F(1));

        Sub("Scales (FromDays(1))");
        Row("Seconds",      day.Seconds);
        Row("Minutes",      day.Minutes);
        Row("Hours",        day.Hours);
        Row("Days",         day.Days);
        Row("Weeks",        day.Weeks);
        Row("JulianYears",  day.JulianYears);
        Row("Milliseconds", day.MilliSeconds);
        Row("Microseconds", day.MicroSeconds);

        Sub("Format");
        Row("\"s3\"",       day.ToString("s3", Inv));
        Row("\"min3\"",     day.ToString("min3", Inv));
        Row("\"h3\"",       day.ToString("h3", Inv));
        Row("\"d4\"",       day.ToString("d4", Inv));
        Row("\"wk4\"",      day.ToString("wk4", Inv));
        Row("de-DE h3",     day.ToString("h3", German));

        Sub("Arithmetic");
        Time<Float128> a = Time<Float128>.FromHours(F(1));
        Time<Float128> b = Time<Float128>.FromMinutes(F(30));
        Row("1 h + 30 min as h",    a.Add(b).ToString("h3", Inv));
        Row("1 h - 30 min as min",  a.Subtract(b).ToString("min3", Inv));

        Sub("Equality / Comparison");
        Time<Float128> oneHr = Time<Float128>.FromHours(F(1));
        Time<Float128> sixty = Time<Float128>.FromMinutes(F(60));
        Row("1 h == 60 min",        oneHr.Equals(sixty));
        Row("Compare(1h, 30min)",   Time<Float128>.Compare(a, b));
    }

    private static void DemoVolume()
    {
        Heading("Volume");
        Volume<Float128> v = Volume<Float128>.FromLiters(F("2"));

        Sub("Scales (FromLiters(2))");
        Row("Liters",           v.Liters);
        Row("Milliliters",      v.Milliliters);
        Row("CubicMeters",      v.CubicMeters);
        Row("CubicCentimeters", v.CubicCentiMeters);
        Row("CubicInches",      v.CubicInches);
        Row("USGallons",        v.USGallons);
        Row("ImperialGallons",  v.ImperialGallons);
        Row("OilBarrels",       v.OilBarrels);

        Sub("Format");
        Row("\"L3\"",       v.ToString("L3", Inv));
        Row("\"mL2\"",      v.ToString("mL2", Inv));
        Row("\"cum6\"",     v.ToString("cum6", Inv));
        Row("\"USgal5\"",   v.ToString("USgal5", Inv));
        Row("\"impgal5\"",  v.ToString("impgal5", Inv));
        Row("de-DE L3",     v.ToString("L3", German));

        Sub("Arithmetic");
        Volume<Float128> a = Volume<Float128>.FromLiters(F(5));
        Volume<Float128> b = Volume<Float128>.FromLiters(F(3));
        Row("5 + 3 L",      a.Add(b).ToString("L2", Inv));
        Row("5 - 3 L",      a.Subtract(b).ToString("L2", Inv));

        Sub("Equality / Comparison");
        Volume<Float128> oneL = Volume<Float128>.FromLiters(F(1));
        Volume<Float128> thouMl = Volume<Float128>.FromMilliliters(F(1000));
        Row("1 L == 1000 mL",   oneL.Equals(thouMl));
        Row("Compare(5L, 3L)",  Volume<Float128>.Compare(a, b));
    }

    private static void DemoSolidAngle()
    {
        Heading("SolidAngle");
        SolidAngle<Float128> sphere = SolidAngle<Float128>.FromSpats(F(1));

        Sub("Scales (FromSpats(1) = full sphere = 4π sr)");
        Row("Steradians",       sphere.Steradians);
        Row("Millisteradians",  sphere.MilliSteradians);
        Row("Microsteradians",  sphere.MicroSteradians);
        Row("Kilosteradians",   sphere.KiloSteradians);
        Row("SquareDegrees",    sphere.SquareDegrees);
        Row("SquareArcminutes", sphere.SquareArcminutes);
        Row("SquareArcseconds", sphere.SquareArcseconds);
        Row("Spats",            sphere.Spats);

        Sub("Format");
        Row("\"sr6\"",      sphere.ToString("sr6", Inv));
        Row("\"msr3\"",     sphere.ToString("msr3", Inv));
        Row("\"usr3\"",     sphere.ToString("usr3", Inv));     // µsr symbol
        Row("\"sqdeg2\"",   sphere.ToString("sqdeg2", Inv));
        Row("\"sp4\"",      sphere.ToString("sp4", Inv));
        Row("de-DE sr6",    sphere.ToString("sr6", German));

        Sub("Arithmetic");
        SolidAngle<Float128> a = SolidAngle<Float128>.FromSteradians(F(3));
        SolidAngle<Float128> b = SolidAngle<Float128>.FromSteradians(F(4));
        Row("3 + 4 sr",     a.Add(b).ToString("sr3", Inv));
        Row("4 - 3 sr",     b.Subtract(a).ToString("sr3", Inv));

        Sub("Equality / Comparison");
        SolidAngle<Float128> twoSr   = SolidAngle<Float128>.FromSteradians(F(2));
        SolidAngle<Float128> twoKmsr = SolidAngle<Float128>.FromMillisteradians(F(2000));
        Row("2 sr == 2000 msr",     twoSr.Equals(twoKmsr));
        Row("Compare(3sr, 4sr)",    SolidAngle<Float128>.Compare(a, b));
    }
}
