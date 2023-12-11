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

public sealed class BigDecimalArithmeticAbsTests
{
    [AbsData]
    [Theory(DisplayName = "BigDecimal.Abs should produce the expected result.")]
    public void BigDecimalAbsShouldProduceExpectedResult(decimal value)
    {
        // Given
        decimal expected = decimal.Abs(value);

        // When
        BigDecimal actual = BigDecimal.Abs(value);

        // Then
        Assert.Equal(expected, actual);
    }

    private sealed class AbsDataAttribute : DataAttribute
    {
        private static readonly decimal[] Data =
        [
            0m,
            1m,
            100,
            1000000000.0000000001m,
            1234567890.1234567890m,
            0.0000000000000000000000000000m,
            1.0000000000000000000000000000m,
            0.0000000000000000000000000001m,
            1.0000000000000000000000000001m,
            -1m,
            -100,
            -1000000000.0000000001m,
            -1234567890.1234567890m,
            -0.0000000000000000000000000000m,
            -1.0000000000000000000000000000m,
            -0.0000000000000000000000000001m,
            -1.0000000000000000000000000001m,
            -78.82479855941408m,
            82.42911340485614m,
            -22.803671239434465m,
            -75.19181599632235m,
            -30.56262679084427m,
            38.29411937880269m,
            46.880152850725686m,
            -10.07253352154861m,
            9.691753821178406m,
            -61.04027616737906m,
            -79.87402393590115m,
            98.43336624591095m,
            -65.87889937416952m,
            -7.593618962998471m,
            22.757763717590663m,
            -93.62499770298777m,
            -62.89722064308866m,
            -42.58391719085148m,
            42.16229220842449m,
            30.2574103885101m,
            -38.853568925510096m,
            38.916869797870476m,
            71.11578872403642m,
            -51.19055448113754m,
            -44.46841980481013m,
            48.39864853279844m,
            -3.1153815141755414m,
            86.04601752103285m,
            -10.135192770405432m,
            -7.953589386220039m,
            -69.71299868069094m,
            -39.954741460256535m,
            79.91016653771996m,
            -16.125768026617347m,
            -64.81445066581769m,
            -1.2468903103625584m,
            38.346835760257306m,
            -88.95079667710972m,
            80.69086026310592m,
            79.58476210714075m,
            91.70687514571607m,
            76.99347095421639m,
            -7.71099221401792m,
            -42.563454260425736m,
            17.76340554667587m,
            57.649908894321875m,
            4.073753021628224m,
            -77.6383879260518m,
            73.22600960111559m,
            12.275051537623415m,
            47.99023424129709m,
            -88.93285601750456m,
            99.71038084046472m,
            -88.14228657665834m,
            89.91476548594305m,
            -62.085711812340804m,
            44.5369522519543m,
            83.92274987665363m,
            -41.19533061384757m,
            -82.65534349063671m,
            -39.018761154467185m,
            68.22534357403495m,
            3.4098333390157554m,
            -80.58459851179319m,
            44.696037338261384m,
            47.42424653652131m,
            -41.01263250751599m,
            30.077547709702124m,
            -36.24000300013538m,
            -14.610310680571326m,
            92.59116588842156m,
            -98.47787533159071m,
            -16.568165616062746m,
            -72.73602798946538m,
            -2.9797685280862907m,
            -1.2555797823582626m,
            -54.72451919165765m,
            96.46793046380861m,
            -80.34408895150615m,
            14.914916909177101m,
            59.03162209722358m,
            -22.307707145820622m,
            -4.61330579970557m,
            -67.60893962598092m,
            67.79898610601485m,
            26.801214028222m,
            -70.95450126525188m,
            20.706851925886472m,
            -52.98888826613115m,
            85.25761172062435m,
            87.2852694372831m,
            9.064044804350758m,
            -85.14836869354022m,
            -22.529502354377207m,
            -31.788761734212812m,
            34.692640526173356m,
            27.474860256616573m,
            -34.99260721958583m,
            73.69437117546809m,
            -0.5282877293458887m
        ];

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            foreach (decimal value in Data) yield return [value];
        }
    }
}
