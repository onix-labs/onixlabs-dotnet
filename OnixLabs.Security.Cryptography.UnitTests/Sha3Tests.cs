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

using System.Security.Cryptography;
using OnixLabs.Core;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class Sha3Tests
{
    [Theory(DisplayName = "Sha3Hash224 should produce the expected result")]
    [InlineData('A', "97e2f98c0938943ab1a18a1721a04dff922ecc1ad14d4bbf905c02ca")]
    [InlineData('B', "b60bd459170afa28b3ef45a22ce41ede9ad62a9a0b250482a7e1beb6")]
    [InlineData('C', "64d7aef1a5121b17a819ed6a6da3ac4930420a916f30acfe909f9482")]
    [InlineData('D', "3cc1b4bd4dcd8ecf49d86d4970fa0e3af4a6266f35c2244d8b19a360")]
    [InlineData('E', "61886c21370ba76d87ef2ef10ee1f508dbb1f7eeb239786568ac8aa9")]
    [InlineData('F', "1e1e62312963bebff717b3b45a47003e0aa77815aee43368b45c2002")]
    [InlineData('G', "9fe797ef1f52cf86c139f14aba3beb34225f96e0f3712ee5648fa120")]
    [InlineData('H', "2aebfcaa105dea01662466ab8d82e304c374328e622ffaa0e796ae82")]
    [InlineData('I', "9439a92d09b67af52e62a43318f9fb5f5f56ff8c5561d66a484966f0")]
    [InlineData('J', "0b79dc2770e78755e9f0ec888f7ab92cee97938d19b6aa066c6e6d45")]
    [InlineData('K', "5ea1f4dccfd9cf83485c4766180f0b7dbe4cc7bc78435d5b749fc0da")]
    [InlineData('L', "90efae0893370cb53c261a30b69fd026d4b1c5c3221bc9a568ac19e3")]
    [InlineData('M', "fe908de89314cebfe8e5b5d10e1b800b0e8fc033ef08dccc0d9c295e")]
    [InlineData('N', "9c65eb6b181a665acf87a19cafedaff7d48f8b66b50b171a283c9370")]
    [InlineData('O', "69a9eed164711b1201e4d3c6db567a3417805ef140d7c2bb168a3ffa")]
    [InlineData('P', "49e17197a1d79e6dc7fac1cb5e1a44f08ee151ec3e01188b57842243")]
    [InlineData('Q', "8f01734a973963b99c731ddf95c7d64d162674aa4382727a56854cdb")]
    [InlineData('R', "2a2d0471bb24c160c7f839b3652f01e292f15aa09278daa1253c88f9")]
    [InlineData('S', "aa50f8575a9e0791313375fd0232300a7aea9dd1fcc20c6de991aea0")]
    [InlineData('T', "b7cd747c390db6bb6847225755a4aa0f7eeb66363452c45284556dbb")]
    [InlineData('U', "35e0a16a4a92f63cab78df6587ec8f227d995516c893d7a966db62a4")]
    [InlineData('V', "a0393621904f8a7f08f8f949428af418b609f7fab8e126175d7489be")]
    [InlineData('W', "e7e8c042c1b9889070330ee25e12a09a5a01c309308a2322a414b878")]
    [InlineData('X', "7b6fc9c4054463d97c8c90dd89049b67bb8f148952c2070eeda1bb86")]
    [InlineData('Y', "d738effb07d0b422f4ccc99a822b32fbc926f6ba3d052cca4b1f93c2")]
    [InlineData('Z', "f41ad234fb099cc6b05cce263862fe3e06bded3d097671bc8f68ad5f")]
    [InlineData('a', "9e86ff69557ca95f405f081269685b38e3a819b309ee942f482b6a8b")]
    [InlineData('b', "8ec94b5ae7bae885e5b1fdeaa6fc2ca2af27febfdb7cdfaa6745fd52")]
    [InlineData('c', "d77fb1a25346cfaecd2b491c3a3b452f10cda5a5b37842f2bcbfa5b2")]
    [InlineData('d', "af81fd2b118fc4b3ed11bd42e7c056de57e29fcde0b0f236adaa4e25")]
    [InlineData('e', "6e22f57104bb4b8dea42a97f517f12e9e28d667c97f2ca0c29d55f8a")]
    [InlineData('f', "3a9de008f526d239d89905e2203fa484f6e68dfc096a7c051eb80f15")]
    [InlineData('g', "84342e0cfccf62d91ef86c2698b3d0294152b8f908bf95fc0273c00d")]
    [InlineData('h', "aea0b0e10eac300d286f13946455240a5978a4e4748ea87062f8a45a")]
    [InlineData('i', "ca0e1bce24fbaf261d98d7492adac27652a5f9e2ba06f3c0199437bc")]
    [InlineData('j', "7689b617e73b395ecfeb897ec177fbe5bf29307c8975dfbf5602ea22")]
    [InlineData('k', "4c483a7e33059a013a845d18195bbaf47b0398c4ae117d16255c737e")]
    [InlineData('l', "35fd6f346c0b2fa77a32a1c8d961d08450da1cd6494afca64b57955f")]
    [InlineData('m', "c3835c6f733db2d1f556e5b2f32f11ad59ad0a7e996a25376951cd81")]
    [InlineData('n', "3250c7b05b5f3af6db4e62ac1a75f0697aeb63921007eb81f24e691b")]
    [InlineData('o', "25ab1ac565856d499f958012c0842e432050c3988037c7919021f011")]
    [InlineData('p', "4295784315214500e4b51a46b3db88b139a811763aa705c5449f962d")]
    [InlineData('q', "774057ce10e1b255cfa747982782e969231ef434a057622021ff5b9c")]
    [InlineData('r', "d0b8f1b118091fbc0486a81919e2d7171a6310d4892130f33c51aa1d")]
    [InlineData('s', "001ad0de4e72638f77b9fa1272d3aa26ac5458b7b3d6f37f1dc1319a")]
    [InlineData('t', "bc3858492b7530bf2adafa0ddc6773863e41b8af69cc6da336d32691")]
    [InlineData('u', "74d73ebda3f089ec06c9d2e6fbf49a88f8b72d67a98f617d2e19c34d")]
    [InlineData('v', "4a7afb90ea8bf1a5c91d22700b7ebfaea729857df2d4797e0acde861")]
    [InlineData('w', "297c52529a495674b68a3c8cc129bcdacc123b65e63a54d3e94a44a7")]
    [InlineData('x', "63e6ceb28ad474fa51c3d5dda2239adb5e58a1ae2600d18c6e116746")]
    [InlineData('y', "5849850aa0264269112e0d8d25f1336e6caf4b9dab34ae092b01f608")]
    [InlineData('z', "3360e4f1a619f1628be65abec0da2992d4c7ceab1d2d3671ef4790b7")]
    [InlineData('0', "a823c3f51659da24d9a61254e9f61c39a4c8f11fd65820542403dd1c")]
    [InlineData('1', "300d01f3a910045fefa16d6a149f38167b2503dbc37c1b24fd6f751e")]
    [InlineData('2', "f3ff4f073ed24d62051c8d7bb73418b95db2f6ff9e4441af466f6d98")]
    [InlineData('3', "b6f194539618d1e5eec08a56b8c7d09b8198fe1faa3f16e9703b91bd")]
    [InlineData('4', "51bbf7daffa13cd37d2517dd38b1be95b200053bd4e36492b5566bda")]
    [InlineData('5', "4a63debd3538267188df39677b980ddf64ff563264554210b43524ea")]
    [InlineData('6', "970a4c0b7081bd6a245822c7e804d704db34d32acc7b771208a8c24a")]
    [InlineData('7', "aa98ecf6824dad085b259424c29f535bc339c94bace0a7a031ec40e6")]
    [InlineData('8', "2531506e5f2f02bd42cbbd39b3f8a181e120eb662b5c85471fa913b3")]
    [InlineData('9', "2299019d2c50f7525fc39a05256f802de6e9d05328c903d298b03d9d")]
    public void Sha3Hash224ShouldProduceExpectedResult(char character, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash224();

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash226 should produce the expected result")]
    [InlineData('A', "1c9ebd6caf02840a5b2b7f0fc870ec1db154886ae9fe621b822b14fd0bf513d6")]
    [InlineData('B', "521ec18851e17bbba961bc46c70baf03ee67ebdea11a8306de39c15a90e9d2e5")]
    [InlineData('C', "2248e6be26f60c9baa59adbda2a136a4a5305d7b475d8465ba4911b4886e39a5")]
    [InlineData('D', "037f4095baddc6f37fde4740c304b1691512d2fc9cf7ede8a93b8c9ec3d1fe07")]
    [InlineData('E', "e63a84c18447bfca5c67b20a58fc6a4fefa762e4fa0e6b3b2e46f64daba345e5")]
    [InlineData('F', "caf04597f01603582b91c53d5dad9c6c481445b5160a976a44c35ed428b439d7")]
    [InlineData('G', "25c69eebe727567130b3e3320395e3ec854138e6ea5034dc79eebcbeb86da200")]
    [InlineData('H', "2cee232f0cf383960ac375e090a647e2afd4ebeb12b4cecb1bf91c2e0f4b3408")]
    [InlineData('I', "c837f30e97185c362830b324e58a3e6782095ee8457109b27f03819ff516e121")]
    [InlineData('J', "5445d3d5a46c7a99219705ea5b6daa7f870b83e721da6f252f2304b94a6d7d05")]
    [InlineData('K', "078773c4efc5ce946952e92d25f89d0cdd1603fc21842df386fa55855613e8cb")]
    [InlineData('L', "266ddf27cb4fb4223962ef29090bcc4f50e363bca75c581756f605fb91f9c7e8")]
    [InlineData('M', "6920014bef534e7eea89545a50d6aef0921f1972efcddce9f22f04a45b47d472")]
    [InlineData('N', "345baaa13bbe3a40695db7697fbe3f64206323b77cf3635902106f9f29667361")]
    [InlineData('O', "60c4004508ddcd8d1b0ea1c56ed1e5679d756d72e40f1a00820dbe5d9f69ff63")]
    [InlineData('P', "e5a73514ffed2f2f59b5112f4ae50cb138f1658633d354ac36c7c1bc019259d2")]
    [InlineData('Q', "ba86a2a6dac23e336a34b4337eb740d40d900fae703bf55dcde8430208bb82e8")]
    [InlineData('R', "d034b2b544e4ffb619a9c156ae578fe21f38eb0997f097ca9569807ca157f4f6")]
    [InlineData('S', "164a93c6619015a4ed2d50a49c0d98252296e3e4c7fa5277656188edb3fe71b7")]
    [InlineData('T', "b3291957374e0a836351d5129cf45a5e0f73a92edff7b2c85ef159062301829e")]
    [InlineData('U', "78fe1396dda648dcbccc3c17af4cd29de873f2cdf5e4c5eb04e0ef08e86cc267")]
    [InlineData('V', "3eecb4a5c11c8bab18ddad1d268c827aaabb17c83f51869832a5af15efdedfcb")]
    [InlineData('W', "4cea338a15eccf7f51d8297c2873b1c5d0e5bea7d52eb7e984500b0759937d0d")]
    [InlineData('X', "31660a8aa8b0991f2d115272fecba9f9fe21e0798377c2b965405039319a1452")]
    [InlineData('Y', "08ad231c95c5b60ab9757d6f95672f4e8731910a8f4573a90a1798ee8127ee94")]
    [InlineData('Z', "1fb80b3947f9fa50760bc627a0341d53715fb79013184b34f4c0a306b62fdf05")]
    [InlineData('a', "80084bf2fba02475726feb2cab2d8215eab14bc6bdd8bfb2c8151257032ecd8b")]
    [InlineData('b', "b039179a8a4ce2c252aa6f2f25798251c19b75fc1508d9d511a191e0487d64a7")]
    [InlineData('c', "263ab762270d3b73d3e2cddf9acc893bb6bd41110347e5d5e4bd1d3c128ea90a")]
    [InlineData('d', "4ce8765e720c576f6f5a34ca380b3de5f0912e6e3cc5355542c363891e54594b")]
    [InlineData('e', "42538602949f370aa331d2c07a1ee7ff26caac9cc676288f94b82eb2188b8465")]
    [InlineData('f', "a0b37b8bfae8e71330bd8e278e4a45ca916d00475dd8b85e9352533454c9fec8")]
    [InlineData('g', "9f2898da52dedaca29f05bcac0c8e43e4b9f7cb5707c14cc3f35a567232cec7c")]
    [InlineData('h', "5a082c81a7e4d5833ee20bd67d2f4d736f679da33e4bebd3838217cb27bec1d3")]
    [InlineData('i', "bf872d20c4ef70ab19c9d413f172ce399a30ddeca771658561b1443111069c9e")]
    [InlineData('j', "f35e560e05de779f2669b9f513c2a7ab81dfeb100e2f4ee1fb17354bfa2740ca")]
    [InlineData('k', "7c712596135d13a73c0dd366151b9440f3e9072371b436371107f12b3d850180")]
    [InlineData('l', "3e5e3e723953551a2ba2e7c5584bcc4ce407414af1ab2569051e7c9bfa33164d")]
    [InlineData('m', "1b42f48aa4371867a7c51ae6f237f35626e02c12eefa592614e1b10af7769370")]
    [InlineData('n', "8ee93ceda95bbe450f7fb53a700c56dfac4387e48eb127881a2a68727bc7810c")]
    [InlineData('o', "12c6debe02a118f89049700e723650d269838a76024a826607b163bc2a237031")]
    [InlineData('p', "14c68e20d8ddb4dbd248ed14bdb2012cfcee23530af0f71328009d1e90bb36ac")]
    [InlineData('q', "8a5e1d339fafc39350fd8cf1d7ca7982091c27f6b77f75bd4ddab3df425b4f8c")]
    [InlineData('r', "f695d5fe6e2c67fe29ccf09341c29ad58154c568c5917a919c31936a3c96d607")]
    [InlineData('s', "cdc56a5028e51232cb28194fb1eb93e7014d60fb7afb447a49a1e1aaa640c9a4")]
    [InlineData('t', "889729e8d2d8864a59db1e195ad67c76949578ff2b4637388564a81dd68fc01e")]
    [InlineData('u', "d7e9468290673221249673d2b82c3cb316819a8496c2f2dba3eaebd9477af44c")]
    [InlineData('v', "453c8391bbd41309b79d7acc1382c2b0fb5f6b67f686d77c410666336ff9dabb")]
    [InlineData('w', "f1cfdca558ac0c00464ca0f3e265ec6fb32c57caeb106fbfed9f174f6b814642")]
    [InlineData('x', "741efa311f97686956946758e0d95f70f11ff2da4f2feb7c54314f44134ac49f")]
    [InlineData('y', "9d0f3db671f9fb22104b984763616732d383154a7a0dcdbb9ec17ab647b64961")]
    [InlineData('z', "3b4aed1c401f71809c93e713f4b86fb6d56c5b668f4ad8b474cb8884756aac46")]
    [InlineData('0', "f9e2eaaa42d9fe9e558a9b8ef1bf366f190aacaa83bad2641ee106e9041096e4")]
    [InlineData('1', "67b176705b46206614219f47a05aee7ae6a3edbe850bbbe214c536b989aea4d2")]
    [InlineData('2', "b1b1bd1ed240b1496c81ccf19ceccf2af6fd24fac10ae42023628abbe2687310")]
    [InlineData('3', "1bf0b26eb2090599dd68cbb42c86a674cb07ab7adc103ad3ccdf521bb79056b9")]
    [InlineData('4', "b410677b84ed73fac43fcf1abd933151dd417d932a0ef9b0260ecf8b7b72ecb9")]
    [InlineData('5', "86bc56fc56af4c3cde021282f6b727ee9f90dd636e0b0c712a85d416c75e652d")]
    [InlineData('6', "0c67354981e9068905680b57898ad4f04b993c63eb66aa3f19cdfdc71d88077e")]
    [InlineData('7', "8f9b51ce624f01b0a40c9f68ba8bb0a2c06aa7f95d1ed27d6b1b5e1e99ee5e4d")]
    [InlineData('8', "d14a329a1924592faf2d4ba6dc727d59af6afae983a0c208bf980237b63a5a6a")]
    [InlineData('9', "7609430974b087595488c154bf5c079887ead0e8efd4055cd136fda96a5ccbf8")]
    public void Sha3Hash256ShouldProduceExpectedResult(char character, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash384 should produce the expected result")]
    [InlineData('A', "15000d20f59aa483b5eac0a1f33abe8e09dea1054d173d3e7443c68035b99240b50f7abdb9553baf220320384c6b1cd6")]
    [InlineData('B', "8283d235852af9bbf7d81037b8b70aaba733a4433a4438f1b944c04c9e1d9d6d927e96d61b1fb7e7ecfcf2983ad816b5")]
    [InlineData('C', "7b95dc4d4d327449e6b84bd769d053f190568504270b32789990ac7b75bef7fcfda2931d16cb5785b9ae61a15bbead55")]
    [InlineData('D', "3ff600ef369ae191ec740e0119926f7e7b7621dbc0f4de818045b88e659230969894e1af894a4edd7a97812f5d20cc10")]
    [InlineData('E', "8d479cadb1a83bd1864a636b1cefd7ac06f7c96940f022ac7732336e96a2cb27198153d314842182b79c063c4ae8f4a3")]
    [InlineData('F', "01c126a5c70fa68dc33d3be695070b4d837e83caa3b76df066283361f4b942abd40510747f449c4e0d19861dc0a4d413")]
    [InlineData('G', "224f9604e3b38a03e335b0a78f6710f36f4ad12e2c58289caba9ddac124bd0076c912fe02a787d4cfe1f29ff4ddb75b8")]
    [InlineData('H', "1ebe9a1bc7ef3153626b1e4726aa6e28800ba39cec42491bb3b526f9b825a7ba7e22e24beaa4ab1b55b6e6a44520d1ba")]
    [InlineData('I', "daf032833345d6ead75be31f8c713590530b5a1a6d7134cdcb8dfcf4eedb67fa124d6cfea0d2a02f68f986062b12dc5c")]
    [InlineData('J', "4f6dace508a646b071eb60546aabe666829a8cf50a8d3c8e97888a2a1db5b12b8991e517e2331983f0fc99b6e0a92431")]
    [InlineData('K', "5c61420db106b2cf478a04948ea59114c1a994c594fc923afacc04858715d69126f0089515872042dfbe6a8d5e062e4b")]
    [InlineData('L', "cd3967f516705f3abbff77e35c8cfcfc6a8f915df60cc5a56ec51248580deedd6d043c364923285932ac00bc85fa6705")]
    [InlineData('M', "55b0a1bb027524210861d27a5554e4c24d7b48da7373c98d398a760edc2a9c62ffa4d2b15c792ea93b5576ba2624e376")]
    [InlineData('N', "c8894d0f096c9232ff7545833642c20061d6e93c75d558c020fdd92ff278336757ab14be364b2bfb306c1af4bd42a312")]
    [InlineData('O', "3f0cb1cfa5db5e4741efa72c8c309f3669efe7ac6619a7528b5ff4f22da37403e9e68bce96aabd7b67cf387b548e608b")]
    [InlineData('P', "04ac39f4be45e26b203fc712e1072801cf0f90dec53a26e1be1fb886340e6cb06375464f7150b58adcad215dccce732c")]
    [InlineData('Q', "c66a74deabfeab86cdfd9fe16d8e20915087ae3b3627113bac0bd739822c07a609b45b29bca82501a6dc4aefe09def41")]
    [InlineData('R', "3bd4e7dcad9d3c02adfa7aa5388727d346278a9a7b007f497b48a4fa2a12b9545c820df150854a8f8c494275bd6fd941")]
    [InlineData('S', "bcc4f6d7426e832ccbdd03e30f6dc6b837c5f5780f8efd58122ab9086edb78d86b985e6b4dbd9f89d24486431dbc89be")]
    [InlineData('T', "daf2c5c49a615b7ca9a92c85800f690788246809a23652f7b613fb231065cdf110a872dfc3e094b47be4a986e9a451e5")]
    [InlineData('U', "351d1a799be78278a04f2e9128d992ff130251a9b88ab26a5525097c10e1209e60a2fbfc379a0ea983a46dac261ff469")]
    [InlineData('V', "08e8ee1a3416fbcb0ca7d0d53556090c17f966de3a7dda5db0267aeb9088fa5486f9a425111c4c9d7a4227118ac4faf4")]
    [InlineData('W', "653fac6cdf0fad1c202f71847e9360d18b0ab62b4e0279226eba96f70631747b3853624cf8160dbad575642563fec65b")]
    [InlineData('X', "7b02671ba315c6c59b8ed4d1fa0aff47ce1c46b6ae534e9f3083d03bcd8af85f730a835daa1608da913478dcaae4caa3")]
    [InlineData('Y', "17b15a22de43482d1a07c944812703b1b147928d23e8a4e9c117e8d7e6aa3132b65d50836eeb64969030bd150dea4d77")]
    [InlineData('Z', "8bceb69fabf2bf9ffed6f132c037301870fcfc2de42fcb01df1f26ad808eca1b6077b37272dab03ebdc258f351ee1537")]
    [InlineData('a', "1815f774f320491b48569efec794d249eeb59aae46d22bf77dafe25c5edc28d7ea44f93ee1234aa88f61c91912a4ccd9")]
    [InlineData('b', "0c851fd986de48f9703a157327512d705e0aec5e339b53d99f4f3d55b02bd81a513e3ab059d20a348c993acd6591d347")]
    [InlineData('c', "fc046ce484597913e61aac55245cc91c7393a9cd69012f22b389ab14ce40ec56f4520cdb1a2054ad3e588e192ec0df5c")]
    [InlineData('d', "0312ab38cafbaa6fffe82ab1aeafcce1d4c656c5fde60444232a374df23d6c364c4f33bb044ae258e25111227c9d57da")]
    [InlineData('e', "114681f5af3e1c7be7bfb31f60b1bab2de762d43258a23f56f92dececb14949956dc9bda8f248b62a881299064600b4a")]
    [InlineData('f', "3f803d234b62ccd243b91510c620c15aaec715c7c46491fb640130322906cf8b52e0e129200459d04746892562cd70d4")]
    [InlineData('g', "aea65684980ae4b2c328ccfc045cf27e8ede51c5ed11e85813f7e5709faa7ed3653df2333fa9310f521813a8cc5b80da")]
    [InlineData('h', "906c31e68be36e29921029db158e0b4a6b083441e50a40bcca6460d5122bdd1c331cb6b867c9451f48e4745239a6219e")]
    [InlineData('i', "a293952c9b39b0ad52e72686526ffb7981cfefa6a9a836b36f87d2f6409b0d41699eb4ad6391a5856978a0f601e43679")]
    [InlineData('j', "42a8f1be481d8b95e919ca68bf684bf4476606dcdcbea2924289b6a10a8daa87fa89cdda0c252a77ef841bad5b258bf9")]
    [InlineData('k', "b3311bb3e3415615a58fc9001704bf32dec9fb294735783e8195cd479979408e7c36d564934d64a5cea541f3f3bf2195")]
    [InlineData('l', "d1a95dab99bdfc5e1f48213027c463387c617881903985572d43433b4e4d8d21e0906abb58c2e4a633cc16abba1a9663")]
    [InlineData('m', "5428b66b2021a5090995a9f4964f8f82fb34712d4a10f69f1bfff92f8534e3939d86c83b441fb83bb937055733b9e6d9")]
    [InlineData('n', "4303b4457f0cd358a24b43c1454327e2b2cdd56d8975fff4be77119dd8d27291a65d26a1e3233f3b773f6d82d2e78cd6")]
    [InlineData('o', "e0133f6d5dd9727860f150073dd6ee154db4e440b65b7d26889e503039d54f5fbfc4646476cd34d808a0089310000dc5")]
    [InlineData('p', "c441ea8b76d530b8a4e95e35f6f4eb0d8de7526b8d9972d7f45fb9f5301805c8b73d559e4c8458a001a1a1ad9b563dcc")]
    [InlineData('q', "a73cf129eac67f6b9e2f3818b3b845572914c3c6821fafdc71d834f7852ba1c1d894c1a1d71669b9090d1a08418d34d9")]
    [InlineData('r', "f01f898eceabaf8863678b738f224815a5746965e7eb1a91d4614f9eef2328791252037a1add0180f7f4f3e7fa66edda")]
    [InlineData('s', "e5c105cc135ede693be4be4bb2c9b624a90fa1e422e43b97948ea6ca14f2ffff36b6f586c46110db208999f8a2f0d139")]
    [InlineData('t', "63644c71a1fc2e1a2d1aaf429338b0e04e34738ef744f7c6d59ae9a9f96d096fa0852f20b4762c581e604d0178000da8")]
    [InlineData('u', "bef30a27a058db0cf7397faa3eee6aad5736618be94a6ea284d2da411ee1f087af864d709d57c0827e647dc722093b69")]
    [InlineData('v', "1e5d1e7b4777d1c827f77385016e0ffc832efce7ab2888808f34de90e44b21751e648b902a5dc3ceaf0c847a9727a047")]
    [InlineData('w', "045ad9ad61b764222e1a1d8f442d1eb62434805d98e516e2394911949a08a3505629b9e3f2fd27ca25fedcb42bb8dc71")]
    [InlineData('x', "5abfc7bc2a09a612f87987ce070634a0932d31891a61a0ec598e81e6ec616c9f00f05ff627070cbf6cb0499b1c334d4d")]
    [InlineData('y', "40567ee6c5b13d88390bbec7b4364d1308960652a87ef83208e1bc23867b96638c5c2e3970a91de68a398ecb51452c90")]
    [InlineData('z', "2b053962ede73a41a0235fd9e29243b2bbf3513a8b1d586525949fde2f38c659808ee0254dda75ee310185635e42e041")]
    [InlineData('0', "17c0608360f9652153b4bf29611b146bbb7ed3336c33d944c8cf7637ffe8ff440b3b0b67a127a183a5d7e2d978f544c5")]
    [InlineData('1', "f39de487a8aed2d19069ed7a7bcfc274e9f026bba97c8f059be6a2e5eed051d7ee437b93d80aa6163bf8039543b612dd")]
    [InlineData('2', "39773563a8fc5c19ba80f0dc0f57bf49ba0e804abe8e68a1ed067252c30ef499d54ab4eb4e8f4cfa2cfac6c83798997e")]
    [InlineData('3', "5f9714f2c47c4ee6af02e96db42b64a3750f5ec5f3541d1a1a6fd20d3632395c55439e208557e782f22a9714885b6e0c")]
    [InlineData('4', "d87826dc897a66ee657458dbbe788e473e809b47c93bb37902b74b53999ae64a0ecdc8f76b28b608c2bf66f836d1b8d9")]
    [InlineData('5', "d17e08f9fd1ec955b2384bba9312e525edad397e244071a0dd499c3403719434c5c21d833e7ecd46ed47f14d2bdbcfa3")]
    [InlineData('6', "7cca6b41713794de552e96349b5f5bf35fa9f12806bfce76a5aa3cdd450e0a98495be64b2023f3188e80cbe27c802d1b")]
    [InlineData('7', "4bf873ccb328cdc95a26473588df6c107706c166e240294fc5c70c2b220adc9314e166b0a77344825a34a835cb422ebb")]
    [InlineData('8', "854ed8ecc48ed40a6bbf2fc0de3cfbd1811937e23340b245d2d618dc3d5349dbb0fea84e54184557247df6f456731040")]
    [InlineData('9', "4894ec28d9d6494918765447867b8fbe65f7a6ec5a30f5aa3ce168c766fb8f9c63cb02602c730e8b259381942ac1f49b")]
    public void Sha3Hash384ShouldProduceExpectedResult(char character, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash384();

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash512 should produce the expected result")]
    [InlineData('A', "f5f0eaa9ca3fd0c4e0d72a3471e4b71edaabe2d01c4b25e16715004ed91e663a1750707cc9f04430f19b995f4aba21b0ec878fc5c4eb838a18df5bf9fdc949df")]
    [InlineData('B', "7b637bc5543d96f49500aaad3b27d8bd37624db23d415c4d0f3dd231e9b9fb061f39b7d8561c540650de8bef02aca43a2069cc2512697bd34f2244ee732743a9")]
    [InlineData('C', "be0c2f5b07cb96d61f0e1e3fd250f0a1709d7388a24e192b9734ae0f9c92abcbd095fed6c5978ad9e55bb62a6a2ca16532eb3f3e5b3bc1e64209546e7e887445")]
    [InlineData('D', "eb3eb4ccf7cb3db0ca62080b7ad8a255879c9aa1334a4a5f751687577e3f03b1afde16a38003b029d426b2dd74dfa0fa3cd561fbf79d86bf2c9f966ac154719a")]
    [InlineData('E', "41b334d0a39eed4c28639643919433d8976248dd70fbb2b03ed2f233a8820a5de496686c6a81a3e1897ccd8794b92743b42617ab38152f2a8a04e588bad5742d")]
    [InlineData('F', "b1845edee6f21b7b4ca587b3b22514972fa681f9380184a8e946a40c432239537b2558a774cd84c28488833fecc9567d57d3a99192983ac931b14f6b0aeda9b2")]
    [InlineData('G', "b32657b8079a2eb23041a360c20b8b7fa4c76e300912ccf019ca2e94ec33c898a93b39fa8c1a6f1bba933db63e1a027bf52ee6d23e81aac71ebb2721031a7881")]
    [InlineData('H', "8e844c36dda15c37458d23c88a6e66782a5ac90cd0135960b658f4d3e3c783fc2bbf68a0a8e2a054d7d52f9b1f5417bdf4f0e2825fcceb0fe0375466426c1f4c")]
    [InlineData('I', "6b8b537691c9252e34bab51bf067ad765065c97f6dfd2c346c8b9f020698187826ed2be1f1669db72a476453341fb0c7cdda49bbf6ec5b55899bf0b64c3e09e1")]
    [InlineData('J', "5a3f7519955a950ffb0f9f839649f8a88a20ba2c746da1fef8d97a5c4d9e4b0a2fbd5a8b63ee74a6322a1d0bc3d447717291f3dc0c504f38b4a7ca5998961253")]
    [InlineData('K', "afd0107aaf7b3ed73353cf54ca923d2c1f8e75c151a19bf8d2e0b7f873db9b5ce33e6a1db692dcb352fba4b89b1a1ee823f19abd1485e8018f9c71e64bacfd8b")]
    [InlineData('L', "6bbac080a582f03fa999fb1cc16a1d856191bd84dbaea946312e980c2ddf5e61bf754b5028f93191d8ff237d6030634001dc8799651062d1a96e44e210599c02")]
    [InlineData('M', "e3c3b7d964d08646d4935b2299fd02d79db5b2132059cbf3cef72066871b85812ab2489182c5fbffedadd67f89cccf08b7b744a4c8ff050117a9b9068f508002")]
    [InlineData('N', "471126be6139191b7c622abbf162fafaaaa2b46caf5ae11787b2d70d9c96628655a6870e58eaee4218864c136ee5d24e81b39000cefce12fbd407924cafa36e2")]
    [InlineData('O', "3d75f1a138ee6317875acc325062b5d4bda8af7a715f372d6bc0e618d8a64feaf462b833471846770cf8648c28a09fc24a97574e52c8a3219e59e2d5048dfeb9")]
    [InlineData('P', "4dec0e71e1250d9edcca403f02023fcbbd88be4d0af99a4dc4494ab4505ec07e0660804af0c53c513f1f3e2981ab4d9f7325c37a6cc075ff5a0f438666767030")]
    [InlineData('Q', "e95b1fd29022cc854c5c7cf4d4704de993ac8d093bfee5b0c6ede93af431458e8e19c31934125aff62984f574ce3a30f6251a7ac11381e92b4a1f52283e13a07")]
    [InlineData('R', "8e48ecc4d3b8a5125db648907fe0ef8ec14b353302779a5c6a7fd8a8142ab97a079602a51f02fdff0bf28dbae03212cfd1538901689f8c90c7425ddac7752d5a")]
    [InlineData('S', "329fadd7c1d5f243d57af8a95da82235d2b846681d725e9eaf7095800fa66197144044622c79b37b9e501966e6a35a866ebf03d0b3033e446b993d3f5b193de3")]
    [InlineData('T', "7282ac39a48e7cbba327dabc6843ba412970d9ade37e49c5ce8f0360d61a6bc4644a3b9947efa013d8429cac9ca02dd28c1132236c09aca9e57910e3a1a8046f")]
    [InlineData('U', "c581306f35e63725c488de906569ed6675e577eab4f75cb1c06b19164c7efa47899d36a365cf228f9b7688820ed0df2b2a4f72a0fadf03a6b2cf3e75440cb6a9")]
    [InlineData('V', "6b212db1ff14f2781c78334afc9d9b8d57a2318856823ec7fa78ee3d39b28ce29e8915f4fceab367c44b52acfed0989945e5d7537f9f89553fe47e443d786be5")]
    [InlineData('W', "cac54800b600476ec1132e9c58b486bb92ec9bcc4eb3c52ef7bb97ea42eac8b08ea047b605eb9d146b273720c37649b9497bc77f8dee1d327fdeebcbf06729bb")]
    [InlineData('X', "c1d414ec5914ecbaa2507f89fe9eaee9eba8ba506bf1e58faf8c7982d4e0329fa8824f7ee8f0258f0b23a304d93997357c4c732d5bdb1288fe7b1af68ba97432")]
    [InlineData('Y', "c66a0c202ae9466c93deb74b21c15cf6bc789e5e88b0327486b5476a1aed0e9a2053b549134d041447156aaf4bd1944862d820873873228f52fba106a059c41e")]
    [InlineData('Z', "fda430c40ee744a4f06e6a750564e16e80451b1943dbd11f8f8f399b1101a06d723c1f730cc8d996f7b5ba5656c6b963dab711dbaf0eb493978db715ee4ac986")]
    [InlineData('a', "697f2d856172cb8309d6b8b97dac4de344b549d4dee61edfb4962d8698b7fa803f4f93ff24393586e28b5b957ac3d1d369420ce53332712f997bd336d09ab02a")]
    [InlineData('b', "8446c46ee03793ba6e5813ba0db4480008926dd1d19efe2c8eb92f9034da974d2171ae483f29ce3a79ed4fdd621ae1ed14fe12532af95ddd0728779ce5aa842d")]
    [InlineData('c', "bfe4d7f7377116dc15f794d902621797b72b32396382de2b6e49d4f1d7eabdfddcfc3bc127bb67f92f9458a5733bb21804e7ccd56b4b6f81049339f477cd279d")]
    [InlineData('d', "4668897682ccd2b1ee0cae8dc55947291f819cc59ee126f5bd243b1852577414413aeed5780b5fb11090038715beed1b00714a15b31c8d9674fbdbdf7fd4191c")]
    [InlineData('e', "6ebb8a73bfd0459bd575b9dbef6dcb970bb11182591f5ecd7c8c0d771b3269b715fcb84005d542ff74306565a46b3b893f64ca41b8519457ae137f6429dfbb1e")]
    [InlineData('f', "10a090626438fdee4f1244562d6a39c56e515dbf70293584c5a20ed2e8e048905ce30af923921276360817aac682ad30f462033d97c00670edeee8281939a60c")]
    [InlineData('g', "2ca04c154f94c314a835993acedc2634cb2dc491673b2cafa8906a5a29bfc3eeac19fc4baef80932c46184e0fd458594ab9e7e020ce70a25db393de171d69840")]
    [InlineData('h', "26cfb46b8264aa515069b0726c0ed4d1c08587a2f1572fcee6a06b6611ba7802e657791c8e64bf372042bd86208995a9a2a1ef2248d1202137ae65b0906f1ae3")]
    [InlineData('i', "baeb91abb764a812dcff52b66a8a92747c07c4ecf08140b73b0e3819abd35cb0634062ca992e74a65bad3c02fc3dc6bac2bd60bc4441771df9ea095842738c39")]
    [InlineData('j', "998e8fc13f2160c6dfef0bace960efc019bf173addd5aa3a1dcab50ec8e0d66637b30d0c37f88b09e57477b87b5ddd88b424191ff8e757b384cea28801fe172a")]
    [InlineData('k', "8771111f7c5b23494cde7bd3edb03a31e0363d7f76d8ceb07a1300164cf1ef2252e0fcb9e404f6d88785ca2a59f0379d277f4dc7b16f76a726478a639b2da8ce")]
    [InlineData('l', "37e275b9f5a7067372be037c5d010e04d3d3ab0df5aab129a9379d9b9c27ecef3f7fa7bffab6582c18323df04c9d0c53cb63fdd5dde484f4227eb60f184756fd")]
    [InlineData('m', "7c8e9b49719fda980d594727bd9d3ce693349bba9b303a492726ed7107551879be951f959c55e11b6b1fbd24668f5ee83339da547c04ebc13b56df8b12a03cc0")]
    [InlineData('n', "10f12efa3c8edddb5343f970ce1a9037d02764e24e0dceff59f9f66afb3e69158f5ea6666f99f4ad33c47ab9e0c93a25032fd24f1aea6d64ad6b61f7a6dae9bf")]
    [InlineData('o', "bb4e922d85066e5ef0a215d59faade03f55728f59a552fc742065d41f189e17dea56a9338764f84bd714f043c2da71d157712921b69a4f23adaebd9366878371")]
    [InlineData('p', "9a768ace36ff3d1771d5c145a544de3d68343b2e76093cb7b2a8ea89ac7f1a20c852e6fc1d71275b43abffefac381c5b906f55c3bcff4225353d02f1d3498758")]
    [InlineData('q', "f435ba3ef2bf43e694c8940fa315641c67f152c2ee2021f121af5e03f9860607f74e61e1451f9489c2ff59f87dc0e1c501566e2324355de32770ec52cc3bce47")]
    [InlineData('r', "d4683a427e18f18bdb592e45c8202bb4f954af961cff43131c358c53980e7936e0e2d2da7fd1babdafddebf8f0ed0ec285637f9fe292241c620633d5524e5122")]
    [InlineData('s', "723a86b292df5e128ff2d1945391b8d9f4bb17de3c13f9b8527b1f49fffa2ec69b91b137294aa6b69b78349e20ec17520c338cf33968f86b9383a33d6b0716fc")]
    [InlineData('t', "28f9769820918857e4ae2b4e91a41d8624d58fd926f7c3678732d69740c5684db562accf252cb8cd06bc4965186f9d6d5c5c5235c2ebede586d6526bcb3fcd92")]
    [InlineData('u', "bb5458b8d4376d42b1f4c18308c8d8a0fe4cf2b97007b6f55d928c41fc19bbf18c47565e0e5ba9ce26612e7327d947769f191cbfc1d542ddd0c05b3c73e4f173")]
    [InlineData('v', "b9a3ad42faec4820b964d8027ce03d6a0a73f9a926cf543002c59ec1e2d385060ec71884faaacbc66e651c9e7a8194add6d1467df0419e841a4cbde075cb0182")]
    [InlineData('w', "35f8e0298f058a564e4b23e3c45c9a42f5a1e8757bd4be0a0ed40fcdff2bb2e78dfa57bf37267fa619766a61c4842e7b403122350b8195a2990af602cb4226f1")]
    [InlineData('x', "0fdb27960308c51467edd49a0f5e0c434c9cca721f4c35bff005feabaf6010e777a1137ee8187c5288af57578d18d502a0bbe4c022f5587541961e10132d9834")]
    [InlineData('y', "ab2962a6563627545935edbb63499e632694f0e8766ac52518e58cb1c1289591729d4a6f92636684ba601b56274d715f17ed4885b05a743f38b27aabdfef602f")]
    [InlineData('z', "ffe4d7127d5e222ac77ded78b503276294960867d5501eda748bbb741dbc238d1d68f5f4c76f38fdb03a491bd9ec8c1e20403440315ac5e8050946a00409a724")]
    [InlineData('0', "2d44da53f305ab94b6365837b9803627ab098c41a6013694f9b468bccb9c13e95b3900365eb58924de7158a54467e984efcfdabdbcc9af9a940d49c51455b04c")]
    [InlineData('1', "ca2c70bc13298c5109ee0cb342d014906e6365249005fd4beee6f01aee44edb531231e98b50bf6810de6cf687882b09320fdd5f6375d1f2debd966fbf8d03efa")]
    [InlineData('2', "564e1971233e098c26d412f2d4e652742355e616fed8ba88fc9750f869aac1c29cb944175c374a7b6769989aa7a4216198ee12f53bf7827850dfe28540587a97")]
    [InlineData('3', "73fb266a903f956a9034d52c2d2793c37fddc32077898f5d871173da1d646fb80bbc21a0522390b75d3bcc88bd78960bdb73be323ad5fc5b3a16089992957d3a")]
    [InlineData('4', "37f558134baa535903c6a88931c8122e334368bf951f2cada569b11774ef9795ef6d2ac961d13ee44a0c837db3817bb9db68ac3bdfb8b19a1308618484a9da8f")]
    [InlineData('5', "c74bd95b8555275277d4e941c73985b4bcd923b36fcce75968ebb3c5a8d2b1ac411cfae4c2d473bff59a2b7b5ea220f0ac7bb8c880afb32f1b4881d59cc60d85")]
    [InlineData('6', "503ad3364d41a2362f28136ee8a9615108277986f52c34ca170b664eb1c663f5e407e9a3084e90017e315b24ba9162021c477e29b3bb1f84a37eea841fe12b9a")]
    [InlineData('7', "72ce921155976b88a4a4bf39a4127c4d9e272eccde35ee864963da855f32330c0f8075aafc3a3aadecf498ee7b5e2f9ee3529ea46d97ee0795bd548b41463771")]
    [InlineData('8', "f30e8484fa863883156c517514c4e2a9096ec6009f40ebfb9f00666ec58e52e50e64f9074c9182a325a21cc99516b155560f8c48be28f11f2ee73f6945ff7563")]
    [InlineData('9', "b55cf27ef01025e3c761a579a63d1c7c1e54e2d12f8f2928c90f5f5516b0d9c71f2fac9e7ccf28c5adf33c3f78d9548ebfed2dc46dea944aed336d1650721487")]
    public void Sha3Hash512ShouldProduceExpectedResult(char character, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash512();

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Shake128 should produce the expected result")]
    [InlineData('A', 16, "a5ba3aeee1525b4ae5439e54cd711f14")]
    [InlineData('A', 32, "a5ba3aeee1525b4ae5439e54cd711f14850251e02c5999a53f61374c0ae089ef")]
    [InlineData('B', 16, "74865dbf56f0d9c27b76300b1872ba5f")]
    [InlineData('B', 32, "74865dbf56f0d9c27b76300b1872ba5f852c2419010ac745bcabf4500db91fc2")]
    [InlineData('C', 16, "2e9587f4196d40eccfc86540d53c34f2")]
    [InlineData('C', 32, "2e9587f4196d40eccfc86540d53c34f2282cbbdac7e17c873594d93fb849b2b3")]
    [InlineData('D', 16, "f0695584e963aa0f01ac26526ffd4061")]
    [InlineData('D', 32, "f0695584e963aa0f01ac26526ffd40614d8920fd947503de8155a6f78c7d4449")]
    [InlineData('E', 16, "9e677faf367b255042d0ff421c11a097")]
    [InlineData('E', 32, "9e677faf367b255042d0ff421c11a097e5f444ebae4051b698c077e1969224ca")]
    [InlineData('F', 16, "a2c949bf7c27b871cbca13f0949f9f2d")]
    [InlineData('F', 32, "a2c949bf7c27b871cbca13f0949f9f2da9a234f53bda26f7b220ebcf69963870")]
    [InlineData('G', 16, "81ca32e51bc34a70ae1a52d1517e1a6a")]
    [InlineData('G', 32, "81ca32e51bc34a70ae1a52d1517e1a6a8c895e43bb772ff39e82d079133ab1bd")]
    [InlineData('H', 16, "db72f8cc2e23e24e6485137fed6e6dd6")]
    [InlineData('H', 32, "db72f8cc2e23e24e6485137fed6e6dd6f5474d929fd5b20a8708c35d5638ba02")]
    [InlineData('I', 16, "9264d43898745dcfaf194719f2f4edaf")]
    [InlineData('I', 32, "9264d43898745dcfaf194719f2f4edaf3145bd35a7be80187e3b7fec30486ce2")]
    [InlineData('J', 16, "5efe2fbaa173fe4a8492e96089f066dc")]
    [InlineData('J', 32, "5efe2fbaa173fe4a8492e96089f066dc55d04f038a012363513c6f924e74a7db")]
    [InlineData('K', 16, "58a8694c7e0364217702e964eeaf927b")]
    [InlineData('K', 32, "58a8694c7e0364217702e964eeaf927b0da95486164f928a607e39561e138dec")]
    [InlineData('L', 16, "73706247bac5dbc741be1ad46f3e62f0")]
    [InlineData('L', 32, "73706247bac5dbc741be1ad46f3e62f0fb834b6403a62448c672b074ef725065")]
    [InlineData('M', 16, "b969a39b683ce8f8e80ac2c08ce116c4")]
    [InlineData('M', 32, "b969a39b683ce8f8e80ac2c08ce116c4c6539100b715c2e01a1dc18bcee9e6cd")]
    [InlineData('N', 16, "9e1bfdaba56285939e655aba57b078f7")]
    [InlineData('N', 32, "9e1bfdaba56285939e655aba57b078f72ddd0bdece0a73c928d61c998ac4569a")]
    [InlineData('O', 16, "4ac61591f84dcb84913e5f6be341f100")]
    [InlineData('O', 32, "4ac61591f84dcb84913e5f6be341f100dd7b99f606e21a449ba7f7236f65ec09")]
    [InlineData('P', 16, "eab4480497429cd15b8bfdb46014d85f")]
    [InlineData('P', 32, "eab4480497429cd15b8bfdb46014d85f89b2621a6785d87dc6e1d160354478e6")]
    [InlineData('Q', 16, "bd3d67b5dbf85163d78b6cf16df8c94f")]
    [InlineData('Q', 32, "bd3d67b5dbf85163d78b6cf16df8c94f5fba33667d77cdea00476314633b50bb")]
    [InlineData('R', 16, "613edcc87e371533095dfaf67754de88")]
    [InlineData('R', 32, "613edcc87e371533095dfaf67754de886a38f321d418d069b06af1d63ea29d59")]
    [InlineData('S', 16, "9934d41dbceac03b9795ef28330708f0")]
    [InlineData('S', 32, "9934d41dbceac03b9795ef28330708f0b494b36786222d6dd148d54193c7e217")]
    [InlineData('T', 16, "9dd7847b4c29404da0cbaa5d1cb69df4")]
    [InlineData('T', 32, "9dd7847b4c29404da0cbaa5d1cb69df451878f576d2c4bef29544ee025367484")]
    [InlineData('U', 16, "286668ef63f9546f20767908cf44d5d7")]
    [InlineData('U', 32, "286668ef63f9546f20767908cf44d5d77c0fef4c680aa3f61660ae3beac3aacf")]
    [InlineData('V', 16, "55360b718e1ed3de9ce9ce7a08361c91")]
    [InlineData('V', 32, "55360b718e1ed3de9ce9ce7a08361c916e8c5d0529f0a9f627eb23e2ca3db8d2")]
    [InlineData('W', 16, "c4abaca1f7f8691f4623cac449557dfb")]
    [InlineData('W', 32, "c4abaca1f7f8691f4623cac449557dfbede55e269c6e0904b01b921d8a2e2dbb")]
    [InlineData('X', 16, "a378c3f63abcc74f0ca75f3b5d0ef6f1")]
    [InlineData('X', 32, "a378c3f63abcc74f0ca75f3b5d0ef6f112d2fa716edf851d0895f7bef485ee0f")]
    [InlineData('Y', 16, "5bf27867460bbc4a67d5f3930078d748")]
    [InlineData('Y', 32, "5bf27867460bbc4a67d5f3930078d74831f08d66190c348b77f0da15b1bafb4f")]
    [InlineData('Z', 16, "dad14f5a3d2237fd294fb618e14eae83")]
    [InlineData('Z', 32, "dad14f5a3d2237fd294fb618e14eae832c0ee3b6c12dd850cb0196c36dc1c204")]
    [InlineData('a', 16, "85c8de88d28866bf0868090b3961162b")]
    [InlineData('a', 32, "85c8de88d28866bf0868090b3961162bf82392f690d9e4730910f4af7c6ab3ee")]
    [InlineData('b', 16, "f0ffa34335ef979fcf79a200874749da")]
    [InlineData('b', 32, "f0ffa34335ef979fcf79a200874749da3054fe398bb6d2137d3c98b82df9160f")]
    [InlineData('c', 16, "a5c1009bf9fce98b7930ae5fe7aa7ca0")]
    [InlineData('c', 32, "a5c1009bf9fce98b7930ae5fe7aa7ca06c7689fe2bd9be8a75373f1a8ffae454")]
    [InlineData('d', 16, "5943bb0f0a00c1990d9a1057656bd983")]
    [InlineData('d', 32, "5943bb0f0a00c1990d9a1057656bd983b8fbf44363127d15848d3ba54c3561b1")]
    [InlineData('e', 16, "bb2b536f0606a0910bf4b98c74c75e96")]
    [InlineData('e', 32, "bb2b536f0606a0910bf4b98c74c75e966d0fbcd792ca55e3c768ca2e92234d50")]
    [InlineData('f', 16, "2c6c3bcd720350d73d4f46724c098901")]
    [InlineData('f', 32, "2c6c3bcd720350d73d4f46724c098901a788d00e2c25ed9d6b789af2c9dc0982")]
    [InlineData('g', 16, "7a529e89a30f8e715331b98032cbda63")]
    [InlineData('g', 32, "7a529e89a30f8e715331b98032cbda633e3625c51414cbfa8843a568bd39374c")]
    [InlineData('h', 16, "ed6dba4b983e0e45039b17d4ff85c81f")]
    [InlineData('h', 32, "ed6dba4b983e0e45039b17d4ff85c81f9a829797def86339dec4b4d0f596625c")]
    [InlineData('i', 16, "78ac692a457de231fd8e5bda490a1d0f")]
    [InlineData('i', 32, "78ac692a457de231fd8e5bda490a1d0fdb6d1274d505385ae196d821fbd2aa51")]
    [InlineData('j', 16, "60e284f4daedb9f6afcd8fe698f7dc7f")]
    [InlineData('j', 32, "60e284f4daedb9f6afcd8fe698f7dc7fffa8503cb41d4a83709bbbbd449aa843")]
    [InlineData('k', 16, "ac20c8330b5c6c163747281430fa7a9c")]
    [InlineData('k', 32, "ac20c8330b5c6c163747281430fa7a9c549ca49ee7d675dbb4511b1cd411a615")]
    [InlineData('l', 16, "5238d98fc5871541974a81275dd5f1ea")]
    [InlineData('l', 32, "5238d98fc5871541974a81275dd5f1eae54c57ebb0393fae9c6fa78accfc09d6")]
    [InlineData('m', 16, "c5c955987cf895c48f31a150f56f57b6")]
    [InlineData('m', 32, "c5c955987cf895c48f31a150f56f57b61c793fdca932fe909ad487bbdc3dc66a")]
    [InlineData('n', 16, "68f8ea7d3b0bd13eb9d7f9c131dccbba")]
    [InlineData('n', 32, "68f8ea7d3b0bd13eb9d7f9c131dccbbac2dc89940edc3d70201fded5363d3e44")]
    [InlineData('o', 16, "a0b20673d0b9a9642857aed7faf41ed2")]
    [InlineData('o', 32, "a0b20673d0b9a9642857aed7faf41ed2c739c5f23ac9e946281c23fd7f88fb5f")]
    [InlineData('p', 16, "f884289b7f8c5ab613344841267a9a7b")]
    [InlineData('p', 32, "f884289b7f8c5ab613344841267a9a7b2a79b0a0fee49aa0affd2aa4fe1f1643")]
    [InlineData('q', 16, "87b8d13b05e63515cb6ee34685ece7c3")]
    [InlineData('q', 32, "87b8d13b05e63515cb6ee34685ece7c3be88a65f5b68949a523b0cd68855e716")]
    [InlineData('r', 16, "95434212048c97e0a06af33459defb3f")]
    [InlineData('r', 32, "95434212048c97e0a06af33459defb3f9cbd95e2fcc0c7f9d572229687ce1dce")]
    [InlineData('s', 16, "bc5ac4a37d609be474d3121af546e217")]
    [InlineData('s', 32, "bc5ac4a37d609be474d3121af546e2177ef9af4f1ad2a0f5ef26d25de1e27b30")]
    [InlineData('t', 16, "ef012cb5d92b651014fa6d9c3118eca5")]
    [InlineData('t', 32, "ef012cb5d92b651014fa6d9c3118eca56a41f923e94b7242d1fd743e0c7fb9cf")]
    [InlineData('u', 16, "4aed09875d70273d35cc94d52d25d4f0")]
    [InlineData('u', 32, "4aed09875d70273d35cc94d52d25d4f0c3ad2c0a8d0d2d4dca4d117834995a1a")]
    [InlineData('v', 16, "904f2f17cfe7bd45d5be9da1f931f7f8")]
    [InlineData('v', 32, "904f2f17cfe7bd45d5be9da1f931f7f817c98f28f448109840ff585fcc5c3f48")]
    [InlineData('w', 16, "1e332829e1b27a670aee5b0c0495b229")]
    [InlineData('w', 32, "1e332829e1b27a670aee5b0c0495b2293e0e2d80c012cb9f8afac446c03dcc92")]
    [InlineData('x', 16, "e472c5e394f30ff8d5d33803f9593e63")]
    [InlineData('x', 32, "e472c5e394f30ff8d5d33803f9593e63e2f3862d48a41ef841e4d6486af49972")]
    [InlineData('y', 16, "4cccd98df122b0736a05ea30c361846c")]
    [InlineData('y', 32, "4cccd98df122b0736a05ea30c361846c5cb89596679b680d08e58a1c19744913")]
    [InlineData('z', 16, "bb66897ef6ac6cbc29c3b14c6f1027f6")]
    [InlineData('z', 32, "bb66897ef6ac6cbc29c3b14c6f1027f6f660b290bae53b4592848c3ed2e87885")]
    [InlineData('0', 16, "628e79cf7948cd1ca156cee7631a3446")]
    [InlineData('0', 32, "628e79cf7948cd1ca156cee7631a3446bc21d4947e0be55c803a0d5c40380b4b")]
    [InlineData('1', 16, "ebaf5ccd6f37291d34bade1bbff539e7")]
    [InlineData('1', 32, "ebaf5ccd6f37291d34bade1bbff539e76c47afb293c5d53914d492e0bdc24045")]
    [InlineData('2', 16, "4e9e3870a3187c0b898817f12c0aaeb7")]
    [InlineData('2', 32, "4e9e3870a3187c0b898817f12c0aaeb7b664894185f7955e9b2d5e44b154ead0")]
    [InlineData('3', 16, "0a7fddc22e37eaf05b744459f6129fd1")]
    [InlineData('3', 32, "0a7fddc22e37eaf05b744459f6129fd1c97cb501aaf497ecb6d5d9b1cfadcbf5")]
    [InlineData('4', 16, "f7275a1ebcf0a3d7fc46e235dc236a3d")]
    [InlineData('4', 32, "f7275a1ebcf0a3d7fc46e235dc236a3d678ea7c47b642b8aec1d0855d6bc7e4e")]
    [InlineData('5', 16, "b485d77fdc221ecb320201c4cd09ee31")]
    [InlineData('5', 32, "b485d77fdc221ecb320201c4cd09ee3146aaccb460a998c1b803ab4186ecdd43")]
    [InlineData('6', 16, "21d93093fe84db44c4d2769ff7e4f2b5")]
    [InlineData('6', 32, "21d93093fe84db44c4d2769ff7e4f2b5dc920dcc58ff7f390cdd4642ef7049d5")]
    [InlineData('7', 16, "f99079a8eac6f051fac4e62b17f6bc86")]
    [InlineData('7', 32, "f99079a8eac6f051fac4e62b17f6bc86ff0ab03eec648e776cf65781fd9fe997")]
    [InlineData('8', 16, "cac75ec753ceb7fcf9e9a9a6d84236c1")]
    [InlineData('8', 32, "cac75ec753ceb7fcf9e9a9a6d84236c1d39b8a013bd48e547c5a7409fc9eef3c")]
    [InlineData('9', 16, "d8ef0690db21f1f2975bb5a860f7c46b")]
    [InlineData('9', 32, "d8ef0690db21f1f2975bb5a860f7c46b92e8383520b71d485cc37b267c247ca1")]
    public void Sha3Shake128ShouldProduceExpectedResult(char character, int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake128(length);

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Shake256 should produce the expected result")]
    [InlineData('A', 16, "5e6812c0bbaaee6440dcc8b81ca68096")]
    [InlineData('A', 32, "5e6812c0bbaaee6440dcc8b81ca6809645f7512e06cf5acb57bd16dc3a2bfc57")]
    [InlineData('B', 16, "9b4033bf5151724308b4b1fc90f15346")]
    [InlineData('B', 32, "9b4033bf5151724308b4b1fc90f1534688ea1a17c911aa3a897d5b6a05db5c25")]
    [InlineData('C', 16, "6c1fcc1b777f8c560a5c6ac7a21d5d4a")]
    [InlineData('C', 32, "6c1fcc1b777f8c560a5c6ac7a21d5d4a73e6948eae9c7c7b93bc5e2085564999")]
    [InlineData('D', 16, "7bb2dce81e4f414e23454084c1c11d94")]
    [InlineData('D', 32, "7bb2dce81e4f414e23454084c1c11d948cafdf5e85932618ceae8b3f9953c51c")]
    [InlineData('E', 16, "4f78bc55b48ab68d5c7c5e91f7a9959d")]
    [InlineData('E', 32, "4f78bc55b48ab68d5c7c5e91f7a9959d4b6748f1580382fe6e2170a6c0dbd691")]
    [InlineData('F', 16, "ba84a59142ae7d8a245819b992152e95")]
    [InlineData('F', 32, "ba84a59142ae7d8a245819b992152e95e167316c74e0cfb6867bf7177f6030a6")]
    [InlineData('G', 16, "a01dc253b94528539c20cf5dfefcab12")]
    [InlineData('G', 32, "a01dc253b94528539c20cf5dfefcab121de3524f701183a1e830e898705dd7a3")]
    [InlineData('H', 16, "fd3d47ec252afaf37ee08bdd346a40bf")]
    [InlineData('H', 32, "fd3d47ec252afaf37ee08bdd346a40bf768c1cb68432d01729b91c1c71b5d42e")]
    [InlineData('I', 16, "ab44b02fab0fb7917eda9e709efeb696")]
    [InlineData('I', 32, "ab44b02fab0fb7917eda9e709efeb6964172fcba345c6fe73ed6219bf77269fb")]
    [InlineData('J', 16, "0bc8dcd68d6402c2ae942cf13d968229")]
    [InlineData('J', 32, "0bc8dcd68d6402c2ae942cf13d968229e28574a5f94110cd22b97afc373f26bd")]
    [InlineData('K', 16, "fba4254262cb96830ca70a363097888a")]
    [InlineData('K', 32, "fba4254262cb96830ca70a363097888a21cfe0fcc0668d7a4952a621bb619f6b")]
    [InlineData('L', 16, "becd3c8f9f288f6728f3bea68a80a026")]
    [InlineData('L', 32, "becd3c8f9f288f6728f3bea68a80a02630e387551bc9530ee1548e37a55f6fd5")]
    [InlineData('M', 16, "38613aed74157de1524d2b24f8ebda7f")]
    [InlineData('M', 32, "38613aed74157de1524d2b24f8ebda7fa0890b869d50625c4f5512f957efa5da")]
    [InlineData('N', 16, "5284ac45c58ae1800442c5d43d067e13")]
    [InlineData('N', 32, "5284ac45c58ae1800442c5d43d067e134bd6bbf40c671343a553739d42cf7d22")]
    [InlineData('O', 16, "4d9223d5444c05e92d9d0abd37d5020c")]
    [InlineData('O', 32, "4d9223d5444c05e92d9d0abd37d5020cb41f10f4a27392c2facc124135b7fdcc")]
    [InlineData('P', 16, "f1654d7130e8d85fca768dd011bd5348")]
    [InlineData('P', 32, "f1654d7130e8d85fca768dd011bd5348f491e6903576c8e478b9fbe8b86322b2")]
    [InlineData('Q', 16, "b64d0e68e2d381fac86172d12f1343b2")]
    [InlineData('Q', 32, "b64d0e68e2d381fac86172d12f1343b20fcda532d59daf77eb44c5fc5943f4d7")]
    [InlineData('R', 16, "4f6bf07cd877ad550fa2aec9ab3a10af")]
    [InlineData('R', 32, "4f6bf07cd877ad550fa2aec9ab3a10af827e9285551c3d4009ceaeb3f9db7699")]
    [InlineData('S', 16, "c5c21f9a333d36e7a9f5926e2e4a9883")]
    [InlineData('S', 32, "c5c21f9a333d36e7a9f5926e2e4a988386c13126a454cce340b5c3a513a87ad1")]
    [InlineData('T', 16, "a695c007e9bba6a81ed1ca7cbb853ecc")]
    [InlineData('T', 32, "a695c007e9bba6a81ed1ca7cbb853ecc66415e06ee027550f26ef287ce51f839")]
    [InlineData('U', 16, "85ce175af72e5877f858a1307089c207")]
    [InlineData('U', 32, "85ce175af72e5877f858a1307089c2074cb5b6573d08f76ed7169bc52b2d1a75")]
    [InlineData('V', 16, "0621c3c0c6dbb3d2c4daa2c4d5c66c02")]
    [InlineData('V', 32, "0621c3c0c6dbb3d2c4daa2c4d5c66c026d2eabcbbbc55fcf811e77a3b306e7ff")]
    [InlineData('W', 16, "77e786c411f0de5be44de0e4b034bbae")]
    [InlineData('W', 32, "77e786c411f0de5be44de0e4b034bbaec33a2c31e372847e342f92a0ae2a6fdc")]
    [InlineData('X', 16, "5404f315ad1550324f15a8ca77b41d77")]
    [InlineData('X', 32, "5404f315ad1550324f15a8ca77b41d7794c0878ec1ef8c3bd800710d47f384f6")]
    [InlineData('Y', 16, "cd7b48e3d243c0912de0cca30e7980ae")]
    [InlineData('Y', 32, "cd7b48e3d243c0912de0cca30e7980aeedb188bc957aff0ccefd43f663ba2a73")]
    [InlineData('Z', 16, "fd48054304d155cff97a6e1c4a531872")]
    [InlineData('Z', 32, "fd48054304d155cff97a6e1c4a531872e99c456174bce19a660fdeb025a5427c")]
    [InlineData('a', 16, "867e2cb04f5a04dcbd592501a5e8fe9c")]
    [InlineData('a', 32, "867e2cb04f5a04dcbd592501a5e8fe9ceaafca50255626ca736c138042530ba4")]
    [InlineData('b', 16, "e5796351f59c6264ac1866da170b79de")]
    [InlineData('b', 32, "e5796351f59c6264ac1866da170b79de04cecb6317de6b05ca08e42abf32c785")]
    [InlineData('c', 16, "b0f080246a623d2588fd6a8427575287")]
    [InlineData('c', 32, "b0f080246a623d2588fd6a8427575287703ae4a8451e043d995f1e8f0afc3bd7")]
    [InlineData('d', 16, "0142aad0876d139fe243d071563a634d")]
    [InlineData('d', 32, "0142aad0876d139fe243d071563a634d56d2e3e1d15a8edf51f16b98d26467d2")]
    [InlineData('e', 16, "24e123d7538cf7a23c8f8e6ed94b7e0f")]
    [InlineData('e', 32, "24e123d7538cf7a23c8f8e6ed94b7e0f8edaf7aa4a10957583373c341fd4d1e3")]
    [InlineData('f', 16, "b967c7cc5249454c318a7fbbbaaeaf36")]
    [InlineData('f', 32, "b967c7cc5249454c318a7fbbbaaeaf36309a492e53ab1f604a9ac59cdfb57b52")]
    [InlineData('g', 16, "10673dc5dbe94c1db4b4d5f909a954ed")]
    [InlineData('g', 32, "10673dc5dbe94c1db4b4d5f909a954eddf17f22a6e05e66724dab315b3e5a84c")]
    [InlineData('h', 16, "f877e016a2c3167ca3df83e5346d1b06")]
    [InlineData('h', 32, "f877e016a2c3167ca3df83e5346d1b061a0bec1055ce45b729b181fc41bba729")]
    [InlineData('i', 16, "3bb3af4ee58cbc3fc615b29774342549")]
    [InlineData('i', 32, "3bb3af4ee58cbc3fc615b297743425497be0843a96a20a4a2b69d201c025eaf0")]
    [InlineData('j', 16, "cb085dbb162ea9c3f2be3025c861c322")]
    [InlineData('j', 32, "cb085dbb162ea9c3f2be3025c861c3222c98e59cb78e4d083a8bab80b0174bb5")]
    [InlineData('k', 16, "fb7e1de572ddc648221bb7f112e43fb4")]
    [InlineData('k', 32, "fb7e1de572ddc648221bb7f112e43fb4bdd0c11d7927816b70019e18c5209f0b")]
    [InlineData('l', 16, "b29190b5fb60edf2f39432e6b65ff662")]
    [InlineData('l', 32, "b29190b5fb60edf2f39432e6b65ff66216a9d1952bddf39a5ca98f5733f0716f")]
    [InlineData('m', 16, "2be3c6b9a40e58d8498a4cba8492252b")]
    [InlineData('m', 32, "2be3c6b9a40e58d8498a4cba8492252b41386129f3aeda88be7bb1f6f6d8a98c")]
    [InlineData('n', 16, "b19698e3b6c338ee13082951975ca366")]
    [InlineData('n', 32, "b19698e3b6c338ee13082951975ca366c01454b4447e4468664b68b88df81da9")]
    [InlineData('o', 16, "fc45b64dee64820667da8ce089f1d670")]
    [InlineData('o', 32, "fc45b64dee64820667da8ce089f1d6708059fe9b72145f0d974c00bca7c678ba")]
    [InlineData('p', 16, "8de786bfe1d6dea5b32cba7febedd6ff")]
    [InlineData('p', 32, "8de786bfe1d6dea5b32cba7febedd6ff144ab0e03fe85108c6270a719d572195")]
    [InlineData('q', 16, "2d8e1bec7cd9bdaddff81fac8bf5773f")]
    [InlineData('q', 32, "2d8e1bec7cd9bdaddff81fac8bf5773f7413082c1bb11d415b23bf376f872ba2")]
    [InlineData('r', 16, "0c4e806212458a793a21390358704530")]
    [InlineData('r', 32, "0c4e806212458a793a2139035870453078cba6a4cee11accd88e366db4b3662b")]
    [InlineData('s', 16, "aaed5602244df2167017e5c0f2f682ec")]
    [InlineData('s', 32, "aaed5602244df2167017e5c0f2f682ec5fa02fd58b7de9a7a264b6b6635b3d97")]
    [InlineData('t', 16, "d72a9756dd8b448a638aa7758df2fc00")]
    [InlineData('t', 32, "d72a9756dd8b448a638aa7758df2fc006eb3241e0924db117a584cb53b46aa9a")]
    [InlineData('u', 16, "4d3948929081f779f08265b0abd3eea9")]
    [InlineData('u', 32, "4d3948929081f779f08265b0abd3eea9ca8761783a7aa2532f6a724935de330d")]
    [InlineData('v', 16, "6894f9ce4cf4da4028eb279cf53ca8af")]
    [InlineData('v', 32, "6894f9ce4cf4da4028eb279cf53ca8afa78946cbfe6cfa1b3e5b672e107b20c9")]
    [InlineData('w', 16, "83901a8d9dc152e79f2bfd4ec75790bd")]
    [InlineData('w', 32, "83901a8d9dc152e79f2bfd4ec75790bd774546aa54c7590f82b6fea524ceb075")]
    [InlineData('x', 16, "c851121abe8095973c9bdc1b446089f3")]
    [InlineData('x', 32, "c851121abe8095973c9bdc1b446089f3249cf74e0b64660b61d73bcae0350f4d")]
    [InlineData('y', 16, "0c0e7900a42d04a00afef323858bc461")]
    [InlineData('y', 32, "0c0e7900a42d04a00afef323858bc461deb73fbf01343d3de7dee05f40f8057a")]
    [InlineData('z', 16, "b9bb5914c3385c795584905092480f74")]
    [InlineData('z', 32, "b9bb5914c3385c795584905092480f74cf633b662308cc1744c39136625b44c1")]
    [InlineData('0', 16, "7e8b1406d903bc9137fb69e769742c8d")]
    [InlineData('0', 32, "7e8b1406d903bc9137fb69e769742c8d3e36f1c4fed51a608809b08de9f3e4a0")]
    [InlineData('1', 16, "2f169f9b4e6a1024752209cd5410ebb8")]
    [InlineData('1', 32, "2f169f9b4e6a1024752209cd5410ebb84959eee0ac73c29a04c23bd524c12f81")]
    [InlineData('2', 16, "a5a4f007abc4dfe1eb19f685efde94ca")]
    [InlineData('2', 32, "a5a4f007abc4dfe1eb19f685efde94ca76f77dff7279de620dd52074b33fa1c6")]
    [InlineData('3', 16, "08946cd494a2c00b0e9321af0c225309")]
    [InlineData('3', 32, "08946cd494a2c00b0e9321af0c225309e9d1b9d14ce8eeb4ed5182031c3f29b0")]
    [InlineData('4', 16, "1d8a904c4fff579bc28fd3a8065762b9")]
    [InlineData('4', 32, "1d8a904c4fff579bc28fd3a8065762b958f81089579cf2177ae7489a90f7d396")]
    [InlineData('5', 16, "172f84a65934fc29776758a22ad080b3")]
    [InlineData('5', 32, "172f84a65934fc29776758a22ad080b341b497b1967d89a20dbd8420f4d4507b")]
    [InlineData('6', 16, "cc2dc8d8adb6439605fa188ed5f0d8a4")]
    [InlineData('6', 32, "cc2dc8d8adb6439605fa188ed5f0d8a43930b8e1eb8fc46e63dd9ab6a643910d")]
    [InlineData('7', 16, "112a104bd5901f13abbfdcd11be28abf")]
    [InlineData('7', 32, "112a104bd5901f13abbfdcd11be28abfeea892133b1861afe6cc4c999cc9c160")]
    [InlineData('8', 16, "09dfb269bed6186424d76680f5b936b8")]
    [InlineData('8', 32, "09dfb269bed6186424d76680f5b936b858b844472cbc5e1ea59d24282e8b3e31")]
    [InlineData('9', 16, "0d869764040d76f626be277bc31072f1")]
    [InlineData('9', 32, "0d869764040d76f626be277bc31072f1e85d9376223b23584817a2ba9834304f")]
    public void Sha3Shake256ShouldProduceExpectedResult(char character, int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake256(length);

        // When
        Hash hash = Hash.Compute(algorithm, [character]);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash224 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("abcde", "f", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("abcd", "ef", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("abc", "def", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("ab", "cdef", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("a", "bcdef", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    [InlineData("", "abcdef", "ceb3f4cd85af081120bf69ecf76bf61232bd5d810866f0eca3c8907d")]
    public void Sha3Hash224MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash224();
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash256 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("abcde", "f", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("abcd", "ef", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("abc", "def", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("ab", "cdef", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("a", "bcdef", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    [InlineData("", "abcdef", "59890c1d183aa279505750422e6384ccb1499c793872d6f31bb3bcaa4bc9f5a5")]
    public void Sha3Hash256MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash384 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("abcde", "f", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("abcd", "ef", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("abc", "def", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("ab", "cdef", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("a", "bcdef", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    [InlineData("", "abcdef", "d77460b0ce6109168480e279a81af32facb689ab96e22623f0122ff3a10ead263db6607f83876a843d3264dc2a863805")]
    public void Sha3Hash384MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash384();
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Hash512 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("abcde", "f", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("abcd", "ef", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("abc", "def", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("ab", "cdef", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("a", "bcdef", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    [InlineData("", "abcdef", "01309a45c57cd7faef9ee6bb95fed29e5e2e0312af12a95fffeee340e5e5948b4652d26ae4b75976a53cc1612141af6e24df36517a61f46a1a05f59cf667046a")]
    public void Sha3Hash512MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash512();
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Shake128 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("abcde", "f", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("abcd", "ef", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("abc", "def", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("ab", "cdef", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("a", "bcdef", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    [InlineData("", "abcdef", "9428dbf9493c942630c0618d8a0983d518e828a7c0f4a39c2a54e013f64ebc125475308324e864c2617062639263a24bd58c26379342b40bad4a81e6f3e2c32e41bcd52927971ad0374c88f3244b6d229652a454fdc4fa422838eab19aa2fac7ddf457d66122ea674424e534b529e65684b2b4e3404914ad814a53")]
    public void Sha3Shake128MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake128(123);
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Sha3Shake256 multiple block transforms should produce the expected result")]
    [InlineData("abcdef", "", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("abcde", "f", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("abcd", "ef", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("abc", "def", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("ab", "cdef", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("a", "bcdef", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    [InlineData("", "abcdef", "81d5e45d095acf3c0decf25bcc63f6ee16c689b909b48040ad91c7c67dfe4e9fec910fa73b44e84541600b5a5736b7b0869c89b1e403e35e550158e09bdb7430a6617cff69c0f10fdaf1035ac62ab6277cd267616c06b9ce4d888543ba5771eecf12df031e9add88f314de262dc1bb0c4aae43c9b5316fea1af11d")]
    public void Sha3Shake256MultipleBlockTransformsShouldProduceExpectedResult(string firstBlock, string lastBlock, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake256(123);
        byte[] firstBlockBytes = firstBlock.ToByteArray();
        byte[] lastBlockBytes = lastBlock.ToByteArray();

        // When
        algorithm.TransformBlock(firstBlockBytes, 0, firstBlockBytes.Length, null, 0);
        algorithm.TransformFinalBlock(lastBlockBytes, 0, lastBlockBytes.Length);
        string actual = new Hash(algorithm.Hash).ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
