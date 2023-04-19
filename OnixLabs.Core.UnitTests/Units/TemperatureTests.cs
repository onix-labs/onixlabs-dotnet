// Copyright 2020-2023 ONIXLabs
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
using OnixLabs.Core.Units;
using Xunit;

namespace OnixLabs.Core.UnitTests.Units;

public sealed class TemperatureTests
{
    [Theory(DisplayName = "Temperature.FromKelvin should produce the expected conversion results")]
    [InlineData(-1000, -1273.15, 2059.73, -2259.67, -420.14, -1018.52, -1800)]
    [InlineData(-100, -373.15, 709.72, -639.67, -123.14, -298.52, -180)]
    [InlineData(-90, -363.15, 694.72, -621.67, -119.84, -290.52, -162)]
    [InlineData(-80, -353.15, 679.72, -603.67, -116.54, -282.52, -144)]
    [InlineData(-70, -343.15, 664.72, -585.67, -113.24, -274.52, -126)]
    [InlineData(-60, -333.15, 649.72, -567.67, -109.94, -266.52, -108)]
    [InlineData(-50, -323.15, 634.72, -549.67, -106.64, -258.52, -90)]
    [InlineData(-40, -313.15, 619.72, -531.67, -103.34, -250.52, -72)]
    [InlineData(-30, -303.15, 604.72, -513.67, -100.04, -242.52, -54)]
    [InlineData(-20, -293.15, 589.72, -495.67, -96.74, -234.52, -36)]
    [InlineData(-10, -283.15, 574.72, -477.67, -93.44, -226.52, -18)]
    [InlineData(-9, -282.15, 573.22, -475.87, -93.11, -225.72, -16.2)]
    [InlineData(-8, -281.15, 571.72, -474.07, -92.78, -224.92, -14.4)]
    [InlineData(-7, -280.15, 570.22, -472.27, -92.45, -224.12, -12.6)]
    [InlineData(-6, -279.15, 568.72, -470.47, -92.12, -223.32, -10.8)]
    [InlineData(-5, -278.15, 567.22, -468.67, -91.79, -222.52, -9)]
    [InlineData(-4, -277.15, 565.72, -466.87, -91.46, -221.72, -7.2)]
    [InlineData(-3, -276.15, 564.22, -465.07, -91.13, -220.92, -5.4)]
    [InlineData(-2, -275.15, 562.72, -463.27, -90.8, -220.12, -3.6)]
    [InlineData(-1, -274.15, 561.22, -461.47, -90.47, -219.32, -1.8)]
    [InlineData(0, -273.15, 559.72, -459.67, -90.14, -218.52, 0)]
    [InlineData(1, -272.15, 558.22, -457.87, -89.81, -217.72, 1.8)]
    [InlineData(2, -271.15, 556.72, -456.07, -89.48, -216.92, 3.6)]
    [InlineData(3, -270.15, 555.22, -454.27, -89.15, -216.12, 5.4)]
    [InlineData(4, -269.15, 553.72, -452.47, -88.82, -215.32, 7.2)]
    [InlineData(5, -268.15, 552.22, -450.67, -88.49, -214.52, 9)]
    [InlineData(6, -267.15, 550.72, -448.87, -88.16, -213.72, 10.8)]
    [InlineData(7, -266.15, 549.22, -447.07, -87.83, -212.92, 12.6)]
    [InlineData(8, -265.15, 547.72, -445.27, -87.5, -212.12, 14.4)]
    [InlineData(9, -264.15, 546.22, -443.47, -87.17, -211.32, 16.2)]
    [InlineData(10, -263.15, 544.72, -441.67, -86.84, -210.52, 18)]
    [InlineData(20, -253.15, 529.72, -423.67, -83.54, -202.52, 36)]
    [InlineData(30, -243.15, 514.72, -405.67, -80.24, -194.52, 54)]
    [InlineData(40, -233.15, 499.72, -387.67, -76.94, -186.52, 72)]
    [InlineData(50, -223.15, 484.72, -369.67, -73.64, -178.52, 90)]
    [InlineData(60, -213.15, 469.72, -351.67, -70.34, -170.52, 108)]
    [InlineData(70, -203.15, 454.72, -333.67, -67.04, -162.52, 126)]
    [InlineData(80, -193.15, 439.72, -315.67, -63.74, -154.52, 144)]
    [InlineData(90, -183.15, 424.72, -297.67, -60.44, -146.52, 162)]
    [InlineData(100, -173.15, 409.72, -279.67, -57.14, -138.52, 180)]
    [InlineData(1000, 726.85, -940.28, 1340.33, 239.86, 581.48, 1800)]
    public void TemperatureFromKelvinShouldProduceTheExpectedConversionResults(
        double kelvin,
        double celsius,
        double delisle,
        double fahrenheit,
        double newton,
        double reaumur,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromKelvin(kelvin);

        // Assert
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromCelsius should produce the expected conversion results")]
    [InlineData(-1000, -726.85, 1650, -1768, -330, -800, -1308.33)]
    [InlineData(-100, 173.15, 300, -148, -33, -80, 311.67)]
    [InlineData(-90, 183.15, 285, -130, -29.7, -72, 329.67)]
    [InlineData(-80, 193.15, 270, -112, -26.4, -64, 347.67)]
    [InlineData(-70, 203.15, 255, -94, -23.1, -56, 365.67)]
    [InlineData(-60, 213.15, 240, -76, -19.8, -48, 383.67)]
    [InlineData(-50, 223.15, 225, -58, -16.5, -40, 401.67)]
    [InlineData(-40, 233.15, 210, -40, -13.2, -32, 419.67)]
    [InlineData(-30, 243.15, 195, -22, -9.9, -24, 437.67)]
    [InlineData(-20, 253.15, 180, -4, -6.6, -16, 455.67)]
    [InlineData(-10, 263.15, 165, 14, -3.3, -8, 473.67)]
    [InlineData(-9, 264.15, 163.5, 15.8, -2.97, -7.2, 475.47)]
    [InlineData(-8, 265.15, 162, 17.6, -2.64, -6.4, 477.27)]
    [InlineData(-7, 266.15, 160.5, 19.4, -2.31, -5.6, 479.07)]
    [InlineData(-6, 267.15, 159, 21.2, -1.98, -4.8, 480.87)]
    [InlineData(-5, 268.15, 157.5, 23, -1.65, -4, 482.67)]
    [InlineData(-4, 269.15, 156, 24.8, -1.32, -3.2, 484.47)]
    [InlineData(-3, 270.15, 154.5, 26.6, -0.99, -2.4, 486.27)]
    [InlineData(-2, 271.15, 153, 28.4, -0.66, -1.6, 488.07)]
    [InlineData(-1, 272.15, 151.5, 30.2, -0.33, -0.8, 489.87)]
    [InlineData(0, 273.15, 150, 32, 0, 0, 491.67)]
    [InlineData(1, 274.15, 148.5, 33.8, 0.33, 0.8, 493.47)]
    [InlineData(2, 275.15, 147, 35.6, 0.66, 1.6, 495.27)]
    [InlineData(3, 276.15, 145.5, 37.4, 0.99, 2.4, 497.07)]
    [InlineData(4, 277.15, 144, 39.2, 1.32, 3.2, 498.87)]
    [InlineData(5, 278.15, 142.5, 41, 1.65, 4, 500.67)]
    [InlineData(6, 279.15, 141, 42.8, 1.98, 4.8, 502.47)]
    [InlineData(7, 280.15, 139.5, 44.6, 2.31, 5.6, 504.27)]
    [InlineData(8, 281.15, 138, 46.4, 2.64, 6.4, 506.07)]
    [InlineData(9, 282.15, 136.5, 48.2, 2.97, 7.2, 507.87)]
    [InlineData(10, 283.15, 135, 50, 3.3, 8, 509.67)]
    [InlineData(20, 293.15, 120, 68, 6.6, 16, 527.67)]
    [InlineData(30, 303.15, 105, 86, 9.9, 24, 545.67)]
    [InlineData(40, 313.15, 90, 104, 13.2, 32, 563.67)]
    [InlineData(50, 323.15, 75, 122, 16.5, 40, 581.67)]
    [InlineData(60, 333.15, 60, 140, 19.8, 48, 599.67)]
    [InlineData(70, 343.15, 45, 158, 23.1, 56, 617.67)]
    [InlineData(80, 353.15, 30, 176, 26.4, 64, 635.67)]
    [InlineData(90, 363.15, 15, 194, 29.7, 72, 653.67)]
    [InlineData(100, 373.15, 0, 212, 33, 80, 671.67)]
    [InlineData(1000, 1273.15, -1350, 1832, 330, 800, 2291.67)]
    public void TemperatureFromCelsiusShouldProduceTheExpectedConversionResults(
        double celsius,
        double kelvin,
        double delisle,
        double fahrenheit,
        double newton,
        double reaumur,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromCelsius(celsius);

        // Assert
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromDelisle should produce the expected conversion results")]
    [InlineData(-1000, 1039.82, 766.67, 1412, 253, 613.33, 1871.67)]
    [InlineData(-100, 439.82, 166.67, 332, 55, 133.33, 791.67)]
    [InlineData(-90, 433.15, 160, 320, 52.8, 128, 779.67)]
    [InlineData(-80, 426.48, 153.33, 308, 50.6, 122.67, 767.67)]
    [InlineData(-70, 419.82, 146.67, 296, 48.4, 117.33, 755.67)]
    [InlineData(-60, 413.15, 140, 284, 46.2, 112, 743.67)]
    [InlineData(-50, 406.48, 133.33, 272, 44, 106.67, 731.67)]
    [InlineData(-40, 399.82, 126.67, 260, 41.8, 101.33, 719.67)]
    [InlineData(-30, 393.15, 120, 248, 39.6, 96, 707.67)]
    [InlineData(-20, 386.48, 113.33, 236, 37.4, 90.67, 695.67)]
    [InlineData(-10, 379.82, 106.67, 224, 35.2, 85.33, 683.67)]
    [InlineData(-9, 379.15, 106, 222.8, 34.98, 84.8, 682.47)]
    [InlineData(-8, 378.48, 105.33, 221.6, 34.76, 84.27, 681.27)]
    [InlineData(-7, 377.82, 104.67, 220.4, 34.54, 83.73, 680.07)]
    [InlineData(-6, 377.15, 104, 219.2, 34.32, 83.2, 678.87)]
    [InlineData(-5, 376.48, 103.33, 218, 34.1, 82.67, 677.67)]
    [InlineData(-4, 375.82, 102.67, 216.8, 33.88, 82.13, 676.47)]
    [InlineData(-3, 375.15, 102, 215.6, 33.66, 81.6, 675.27)]
    [InlineData(-2, 374.48, 101.33, 214.4, 33.44, 81.07, 674.07)]
    [InlineData(-1, 373.82, 100.67, 213.2, 33.22, 80.53, 672.87)]
    [InlineData(0, 373.15, 100, 212, 33, 80, 671.67)]
    [InlineData(1, 372.48, 99.33, 210.8, 32.78, 79.47, 670.47)]
    [InlineData(2, 371.82, 98.67, 209.6, 32.56, 78.93, 669.27)]
    [InlineData(3, 371.15, 98, 208.4, 32.34, 78.4, 668.07)]
    [InlineData(4, 370.48, 97.33, 207.2, 32.12, 77.87, 666.87)]
    [InlineData(5, 369.82, 96.67, 206, 31.9, 77.33, 665.67)]
    [InlineData(6, 369.15, 96, 204.8, 31.68, 76.8, 664.47)]
    [InlineData(7, 368.48, 95.33, 203.6, 31.46, 76.27, 663.27)]
    [InlineData(8, 367.82, 94.67, 202.4, 31.24, 75.73, 662.07)]
    [InlineData(9, 367.15, 94, 201.2, 31.02, 75.2, 660.87)]
    [InlineData(10, 366.48, 93.33, 200, 30.8, 74.67, 659.67)]
    [InlineData(20, 359.82, 86.67, 188, 28.6, 69.33, 647.67)]
    [InlineData(30, 353.15, 80, 176, 26.4, 64, 635.67)]
    [InlineData(40, 346.48, 73.33, 164, 24.2, 58.67, 623.67)]
    [InlineData(50, 339.82, 66.67, 152, 22, 53.33, 611.67)]
    [InlineData(60, 333.15, 60, 140, 19.8, 48, 599.67)]
    [InlineData(70, 326.48, 53.33, 128, 17.6, 42.67, 587.67)]
    [InlineData(80, 319.82, 46.67, 116, 15.4, 37.33, 575.67)]
    [InlineData(90, 313.15, 40, 104, 13.2, 32, 563.67)]
    [InlineData(100, 306.48, 33.33, 92, 11, 26.67, 551.67)]
    [InlineData(1000, -293.52, -566.67, -988, -187, -453.33, -528.33)]
    public void TemperatureFromDelisleShouldProduceTheExpectedConversionResults(
        double delisle,
        double kelvin,
        double celsius,
        double fahrenheit,
        double newton,
        double reaumur,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromDelisle(delisle);

        // Assert
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromFahrenheit should produce the expected conversion results")]
    [InlineData(-1000, -300.18, -573.33, 1010, -189.2, -458.67, -540.33)]
    [InlineData(-100, 199.82, -73.33, 260, -24.2, -58.67, 359.67)]
    [InlineData(-90, 205.37, -67.78, 251.67, -22.37, -54.22, 369.67)]
    [InlineData(-80, 210.93, -62.22, 243.33, -20.53, -49.78, 379.67)]
    [InlineData(-70, 216.48, -56.67, 235, -18.7, -45.33, 389.67)]
    [InlineData(-60, 222.04, -51.11, 226.67, -16.87, -40.89, 399.67)]
    [InlineData(-50, 227.59, -45.56, 218.33, -15.03, -36.44, 409.67)]
    [InlineData(-40, 233.15, -40, 210, -13.2, -32, 419.67)]
    [InlineData(-30, 238.71, -34.44, 201.67, -11.37, -27.56, 429.67)]
    [InlineData(-20, 244.26, -28.89, 193.33, -9.53, -23.11, 439.67)]
    [InlineData(-10, 249.82, -23.33, 185, -7.7, -18.67, 449.67)]
    [InlineData(-9, 250.37, -22.78, 184.17, -7.52, -18.22, 450.67)]
    [InlineData(-8, 250.93, -22.22, 183.33, -7.33, -17.78, 451.67)]
    [InlineData(-7, 251.48, -21.67, 182.5, -7.15, -17.33, 452.67)]
    [InlineData(-6, 252.04, -21.11, 181.67, -6.97, -16.89, 453.67)]
    [InlineData(-5, 252.59, -20.56, 180.83, -6.78, -16.44, 454.67)]
    [InlineData(-4, 253.15, -20, 180, -6.6, -16, 455.67)]
    [InlineData(-3, 253.71, -19.44, 179.17, -6.42, -15.56, 456.67)]
    [InlineData(-2, 254.26, -18.89, 178.33, -6.23, -15.11, 457.67)]
    [InlineData(-1, 254.82, -18.33, 177.5, -6.05, -14.67, 458.67)]
    [InlineData(-0, 255.37, -17.78, 176.67, -5.87, -14.22, 459.67)]
    [InlineData(1, 255.93, -17.22, 175.83, -5.68, -13.78, 460.67)]
    [InlineData(2, 256.48, -16.67, 175, -5.5, -13.33, 461.67)]
    [InlineData(3, 257.04, -16.11, 174.17, -5.32, -12.89, 462.67)]
    [InlineData(4, 257.59, -15.56, 173.33, -5.13, -12.44, 463.67)]
    [InlineData(5, 258.15, -15, 172.5, -4.95, -12, 464.67)]
    [InlineData(6, 258.71, -14.44, 171.67, -4.77, -11.56, 465.67)]
    [InlineData(7, 259.26, -13.89, 170.83, -4.58, -11.11, 466.67)]
    [InlineData(8, 259.82, -13.33, 170, -4.4, -10.67, 467.67)]
    [InlineData(9, 260.37, -12.78, 169.17, -4.22, -10.22, 468.67)]
    [InlineData(10, 260.93, -12.22, 168.33, -4.03, -9.78, 469.67)]
    [InlineData(20, 266.48, -6.67, 160, -2.2, -5.33, 479.67)]
    [InlineData(30, 272.04, -1.11, 151.67, -0.37, -0.89, 489.67)]
    [InlineData(40, 277.59, 4.44, 143.33, 1.47, 3.56, 499.67)]
    [InlineData(50, 283.15, 10, 135, 3.3, 8, 509.67)]
    [InlineData(60, 288.71, 15.56, 126.67, 5.13, 12.44, 519.67)]
    [InlineData(70, 294.26, 21.11, 118.33, 6.97, 16.89, 529.67)]
    [InlineData(80, 299.82, 26.67, 110, 8.8, 21.33, 539.67)]
    [InlineData(90, 305.37, 32.22, 101.67, 10.63, 25.78, 549.67)]
    [InlineData(100, 310.93, 37.78, 93.33, 12.47, 30.22, 559.67)]
    [InlineData(1000, 810.93, 537.78, -656.67, 177.47, 430.22, 1459.67)]
    public void TemperatureFromFahrenheitShouldProduceTheExpectedConversionResults(
        double fahrenheit,
        double kelvin,
        double celsius,
        double delisle,
        double newton,
        double reaumur,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromFahrenheit(fahrenheit);

        // Assert
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromNewton should produce the expected conversion results")]
    [InlineData(-1000, -2757.15, -3030.3, 4695.45, -5422.55, -2424.24, -4962.88)]
    [InlineData(-100, -29.88, -303.03, 604.55, -513.45, -242.42, -53.78)]
    [InlineData(-90, 0.42, -272.73, 559.09, -458.91, -218.18, 0.76)]
    [InlineData(-80, 30.73, -242.42, 513.64, -404.36, -193.94, 55.31)]
    [InlineData(-70, 61.03, -212.12, 468.18, -349.82, -169.7, 109.85)]
    [InlineData(-60, 91.33, -181.82, 422.73, -295.27, -145.45, 164.4)]
    [InlineData(-50, 121.63, -151.52, 377.27, -240.73, -121.21, 218.94)]
    [InlineData(-40, 151.94, -121.21, 331.82, -186.18, -96.97, 273.49)]
    [InlineData(-30, 182.24, -90.91, 286.36, -131.64, -72.73, 328.03)]
    [InlineData(-20, 212.54, -60.61, 240.91, -77.09, -48.48, 382.58)]
    [InlineData(-10, 242.85, -30.3, 195.45, -22.55, -24.24, 437.12)]
    [InlineData(-9, 245.88, -27.27, 190.91, -17.09, -21.82, 442.58)]
    [InlineData(-8, 248.91, -24.24, 186.36, -11.64, -19.39, 448.03)]
    [InlineData(-7, 251.94, -21.21, 181.82, -6.18, -16.97, 453.49)]
    [InlineData(-6, 254.97, -18.18, 177.27, -0.73, -14.55, 458.94)]
    [InlineData(-5, 258, -15.15, 172.73, 4.73, -12.12, 464.4)]
    [InlineData(-4, 261.03, -12.12, 168.18, 10.18, -9.7, 469.85)]
    [InlineData(-3, 264.06, -9.09, 163.64, 15.64, -7.27, 475.31)]
    [InlineData(-2, 267.09, -6.06, 159.09, 21.09, -4.85, 480.76)]
    [InlineData(-1, 270.12, -3.03, 154.55, 26.55, -2.42, 486.22)]
    [InlineData(0, 273.15, 0, 150, 32, 0, 491.67)]
    [InlineData(1, 276.18, 3.03, 145.45, 37.45, 2.42, 497.12)]
    [InlineData(2, 279.21, 6.06, 140.91, 42.91, 4.85, 502.58)]
    [InlineData(3, 282.24, 9.09, 136.36, 48.36, 7.27, 508.03)]
    [InlineData(4, 285.27, 12.12, 131.82, 53.82, 9.7, 513.49)]
    [InlineData(5, 288.3, 15.15, 127.27, 59.27, 12.12, 518.94)]
    [InlineData(6, 291.33, 18.18, 122.73, 64.73, 14.55, 524.4)]
    [InlineData(7, 294.36, 21.21, 118.18, 70.18, 16.97, 529.85)]
    [InlineData(8, 297.39, 24.24, 113.64, 75.64, 19.39, 535.31)]
    [InlineData(9, 300.42, 27.27, 109.09, 81.09, 21.82, 540.76)]
    [InlineData(10, 303.45, 30.3, 104.55, 86.55, 24.24, 546.22)]
    [InlineData(20, 333.76, 60.61, 59.09, 141.09, 48.48, 600.76)]
    [InlineData(30, 364.06, 90.91, 13.64, 195.64, 72.73, 655.31)]
    [InlineData(40, 394.36, 121.21, -31.82, 250.18, 96.97, 709.85)]
    [InlineData(50, 424.67, 151.52, -77.27, 304.73, 121.21, 764.4)]
    [InlineData(60, 454.97, 181.82, -122.73, 359.27, 145.45, 818.94)]
    [InlineData(70, 485.27, 212.12, -168.18, 413.82, 169.7, 873.49)]
    [InlineData(80, 515.57, 242.42, -213.64, 468.36, 193.94, 928.03)]
    [InlineData(90, 545.88, 272.73, -259.09, 522.91, 218.18, 982.58)]
    [InlineData(100, 576.18, 303.03, -304.55, 577.45, 242.42, 1037.12)]
    [InlineData(1000, 3303.45, 3030.3, -4395.45, 5486.55, 2424.24, 5946.22)]
    public void TemperatureFromNewtonShouldProduceTheExpectedConversionResults(
        double newton,
        double kelvin,
        double celsius,
        double delisle,
        double fahrenheit,
        double reaumur,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromNewton(newton);

        // Assert
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromReaumur should produce the expected conversion results")]
    [InlineData(-1000, -976.85, -1250, 2025, -2218, -412.5, -1758.33)]
    [InlineData(-100, 148.15, -125, 337.5, -193, -41.25, 266.67)]
    [InlineData(-90, 160.65, -112.5, 318.75, -170.5, -37.12, 289.17)]
    [InlineData(-80, 173.15, -100, 300, -148, -33, 311.67)]
    [InlineData(-70, 185.65, -87.5, 281.25, -125.5, -28.88, 334.17)]
    [InlineData(-60, 198.15, -75, 262.5, -103, -24.75, 356.67)]
    [InlineData(-50, 210.65, -62.5, 243.75, -80.5, -20.62, 379.17)]
    [InlineData(-40, 223.15, -50, 225, -58, -16.5, 401.67)]
    [InlineData(-30, 235.65, -37.5, 206.25, -35.5, -12.38, 424.17)]
    [InlineData(-20, 248.15, -25, 187.5, -13, -8.25, 446.67)]
    [InlineData(-10, 260.65, -12.5, 168.75, 9.5, -4.12, 469.17)]
    [InlineData(-9, 261.9, -11.25, 166.88, 11.75, -3.71, 471.42)]
    [InlineData(-8, 263.15, -10, 165, 14, -3.3, 473.67)]
    [InlineData(-7, 264.4, -8.75, 163.12, 16.25, -2.89, 475.92)]
    [InlineData(-6, 265.65, -7.5, 161.25, 18.5, -2.48, 478.17)]
    [InlineData(-5, 266.9, -6.25, 159.38, 20.75, -2.06, 480.42)]
    [InlineData(-4, 268.15, -5, 157.5, 23, -1.65, 482.67)]
    [InlineData(-3, 269.4, -3.75, 155.62, 25.25, -1.24, 484.92)]
    [InlineData(-2, 270.65, -2.5, 153.75, 27.5, -0.82, 487.17)]
    [InlineData(-1, 271.9, -1.25, 151.88, 29.75, -0.41, 489.42)]
    [InlineData(0, 273.15, 0, 150, 32, 0, 491.67)]
    [InlineData(1, 274.4, 1.25, 148.12, 34.25, 0.41, 493.92)]
    [InlineData(2, 275.65, 2.5, 146.25, 36.5, 0.82, 496.17)]
    [InlineData(3, 276.9, 3.75, 144.38, 38.75, 1.24, 498.42)]
    [InlineData(4, 278.15, 5, 142.5, 41, 1.65, 500.67)]
    [InlineData(5, 279.4, 6.25, 140.62, 43.25, 2.06, 502.92)]
    [InlineData(6, 280.65, 7.5, 138.75, 45.5, 2.48, 505.17)]
    [InlineData(7, 281.9, 8.75, 136.88, 47.75, 2.89, 507.42)]
    [InlineData(8, 283.15, 10, 135, 50, 3.3, 509.67)]
    [InlineData(9, 284.4, 11.25, 133.12, 52.25, 3.71, 511.92)]
    [InlineData(10, 285.65, 12.5, 131.25, 54.5, 4.12, 514.17)]
    [InlineData(20, 298.15, 25, 112.5, 77, 8.25, 536.67)]
    [InlineData(30, 310.65, 37.5, 93.75, 99.5, 12.38, 559.17)]
    [InlineData(40, 323.15, 50, 75, 122, 16.5, 581.67)]
    [InlineData(50, 335.65, 62.5, 56.25, 144.5, 20.62, 604.17)]
    [InlineData(60, 348.15, 75, 37.5, 167, 24.75, 626.67)]
    [InlineData(70, 360.65, 87.5, 18.75, 189.5, 28.88, 649.17)]
    [InlineData(80, 373.15, 100, 0, 212, 33, 671.67)]
    [InlineData(90, 385.65, 112.5, -18.75, 234.5, 37.12, 694.17)]
    [InlineData(100, 398.15, 125, -37.5, 257, 41.25, 716.67)]
    [InlineData(1000, 1523.15, 1250, -1725, 2282, 412.5, 2741.67)]
    public void TemperatureFromReaumurShouldProduceTheExpectedConversionResults(
        double reaumur,
        double kelvin,
        double celsius,
        double delisle,
        double fahrenheit,
        double newton,
        double rankine)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromReaumur(reaumur);

        // Assert
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
    }

    [Theory(DisplayName = "Temperature.FromRankine should produce the expected conversion results")]
    [InlineData(-1000, -555.56, -828.71, 1393.06, -1459.67, -273.47, -662.96)]
    [InlineData(-100, -55.56, -328.71, 643.06, -559.67, -108.47, -262.96)]
    [InlineData(-90, -50, -323.15, 634.72, -549.67, -106.64, -258.52)]
    [InlineData(-80, -44.44, -317.59, 626.39, -539.67, -104.81, -254.08)]
    [InlineData(-70, -38.89, -312.04, 618.06, -529.67, -102.97, -249.63)]
    [InlineData(-60, -33.33, -306.48, 609.72, -519.67, -101.14, -245.19)]
    [InlineData(-50, -27.78, -300.93, 601.39, -509.67, -99.31, -240.74)]
    [InlineData(-40, -22.22, -295.37, 593.06, -499.67, -97.47, -236.3)]
    [InlineData(-30, -16.67, -289.82, 584.72, -489.67, -95.64, -231.85)]
    [InlineData(-20, -11.11, -284.26, 576.39, -479.67, -93.81, -227.41)]
    [InlineData(-10, -5.56, -278.71, 568.06, -469.67, -91.97, -222.96)]
    [InlineData(-9, -5, -278.15, 567.22, -468.67, -91.79, -222.52)]
    [InlineData(-8, -4.44, -277.59, 566.39, -467.67, -91.61, -222.08)]
    [InlineData(-7, -3.89, -277.04, 565.56, -466.67, -91.42, -221.63)]
    [InlineData(-6, -3.33, -276.48, 564.72, -465.67, -91.24, -221.19)]
    [InlineData(-5, -2.78, -275.93, 563.89, -464.67, -91.06, -220.74)]
    [InlineData(-4, -2.22, -275.37, 563.06, -463.67, -90.87, -220.3)]
    [InlineData(-3, -1.67, -274.82, 562.22, -462.67, -90.69, -219.85)]
    [InlineData(-2, -1.11, -274.26, 561.39, -461.67, -90.51, -219.41)]
    [InlineData(-1, -0.56, -273.71, 560.56, -460.67, -90.32, -218.96)]
    [InlineData(0, 0, -273.15, 559.72, -459.67, -90.14, -218.52)]
    [InlineData(1, 0.56, -272.59, 558.89, -458.67, -89.96, -218.08)]
    [InlineData(2, 1.11, -272.04, 558.06, -457.67, -89.77, -217.63)]
    [InlineData(3, 1.67, -271.48, 557.22, -456.67, -89.59, -217.19)]
    [InlineData(4, 2.22, -270.93, 556.39, -455.67, -89.41, -216.74)]
    [InlineData(5, 2.78, -270.37, 555.56, -454.67, -89.22, -216.3)]
    [InlineData(6, 3.33, -269.82, 554.72, -453.67, -89.04, -215.85)]
    [InlineData(7, 3.89, -269.26, 553.89, -452.67, -88.86, -215.41)]
    [InlineData(8, 4.44, -268.71, 553.06, -451.67, -88.67, -214.96)]
    [InlineData(9, 5, -268.15, 552.22, -450.67, -88.49, -214.52)]
    [InlineData(10, 5.56, -267.59, 551.39, -449.67, -88.31, -214.08)]
    [InlineData(20, 11.11, -262.04, 543.06, -439.67, -86.47, -209.63)]
    [InlineData(30, 16.67, -256.48, 534.72, -429.67, -84.64, -205.19)]
    [InlineData(40, 22.22, -250.93, 526.39, -419.67, -82.81, -200.74)]
    [InlineData(50, 27.78, -245.37, 518.06, -409.67, -80.97, -196.3)]
    [InlineData(60, 33.33, -239.82, 509.72, -399.67, -79.14, -191.85)]
    [InlineData(70, 38.89, -234.26, 501.39, -389.67, -77.31, -187.41)]
    [InlineData(80, 44.44, -228.71, 493.06, -379.67, -75.47, -182.96)]
    [InlineData(90, 50, -223.15, 484.72, -369.67, -73.64, -178.52)]
    [InlineData(100, 55.56, -217.59, 476.39, -359.67, -71.81, -174.08)]
    [InlineData(1000, 555.56, 282.41, -273.61, 540.33, 93.19, 225.92)]
    public void TemperatureFromRankineShouldProduceTheExpectedConversionResults(
        double rankine,
        double kelvin,
        double celsius,
        double delisle,
        double fahrenheit,
        double newton,
        double reaumur)
    {
        // Arrange / Act
        Temperature<double> temperature = Temperature.FromRankine(rankine);

        // Assert
        Assert.Equal(rankine, Math.Round(temperature.Rankine, 2));
        Assert.Equal(kelvin, Math.Round(temperature.Kelvin, 2));
        Assert.Equal(celsius, Math.Round(temperature.Celsius, 2));
        Assert.Equal(delisle, Math.Round(temperature.Delisle, 2));
        Assert.Equal(fahrenheit, Math.Round(temperature.Fahrenheit, 2));
        Assert.Equal(newton, Math.Round(temperature.Newton, 2));
        Assert.Equal(reaumur, Math.Round(temperature.Reaumur, 2));
    }
}
