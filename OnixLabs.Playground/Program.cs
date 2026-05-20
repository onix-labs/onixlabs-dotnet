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
    // 37 units, grouped: 13 canonical, then 24 composite.
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
        ("Acceleration",        DemoAcceleration),
        ("AngularAcceleration", DemoAngularAcceleration),
        ("AngularVelocity",     DemoAngularVelocity),
        ("Density",             DemoDensity),
        ("ElectricCapacitance", DemoElectricCapacitance),
        ("ElectricCharge",      DemoElectricCharge),
        ("ElectricPotential",   DemoElectricPotential),
        ("ElectricResistance",  DemoElectricResistance),
        ("Energy",              DemoEnergy),
        ("Force",               DemoForce),
        ("HeatCapacity",        DemoHeatCapacity),
        ("Illuminance",         DemoIlluminance),
        ("Impulse",             DemoImpulse),
        ("LuminousFlux",        DemoLuminousFlux),
        ("MagneticFlux",        DemoMagneticFlux),
        ("MassFlowRate",        DemoMassFlowRate),
        ("MolarConcentration",  DemoMolarConcentration),
        ("MolarMass",           DemoMolarMass),
        ("Momentum",            DemoMomentum),
        ("Power",               DemoPower),
        ("Pressure",            DemoPressure),
        ("Speed",               DemoSpeed),
        ("Torque",              DemoTorque),
        ("VolumetricFlowRate",  DemoVolumetricFlowRate),
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

    // ============================================================================================
    // Composite units
    // ============================================================================================

    private static void DemoAcceleration()
    {
        Heading("Acceleration  (Speed / Time)");
        // 9.81 m/s^2 = (9.81 m / 1 s) / 1 s
        Acceleration<Float128> g = new(
            new Speed<Float128>(
                Distance<Float128>.FromMeters(F("9.81")),
                Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));

        Sub("Construction (gravity ~ 9.81 m/s2)");
        Row("Magnitude (m/s2)", g.Magnitude);

        Sub("Format");
        Row("\"m/s²:3\"",   g.ToString("m/s²:3", Inv));
        Row("\"m/s2:3\"",   g.ToString("m/s2:3", Inv));
        Row("\"km/h/s:3\"", g.ToString("km/h/s:3", Inv));
        Row("de-DE m/s²:3", g.ToString("m/s²:3", German));

        Sub("Arithmetic");
        Acceleration<Float128> a = new(
            new Speed<Float128>(Distance<Float128>.FromMeters(F(5)), Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));
        Acceleration<Float128> b = new(
            new Speed<Float128>(Distance<Float128>.FromMeters(F(3)), Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));
        Row("5 + 3 m/s²",   a.Add(b).ToString("m/s²:3", Inv));
        Row("5 - 3 m/s²",   a.Subtract(b).ToString("m/s²:3", Inv));

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Acceleration<Float128>.Compare(a, b));
    }

    private static void DemoAngularAcceleration()
    {
        Heading("AngularAcceleration  (AngularVelocity / Time)");
        AngularAcceleration<Float128> alpha = new(
            new AngularVelocity<Float128>(
                Angle<Float128>.FromRadians(F(2)),
                Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));

        Sub("Construction (2 rad/s²)");
        Row("Magnitude (rad/s²)", alpha.Magnitude);

        Sub("Format");
        Row("default",          alpha.ToString());
        Row("\"rad/s/s:4\"",    alpha.ToString("rad/s/s:4", Inv));

        Sub("Arithmetic");
        AngularAcceleration<Float128> a = new(
            new AngularVelocity<Float128>(Angle<Float128>.FromRadians(F(1)), Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));
        AngularAcceleration<Float128> b = new(
            new AngularVelocity<Float128>(Angle<Float128>.FromRadians(F(3)), Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));
        Row("a + b",        a.Add(b).ToString("rad/s/s:3", Inv));
        Row("b - a",        b.Subtract(a).ToString("rad/s/s:3", Inv));

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    AngularAcceleration<Float128>.Compare(a, b));
    }

    private static void DemoAngularVelocity()
    {
        Heading("AngularVelocity  (Angle / Time)");
        AngularVelocity<Float128> w = new(
            Angle<Float128>.FromRadians(F("6.283185307179586")),  // ~2pi
            Time<Float128>.FromSeconds(F(1)));

        Sub("Construction (one revolution per second)");
        Row("Magnitude (rad/s)", w.Magnitude);

        Sub("Format");
        Row("default",          w.ToString());
        Row("\"rad/s:4\"",      w.ToString("rad/s:4", Inv));
        Row("\"deg/s:3\"",      w.ToString("deg/s:3", Inv));
        Row("\"deg/min:1\"",    w.ToString("deg/min:1", Inv));
        Row("\"tr/s:3\"",       w.ToString("tr/s:3", Inv));

        Sub("Arithmetic");
        AngularVelocity<Float128> a = new(Angle<Float128>.FromRadians(F(2)), Time<Float128>.FromSeconds(F(1)));
        AngularVelocity<Float128> b = new(Angle<Float128>.FromRadians(F(5)), Time<Float128>.FromSeconds(F(1)));
        Row("a + b",        a.Add(b).ToString("rad/s:3", Inv));
        Row("b - a",        b.Subtract(a).ToString("rad/s:3", Inv));

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    AngularVelocity<Float128>.Compare(a, b));
    }

    private static void DemoDensity()
    {
        Heading("Density  (Mass / Volume)");
        Density<Float128> water = new(
            Mass<Float128>.FromKilograms(F(1000)),
            Volume<Float128>.FromCubicMeters(F(1)));

        Sub("Construction (water = 1000 kg/m3)");
        Row("Magnitude",    water.Magnitude);

        Sub("Format");
        Row("default",          water.ToString());
        Row("\"kg/cum:3\"",     water.ToString("kg/cum:3", Inv));
        Row("\"g/cucm:3\"",     water.ToString("g/cucm:3", Inv));
        Row("\"kg/L:3\"",       water.ToString("kg/L:3", Inv));
        Row("\"lb/cuft:4\"",    water.ToString("lb/cuft:4", Inv));

        Sub("Arithmetic");
        Density<Float128> a = new(Mass<Float128>.FromKilograms(F(500)),  Volume<Float128>.FromCubicMeters(F(1)));
        Density<Float128> b = new(Mass<Float128>.FromKilograms(F(2000)), Volume<Float128>.FromCubicMeters(F(1)));
        Row("a + b",        a.Add(b).ToString("kg/cum:3", Inv));
        Row("b - a",        b.Subtract(a).ToString("kg/cum:3", Inv));

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Density<Float128>.Compare(a, b));
    }

    private static void DemoElectricCapacitance()
    {
        Heading("ElectricCapacitance  (ElectricCharge / ElectricPotential)");
        // 1 F = 1 C / 1 V
        ElectricCharge<Float128> q = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(1)));
        ElectricPotential<Float128> v = new(
            new Energy<Float128>(
                new Force<Float128>(
                    Mass<Float128>.FromKilograms(F(1)),
                    new Acceleration<Float128>(
                        new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                        Time<Float128>.FromSeconds(F(1)))),
                Distance<Float128>.FromMeters(F(1))),
            new ElectricCharge<Float128>(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(1))));

        ElectricCapacitance<Float128> c = new(q, v);

        Sub("Construction (1 F)");
        Row("Magnitude",    c.Magnitude);

        Sub("Format (named SI alias 'F' with prefixes)");
        Row("default",      c.ToString());
        Row("\"F:3\"",      c.ToString("F:3", Inv));
        Row("\"mF:3\"",     c.ToString("mF:3", Inv));
        Row("\"uF:3\"",     c.ToString("uF:3", Inv));   // µF rendering
        Row("\"nF:3\"",     c.ToString("nF:3", Inv));
        Row("\"pF:3\"",     c.ToString("pF:3", Inv));

        Sub("Arithmetic");
        ElectricCapacitance<Float128> a = new(q, v);
        ElectricCapacitance<Float128> b = new(
            new ElectricCharge<Float128>(Current<Float128>.FromAmperes(F(2)), Time<Float128>.FromSeconds(F(1))),
            v);
        Row("a + b",            a.Add(b).Magnitude);
        Row("b - a",            b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    ElectricCapacitance<Float128>.Compare(a, b));
    }

    private static void DemoElectricCharge()
    {
        Heading("ElectricCharge  (Current * Time)");
        // 1 C = 1 A * 1 s
        ElectricCharge<Float128> q = new(
            Current<Float128>.FromAmperes(F(2)),
            Time<Float128>.FromSeconds(F(5)));

        Sub("Construction (2 A x 5 s = 10 C)");
        Row("Magnitude (C)",    q.Magnitude);

        Sub("Format (named SI alias 'C' with prefixes)");
        Row("default",          q.ToString());
        Row("\"C:3\"",          q.ToString("C:3", Inv));
        Row("\"mC:3\"",         q.ToString("mC:3", Inv));
        Row("\"kC:6\"",         q.ToString("kC:6", Inv));
        Row("\"A*s:3\"",        q.ToString("A*s:3", Inv));   // compound still works
        Row("\"A*h:6\"",        q.ToString("A*h:6", Inv));

        Sub("Arithmetic");
        ElectricCharge<Float128> a = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(3)));
        ElectricCharge<Float128> b = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(7)));
        Row("a + b (C)",    a.Add(b).Magnitude);
        Row("b - a (C)",    b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    ElectricCharge<Float128>.Compare(a, b));
    }

    private static void DemoElectricPotential()
    {
        Heading("ElectricPotential  (Energy / ElectricCharge)");
        // 1 V = 1 J / 1 C
        Energy<Float128> oneJoule = new(
            new Force<Float128>(
                Mass<Float128>.FromKilograms(F(1)),
                new Acceleration<Float128>(
                    new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                    Time<Float128>.FromSeconds(F(1)))),
            Distance<Float128>.FromMeters(F(1)));
        ElectricCharge<Float128> oneCoulomb = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(1)));

        ElectricPotential<Float128> v = new(oneJoule, oneCoulomb);

        Sub("Construction (1 V)");
        Row("Magnitude",    v.Magnitude);

        Sub("Format (named SI alias 'V' with prefixes)");
        Row("default",      v.ToString());
        Row("\"V:3\"",      v.ToString("V:3", Inv));
        Row("\"mV:3\"",     v.ToString("mV:3", Inv));
        Row("\"kV:3\"",     v.ToString("kV:3", Inv));
        Row("\"MV:9\"",     v.ToString("MV:9", Inv));

        Sub("Arithmetic");
        ElectricPotential<Float128> a = new(oneJoule, oneCoulomb);
        ElectricPotential<Float128> b = new(oneJoule, new ElectricCharge<Float128>(Current<Float128>.FromAmperes(F(2)), Time<Float128>.FromSeconds(F(1))));
        Row("a + b",        a.Add(b).Magnitude);
        Row("a - b",        a.Subtract(b).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    ElectricPotential<Float128>.Compare(a, b));
    }

    private static void DemoElectricResistance()
    {
        Heading("ElectricResistance  (ElectricPotential / Current)");
        // 1 Ohm = 1 V / 1 A
        Energy<Float128> oneJoule = new(
            new Force<Float128>(
                Mass<Float128>.FromKilograms(F(1)),
                new Acceleration<Float128>(
                    new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                    Time<Float128>.FromSeconds(F(1)))),
            Distance<Float128>.FromMeters(F(1)));
        ElectricCharge<Float128> oneCoulomb = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(1)));
        ElectricPotential<Float128> oneVolt = new(oneJoule, oneCoulomb);

        ElectricResistance<Float128> r = new(oneVolt, Current<Float128>.FromAmperes(F(1)));

        Sub("Construction (1 Ohm)");
        Row("Magnitude",    r.Magnitude);

        Sub("Format (named SI alias 'Ω' with prefixes; ASCII 'Ohm' also accepted on input)");
        Row("default",      r.ToString());
        Row("\"Ω:3\"",      r.ToString("Ω:3", Inv));
        Row("\"mΩ:3\"",     r.ToString("mΩ:3", Inv));
        Row("\"kΩ:6\"",     r.ToString("kΩ:6", Inv));
        Row("\"MΩ:9\"",     r.ToString("MΩ:9", Inv));
        Row("\"Ohm:3\"   (ASCII)",  r.ToString("Ohm:3", Inv));
        Row("\"kOhm:6\"  (ASCII)",  r.ToString("kOhm:6", Inv));
        Row("\"MOhm:9\"  (ASCII)",  r.ToString("MOhm:9", Inv));

        Sub("Arithmetic");
        ElectricResistance<Float128> a = new(oneVolt, Current<Float128>.FromAmperes(F(1)));
        ElectricResistance<Float128> b = new(oneVolt, Current<Float128>.FromAmperes(F(2)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("a - b",        a.Subtract(b).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    ElectricResistance<Float128>.Compare(a, b));
    }

    private static void DemoEnergy()
    {
        Heading("Energy  (Force * Distance)");
        // 1 J = 1 N * 1 m
        Force<Float128> oneNewton = new(
            Mass<Float128>.FromKilograms(F(1)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));

        Energy<Float128> e = new(oneNewton, Distance<Float128>.FromMeters(F(10)));

        Sub("Construction (1 N x 10 m = 10 J)");
        Row("Magnitude (J)",    e.Magnitude);

        Sub("Format (named SI alias 'J' with prefixes)");
        Row("default",              e.ToString());
        Row("\"J:3\"",              e.ToString("J:3", Inv));
        Row("\"kJ:3\"",             e.ToString("kJ:3", Inv));
        Row("\"MJ:9\"",             e.ToString("MJ:9", Inv));
        Row("\"N*m:3\"  (cascade)", e.ToString("N*m:3", Inv));   // cascade via Force alias
        Row("\"kg*m/s²*m:4\"",      e.ToString("kg*m/s²*m:4", Inv));

        Sub("Arithmetic");
        Energy<Float128> a = new(oneNewton, Distance<Float128>.FromMeters(F(2)));
        Energy<Float128> b = new(oneNewton, Distance<Float128>.FromMeters(F(8)));
        Row("a + b (J)",    a.Add(b).Magnitude);
        Row("b - a (J)",    b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Energy<Float128>.Compare(a, b));
    }

    private static void DemoForce()
    {
        Heading("Force  (Mass * Acceleration)");
        // 1 N = 1 kg * 1 m/s²
        Acceleration<Float128> oneMperS2 = new(
            new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
            Time<Float128>.FromSeconds(F(1)));

        Force<Float128> f = new(Mass<Float128>.FromKilograms(F(5)), oneMperS2);

        Sub("Construction (5 kg x 1 m/s² = 5 N)");
        Row("Magnitude (N)",    f.Magnitude);

        Sub("Format (named SI alias 'N' with prefixes)");
        Row("default",          f.ToString());
        Row("\"N:3\"",          f.ToString("N:3", Inv));
        Row("\"kN:3\"",         f.ToString("kN:3", Inv));
        Row("\"mN:3\"",         f.ToString("mN:3", Inv));
        Row("\"uN:3\"",         f.ToString("uN:3", Inv));   // renders µN
        Row("\"MN:9\"",         f.ToString("MN:9", Inv));
        Row("\"kg*m/s²:3\"",    f.ToString("kg*m/s²:3", Inv));

        Sub("Arithmetic");
        Force<Float128> a = new(Mass<Float128>.FromKilograms(F(2)), oneMperS2);
        Force<Float128> b = new(Mass<Float128>.FromKilograms(F(3)), oneMperS2);
        Row("a + b (N)",    a.Add(b).Magnitude);
        Row("b - a (N)",    b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Force<Float128>.Compare(a, b));
    }

    private static void DemoHeatCapacity()
    {
        Heading("HeatCapacity  (Energy / Temperature)");
        Force<Float128> oneNewton = new(
            Mass<Float128>.FromKilograms(F(1)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));
        Energy<Float128> oneJoule = new(oneNewton, Distance<Float128>.FromMeters(F(1)));

        HeatCapacity<Float128> hc = new(oneJoule, Temperature<Float128>.FromKelvin(F(1)));

        Sub("Construction (1 J/K)");
        Row("Magnitude (J/K)",  hc.Magnitude);

        Sub("Format (compound default cascades through Energy 'J' alias)");
        Row("default",          hc.ToString());
        Row("\"J/K:3\"",        hc.ToString("J/K:3", Inv));
        Row("\"kJ/K:6\"",       hc.ToString("kJ/K:6", Inv));

        Sub("Arithmetic");
        HeatCapacity<Float128> a = new(oneJoule, Temperature<Float128>.FromKelvin(F(2)));
        HeatCapacity<Float128> b = new(oneJoule, Temperature<Float128>.FromKelvin(F(4)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("a - b",        a.Subtract(b).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    HeatCapacity<Float128>.Compare(a, b));
    }

    private static void DemoIlluminance()
    {
        Heading("Illuminance  (LuminousFlux / Area)");
        LuminousFlux<Float128> oneLumen = new(
            LuminousIntensity<Float128>.FromCandelas(F(1)),
            SolidAngle<Float128>.FromSteradians(F(1))); // 1 cd * 1 sr = 1 lm

        Illuminance<Float128> lx = new(oneLumen, Area<Float128>.FromSquareMeters(F(1)));

        Sub("Construction (1 lm / 1 m² = 1 lx)");
        Row("Magnitude",    lx.Magnitude);

        Sub("Format (named SI alias 'lx' with prefixes)");
        Row("default",      lx.ToString());
        Row("\"lx:3\"",     lx.ToString("lx:3", Inv));
        Row("\"klx:3\"",    lx.ToString("klx:3", Inv));
        Row("\"mlx:3\"",    lx.ToString("mlx:3", Inv));

        Sub("Arithmetic");
        Illuminance<Float128> a = new(oneLumen, Area<Float128>.FromSquareMeters(F(2)));
        Illuminance<Float128> b = new(oneLumen, Area<Float128>.FromSquareMeters(F(4)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("a - b",        a.Subtract(b).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Illuminance<Float128>.Compare(a, b));
    }

    private static void DemoImpulse()
    {
        Heading("Impulse  (Force * Time)");
        Force<Float128> tenN = new(
            Mass<Float128>.FromKilograms(F(10)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));

        Impulse<Float128> j = new(tenN, Time<Float128>.FromSeconds(F(3)));

        Sub("Construction (10 N x 3 s = 30 N*s)");
        Row("Magnitude (N*s)",  j.Magnitude);

        Sub("Format (compound default cascades through Force 'N' alias)");
        Row("default",          j.ToString());
        Row("\"N*s:3\"",        j.ToString("N*s:3", Inv));
        Row("\"kN*s:6\"",       j.ToString("kN*s:6", Inv));

        Sub("Arithmetic");
        Impulse<Float128> a = new(tenN, Time<Float128>.FromSeconds(F(1)));
        Impulse<Float128> b = new(tenN, Time<Float128>.FromSeconds(F(2)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Impulse<Float128>.Compare(a, b));
    }

    private static void DemoLuminousFlux()
    {
        Heading("LuminousFlux  (LuminousIntensity * SolidAngle)");
        LuminousFlux<Float128> lm = new(
            LuminousIntensity<Float128>.FromCandelas(F(100)),
            SolidAngle<Float128>.FromSteradians(F(1)));

        Sub("Construction (100 cd over 1 sr = 100 lm)");
        Row("Magnitude (lm)",   lm.Magnitude);

        Sub("Format (named SI alias 'lm' with prefixes)");
        Row("default",          lm.ToString());
        Row("\"lm:3\"",         lm.ToString("lm:3", Inv));
        Row("\"klm:3\"",        lm.ToString("klm:3", Inv));
        Row("\"mlm:3\"",        lm.ToString("mlm:3", Inv));
        Row("\"cd*sr:3\"",      lm.ToString("cd*sr:3", Inv));

        Sub("Arithmetic");
        LuminousFlux<Float128> a = new(LuminousIntensity<Float128>.FromCandelas(F(50)), SolidAngle<Float128>.FromSteradians(F(1)));
        LuminousFlux<Float128> b = new(LuminousIntensity<Float128>.FromCandelas(F(150)), SolidAngle<Float128>.FromSteradians(F(1)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    LuminousFlux<Float128>.Compare(a, b));
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

    private static void DemoMagneticFlux()
    {
        Heading("MagneticFlux  (ElectricPotential * Time)");
        Force<Float128> oneNewton = new(
            Mass<Float128>.FromKilograms(F(1)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));
        Energy<Float128> oneJoule = new(oneNewton, Distance<Float128>.FromMeters(F(1)));
        ElectricCharge<Float128> oneCoulomb = new(Current<Float128>.FromAmperes(F(1)), Time<Float128>.FromSeconds(F(1)));
        ElectricPotential<Float128> oneVolt = new(oneJoule, oneCoulomb);

        MagneticFlux<Float128> wb = new(oneVolt, Time<Float128>.FromSeconds(F(1)));

        Sub("Construction (1 V x 1 s = 1 Wb)");
        Row("Magnitude (Wb)",   wb.Magnitude);

        Sub("Format (named SI alias 'Wb' with prefixes)");
        Row("default",          wb.ToString());
        Row("\"Wb:3\"",         wb.ToString("Wb:3", Inv));
        Row("\"mWb:3\"",        wb.ToString("mWb:3", Inv));
        Row("\"uWb:3\"",        wb.ToString("uWb:3", Inv));   // µWb rendering

        Sub("Arithmetic");
        MagneticFlux<Float128> a = new(oneVolt, Time<Float128>.FromSeconds(F(2)));
        MagneticFlux<Float128> b = new(oneVolt, Time<Float128>.FromSeconds(F(5)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    MagneticFlux<Float128>.Compare(a, b));
    }

    private static void DemoMassFlowRate()
    {
        Heading("MassFlowRate  (Mass / Time)");
        MassFlowRate<Float128> rate = new(
            Mass<Float128>.FromKilograms(F(60)),
            Time<Float128>.FromMinutes(F(1)));

        Sub("Construction (60 kg / 1 min = 1 kg/s)");
        Row("Magnitude (kg/s)", rate.Magnitude);

        Sub("Format");
        Row("default",          rate.ToString());
        Row("\"kg/s:3\"",       rate.ToString("kg/s:3", Inv));
        Row("\"kg/min:3\"",     rate.ToString("kg/min:3", Inv));
        Row("\"lb/h:3\"",       rate.ToString("lb/h:3", Inv));

        Sub("Arithmetic");
        MassFlowRate<Float128> a = new(Mass<Float128>.FromKilograms(F(10)), Time<Float128>.FromSeconds(F(1)));
        MassFlowRate<Float128> b = new(Mass<Float128>.FromKilograms(F(30)), Time<Float128>.FromSeconds(F(1)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    MassFlowRate<Float128>.Compare(a, b));
    }

    private static void DemoMolarConcentration()
    {
        Heading("MolarConcentration  (AmountOfSubstance / Volume)");
        MolarConcentration<Float128> c = new(
            AmountOfSubstance<Float128>.FromMoles(F(1)),
            Volume<Float128>.FromLiters(F(1)));

        Sub("Construction (1 mol / 1 L)");
        Row("Magnitude",        c.Magnitude);

        Sub("Format");
        Row("default",          c.ToString());
        Row("\"mol/L:3\"",      c.ToString("mol/L:3", Inv));
        Row("\"mmol/L:3\"",     c.ToString("mmol/L:3", Inv));

        Sub("Arithmetic");
        MolarConcentration<Float128> a = new(AmountOfSubstance<Float128>.FromMoles(F(2)), Volume<Float128>.FromLiters(F(1)));
        MolarConcentration<Float128> b = new(AmountOfSubstance<Float128>.FromMoles(F(5)), Volume<Float128>.FromLiters(F(1)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    MolarConcentration<Float128>.Compare(a, b));
    }

    private static void DemoMolarMass()
    {
        Heading("MolarMass  (Mass / AmountOfSubstance)");
        MolarMass<Float128> water = new(
            Mass<Float128>.FromGrams(F("18.015")),
            AmountOfSubstance<Float128>.FromMoles(F(1)));

        Sub("Construction (water 18.015 g/mol)");
        Row("Magnitude (kg/mol)", water.Magnitude);

        Sub("Format");
        Row("default",          water.ToString());
        Row("\"kg/mol:6\"",     water.ToString("kg/mol:6", Inv));
        Row("\"g/mol:3\"",      water.ToString("g/mol:3", Inv));

        Sub("Arithmetic");
        MolarMass<Float128> a = new(Mass<Float128>.FromGrams(F(10)), AmountOfSubstance<Float128>.FromMoles(F(1)));
        MolarMass<Float128> b = new(Mass<Float128>.FromGrams(F(20)), AmountOfSubstance<Float128>.FromMoles(F(1)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    MolarMass<Float128>.Compare(a, b));
    }

    private static void DemoMomentum()
    {
        Heading("Momentum  (Mass * Speed)");
        Momentum<Float128> p = new(
            Mass<Float128>.FromKilograms(F(2)),
            new Speed<Float128>(Distance<Float128>.FromMeters(F(3)), Time<Float128>.FromSeconds(F(1))));

        Sub("Construction (2 kg x 3 m/s = 6 kg*m/s)");
        Row("Magnitude",        p.Magnitude);

        Sub("Format");
        Row("default",          p.ToString());

        Sub("Arithmetic");
        Momentum<Float128> a = new(Mass<Float128>.FromKilograms(F(1)),
            new Speed<Float128>(Distance<Float128>.FromMeters(F(2)), Time<Float128>.FromSeconds(F(1))));
        Momentum<Float128> b = new(Mass<Float128>.FromKilograms(F(1)),
            new Speed<Float128>(Distance<Float128>.FromMeters(F(5)), Time<Float128>.FromSeconds(F(1))));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Momentum<Float128>.Compare(a, b));
    }

    private static void DemoPower()
    {
        Heading("Power  (Energy / Time)");
        Force<Float128> oneN = new(
            Mass<Float128>.FromKilograms(F(1)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));
        Energy<Float128> hundredJoules = new(oneN, Distance<Float128>.FromMeters(F(100)));

        Power<Float128> w = new(hundredJoules, Time<Float128>.FromSeconds(F(10)));

        Sub("Construction (100 J / 10 s = 10 W)");
        Row("Magnitude (W)",    w.Magnitude);

        Sub("Format (named SI alias 'W' with prefixes)");
        Row("default",          w.ToString());
        Row("\"W:3\"",          w.ToString("W:3", Inv));
        Row("\"kW:3\"",         w.ToString("kW:3", Inv));
        Row("\"MW:9\"",         w.ToString("MW:9", Inv));
        Row("\"mW:3\"",         w.ToString("mW:3", Inv));

        Sub("Arithmetic");
        Power<Float128> a = new(hundredJoules, Time<Float128>.FromSeconds(F(50)));
        Power<Float128> b = new(hundredJoules, Time<Float128>.FromSeconds(F(25)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Power<Float128>.Compare(a, b));
    }

    private static void DemoPressure()
    {
        Heading("Pressure  (Force / Area)");
        Force<Float128> oneN = new(
            Mass<Float128>.FromKilograms(F(1)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));

        Pressure<Float128> pa = new(oneN, Area<Float128>.FromSquareMeters(F(1)));

        Sub("Construction (1 N / 1 m² = 1 Pa)");
        Row("Magnitude (Pa)",   pa.Magnitude);

        Sub("Format (named SI alias 'Pa' with prefixes)");
        Row("default",          pa.ToString());
        Row("\"Pa:3\"",         pa.ToString("Pa:3", Inv));
        Row("\"hPa:3\"",        pa.ToString("hPa:3", Inv));      // weather convention
        Row("\"kPa:3\"",        pa.ToString("kPa:3", Inv));
        Row("\"MPa:9\"",        pa.ToString("MPa:9", Inv));

        Sub("Arithmetic");
        Pressure<Float128> a = new(oneN, Area<Float128>.FromSquareMeters(F(2)));
        Pressure<Float128> b = new(oneN, Area<Float128>.FromSquareMeters(F(4)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("a - b",        a.Subtract(b).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Pressure<Float128>.Compare(a, b));
    }

    private static void DemoSpeed()
    {
        Heading("Speed  (Distance / Time)");
        Speed<Float128> highway = new(
            Distance<Float128>.FromKilometers(F(110)),
            Time<Float128>.FromHours(F(1)));

        Sub("Construction (110 km/h)");
        Row("Magnitude (m/s)",  highway.Magnitude);

        Sub("Format");
        Row("default",          highway.ToString());
        Row("\"m/s:3\"",        highway.ToString("m/s:3", Inv));
        Row("\"km/h:3\"",       highway.ToString("km/h:3", Inv));
        Row("\"mi/h:3\"",       highway.ToString("mi/h:3", Inv));
        Row("\"nmi/h:3\"",      highway.ToString("nmi/h:3", Inv));
        Row("de-DE km/h:3",     highway.ToString("km/h:3", German));

        Sub("Arithmetic");
        Speed<Float128> a = new(Distance<Float128>.FromMeters(F(10)), Time<Float128>.FromSeconds(F(1)));
        Speed<Float128> b = new(Distance<Float128>.FromMeters(F(25)), Time<Float128>.FromSeconds(F(1)));
        Row("a + b (m/s)",  a.Add(b).Magnitude);
        Row("b - a (m/s)",  b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Speed<Float128>.Compare(a, b));
    }

    private static void DemoTorque()
    {
        Heading("Torque  (Force * Distance)");
        Force<Float128> tenN = new(
            Mass<Float128>.FromKilograms(F(10)),
            new Acceleration<Float128>(
                new Speed<Float128>(Distance<Float128>.FromMeters(F(1)), Time<Float128>.FromSeconds(F(1))),
                Time<Float128>.FromSeconds(F(1))));

        Torque<Float128> tau = new(tenN, Distance<Float128>.FromMeters(F("0.5")));

        Sub("Construction (10 N x 0.5 m = 5 N*m)");
        Row("Magnitude (N*m)",  tau.Magnitude);

        Sub("Format (compound default cascades through Force 'N' alias; intentionally not 'J' for torque)");
        Row("default",          tau.ToString());
        Row("\"N*m:3\"",        tau.ToString("N*m:3", Inv));
        Row("\"kN*m:6\"",       tau.ToString("kN*m:6", Inv));

        Sub("Arithmetic");
        Torque<Float128> a = new(tenN, Distance<Float128>.FromMeters(F(1)));
        Torque<Float128> b = new(tenN, Distance<Float128>.FromMeters(F(3)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    Torque<Float128>.Compare(a, b));
    }

    private static void DemoVolumetricFlowRate()
    {
        Heading("VolumetricFlowRate  (Volume / Time)");
        VolumetricFlowRate<Float128> q = new(
            Volume<Float128>.FromLiters(F(2)),
            Time<Float128>.FromSeconds(F(1)));

        Sub("Construction (2 L / 1 s)");
        Row("Magnitude (m³/s)", q.Magnitude);

        Sub("Format");
        Row("default",          q.ToString());
        Row("\"cum/s:6\"",      q.ToString("cum/s:6", Inv));
        Row("\"L/s:3\"",        q.ToString("L/s:3", Inv));
        Row("\"L/min:3\"",      q.ToString("L/min:3", Inv));

        Sub("Arithmetic");
        VolumetricFlowRate<Float128> a = new(Volume<Float128>.FromLiters(F(1)), Time<Float128>.FromSeconds(F(1)));
        VolumetricFlowRate<Float128> b = new(Volume<Float128>.FromLiters(F(3)), Time<Float128>.FromSeconds(F(1)));
        Row("a + b",        a.Add(b).Magnitude);
        Row("b - a",        b.Subtract(a).Magnitude);

        Sub("Equality / Comparison");
        Row("a == b",           a.Equals(b));
        Row("Compare(a, b)",    VolumetricFlowRate<Float128>.Compare(a, b));
    }
}
