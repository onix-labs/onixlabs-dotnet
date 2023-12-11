// Copyright © 2020 ONIXLabs
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

using System.Collections.Generic;
using System.Reflection;
using OnixLabs.Core.Numerics;
using Xunit;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticModTests
{
    [ModData]
    [Theory(DisplayName = "BigDecimal.Mod should produce the expected result")]
    public void BigDecimalModShouldProduceExpectedResult(decimal left, decimal right)
    {
        // Given
        decimal expected = left % right;

        // When
        BigDecimal actual = BigDecimal.Mod(left, right).Round(expected.Scale);

        // Then
        Assert.Equal(expected, actual);
    }

    private sealed class ModDataAttribute : DataAttribute
    {
        private static readonly (decimal Left, decimal Right)[] Data =
        [
            (-21.057950715364694m, 50.15772245657748m),
            (-35.55512677520725m, 36.83944549185064m),
            (-70.89067274309353m, 60.842380874872234m),
            (93.20369673954801m, -18.438823667919447m),
            (77.63611004051366m, -71.3100617598458m),
            (-66.08706907588629m, 59.128417540374215m),
            (-52.65043561712284m, 93.53051957894904m),
            (68.64299608031276m, -25.311313460434913m),
            (57.66422368867187m, -52.70224026829917m),
            (44.415798851361956m, -64.10629163706093m),
            (-79.31482075036986m, 48.2857721550754m),
            (46.1794759554348m, -90.90197819659339m),
            (-40.83607647768011m, 87.22330175705737m),
            (92.01506053830228m, -84.39602614794104m),
            (-53.415970331981846m, 89.40555399511894m),
            (14.654615101549751m, -49.45122171201204m),
            (56.31541267699364m, -1.2360922169676547m),
            (67.61601228241415m, -34.634760200453115m),
            (18.5714398264082m, -86.94457670556336m),
            (15.636106498813351m, -58.34667221154195m),
            (-19.448239797941614m, 33.68713561805943m),
            (-43.09711105601577m, 39.051622610875455m),
            (8.510880568462598m, -44.24317129425104m),
            (-34.81494088015593m, 2.7607740873018205m),
            (-44.314663980751554m, 3.8322895626382247m),
            (90.2481510895111m, -17.452449505821367m),
            (-83.89694672750358m, 67.70942207568969m),
            (-16.314882690127654m, 55.88132886440498m),
            (-41.21918505179709m, 59.5594686557538m),
            (-92.82138173634009m, 28.063327089990242m),
            (33.94296765136916m, -67.82045869819507m),
            (-80.58266942900954m, 37.61052345173397m),
            (-32.29071819931201m, 46.22625831348304m),
            (1.5680780000291716m, -60.65131184485409m),
            (15.29415697873987m, -51.810970331115136m),
            (32.781228104474415m, -5.5307865116745125m),
            (-46.03498751591657m, 70.64655251765643m),
            (-1.4889537163105482m, 3.19159644258582m),
            (-42.279155540864366m, 83.62443702903263m),
            (-30.82509388094753m, 61.61007248709234m),
            (-82.40759405503707m, 1.23514398755733m),
            (-99.41775956871145m, 49.25339973692972m),
            (-25.810173079465027m, 78.26432643062368m),
            (10.299950153197857m, -12.372147032372649m),
            (13.33266971627759m, -53.582799135745496m),
            (-57.4183626622371m, 82.29971404929121m),
            (-2.413166849219539m, 97.6418148737852m),
            (19.45393179061592m, -93.34427883655766m),
            (-87.75006773080365m, 31.949203226342547m),
            (37.07861649436121m, -37.451520053010526m),
            (61.54660822273414m, -40.182931066637195m),
            (45.016767919731436m, -28.73775639761209m),
            (25.672556821902948m, -33.49408802185144m),
            (-93.98855273503422m, 62.01168877564876m),
            (39.583812822887396m, -62.16087720685288m),
            (72.03428239150091m, -88.49412981507662m),
            (40.25085136506249m, -76.03345081381636m),
            (-66.14399575993066m, 85.39831737243031m),
            (-12.755990845303122m, 60.95824034652411m),
            (67.09158854647525m, -80.1774339951448m),
            (18.46505290314915m, -75.30231801189697m),
            (-81.53200808275913m, 99.0575518931603m),
            (-77.446287107583m, 7.536435304106992m),
            (-57.05595264973597m, 7.023500331289978m),
            (-13.215878965838213m, 3.0536823850534134m),
            (-98.08681511803533m, 83.47599747806721m),
            (39.64475488412539m, -59.84015894458287m),
            (-74.83890738574227m, 56.78630456130325m),
            (-14.534079364709262m, 13.767800734839442m),
            (39.725408701643126m, -87.38131677288636m),
            (-79.08813102103296m, 77.71182212513953m),
            (-55.34682116405493m, 74.75093931160852m),
            (57.25226622705778m, -91.39045305440423m),
            (12.609041361359075m, -45.261323382322956m),
            (-5.44880087454821m, 34.77745229439152m),
            (35.65048499159199m, -89.07565641303684m),
            (35.90292179118521m, -60.76098284842863m),
            (43.54582211098435m, -18.418463831127486m),
            (-28.4092797047515m, 29.995055588570317m),
            (22.65902136416178m, -70.20077244163747m),
            (44.49417275268852m, -99.88407846751616m),
            (-35.07222318997481m, 57.38489006314258m),
            (47.64720958518945m, -9.103786085411425m),
            (75.27039740873646m, -20.579437903558816m),
            (41.84129210506282m, -66.20000272726145m),
            (-39.55153899655177m, 32.82900743724837m),
            (-71.97800154576181m, 61.40489795428707m),
            (75.38502689576495m, -66.05294520882873m),
            (66.30981906215585m, -68.5632628420185m),
            (20.254983742418943m, -15.434237543482698m),
            (-11.346619566026483m, 45.24013683640595m),
            (-24.15528644288637m, 41.50458479760862m),
            (39.82380385953533m, -56.490341049856674m),
            (21.013509899757167m, -9.759807445077872m),
            (-83.5553475311269m, 26.70943280823166m),
            (-46.729824862258305m, 93.62548468645095m),
            (-51.14163470571271m, 61.44427403072144m),
            (-91.74429893620747m, 51.97312222523864m),
            (33.25653587484444m, -8.960460998222908m),
            (-31.757758633230914m, 35.35557012416908m),
        ];

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            foreach ((decimal left, decimal right) in Data) yield return [left, right];
        }
    }
}
