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

public sealed class BigDecimalArithmeticSubtractionTests
{
    [SubtractionData]
    [Theory(DisplayName = "BigDecimal.Subtract should produce the expected result.")]
    public void BigDecimalSubtractShouldProduceExpectedResult(decimal left, decimal right)
    {
        // Given
        decimal expected = left - right;

        // When
        BigDecimal actual = BigDecimal.Subtract(left, right);

        // Then
        Assert.Equal(expected, actual);
    }

    private sealed class SubtractionDataAttribute : DataAttribute
    {
        private static readonly (decimal Left, decimal Right)[] Data =
        [
            (0m, 0m),
            (0m, 1m),
            (0m, 1.01m),
            (0m, 1.00000000000000000000000000001m),
            (1m, 0m),
            (1m, 1m),
            (1m, 1.01m),
            (1m, 1.00000000000000000000000000001m),
            (-67.59798090801249m, 56.0437411975583m),
            (91.80697462900132m, -95.91515916899247m),
            (40.850324005024575m, -7.563699857059336m),
            (-54.25480795278146m, 39.70056024147755m),
            (58.94134265318585m, -21.38411649363364m),
            (12.036083093952755m, -89.64872747485153m),
            (-26.711321165489288m, 53.152995529457094m),
            (-98.55406401003634m, 13.987632061358845m),
            (-54.761457535393056m, 67.35643940320844m),
            (81.60581307558496m, -62.04971199683625m),
            (28.764708351444646m, -94.11297333608803m),
            (24.202228776440627m, -20.75262724704575m),
            (-47.54556908121948m, 83.94186011636127m),
            (-82.56418309868874m, 94.1305827922824m),
            (59.18944621120151m, -33.7619369243068m),
            (1.950063689272119m, -16.018115141132583m),
            (23.378126668229715m, -23.12345026250382m),
            (-49.460226926487984m, 93.46402025111509m),
            (-38.83851751167342m, 72.59691116980719m),
            (-82.94768896156575m, 12.57393364063043m),
            (44.83912717736196m, -21.93355728470825m),
            (82.56912429822908m, -80.97711812485528m),
            (-59.25361174378764m, 68.52992401960829m),
            (-4.563625741993748m, 27.058735378756015m),
            (37.028363632083774m, -54.59039707046305m),
            (98.75754674205946m, -7.755401636670445m),
            (-11.259152122942917m, 75.26752017266917m),
            (-57.081799291404m, 89.5253073755953m),
            (-50.99286021339879m, 0.33932045537431454m),
            (-29.91782752488611m, 77.71997903673295m),
            (-8.018430816622413m, 59.63769475726066m),
            (-6.595133962273181m, 18.024601177557955m),
            (96.46750457215524m, -30.018766880598758m),
            (95.80632056620829m, -43.94337580256198m),
            (-83.46489647884337m, 61.44340481440662m),
            (-61.42261702623524m, 6.82859541884917m),
            (88.33317613358903m, -86.34771878279854m),
            (-42.01412048020181m, 43.053953825948696m),
            (4.547755961648514m, -9.719884278164981m),
            (81.67966010950832m, -9.068443206869215m),
            (19.839636377627347m, -47.939557929802504m),
            (11.300163050118673m, -44.420886541627745m),
            (-69.31794670285099m, 73.8286156834492m),
            (20.12177994897427m, -41.639669083827414m),
            (1.509402084961875m, -10.69233968772484m),
            (59.002171678888296m, -25.527218264470676m),
            (-62.68641196850253m, 77.73313388583574m),
            (27.288261074262365m, -93.08755537812473m),
            (26.34141043737849m, -46.19866471923062m),
            (67.84349726408774m, -82.82434134285963m),
            (-58.188813735874376m, 96.98002395214263m),
            (-70.9667441319584m, 35.65757799676707m),
            (43.05510061937672m, -66.91386129633065m),
            (48.44281996903375m, -61.89176183739742m),
            (-76.64389308554314m, 77.7338058966508m),
            (-63.1004630700474m, 71.10304286198289m),
            (38.90766248616412m, -83.63250497497275m),
            (-46.14061842477201m, 76.71625062287407m),
            (-44.97700481467306m, 18.746431429112654m),
            (66.59619220403991m, -82.33519783626973m),
            (62.6545512165669m, -25.96808054218549m),
            (64.4586715067121m, -35.38025004009715m),
            (19.621226064442055m, -29.326127259612278m),
            (82.95006284817157m, -34.17071144342505m),
            (-90.32016805210607m, 13.372649808031746m),
            (-42.56276765144352m, 41.33657492173302m),
            (5.398765406859196m, -46.86505629262573m),
            (53.87865436349182m, -81.96368699424143m),
            (-59.98324607857495m, 99.82368669590942m),
            (25.333637218453774m, -56.493718196589505m),
            (-29.95144498754977m, 48.608478083898156m),
            (6.518617508589042m, -31.971310789542162m),
            (-37.986888391935246m, 27.02895801944747m),
            (20.465383764617695m, -24.965500633842552m),
            (11.294440363085723m, -60.24401624524265m),
            (2.7179174840186504m, -4.956348614405304m),
            (-57.542814283158485m, 95.64471962638204m),
            (96.08833405974708m, -90.92958892331724m),
            (78.73022535354453m, -52.858256930281854m),
            (42.899604045490435m, -95.46256111167007m),
            (-29.53013510801078m, 63.840509873701365m),
            (80.51982866303243m, -58.07509587963228m),
            (-33.23295041821509m, 39.865871169966404m),
            (22.451146819495992m, -45.41497712124687m),
            (-98.87316032215148m, 27.40117032725642m),
            (-6.986821174605318m, 42.971509161534435m),
            (-46.065151910511034m, 21.26609757233745m),
            (-38.438167382255564m, 85.28933662269009m),
            (-93.89107256114859m, 54.13234448413887m),
            (49.26443325180706m, -5.988575597750268m),
            (-57.349778431894805m, 31.737616995245798m),
            (2.1936246170527585m, -40.774837881655635m),
            (-49.304545179189184m, 43.03640557709868m),
            (4.467891779400657m, -42.54213594093024m),
            (18.173626049067703m, -46.42533289993459m),
            (47.53078671779611m, -6.347744857449344m),
            (44.935894577817656m, -67.04616207881217m),
            (55.535682210921614m, -24.479580887097917m),
            (15.161634280264579m, -80.38570672738788m),
            (-32.94658021051805m, 92.16560514809105m)
        ];

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            foreach ((decimal left, decimal right) in Data) yield return [left, right];
        }
    }
}
