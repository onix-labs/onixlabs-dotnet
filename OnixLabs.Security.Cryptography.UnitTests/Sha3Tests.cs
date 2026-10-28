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
using System.Security.Cryptography;
using OnixLabs.Core;

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

    // The vectors below were generated with Python's hashlib (an independent SHA-3 implementation) over inputs where byte i is (i * 31 + 7) mod 256.
    // The lengths straddle every SHA-3 rate (72, 104, 136, 144 and 168 bytes) and include long multi-block inputs; SHAKE outputs are 500 bytes,
    // which is longer than either SHAKE rate, so squeezing has to permute between output blocks.

    public static TheoryData<int, string> Sha3Hash224ReferenceVectors => new()
    {
        { 0, "6b4e03423667dbb73b6e15454f0eb1abd4597f9a1b078e3f5b5a6bc7" },
        { 1, "cede129be98611a262bb6aa1ca460aec32328141ef164038c3d39fb5" },
        { 71, "981c6d9ddb829d1e02fa078623606994b8fc7c9911ae8e2b2d9218a8" },
        { 72, "d7299d718f34a0027ec2b6b674e63f975aa379cfe8d5e95dbc2a9e11" },
        { 73, "dabdd71d41daf728f2be92e43c4867c0744d45cc9863ee9ced19a650" },
        { 103, "7a4aa8c0d6f9210cc4843a535752259ff22f80f54e7af51a60561b86" },
        { 104, "51b14007010580d4f024604a120c491c4df5226ebc5d74598494eb47" },
        { 105, "9f9ff20debb2d91f2adcd67c17d837621b5048e41c3c8579ac3d5862" },
        { 135, "fc86f05344e72793ec11a3c2d6cbd916415eb58ad2b343aafd811654" },
        { 136, "4e12a11213ab7f51a33e14df669854f4fc0651bd516ccb6f8835eadd" },
        { 137, "af07ebe4b19474ecda9d1cb30d60c94f7aa05471fceabbc5b6490ec6" },
        { 143, "cebd5e21a7cf837343b82f921dcdb51b20e94a30fa97241cb59d859a" },
        { 144, "3ce60a8c0c0455d78859a85f69a988499f50f34404b7552fb72244f3" },
        { 145, "62eb48af0677968d04a8b23cfb27e516af1b3126c2cd9900f290c5e8" },
        { 167, "5af4e26e23be417856a969ccbf5f15981eff905103554c36699d9f01" },
        { 168, "fcff57c721db3053c87981e47e68d87f3cc614d4f7709ba869b1c1fc" },
        { 169, "be62598e576fa7d0b3dfdf6c2d60cf707e969d3c31cca368ba258f74" },
        { 1000, "7989dfd171a962c2ddef4ca6034a480e33f92d9ec131d6e378309cee" },
        { 4096, "0827e127f3ab3d695286a777d462a51800bffc152cb15d412517dd2f" },
    };

    public static TheoryData<int, string> Sha3Hash256ReferenceVectors => new()
    {
        { 0, "a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a" },
        { 1, "5223f7670b3b9ba04f57d477478ae77a58190d89f21da0b0be774735e23f9c96" },
        { 71, "08dc4b08103212f7147acb126935f702831ddc658f1af45eb56022bc006ea9c7" },
        { 72, "e1db4b2b87ec8b848450dc0b2696aa28d27ffca06840f184a3b96113275288e4" },
        { 73, "768142f9b927a4ee4975ff9eaf63de4b66e299a42a136d5523f85f49b2e123bf" },
        { 103, "27aee5290a67aade8ab6f8fe24b7046169658e8a403684eda825387cff4e33ad" },
        { 104, "a206ad75da5a1cc045683988b7caf2b3802e1d994946f5ed9b9d327cc5de1a06" },
        { 105, "00bfd3d8e81ba1c8e0edb1339d99d88c504c6cfda5bbc17d1ace29bbb0d32db8" },
        { 135, "27177c35076523ab12bf3c60c7e340ac833bee9017b0b9d273d25f1bc8759ce1" },
        { 136, "bf55dd8c0b4dd0f226d019f9b85d74e3d77cd13409e1306665a44cc688a93d6e" },
        { 137, "b88ad98270115d9a7d164a81476b4b1c999eac7a9b67cfe013c365496a599e11" },
        { 143, "61146d07a4fd5f159342f66f43a0b0cdb4b5ec9a886b2b9e4d12979f0b6ddc30" },
        { 144, "40d4e5a107f97724a6120a755ef7f314d77e7e5cfcaeb90fa5c65bb6fb370bc4" },
        { 145, "554a8399c4522575775d9f61529d3cf989ed2f003b40a237cab95f37c66aa075" },
        { 167, "c6f3aa8d05881d37ba5849f32c719edfa77e3a98f0fa6faf0c13807bde8f9f78" },
        { 168, "52721f3b517c6521539677e4252d4c7c550eeacfd2befa64699e5b27fb19c7e3" },
        { 169, "707d8feeca714529fc25db07d8c40fe594dd76518eccfd9b38d08a491c18892d" },
        { 1000, "e9612e6ecfc2fc3c9467302e2563d3155906eaf49e0bb663e460791ef2fec839" },
        { 4096, "36b10ea74c2d902eadf8d91eef3f3640e2e362ec5ecaf77eb47a758bc4f632a5" },
    };

    public static TheoryData<int, string> Sha3Hash384ReferenceVectors => new()
    {
        { 0, "0c63a75b845e4f7d01107d852e4c2485c51a50aaaa94fc61995e71bbee983a2ac3713831264adb47fb6bd1e058d5f004" },
        { 1, "63f5763974f03d59b3305ba886d80e8872d9695ca21d3ecb1612e50ed45c3ea4b69c2a2004f5b19d57e455f6d276e0f8" },
        { 71, "dd449c999dc3b39d30b7b3f9701426c007ffef2af5019ee2696d5f57779584551d8ef47d3d2b716917ac77dae1e423a0" },
        { 72, "90d6bb5f54e1f79192981d60d5e22117c86518789019086227796e41b583ba8e4571bb47918826a1990db22b5262ccd0" },
        { 73, "8bfe321e745e3660665743c46f4cf2c8a532daec4a66581c636a6d43548177106acf51cd376c68a7a92f3b11f17086e0" },
        { 103, "89d3106193588af14d213f1c255f5c41d777c9343d93f761a1f3a62e6bf71e8e0487af222ab85777de9700043b5f9bbc" },
        { 104, "4f31c05e040f370e5a5fed88aade66aea3224117cdabc87510439766e595091c87ec72f91045d3a637f360ba957f14e9" },
        { 105, "e146b047ccae1f418cc3816d4ee139ce0a639b9f6d062e78b20fd0b00706e50cf1acd34b728c0729f804dd7a819a81b8" },
        { 135, "96978753dd68d38945aafa0f3cfc31fad8b6771868959d154201f59d3f666a4674c4034a29d0e00e7053171466bae0d0" },
        { 136, "11a32a1dab51c3bbbfb2b21c2c7dfe431a696bf954cddc88f743561e8a8c2deb3e11584702c747890f02fca70a802b92" },
        { 137, "be53b34438176f7e70b73be342c383ecc43da08b0173f19919342a548da61b7959eb3eb9248e185716f99192e003efbc" },
        { 143, "9d5f2cbe7363e8690ac7421f98503634729a8f81b596528c413f665944fb17fcd5762ce44ca4a2cb6032bc17bddb5976" },
        { 144, "d288cde4b4879f3d784412ead6fde4fe325d0e7f90486459732ad908a3be27a71bc655b516a52e6b3642481cbdc6615a" },
        { 145, "2d8521b887516eaefdf63376fd09eefd7e37e5ddfb3f4ea027fb2abf9e45d1e9ce78d3271507310b99737bdd11d2f188" },
        { 167, "c9bf95a6b52d4f6160d25da21a6f42c1ddf791b207dc3fe80627f20281a5338f71eaead313df47abfb352fa9cb90fe45" },
        { 168, "72a663b78cbb1e7f338be57491c343c03428c0ffd797839964f3af49bd6c6ca9e4dc3ebcb3c391b89082bc63dc2ede24" },
        { 169, "376579f9e75bc4c491fd0613bfd11a1e6f245b48cc660224caa4dbafba0bf86c0afffe13b9022149dc0bcc94a9c057b5" },
        { 1000, "70f30a5988edf28989c9d1bce5b07762120fbf8c9f53a466db2a02aed8c0f08bd24f70a57da28269c565575524967823" },
        { 4096, "2c7fc264963d4911922d91f6ee874dca82bb427154a2a675106ed4ee2e146ba7d621c14c0b7e230063c0896fea6e9895" },
    };

    public static TheoryData<int, string> Sha3Hash512ReferenceVectors => new()
    {
        { 0, "a69f73cca23a9ac5c8b567dc185a756e97c982164fe25859e0d1dcc1475c80a615b2123af1f5f94c11e3e9402c3ac558f500199d95b6d3e301758586281dcd26" },
        { 1, "340d7ff9f80cab5032d68e7437a7aad37695f663934c54922f7f09b22be42c32e17e6ab08abf273f9f23121120fa1660e262f8625c97419bacd73310392f8b3f" },
        { 71, "15e93a11d76e7f88387b7aff41ec4e220da38c1bfb86ae472321667489308ad1b8b1aa6e2c3413a9e6d558381f27e62177924b53af5c5bcbfaed6e3b42b4105c" },
        { 72, "9032e99e709fc9205b3f5f15f68f4f02f5afa4ffbed99628ad385247be2b492d82cf4e7871339535baf2424443100cff38587507401d0f241e5006a474788726" },
        { 73, "8d24425518611222be23bf73f200e9bb9985b21143ddc579c6884cdf6137122477e039b8c174fcfd9df773459c9b048edc855fcd5bccbee8c6533556113f7ecd" },
        { 103, "74013254c8b66f8c479a3bea2e0869641ed9ad3dad4e21f98464e420f79d782e0395e453c2527659f4dc9de200017ef4ce50d4f0a7910aa3a108cc84d8b18a45" },
        { 104, "4a6f8541a85dbc9916e54adeaad03b6d73a3313f2019ee6b05e7429a3578d215dd5757beb34d5a47f202d35ec6bbb22fa75842241f0468621c661584a274f8f5" },
        { 105, "46ad3d779b4d4b03538b994febf8759a4ee61ac99e8af3edae5668b3bc47f3ce4d9b5b04f81cce81cd110db7d3f52533504898ed3144ab081604f040a90e60ec" },
        { 135, "583439ef05460f9775ab2dd129cf86e3619cb0cdd5b08414ed4a234d061c5f082a2a0b4879d8dff826110fedbde9d49ef97bed427d6f7295a2dcf5e59bdb37ce" },
        { 136, "b58e567cc5e9e7fbf1b6485c814f97030748ce1218ff693618530cd199e9a5e731bb09e8d6348528ab16bea466e18a2c0f2d0eb48d2d1de63cde06cb5cbfb685" },
        { 137, "c06410f36f7850445951bde73059bab501cbc3300939cc5a880385d300c1ae0b6d569d196e9291c63fa97a1ad3094f2f8bdd4a7d2a1d1033db6b769d65832f8b" },
        { 143, "f5936fb3d32236ec8e833628620036519d05a5a5e018737d6fe601b665e88a183f1aaff742f017690a82c6e667c2b3232439967329a9f80646df439c97bbf8e1" },
        { 144, "325163bfe34550a34c220034f05427f523a2f9c23f8a1dda8c52543559b4a3977f6b1c4eae10e219cb0b6f82bd0f8625e44cd750db06e1a808f864a9777b097a" },
        { 145, "0b9d4c5626ad8d86d0b513b69f32a1e5fc441a5d641b96dab364fa748c555baffe90d3414c912c3a5b6f0b790a1d40f28f3c89167a9dbd5310ba5f55a8c7adb6" },
        { 167, "de602352e16adb513c498647ccf1e5c0e88d13020ff2f5d61186adcb4b7d04c0b69b28db79a90a9f4e91676c56007b1d6b14cdc2af00fb8c3c2a24a29777b6dd" },
        { 168, "7e2d681e8626f47c8445adcdc1038b99a6eb21a0cce36bb0d267623fdeed2367bff61647f621aa5f43273eed83264e22ddb9e1b17528b61c9ce62a60680df92d" },
        { 169, "b73230ef068d3740f4e50c1c0ee454bc80485bb2d960832e0b21dec2f7cf4f706660fb74d7488b0b006eef3d4a229fbd29d3742224758c09437e5590cc58decc" },
        { 1000, "842d060a3f1a6d6b80aaf57258f0edb7e1c0abcfa112479ac989b1527d0b0c5ef6da88b7f6c80cd05a7a1e08d46865bf02620044ded6ef5c21c8902a38d4f909" },
        { 4096, "2a9bb1f20a724c516dfe5bdb1d8ba0d308f5975e668dd7feeaa34b8e1780f07088a14b2ec39967ce4e77165a4dd9c4539429b2187be21f03c4a257333f4acd9e" },
    };

    public static TheoryData<int, string> Sha3Shake128ReferenceVectors => new()
    {
        { 0, "7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef263cb1eea988004b93103cfb0aeefd2a686e01fa4a58e8a3639ca8a1e3f9ae57e235b8cc873c23dc62b8d260169afa2f75ab916a58d974918835d25e6a435085b2badfd6dfaac359a5efbb7bcc4b59d538df9a04302e10c8bc1cbf1a0b3a5120ea17cda7cfad765f5623474d368ccca8af0007cd9f5e4c849f167a580b14aabdefaee7eef47cb0fca9767be1fda69419dfb927e9df07348b196691abaeb580b32def58538b8d23f87732ea63b02b4fa0f4873360e2841928cd60dd4cee8cc0d4c922a96188d032675c8ac850933c7aff1533b94c834adbb69c6115bad4692d8619f90b0cdf8a7b9c264029ac185b70b83f2801f2f4b3f70c593ea3aeeb613a7f1b1de33fd75081f592305f2e4526edc09631b10958f464d889f31ba010250fda7f1368ec2967fc84ef2ae9aff268e0b1700affc6820b523a3d917135f2dff2ee06bfe72b3124721d4a26c04e53a75e30e73a7a9c4a95d91c55d495e9f51dd0b5e9d83c6d5e8ce803aa62b8d654db53d09b8dcff273cdfeb573fad8bcd45578bec2e770d01efde86e721a3f7c6cce275dabe6e2143f1af18da7efddc4c7b70b5e345db93cc936bea323491ccb38a388f546a9ff00dd4e1300b9b2153d2041d205b443e41b45a653f2a5c4492c1add544512dda25298" },
        { 1, "7c5f30fe262c65b3a20a2c5193a291d33a1022466825e0513dc744f1821aeaeda46d7bc57538c0ae196ae3b7ec580a9e167d7fac2abd85c2dfacabfcde6ba06428b729e5f8f99bba5a361ae9d0aa17717041fff940c257ff42934105f96a4807bb2b0c2994a8ebab31ee1d4442524a8e0cd793e6be6d80e0bb6033472bac25e9914d6953dd84bea6cf16408d7f38bc30e4f8f694aff996f37373b659362585ccc388362060c4a3fbaa622f5630a09e097e9ed9e6e4f3c737d2673eb836d688605766944e7373c72dd6197f855ed872dcdb98cdcb578bc703ab4a0794760be9fb41fa10eca8fcfabbd88bc4a291d67720ec9303ad9805f254cabaa6c9c2f11ef7a207df5c8cd09ffbae049f5d9e3aec83fd6afa91230af0668d47bb50db7802d8065776f7e6ca19e9b48bcf224adf2d75a2dff87311d21ec582618cce9a266329049d59ef6092b1e3c903be26678c8cc93ed104a1f0033bd7c23e397b280f050676707bc32ee81e305a30e14f5946568d784f0f52d1ad7f01b1490c61efab4d9c3ffca5650c1cf1cf0ee0c43c3e44ecd7da34311d30fd675ae6b352f9b57d8cd96654545ed431c91b090afe5d818ff0297200cd81ade83f8e6dfa1f6082f6bc42f5a39ed3c76fde96f24db012ae2abc47c745abf1905dc41f7ed8956ecb9b046e716725b7d06d339252ebf58ffd1bc52ff650f021" },
        { 71, "3ab54c5ebbb81171374c0490df61c93886abe0abac1a331074f57b7853f2061a292c5207b97c6362bef9572580cffbefc5650e49698149adae8bdc2d67072a1baa49634b177aa9421eba5faa18157c0275808812ad117cd282df3effce52a696bc2fcd88a95c00a9f8e811d1cdea818cb02245dad1148cf0d6c40e0a9b087a7f114c637b7df824b31835ef07b624fd7e6e9de19886edeb9bb433a2ea95e81371dce40c621f1f2e3b12b2e9539177931b2c34165a5cab6c669de875fa569b4a19bb112b06d9de418d76f4c98d0cc3fb392acb2f31342d6537662915d45a5cae13ddca5ae814ae8d3049bd7604ecf15892c724fcb52462a34ac970c1971f1e8db4ce1c269a46e347482e137d167bf76e455cf84101061ef2699148507686da9e100f03a312ddc2779789a2b5d2859e6d7cfd546798582fc56547050a2d88e2edc9b46a99abc3b1011c2a5de9fe4ffddd522c16fbfe5f319e41bb7d63f86f29588699e731c882020d7a6add8c188922d21b01b8750baf72566bdc9695dadd32177298c3294b5ec2305ce4182eb84be8fc9faeaa7381f95e5db813b2543bcfa6b524b9faa7ee5de5f29211703824bfb42193ffd30211dec9f83ea6c38edf1a3afc35c72330f3a309bcdbbd7e09c5d3119275bab5fa2de539296ce7f79e241f62acb942c908a652c7babf82f2f9c91eb3fd525bc79ad0" },
        { 72, "2d66773589e79998061ea4552e73bd219af2e6d200f0dc684855edf534cb4cdd7acbdca4cc27ab97c04c99b6a2777946374a1f058377afd13f23c794596038bd9ab41deecf9458c8612d27ccf52b7b23debd70b4237af3043e0a42d3d89c18445ac02a4aa06ebc44118119ad419c34770e59d6baa6cb052c323b3cd7315f7c07a96310ca9cb9696e0e26f845f3b2f28d042d226f98b64ed5d6f0cf758315bd72641e3ff43c954567b33951e4ee9acc04e399861d881d184ebe2daa4d585d50f70f21c6c3459c28780d8a39c2d81bd38ae879fee7fbb68c8bbd81e186e7ad6481598210414884dea1be83273943c5a8a901cb15900ddcd5ed0369c1ca782bc5e197683d42c58bb20b2caad5e61a38a89625c705c89417480677bb853541faaf4c03fc9e926f74c83a9f983bc11ad620804ffb264589af2e4bbb7cefb7786150d3bc287e52f4d764e60a1aede60c3c48887ef808c33c0b6d2a24c5e7fc6a880629163e627440d15e3218705eedc3850e96723c9a5649f33e4d6b1ee6ad74480439591f9359924435e6da15f991e6b86916731cc579b57beb9920e91b0d4d8633304458e73fd1cc3046691848f53bfc517ecb5e947c949a5b1372679d1ea06b0129fcae1cdd7baabfd27f019fe7686c9f91405884bad2dd41ced3ad890767d618fb52ad94b2355492c57c70f48506459d545cc01d1f" },
        { 73, "d5c755efed573110caba809821086794267252775cb44c1c436a1e12b7ac297062eead6355500f4a790599c56d4b28fc813a37de35d547e3391c804ee20fdbbdd97dc7e13870e7c5cf4485462b9a4b3c147e69a63fefa419bb7eead4c8c3262e321a45f1f7a271eccf1bb67c9d6e7519100233128c011c7663d6cdb561466bdf6570d63325e842b37c9c67e4a69c4fe435788b4dd1f80f9192aff3695036335c0eaf152b34672d310d936227dff9ba108a092ae86b5a0e76473d17e7f5aae76ee8665a7bccd86119e011f05c4e5c82e3b8ef53dc3881bb10ecf0d4542c97e423495356143dd640bcfaaf00be0d87e093abcb5653b292d249a5e60116834a82247b61a472c23d31932064ab79e48f24f843980874ca180c8bda5892441a5b21d1458395d0a1b2fa760f303d0005006cbf9af430e2ac98ad02d964c2ad82e35575311744aa0c317d0b2b64e4c7c92713bf191eb96adde95212e696de1cf96781443e0175da16caa5a45393714f57c05d4565feb9a31f6f44aeb956f84851087cede7bce7f3a4761f77de38e50cd9a7e05c8ce32d4661181486a710495d6e2f4c3d88b5cd26a73561981144d8cb54fd63f36bc5d8e52b35f6d1bf04e8b37e1176c6089d40b00792cceaa334743dc1f86fd6833163cffc4309e14d096192034d47c88b50c145f4402bb2e776933757708924b24c5971" },
        { 103, "14ed0f355a3367b32701a141c228a20b90991d138a3657f46772609f97e448d4d411a70b845d39042956de5de672c9d004e0f124d6278ba1a05360ec3e4ffb373c618d3d4bd6586317c5bb30632e791fc05c8f45b9a2ece93a329b02a53224e3243540871f1194ca9bdf07832246646b278521c177960016cb06fc17f2200fc661a533979584dbebed5a34a730408349401c233d9c5516ed386fa4d2d0f9588d8382a890a60fd1a0004294c618fd35faf1bc2503fd58e4abb40fe13e6addbee96af511beb15d72216f87ae693b623fafcc35ef8231b0073163a9dbf6f0287823128b12ca17d2814d8265cb73940a5dd92acd919f38d3358c19954243d1150a777f6ecb2f21c4975a5d523f080e346f48f81454a80b3e32d48c392d249d7c701071cf6d234023ed05ca477d9bcef3d0cfc93771c7bf1d1534b571bcd325911b826542bf310e9f352a04a235db95268ec5940ee066a0ef2b70c4edc06e464da6fe4c4c8552159d949e632e8319fb44133605e2111f6937fb4b4e3bbe6c3aefae7f13612c7da0f2a2e7d3b521092dad61a2be66e680bed9f2047469802731e6ace1a9c2b233391d0da4f15167a4ec8d16c6864a4579781bd7ff6d71fb576040449bb1e72a8e57caf1918813f75fe6ad8820c624910372deef4c70064db3caf6a0bb92f991c6a396773528183cb92bf5361cb102cdf7" },
        { 104, "0e713f57758e34f1baf8d9f7a16189a92f600f015cb54d57002bc7b15c6eb6d90b3c8a9ed087392d24cd5f9a4e9ae962f017067324b42d48aee908f1d79f297007c9608075bb5f9b860ae7c767f4a41b6e78a5843c6c4db43b99cf1d677f07a2a95b3727ae342d3dc4628c2122b2354138097239218cdd164b2ea753d7568c3c904872e2e137306077c4f51f47870b6839b1d5d3c46fc090a8929420b10ecc9e303564d732697e3577536f530dd3e68ae802b1f1a4b1b270b5954816bab596d0d26c9c0f8582ea3a84634a0c8bee4a9fdd79c1e67b796e7629f48cf001d00075a240df761b50c3f5ee6e824b2e2e1ea181be8ddf37e7e8c1fcccf75ddff58680f7008539c66735ccfef2aa2d0a91af51680c642a40be82e3503f7ed00411545e925b6afe44d9f252c5cfa6b44f9c2b4a9678d725c517eabc28889ae494467ee7a120258ecba9ce4edc27d4f08ab6ed27ef5dc62f9b409d83ca862582b33ccc7c6569a2e64b0a7e00d557e536db808f7bd980b9e15623a744b05c94deb16eac51b5749524ea3764f358641ff6e7fe8b5bbf172692dcc1733a0f4aa3011d8ced1eb11279960d838d0ef0a53552312b00484ba4983ca2af95af1a9d69b748ca32287442f5312dc2ad741dfe4682e3eee09d42150af92d658cf038d80907e8851c54eb4ce7e71c9f50a838a0e889f49e607042603070" },
        { 105, "c8805de20de025503c9a5f5d45a88d3045f7918a6e462f3b5ef79c489414fd64560b1ab120f814e54875346c1760af62582ad11973b45225edf9e1009b3378afbbaf09502e80b1cc9dc319bc091101642e211b25a6b628bdd581e60e3b2eb2f63546358996d37610f52333f61234933cdd7ab30316317b8770f2f05555a162a2576a46e7dea03f6f8080dd024cc0b083b47f350ead06fc2d6471844cb33f5a8c5b78f562d3de34728ab3503d8814cacb24f77b52081e83d4fd2f141c9909d593a32917c6a31c74e3c34d0a003cd995e2f80c783f63de83744d0894a12946b57e7525bd899a850db57b9948a6708e85f33bed8b45f93bdb14260ea4e315ab86feeb5edfdb0eebbbcde4fc564829afe31f5ecf927cbbac0ebc4d6f27eb6e9e60a92697a94e6d67f83f6146ce87d3e2cb323dd62a22789bd04d005e2bcf1d9f46fa4011a410e75196e3e8f2f362aa162dde3c064cd6955fb6de845cac6da96653836d2148e301559540ae6e147722aedc3fdcc03264db1e2772c51d7fa7822ee6819ac79244c055b6e7616b479d4b7af4e93763a29794824defea0b616f869c658fd658794591bd5591d270dc3834d6335e90bd9479528d8bd1cb52f9df45c043c1394ebcca188a995eda178981f56a52efd91502fab8e1467dc30ad26a9e879604d19d8b9f455efdbbb20358fd19e5bd456572d04d" },
        { 135, "1387cd7ff54246b6a3c54a442c5517d10038fcbeb745c1ffbfb3cc580133709c892b5b8cc59d806478d7d0bdd045f096153b6a4d26956d4de4b315ce254a550649c260b273970e4db1dcc9bceef693a627f8e097b229a8cedbe29e3cb2a4bac3e9eefc4b963be13c13f668c330d65ab9a3c818ab2a5cec3cafe0a2f466314ba57d352cc0f9c0a180d714c65970e655c12cd3e3c712db14919860ff395ba4c2c039069fe8491209dd10536ec11cac55079040c81ba8dc3112af8e2e7b0f023cab702934de7692fe681fba2b7d956977c91b73a161f158313b5ee759d1520bb654283bc4c633f732a72bc459fbe18c6aed6db8b9f8b0939a56859a3523e63d8ca26922f57b409f9d761d72099d16a88e7bfdbf062bdf3370db63987951b8cdc128f6b1490519e0978f321faa34bd1510fa8f663f0ff914ac8bd850d1d8b918a5d19a9018ce57cade43b4abdbb27b03aa0d9d2ce11724c6e2ff7b8181271db7a9bf64a1f2fc5fa3ce2b96a70e1c61c13a0089ce19599fc8c693839f9cfebfd0d95cda6700f5579f59606e3608fdf9bcc3000bf7644632a56d5566efeec2ab8b37929b49ef0e6afb91bf88ebfd0cdaaabd90a0a750710cdf64036de38f7ed369544483ad29a8a47aced06eeee7ff68f12ae78d7f844d8438ac562ec8a40b0af0df75fcaf510a17c2fb3a0ef47d1d28c9d946cb5f6208" },
        { 136, "73681d0342aabcb50f5f50b0e5dff20113750daed75f0a2ddb263966a29057017d8d590b63f689ad6c5ed8540a24555c66717b28d2f5ae2458c1aa38e6c2ededf61d36df36b8a591a7b5bb737fb6888511237a9129b990489298bdbda928580f8f719d8610ce2318c5214efa9ee332fd01b7472ec27ce33dcb090e4163d9750853f80ba271a26f0c1e31179ac0473de04f6328fa639a6452ca8d4d12a9669c9c2b8e92cf405e1ef93fa93f2f3e6e7985af99624e1e7d5ee77758ba462447edc73d88eb69371bff5bf6e5aaf383ef394cddefc318c93014e27bcf38b1b0a47086d4e144fe360f184ac5a197a6199e2e92aa350c2021bc2c6fd364413ff55d7530bdad9fcae144de9a074ca439f3388a39458214c2648de67e8e98ac020def9edd087403f9f5681def29cdef9d64903ea72b4a9afaff0e3237cc4c2ca476ba33f91d0410437084a80d7bc64487c80870f76ed0392d69a1d1fd7ad6ad6231bc4c022cfa3542dac5d65c76767071f68cc0d706a30d4e5a1507b6adafdbac11591b7959584f9489581db79fd698ef33f85d2479deafeacf4450b7b5929a622b2263efe0cafefd80b4e79725de98702136ea2e198af3ba2429b7421c4031880a84993607e2e401b98241b58b9c9ad425adf8332ee1c80832eb3d4d4c2ff4b55c6bd8d3b0a81d9d81ed716acc8511bba63a9868ecc2b111" },
        { 137, "1144f604cc80164a9796a8663f59e641f04a32fa6b25f4e8fc6cc6d95351c8f4a640e5cc20726190bf46303afa7f206f23699e82285fc3ab80b5f60db597ee725ce802edabf656aeba1b59c7b848a8701de0b4df14570c708afab3c79b572c74346a05009e5e33be299784efbf28a864efe155bef2137ef5a38312eb5dbe5f17b7484e1b7f416ae71a274ab8dfc825377c662e6068a9dba94064bfdd5a08acefbe07375868402f891e105079c9b1acf5164b332e667df53d0dfdc62e704146963a013323bcc77944fa0da27619dd7296f1c50dd34f73fe7a31b338632df860387625fb1903009098ee45d8b73ad6edc3753bc6760541a83e83f3b361ce3bc67e5da0518ab25407618fc0010fc732ad482cb7442029f2833ed40c4a80e71eb362b7f228fd49cb2e735cd590184a2d88d3276ea77432d1c82906cd180e0286001d814def84ae7536856c965b11398bacc0355c54371a8221cbd8323d27eb7179bac23542e8d90125ffba0ecf27ae3d729fd509f74b0d129ece7df64d0c3891704ea799635071031cfd7c9cec23ecb528570e2d734009e2b6dd1e4e13c95dfd60b6cb412a44290be779efd409124a539a2cc8dfb082c618bd627e422cafbcec5332de22ce634933bb061225993725ed9168b79ec8dafe0d5e60a039710c980243b9ca1e9bbad3facdcb719f657527c616fa57e4f9d7" },
        { 143, "17e12127c54c3f473ee3e70f417f58685005e529d4b80679c913b1830987ed68287c4d167b419e0d1acd23042a2b010eb2228ff5d2fb86881cf8a304bb2e2653d488eff258c22c968f73196437c06d60a31313d64955f46758c364c16f382d11a646e63b9e8442dd3ae4b5ef4b245428e63901ff16d47418e7ca80f4b3dcc3bf957bc2ada1d3dc88f48d9c88547a43530a0efe9c192c7a5c0a7d5c519bb0b1993defe920a8fee2c8a5c3b192625899913c0427c2ca8774634039ac84be996f0199f5e2d606d461bf9a8a2bdf57c601a73fa447a02ca81e5273dbbb1613966871c28422f65436bb4d4e4627d0f23563cedfb9be41ca3ca61ada82620376ad5a36e3344c3305d0862efe65d4ed29dff9cad05b16ad6ae3cce07e52f958db277e3310396d6dd335dc5a0f139be31d564a5afd11e81c727f12a2dcf15813ab280f4b1c6323a9e2da4ef953901fd5cf5775b64336f91fce90522033bd57c0f271e3a5539cce507f795bd56655da504677a75a25ce31cb8379198ddad08116f3bc9222af47c2dd83c01f529202240165d2aa5dce8e70bd2b1b62cbf766421bc327a81fc30dda46544f3d663bc016749a3231cd33c97770fe74ff8bf5fd5a5f4d91d4170db65183752a5c6bdeb90c4322a65eaa420fe92e97d7dd60543045976d870494676209827552c5f93b4eec8ae0bdb2425b387539" },
        { 144, "950d834f5faaf9d167b2f47f8775f895ccd5da16ce7b27674956a99167afd328ce51eab6e4b766ad930fe38f52e9a97cabe489ef93d397a40abdda523c630aa00faea34c741410f9249fafb121f48ba9a5bce50bbdaf6ec49577c8774c6f1ef7143e1f71b026cead658fd957f5eb29e42889ad37834a150ce66a93181bf59d9f58cbcdb5ae048dade966562b500486f88c40ab8d36d3947d17e93f107668d6485a1d8194adc1a200755e4cd0d3d30dc8f9c41119765bf817661147caf32126ab49a214dd68f47cb4218f1f4ec4613997dd6871a16c5fabd0e881d9936f9d7c569181426beacfbe3c66709a8b23d4e496944ac4358524f68d11c109629bb5441a84dc3cfa57053c36f78c466939146ffea0d2988db5a574136e8b2a1e05c852f23f8aaef9d57f19cb76090938556325b29f19b9d7c9d7755a37e2cc454d51025e5d8c6a38652635bf6d352cb9e28eb6e0321166a626dd40bcde7a7124567deab44642415f03909f553f2e15311598f2b3172ca810dfb7fa5ae088cb08698447102f93249d780a5b9dd2063c0ab92fc696f96a3be05e0031c2282fb7e37d70b5a0a477102ed65d6602c75881c2fdcd27b253f6179862962ad7ff22e1e3c1ffb3c02b39430837ff01080041dbf2f0246c48af3a912d979b389ca1332686a81e225f43f07b686b7728b12753a5405c186bb43a7f191d" },
        { 145, "8bfefd16fa9a080b2e8690526c512b5d24ea5c07731aa5a5fd3db1a0a4ee20ca0ce147936b358fc418f4e790176837b8ace76f3842043c2f9af0428dfd1a76de0136c9ea498a7a8e293f77e9c4d8874aa5ee5324474992301185380da42f9f20b44476caf07e9d3cae25af5338ce5041dd9a27c58a7f19e44e7b6877012d9d8088e9560782d7cabfe3a894054fa6721906a010076172d2de12b6b28bc62787626cdeb3f866780f1d3f87112bd448a2325872ea4b73baeb89a9299a782f9afc6875ab2c01da9d26f6f907f089f4760be20a70fb4c661bfac874c99184fb6ec804a418eba9ce558e175b01a135287e0102766a14b25bd3a36e5bebe1483505046c0659f9d7cb7be5244e39f597310f37aee6dbd19e2a8f414bd55c503595e029b986d16b0756b3cc7a0f78400ffddf3cc3ded2055e59a49c434762798e75d72b528236dcea3795545273d967d3c4d369008a0b0e24b215db10db0e1075fea8d6364892df7be776f93277648fe28dae1b38b06d61fd68ce5bbfe3d902918d98b0730483f97f13d9db5ee723d549ada83cac2786f7cc57c64c5cd02f34f2a640498c6e45c121b2f6a2ba084479396dc571366bf4f35e6d4f3f43f49f6120188979fa19c4338818317337b03186a789283570830d3ce771266c340d4b36e7ba444b6f3873c1ca833abc237c65e220698a050bd3318594" },
        { 167, "1efd16dc15d14b107dbac8911e62ca44fc58514d09f3964a32d5fa06bcc3a46d88e0903cad9423bbc00640404a5de60022c2fe96486352ced1f4e12f17ea09098b2c2ae31387fa4f9dd019b47f9c54e555718292435e94a023554fe89dc1dd7bfff5417dddbd378bee9289542eebfa4e674d0568adc72a35311448c0e2192a83e95b1ce066c0dab0bbf54b9e984107a52a21c68664dcda5cd086b4f7330257faf46cbd3efdc45d476ff50bad48e7d9df8f521f2e4a36e34a6c7b9020214ee61770c4b711dffac7cb17e3a5f46980143bd0813e908aceb0ba13eee8779a4a479d12cb68285ca33954bf33c50f2541a403bbe3adae7853b3da4edd2cad6869e9f3c8f3c683531c50b281f9d4cd6519763b793ebda4d0e8628940533f17e8dfb55622280e6c4223cc27e750f2b8eb072ec3c0da0ee86371d7b547311135a2ac6f4c4528e8c0630ecfcc0dcefe0d3b71207e483f0bde66f18d462429ccce2914a06f421898de61911b6870ce6b5ad3570080dac66df958e5e453f68208973785d4549daa4275550b375f5901f9fda38e64334b87ba5704b05b570dc9175a80b43aaadbb135794bf5f1d7855f5b82f0740b923b620379c35e3fea73e086c49fc411c8421d77edc63bc5db93610743ed9d5fded04943ae11346fa25b8f755841826bb6280bcc225528b2f120cb8e0f987e84a19b74739f" },
        { 168, "ed300818a030af92239f7140176a0e468fad27ea97b3f01578817e8e80f16fce12253d73f19161a3e432f9ff6a2ccd6d9a8c5f2dc66006fb02f32aca812d5ad2082fafdb66b32a81c5da47ca29918199993e80bbde517d1f3021f00463fce0907e84183d4c8d3cb0d1cb57243ce76964185890b19d663663920a4b842204745865f68662916ef695257b218f94d3f0b0be9bfe7f665abeb956cb07a801421ff880cbec4f5d04cde0be368b480a6a323cf40b4c3023d15ec68b3c3a3ac9dc89300f43a980c45c528d154b3bdae260a43ed399a5a757c7aa6d6214d09675332168df184ec04240ce1ad5f4f0f799e5fa15aef744a4411b3253afbd737b96dde9fecb1c82419e32f7fecf852af07ac4dccf5b5c8a1ac3c210e8a2cbfc5620541f470367ab995bdb953f5d788883f6cfe7975288e91a35a8011edcb06ad2642fd378be01e39370f214c1fae7dcf6dfb6215d5a8c8dfb7a62be4d34281306d25fe6574303c5bb66e397824a963caccae00a088be664552467600fd227af8110a79bb94df4500385c206dcbac009bf7d222d8076aaee0fadecef6ea861216e9f8af2b2d81307d9a55321e9e1f0ed90cc0c144efbf5520cb17b2b983c8872e53308d4d0bcf112406f61412fabe8d72d197736650c872822b6166c31ac4cdbed06ff275c8acdd0671cbb8d92fd2ec4902be45e9221482cd9" },
        { 169, "3d052d31961f49122056846ddd7b43b37b680610f8343eb30372a0f3c67f648f3068de6ea7ff082e9ecc448af44133872958a2b5fc1d30f87edd539fe7d37738f481ed9063b198340b4f226c5bd71178fe9f25797f48285b52dac0b80fce4444ae6c19e6e197c3adcf607821fa6b6d3f111633131cd63c0da058a8ab79bd9a1ae447531dfd453b45fb70d7d5461ca96e6f4acaa9a23f213b65bd219773fd146af580183d924ce3be522323e3c6234a3505721cdebb3f7fb4d0c53fdd0ea60cca440963c229c784e372974f5c3f470bfa5a10e4885ef228babbae6f2316422f355740b9395ea96a0e3e4e74bdddeff36d840964b2e0fa4f26c93cd88f262216001c3f2b0f13337609359af5bd8fe923251cf66f9b05bc013d21a86ac7516fc893025c7838e77b0f67e1a71a52e91dbcc686ee4ee2ad86ed0ba601b508552a08f04571f7b724d4eea2cdc95e2d2eeb4caacc41198ce33b5c98d420b554acd17831b31578834dfebe032d29380d454912f25c712da66f319c6921a95496c2c0dfbd7188f1a471b2c580e14f355a4dc4ca15e3d16643bba85e1569d64f4288bb9e952b03c3a1ddd1a5cd2fc6c2bda7524c806baa671c8f3433ced4a2ffff590967ed4cc8643a9f6549b18b0e5abee1a2292b056ca6e7055d54daaf5d542963601f02ac8ad3bde4b6877b1e3fd26eda31c8a003884e35" },
        { 1000, "b0e0c141ce415f44188aa69075387c0af2114a07797fd7ffd9a9afe7257b9ad3aa04808403907446cf81ae5ff616a12b4700f0ec1240254d5fd8322ed42d6a28ccf253ba573046ba49cd001120b0ed33aeabc548682f6012188948cff1ed71cee2717ee6fd973dc78cf7b92774f3c35a0404f330fa320bac55671569d28d34cb949bae31783d8d5cb1baa0200b3c8e4dfb73a9f3f1de9a4a893c309990ffe27ba8d33eb148c2556cb822e67c49ba45dc111ce2e924dc0cdd4cb072452c889cd03c967b34eae89c525269758682c1bc955956b8ae859ef79f73b6b19c0ad2be426cebee87d82cf444f96512d0d064e5cb7a4af583f5f7200e2c19c29dc6b8966121680015e2c41f98a5bd4afd3a1aec6e7d5a7f01fdc0f7b629b70426105f4fed3b4feaddc2db350099a9ef269b8d4becd538cc2d0547c061cf222c49dc50bef03436d2ee93a792f694068a6945756a2a68db45ff29fc06c52f011a44057949c0a4033788114dfc106ffdf94fa865296032eaa524ea54f075a54799f3ab79c85609f8e6fdf36abadf4e9b0c5c1593f6bcf8d4df55956ca5a1e1bb8f244481a5fe90140e290441740b61843c76a9f0f978f477c09b7925f0466acafed93c3c838f6c380d875d697e7089b112360c222de309401514c81682cc3fb274a9736193329448eb92ade29da6d584e3e01aa8a2baa7ae12a4" },
        { 4096, "19f44c0865dbef9e7950e396982bfb74482b905b32d9ee57d6465db08bfac66fe9d668fb3d5795b8721da491ff83910f3c8b8854162628c15c26509b83b9b7d4d16da381b1e24748985e3ede8bf2b69d0d57956598ec9494edef7ddb6d754eb3d11dd38a6490a15aac5bfe8b77442eba5dd05bd7a243a29f7c948385ce53101dfc04922203eeb6fac09d91dfd9fc196f61f3a38c1d5434dbccba424a8bb300eb151118b4d5cb3ba25622f6dd2195908feadfebc289102e11f8a339c38c222e75e2ada95a1ea656581069225c97e6249532932918ca1ade48e71e3b9860660cdcb78cdbdebdada9d30b938bdc31f398832160dc348261c3171625203f6c2fe05be8be86803fdf817f33a0944ca77b99e25dc6d221b64ec89902ad9d1bda83c5a0eb55aca4750372cbe62b8d4350fa42bec4386433e7c9e25fa8d72ae357ab81b5c670d888ff8b85c78709198f816a6d9ca2f4876431a3414019090b146b92f68ae8a44278793a97ae3f3d41db968d973dc878d94171f6d3e06b7d10bfd4b73bac408585776c12289b0d989a4c91d0a3386bc4fc4ccee139fa37b6f4e45fb74cfa88ced874583b8e9e6657093f9f3de90a623e5d03c6a78c6fda8e243ba4fd0e10fdb4996809f2b45a55568e7cde6b972f142e6d724c4e2c70605babb9c4a3f37236e080d39f19f9322d8e242706fa060fe1be1c52" },
    };

    public static TheoryData<int, string> Sha3Shake256ReferenceVectors => new()
    {
        { 0, "46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762fd75dc4ddd8c0f200cb05019d67b592f6fc821c49479ab48640292eacb3b7c4be141e96616fb13957692cc7edd0b45ae3dc07223c8e92937bef84bc0eab862853349ec75546f58fb7c2775c38462c5010d846c185c15111e595522a6bcd16cf86f3d122109e3b1fdd943b6aec468a2d621a7c06c6a957c62b54dafc3be87567d677231395f6147293b68ceab7a9e0c58d864e8efde4e1b9a46cbe854713672f5caaae314ed9083dab4b099f8e300f01b8650f1f4b1d8fcf3f3cb53fb8e9eb2ea203bdc970f50ae55428a91f7f53ac266b28419c3778a15fd248d339ede785fb7f5a1aaa96d313eacc890936c173cdcd0fab882c45755feb3aed96d477ff96390bf9a66d1368b208e21f7c10d04a3dbd4e360633e5db4b602601c14cea737db3dcf722632cc77851cbdde2aaf0a33a07b373445df490cc8fc1e4160ff118378f11f0477de055a81a9eda57a4a2cfb0c83929d310912f729ec6cfa36c6ac6a75837143045d791cc85eff5b21932f23861bcf23a52b5da67eaf7baae0f5fb1369db78f3ac45f8c4ac5671d85735cdddb09d2b1e34a1fc066ff4a162cb263d6541274ae2fcc865f618abe27c124cd8b074ccd516301b91875824d09958f341ef274bdab0bae316339894304e35877b0c28a9b1fd166c7" },
        { 1, "b80ae24d9eb153769f5255dd09ac372349aa2571ae69543fa6a7a837532d06f279efb194fe5373e2eb9835510116f9853795204149958bb679a1264632a12cf0f5280c3f3617ef7c7cb6d8d8660d4b7d7ce2df3ce5ee5a5a98be0303504c86ddf6c4093eb5ae3012df1a9ca4962e2a38c01ac0489c491f5057ca9236b9b0b4c25914cbec7d4dd7e6e170d9db56738a8ef6f36c444d745fa1e34e08af6025fd7d08ed7efda214361d73da16346fff06137f1f7f2255e428f8dbfafb599e0528ed4ab2b28f1b23515f6dc82bb2ee6815b81a4425910ab1174f71f35ef173078d581f94b878254a59f476ece3229da8a7be80ba2d14b2b5dde5072544a4ceb7dfa0d7e20e0a4d3d92c73417b6bd0c294600a5bc91d9871f17dbe9b1440a9c8dc492264746a9a1d7848cce85ce47e9e4f3e2aa1c19caaed4f541b9f0a495e86e47dd9a68d01d1595a67a2a3b0158ead95400b04b7c9f9c00cb897c59ebc75f9a09287d3a66a77f80b7037f2fca301eb28b7e13ea4b82da58a7616f0e3906809384e6d005075d9b195e4520fd8d4a39ff418556beb330786c4bc65e3fd9768821e67e4d20cdf91cd0b7712aeb4ff5a0a9aaf62991752d551edff10301b00279f1e7cca2ff8c4bd6ba792a5a0c684d921635e6d27bee5d603d3921865bbf57751c7053d0c98acf8aafb193d017ece87b597c31d8d5cb58" },
        { 71, "0320c35b0251bf55a7a999f76faacd43e1eeb610fbe5641536137c848cd056013a47589727ff5bbe4c2ffa18635dc27d5d104c67e73fd828f0de9276a8af3f6d3ce1a3afded796f2a358bb273931456280131fcd7ed41a6bd870d6662a600fe72745ddfc9fea9d1622b8654976e41a4ed9f564d33178f4c941a2f682281fd59486c2344aa09daf18eae2b0ea516c5408b66f1338a7712bb88fcf9c51157286905d28382d93a50ae5743996938e7ececabee64f698eea167510d48c7a9ce4c3e600cf8d9c7cf32c4a5eeb37cad812999faa145fc7f7c7705ae313a7c1d1cca09b87ca00656b8f90366ca43edf266788aba93f1aac9ac3bff889eefcbfda6d08541614db9949bb4d62292ad91e02cac48d107177e4c2bf5d0cb982d7fed2f22455a071e31d8699220743e7f966b3f93ea5c6e2843d9190348d4b638383a55c36209af7038a8c2c16f06763911075811015f0cbd0d6da9ece79934306cde751cece6dddd17999458118454fbc5c70148b364f5b2de6b17f3b14ac20c739209e49cbc43d8536b8ba8da640ddf87689d2d2ed0d687c099a86224b3901f1aa52ff0e3036c456089a105066371a5958706290b789090f47faf13146e37f988afcb1f89ab64858d2b8a683d91a8c4a4331e29862e796acca57e6a338c1d883c940d65c823a29d0e16f56e01b4f5b66d15f8b7e275151ea68" },
        { 72, "bb30ea8269d3941faac5727ab89a5468513869dad6304299856b5a88b6dcf07df39b728fe006560ba76bba98e0b6d40864d1dc2cbe033e1c25f7a678465778a123a287c20ba12a6fedb127553e7520cf12d16a3b2b5d4c13b1ea69b622514102a2fdbf36b71adb8488c7e7e50f550e69fbd88f658321de5c71d93a53ff8404206a1dd342ec3d23d68e52c5bc78cf9a408be9df28faa037a85130dada10a24d0b1fecabc7cbf5e2b78413cfecb84fe734d885f4fdfb72d9a20471f7d96be481a8579880fd279a1fdfdc613d1f43878919627fd2f8cfe48139171c143d396bc33e711afffadbb78648b291d0d768608f23ad9538ea07d1f69fe298b3e57bc037f135f21a667b8aed815aac45dbee2b78af59bb678976bdf1a05fe94744ee4d40f7066c1dd2a2f9a1041628c129837b6852746cb1532c78fcef94920f0f7911471da9ce92ed6e9380022b5e0b8bce28e00584efafcfc191ec047a57b63d282717c9637e99e4db22dbd5ea89aae2a138af8730d68e154778571085bbb32903e2944e39cc2a8b2621d88f1b800ee2ed3f7e6235303b893f12e0caeb764e9e24fc4cf68476ab7513a889eb827b9160761c60a79c85e797350053905faca4ece21e47bd833f3bc2ebf6e2fd2236362ac5e47bf35d40eb5d36386012b18c806cd7e3393376de222232541e970d882dea2bb13e10bbca3b53" },
        { 73, "0b4f4fc3a789c1c7b9fb4b5f87c5b0e3fdb4c0807dbcf019678c064608d138de810bc048c261f81d22a3874b083a12970a162215d31ccb4a148c45372301e411f3ce2e07272ae9d730472758d75dee58a5d27a25e3363d01808350f1f6dfbee1ba62a98078644d50d86e8e6a2d4a0ff28fb95a117ec2809870cdcbd9b517132ee017dd7850ce260e6d6199c369a947c64adedb4525a9956502dada4ac40be373bc50311985c831f95ca6d2f88ffeebdb46f22cfc410bf89d64ded289deb2155e2eb6d627b293c6c49c8f01a21aa69e58481aeb5b826f419eef669dd0e5348a607ca30d7ed2c6132723b65b713ea369d8acb531d971a352636ffe501f996c9af6345a38f5149cf3e900cf4b42b54c039599d38f4364b898e7a19bcc95c710a009772439c6f02bcc7382cf2c67bf2813f52f02e90637d6d1164ff9a362968960942c115100b57121c5bf68107ec3b5cdf5a54e086d80498071a4aaecccde7e7014c895fcb11220121d7031439c3a238e960f3994a9c253a50a7d2fe1dd67d9dcab369002e34f68c5bbf4fa6c46ef6010b7b487a9bd500373f107bd784e2216ac9384af2d99652d1aed68eba4736b50d486a9579b86f293030ccc467d8a26b40719e0b78b96dfb960f2b24fc6f0774d2deb916eeaf48dd3290048acf3c601c1a4007e804951bc2914bc2f2e23a9316cef85573cf621" },
        { 103, "304cab218984a5dae02231a40312674a71dfee07f30c48dcc14e29fe23895a66312be6cd08d139ae08405942f73f33c7c3baffbf19b3a9084f9c4851e9f3ad21786ad55a5c00662dfaab9b2a312dadae8673598e36ced670ebcdb92ccb0525e033953641f41cba43187ec60d75a3e0123ef5be35b28eb0271a49ba489a9dd505055dec387170f44292dbcde150d0f5e8affa8ce46ee19fcf6c7ceb3577284f453d175187055fac3be1546680f7a3f00a5c6650b643faad80f95b52d78ba0f7b03835613216870667379b88224c9a308eb13afca307cd7236073b7dfc3dc41c0dba17f45b3070e9df7a0f077db25b16a4a93ca9872850fbd4c7c3e3765a419a5bf5c239435efdd51831342e2a3a218a4774fa58626697f3a288d786ee4c279983b238078e40155ba7dc67b7c5fb86166ac8a49b5e79f92c0c9aab04adf9453c3b0b87b500ad6151810af7320020e5c40a6377d5996d53c7ffd7947cbcaece1bd2f3044a580ec62430ebc6ef05d98f21b5b2a5a5279a4897399f6cb88c32253d8bf877a595d2aafc20a9d5ba735b4099c70a859b19ec852dc4973a8203558e3235957e8f82ebac3499147251686f33e730ec8ee62953fc646cf12e17fb8091b80f99b593117d562a984c81ebf3edc99967cdbc21b5f4ef574d57d56c3ed28c22a7d8983584e96f37cac5b7a81ae958ba2c8f277df8" },
        { 104, "342c1af79b0c519daa7b76bcf34f636826b131bb3b82d674b2fc051e1d366cadd036c7c56d7cda6fb427cd3fc825fa194236c2c1cd6d6ab36327613224b51cbe2589753c66cf8ee718667c4afbb3dbbb8089b0dfb502cb3445052b62fd218b8d9ef8b31090b2cea6afe61edb39d7fa66bbc1c809740a78ca005432945ff88c9fd1696ce0b621fbe8c287978bac013a140ea67093307854264323d065aa41911aa85a897ae7eec276f58fe7fc683a1d58f669536f3db4848fff94e45a7845bcd02be14497eed2a859e3bc9962ad8337a93b3095746a3ff611cdb172cdacd9914e9427538b14ef32640f0a2187de60b0bb48d3fad41698f4276ebc7601d906a65afcc95fa41c5cbdd65e383cc9b38c94da5c2b308818fcf98a2eaca9638fbf2a2bdc0850f86b159c4df547f9ae625bcc1c181bb3a243ae291d3cb67dc144d6a8d2e511a1426f883c01b670889e79bbbec80c56e244e05f2e55f406ca7ef1535a5c6c2951d027d6595023d1715a5e51d6c25abedffe649538473c7a3231d80235ad235564aa9966d4979b8de876ff213428eb015a12e855ece27d3b6d9fe1c71e996a306c937431252e3c73a1ee3556b83696c46d081002e8dbdbce5bb702df1e101cd9a6fd19b574ad1a3127bf52333cdfe5ea12ea517e6812f6b3772b76c034bdda166cbc0ff05da43c36820b8c5166ea1586d832" },
        { 105, "beb77e87b517f40cb06af8da64fab1f64be7c87a9e17a1cb879146a6a1b4ffb162264deddb4048c5675184e3099f4d5003b32c5290993f9a687231f658571f4de3a657485c81abb37931e6f58a5ab127f5bb2625e70f220e75423e8b7cdfbd957f2e317c2812ffeec862c28b32de7baad01b55a155cf219f64ab4cdb148987e29a7614967110e1bd438d30b6477cc7fce8e47fa14b6d39ab528daafdde358a547ff5041348599661ab9be6176c79f33321f78c73c38a66d1029a95d389ec6dd21bc5fbc3320c6c1c6d9c6fbcb08f1500d86b9c630bf76414813ef2d832c49b7a6b7064b1d332342367521c4c0427f5bac739e8747e869d02a462deebe8d0dae05fb04fbd17b6af50b0035affd9d44fc6a83f2f79e289f1eef0946203cbc243bf4f3159a0ef6eb882e723b5a1b4eae5d39f49c71bb4a713ef9204279b582564feb3f4b0df5a72e11e26ed79d6a9c1ad6bafdeeb667d9c0c03204e7523697df186d1f14470aa107760eefc8c89e93ef13f17ebd4485677806322a9fcda19bd7e0c7b9fa3174115342bcf3f26c4e9defe83733ef628211b8b054c02959a55347d3c4df27f4835ffb9aa83376cbcb4cd33250a859f98112deb26da61d36d316f9cb045a3ae26b1cbb789e718b1d2c0b92a95e4b199947ffd959eff7a248d14dc8bb09bfda32db626b50310a51f7b8fffce9b6c763355" },
        { 135, "9c585fe7ff9ec0f1b00a968b3f5a09b8aef98b0b1412d83817457c22a898ec7382d7437d2d78943d8ec1f423a98c9b45dff62a9441d7e781d15046e717bae78abe15a04305e22be20b850fc1643f330beee0cb927b90a345ebf1710f6398934e677b930c25e19c7fb750e441f90829de695dbebb6829c84814dbcc09d3d2b2c7e2b125536ea9421d285fb787589520e0368841cdfd47280d4b062e5af955895d4ecc57557b78fe95df3b48ed8093565ae6e9825a13922152abe9494493c9899fdbbb9495962faa8352bda830ff1700897ba1609c980ebf61ca1d6f0feea799bc20d80f9ef0d51ced494abb9cac5b4aebf4b8d4aa138069e2e4a4b20edba9019c2ddf23573c07eaeb25d3a8c304f585ac668626392547a5d058a2f370f188a46a1f415520b753f37a3022a7a3215f0a96575c5882153739a4aeaf49570b5d3b1c0257be388bbd6a3164796dd87860d7729e5d56f74956af1fb4bc70ac91cd8c9ed0c1a35f6cce86f53a43d986bcbc232230ba8b4c9feede794506422dcde6beb40500b348cb8a84110e10de443a8667b7af71f23474393a41c9b1e8d4c518940a39ff4c3ccb320fae772e44b4e3d5d91b499cd65b1d49c4c6330e8863d0b56a4d5efccc6e7859f550d52762735f95a65710120111385ba5e52b09f27e241c348705d9d229e9f419c3b2b8b183544a7fa9c242c2fc" },
        { 136, "b4528271407be681bf55998f4f3918c97019bb6280bd42d3b9300822760586f5c4a0e6f1349d2e05a8f96bffee2e9cd217015ba8a116c23b0f287ecc4c73a6bdc62a9a50e7475fa42777049e35d54e52b877c6e4191574dac40f9a1f3356a0a5860c87ed43707281cf022ca384970d4547270e2dcd2e0362bb7a91b270153fe8949bade1b19bedbad0d36b5c6b85bb9c32744dd1917bc081bf4af1c195a83d89ac77c210b5198aaafa82ab5712c10d4939e0e7af3b50d43f2d62c4c8ce6565597ed5a64b29021d01ba9144efdd9b7d25a70f146b430bdda17761e414e1dda5bf0550c344312f90d35dcbfbf74677e9f6fc3490ad13bcdc2b527563d8e5f5ea854f505d3648a698e7aaa23b2989099911197bd894ef33aed1dd4e4f8c4b215a4de848d34c2b1b51f5d48d273937a6b0bba09858bad806004860a5e805f521455877fdd82d59245f4818cc9bca2d303e760f052d33b4d54b6b87d396f88fdc5aff24ebb7a16c647db67853974d0ecae50a9e0595a752ffa78aae2b82eb7a8e8f9c421e007222e821863c922c68030ae669414b1145183e3cf63acc6c439612f8cf3bf451edbc7a2746e113049d4fd2b85a369c8a8e2fffcc15e2789dbf4d7210899e6b2beb7f190e6b96d470c54110fc63918f61c272ef51037c045fcb02213ab3c8e55a724d22ca6200bdac61585e7a389c4646f6" },
        { 137, "6a774f0b3998136c58af9ed6ab80b9ed1ece811e5018a0039fb04c6a32a6302de028d03986a5f04be2345bcf4c29cf88bd2e91b1c9579f0772b28db917c4ba9a1755b23c2d14375cfeb9b11d59ebd1b7c50f179f9c830468487e4cc1f544c8a15ca7a5fba264a907e66cbbe3cfcb474975dde917a974861cba9cd3b5a316134f8c8491f6db702d40f18297b6bf511f1cdcabcc0d5189cdef83dfacd53f30b3fde7c3e81d85c1dea44f8714154abe80d4171a00e4cfe59e3c7380869bdd3954b4a0a2d86c6cb4608d8bfc86602cbb0da472bc1d1ba898c33b052aee1bb66b97b2337f89a6b184593b83803b370086868252768f0a48eb93a26a45f7e579a1ac6ee4da8310fe802fa37e28fe273bb93766d8656baaed5e44adc794bf709157a8847d87c21f5bb86e82c113f3c89a1d14fe02c2abe7b36f2bc229f02704cb955d3c6c88e3f612f461596a0a0ff4331036464e645c9024023fca1e96c47a5125a79a4c1fb00c8a58a059bf3f9e82320d598e88602fbab12681ab4116b2a8d53a752f407bced1ad3ba677adf5eedba8e16b5bb3c11bdd7a8771af790c19a7eb6b40cc312d06f420fcbc5bdefd191bf5b3b1248d007812dd23d9714f6cf60e1bffa4d75c66a08e033ca047acef94d71252fd880cbbe812601d203cd51037e9840399bb99e3c2b7ffacf90948ec8a3b1b8a4e41bbac0a59" },
        { 143, "907d61fee0118eaf9cb50cde1c7bc270e9f2e2fa02d791a977c9b1030d8778902da0f11cae88335752350bdba0a3cc8b0a53022655ab6e331f1b3aeb292d881e07931dd3d2ca16cffd3720e138a4ad9b7ae7258ac62a3fb50c96c4fe6b1fdc5cfbd94d060751ba8dc312abe558e60c898f5068ef4acff864111e76c8644f962914171867bde80d8ca475d1ffabcdd9e47d192e0d665f0a75eb985a152333b1890e060199dc4f878cd8783d2e7545097ef76a5e69790dfa7b49b3eca5f0de5034f7357ef575271c99c8af6e3e8d6ca3cdbd72c0bc570f045bdb4422dbaee17020f4aea9689b46b0adf045eeb6ab63bc39e56e33dc0ed6e20643959333280b60aabae46ef6879427d4a121817d3555879faa512fe3de487b6b5ecbfa2a123b012e44031fe787fdcb729c395fbd9ed77e6a7b7118bb9c5fa6a223d3cee50f12aebe492cf141e3aa0beaeb6fe200542e2c954bebdbd82c296d4fbcd8e83770ba350adf1d6d3f6e34089c065dfa6e3ee8d0e126990ba414b46ff2dd7cfd0c0951a812882156c52d9068f8cf03f80b9a727188aca7dd5243a4d3f1213272ab138f58c91cf38b5b9aeace0fefefc15d38e7e51df5d0f99cdc0b6ab3d034179f2321d4d8f8e59514edd64e93a1764c92c9974229e13ea0af173a37291fc6d64a0836fcf3c6a02f056da0bdbd4965741e6f8fb986d9ec6014" },
        { 144, "46712f5c2f7213768a3697a1b6a06e9e102de36048a9f44822eec01597ae9e4f208a0d53c23d0fd3cedb16b2df8ea494f9f1046fb793f93136169d9115d90b55f94178b3b61ee589151054e8ad9e18d1d6831dd4aca2baeb3d9cbd7a04d6fc9854ebadd133d203283626572d46669ec81bf06533a7c047d0ff07a859b66c3f20918c1e4e3847b4aac2c16a09f494a849344f366d5b14a5364e3c71567778caf8a2a9221aeadc5fdfda677015a902a07e0c2fc87e953a2dbc8e1a199bdc3175446c1ec128141f26b9de5f2c26a90aba209516a7c19d6d0f08f844b4f97dfde1adea89d244e48178737476cb2d7bd15e695fbd78d9e613e9b4ac07c07dafcb3a7ef2f0a16a41712994b23efc98f85dcd8607580cdb123ecd6bacea6b43bb47b7dbb800afc4b6fb96fa861f7c1c77d18a107653bb8374d258aff6949a63ced9ee7ef1a3a6bb1fdb4f6431c1c52fc6e2cea947e7c4168ad927a7b6ea233e3f5d61bfbb7376147fbfc2ecab087e8af8d55be8f9837698de01444d9c359425bd48529439e8cd5546bebca8cbda4b342eee05498f2d8ae5b83e3a844a800b826b1d96475d55da270ecd1764789ea39a2ec7328555fd794866a833aa47ba7cff19bf8834e3afd00e1902cf8c75950f3cf5ce46608f3237543b67cc4d065b224278eb4154d34728dc260cfafba6cf2d7be186a46005051986" },
        { 145, "5514356b071c1cfe1a3548f5d6f84d63befb687a51b25b24ead43eef15b9296ba13cc2d7aee8f1d406e9684a05386b512e09249ad83eb325ce9101f00507692b207903130a1c301ced500a6d39e868939bc2dee27bdcb400c147546849239d51bbcbfb1b18a049ce5ff3f109db6e2416456c95dfe03c968d2326238963cae9bc6a2c8c27e86160d7a84365ffd659a7ff224b30d4e024e1efc39bbadad131db0a71a3f85090faf0a834f0ab81a5c59cd6147288b2b5512cc4b8cf6ebbe67786ac9d08a0598db3ab13a32cb9098c7bf961c521db197148fb570eba86bca0a881ad1a495bc4e16d1be93b62b54fff5ea94df4cb9a372711fade62103533989b4ce926cd624d29560ecd46283120092430d75f32599ed28fb1c8ad25cf6d55e86256b72df17463c3df5b0181ca3ad93ae9d10d6da69354a0b141f526438bd9fa991b7a6af49f700e0dbdc35ecb5416d24f9eb2422b8524b4242b3a9b509cd3b81784c615e6ab9bcc98fbb85c6ee9b97e1c70dd956a51cd423fc652ae1d616f4716f57c2a090d647dca89b8f0b2651a52c2340ca134e2194d4821788c6685a2cab4587368cc7f70518300a14948d7ee8debca67e4d5ef64a5fb36b09f7fed0415db0a176c364712cd8b69c9ae2279e7045f244d7d7dd0266041e6840e7997457283613e8045dc7c09b87540e5657bb7a8e50fc6f5a959" },
        { 167, "a749bff3fc294a41eb579624e75526cac6edc3aa13ce7505074b30a979737676a173e31ab2c1e43fc3404e3cb003701e1ace7ec44147cda42259ea1cb767068c946b7a4624e0a4b8338927444a507d452a6402c62fdac8a52b1c1435a22b713a3f8fc71b36a4136bfbd393cf6f5e49452eb847bd0c2814627b8d7bb1e0531eeff943a6492a0450812dbbde55d5c1fc18df888bf8d9ea8c924a0ef78aa4a032c8a2285f665c1d8756a1230c8625ca5c6fe73fa87dbde19d6076de9026e00b9eaf97655c642ee84d7eb4f8620eddef3cb3f014ccb82d9c0ae014998271be47828bd74233c31260a1152ee71afadc8354370159a27fd9e904be9c166917e952e43beba4b8abdf68e6bf43be124567e21ab8d7966bb73285bde7b08c3418ef0804bdf008f3d32b2ef43f4cc81a3abbc2d45fb3cb397e926d5fd6e6dcd290f5df3d1b5e6f3e3cb171da63f93455725c5934c296ce7d0bb8b35396defd612009106f277c7fa9380fa7c5c6332dc13dcd076e8ef73f0c1e8f9b6777fc96054a39129a6b215bd8c6aa0957b4572e40e61da72e43ffe239fd3b86ceb62c25cca563472ec9004cdb4a5b90247fa77260ceccf083c8bff80b426fd5c0672ace5921f0e2e748677207963a221f87bd25a391ef61074390f1fa389043b7f5dc3f7d962f44d219f91c9bfaaa7cfa35ceb365a1f0a5ea92ad23ef78" },
        { 168, "5fd8a83a33b38b81535272932cf775219a081d4c9ed18a7f2779daaffe1f15d0ef25ffe1d20cbbdfcb382a080a06144785230befff8886e4fff1477de505d66b20fd978d3b1a5f1de0c53c759ec32cc71c6ca97b14692ac3234fa1fa5a3991ef5621ee1d711ce8d0cb458cbd94ccf0f3ad843a925ee08b32631decad017c96a0ca8adf30e89c1c17eaebfda007be0b14b68e8d287f2f43d4bb2369a9c596a0827e8e767022c559f3db516b43db3dca9272e514863f0824782162610da0746a745b11bfb227f9e0dd4661c9ef004aad296585b2dcb091c0f1ff0a625fd58fe1d45a274546ba595e69efebda358e0c2c1ae6c3cee99d8fffa13f1f928f952b259d137224ff52dac3490e308318e23683581e057a3438e0d3281c86f2b499c666c2d9d673ad6a1fe85fc06b0dd8994cdcadbf31037f73f43ac65efd864443d0c037ac4f3942455ecf65297b605d42b7eb80ed23f0c4b11a9ab35ee814539038bfa0d14738539fa16bf4363f9ca65f1774c5e6107a5cb1075bfc6b7369e97446fa8bbeda98cd406e31a76394af14cc4f1e9e1bffcee5b7f858d3f075ee11623bbc4d5d1f40e307d120f82a9e35990f27731c2a79d0b40541c58c07c98e1b2da4483fb8435dae6a45b6c3fb57a16f0bbde43eeb06a2d432bfeba0e230ba7ca22980c9d72f5ae4d75743560da98e67d4dff87c1813b035" },
        { 169, "b4478936cd86da284cf65a893d79fa447c14bfed298ca1a9c8632116ec0e58c54a089a4d6617e9759733c72eb81b81d3d3a3737f61544c96cce4ea18a922bcf2a20a0795303a3ba9bf5f13378a2a52864b9b86ef2dfaa0423300c8efc50054d77bee4aae3b7e9369dde9fbeb44adb3c377238fe2965737e5c986a908586a2498b8169a4dc772f4924ae8b91c808cbcb339ee2540a0e80e7daa4185b94d772759ef862a3203ab8f9bbc14e62d06185da88a0f5ded08206e832fb1e0b768b6ae2fe52d1d8d14fc71c0af00591f2b8f6e9fae58cb923059ea72950429efd92454688cc18488e796a2abfdeafbaf3e46399f81bb9f1e90ff19658f077255d5c82a0cd23a7dfd53a7dc3975b5899d2537a9107ff662c627740bfa8a95370c1af1caf679e7b2f8a15d4cb693595eb72d23d955888bfca9e319dd87725ac10a8f9a62e9cf4012283df4c8c188fc7457ea9d08ae20670919d83ab807032760c1a14823611b37852538006e32d19724821f2327f713d7755d36087ade539ed923019f4f01d18fa2cbd53f2450a828f5031b39321c8cc3b4e3b3abcdc083dbc61fdf217dbc85011661e4d02889c5deb7fccc87fc58cf7ba9d1f31cb2fb45e750bdf1ca4fa0119aa9c0c70ad725bc919dab578478aca6f85d121bade7c17269e897ec1d47f6ff13969b33770d727596e2fdcb19cdf4a6f7622f" },
        { 1000, "1f9024b7861ad474ce4d8253ae8b899dec5219a9c5818db284d61b06be35774b04cc09cd7a29b84caabd9637030e7bc8f8d86532c49fa2a1b0fb6781121d7d455cb4e233c51f167f9962a4eeed5c8a0ce5f6a93f01a844797caa4f2932fa630d953112accdbfaa07a4cc47a4302aa8c45953e391728d29c7bfd29f66718ae80cf69933af60850c83a955b40ddddd0e5b28eddca1f5d00d407c716d3918c5acf602201d9fe9a023356eed93ea2e25807d59b75ff71623ec4129445af8f8dad4c11bff5ff691a2b1244280373659030a81ac5973acb649ce6d4b97680af1626d90358aed7c1489530c9f39770d1796d11cde49a5aa237ab2d04ae5afbb088acafdac1c7062ca0ef2be0bb185de94b193ce2ed65194823745afbb708a454a74418f30b23e581944778c9c7ce55d671d1c813ee07369ef18c56748e6803915b6fa2924121086777bb82acd28306496df960374935b53cbe0d2c476917729e08084b9307d435d5cca12244db9fcb958660bf5faf96eaf68fa3a604c135b8e54a4ef2c7cc3986e109bd3a6adea51477a80e86de86357d1cdf716e07c570cfd6941d370949c63816fb7cc6f52529b27d134155358dc002c7c70a5fb7080ce11f759abd914672edd7d7fc7e4bfbf5b49752746c2d100cba2d140b76d81c39b63b30c524735fb3d343b455ab47f80f506e383c4130a005860" },
        { 4096, "4db97b3a78c7dc47a1d9be7647c58d0279d1bc445327eb3a4efc60d05951a426abf6f7fde927c1d1f75f932dcbac51bf031fb3504aa780e421c7d7b859d7ebf98a07a9836ba2418ae945593c549c1dac6a43b022cf479ceb71d3f6742df9258b101bfb1bf4647a0fc37e27809e5298f769d4c22344fe88d858064e64034bfad96fcec40ce19a01ec41ae69dc12916ac55467e3ec7afec8c470eae2ed87047cad17ba84390bfed3a3ff5caedb861dcef5177f82a8257f517781e26f032b8e2af46c8da6a3d5596854a3df58f0f1e401790ab2bfe4a8dcb97c592dd12e64200aa2832fd2cc0338c196c77673f5ab3efc90ec75bec65a2c9c18ff47be99f289e70d4b7a2e8df49ac8b9e28bca7955faeb09cbf40dac92d9c1e706ace02f7614b31f569a9f0c158c56e0317b1c595ac992af21c23c00f4e7645fc7e83d0ed66227fa15605233d7028cd948ad4f871205215eb50400916651d8a573f7c9740b46e75368a7f1479b069dfedd858ec714252c429a8e23b38b9c0f4f5e3f30aa8b07f33ef6a8b2edc83ecab23528b3bc67b738646052bdca45415f2ef524ae840f27e59fdd98158c36421e3e9dfdea5d7843e70ca0dc8ad066b9060911932017b05f03ef68da10a969d8a4e8064f341c31538601567beaa98441e872de4e24f0377729dd2381eae687f0fbbc3e3473e1900a0639045608fb" },
    };

    [Theory(DisplayName = "Sha3Hash224 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Hash224ReferenceVectors))]
    public void Sha3Hash224ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash224();
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Theory(DisplayName = "Sha3Hash256 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Hash256ReferenceVectors))]
    public void Sha3Hash256ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Theory(DisplayName = "Sha3Hash384 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Hash384ReferenceVectors))]
    public void Sha3Hash384ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash384();
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Theory(DisplayName = "Sha3Hash512 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Hash512ReferenceVectors))]
    public void Sha3Hash512ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash512();
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Theory(DisplayName = "Sha3Shake128 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Shake128ReferenceVectors))]
    public void Sha3Shake128ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake128(500);
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Theory(DisplayName = "Sha3Shake256 should match the hashlib reference vectors")]
    [MemberData(nameof(Sha3Shake256ReferenceVectors))]
    public void Sha3Shake256ShouldMatchTheHashlibReferenceVectors(int length, string expected)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Shake256(500);
        byte[] data = CreateReferenceInput(length);

        // When
        byte[] actual = algorithm.ComputeHash(data);

        // Then
        Assert.Equal(Convert.FromHexString(expected), actual);
    }

    [Fact(DisplayName = "Sha3 span-based TryComputeHash should match the hashlib reference vector")]
    public void Sha3SpanBasedTryComputeHashShouldMatchTheHashlibReferenceVector()
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] data = CreateReferenceInput(4096);
        Span<byte> destination = stackalloc byte[32];

        // When
        bool result = algorithm.TryComputeHash(data, destination, out int bytesWritten);

        // Then
        Assert.True(result);
        Assert.Equal(32, bytesWritten);
        Assert.Equal(Convert.FromHexString("36b10ea74c2d902eadf8d91eef3f3640e2e362ec5ecaf77eb47a758bc4f632a5"), destination.ToArray());
    }

    [Fact(DisplayName = "Sha3 span-based TryComputeHash should fail when the destination is too small")]
    public void Sha3SpanBasedTryComputeHashShouldFailWhenTheDestinationIsTooSmall()
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] data = CreateReferenceInput(100);
        Span<byte> destination = stackalloc byte[31];

        // When
        bool result = algorithm.TryComputeHash(data, destination, out int bytesWritten);

        // Then
        Assert.False(result);
        Assert.Equal(0, bytesWritten);
    }

    [Theory(DisplayName = "Sha3 should produce the same result when input arrives in uneven chunks")]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(135)]
    [InlineData(136)]
    [InlineData(137)]
    [InlineData(500)]
    public void Sha3ShouldProduceTheSameResultWhenInputArrivesInUnevenChunks(int chunkSize)
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] data = CreateReferenceInput(1000);

        // When
        for (int offset = 0; offset < data.Length - chunkSize; offset += chunkSize)
            algorithm.TransformBlock(data, offset, chunkSize, null, 0);

        int finalOffset = (data.Length - 1) / chunkSize * chunkSize;
        algorithm.TransformFinalBlock(data, finalOffset, data.Length - finalOffset);

        // Then
        Assert.Equal(Convert.FromHexString("e9612e6ecfc2fc3c9467302e2563d3155906eaf49e0bb663e460791ef2fec839"), algorithm.Hash);
    }

    [Fact(DisplayName = "Sha3 instances should produce correct results when reused for several hashes")]
    public void Sha3InstancesShouldProduceCorrectResultsWhenReusedForSeveralHashes()
    {
        // Given
        using HashAlgorithm hash = Sha3.CreateSha3Hash256();
        using HashAlgorithm shake = Sha3.CreateSha3Shake128(500);
        byte[] small = CreateReferenceInput(137);
        byte[] large = CreateReferenceInput(1000);

        // When
        byte[] hashOfLarge = hash.ComputeHash(large);
        byte[] hashOfSmall = hash.ComputeHash(small);
        byte[] hashOfLargeAgain = hash.ComputeHash(large);
        byte[] shakeOfLarge = shake.ComputeHash(large);
        byte[] shakeOfLargeAgain = shake.ComputeHash(large);

        // Then
        Assert.Equal(Convert.FromHexString("e9612e6ecfc2fc3c9467302e2563d3155906eaf49e0bb663e460791ef2fec839"), hashOfLarge);
        Assert.Equal(Convert.FromHexString("b88ad98270115d9a7d164a81476b4b1c999eac7a9b67cfe013c365496a599e11"), hashOfSmall);
        Assert.Equal(hashOfLarge, hashOfLargeAgain);
        Assert.Equal(Convert.FromHexString("b0e0c141ce415f44188aa69075387c0af2114a07797fd7ffd9a9afe7257b9ad3aa04808403907446cf81ae5ff616a12b4700f0ec1240254d5fd8322ed42d6a28ccf253ba573046ba49cd001120b0ed33aeabc548682f6012188948cff1ed71cee2717ee6fd973dc78cf7b92774f3c35a0404f330fa320bac55671569d28d34cb949bae31783d8d5cb1baa0200b3c8e4dfb73a9f3f1de9a4a893c309990ffe27ba8d33eb148c2556cb822e67c49ba45dc111ce2e924dc0cdd4cb072452c889cd03c967b34eae89c525269758682c1bc955956b8ae859ef79f73b6b19c0ad2be426cebee87d82cf444f96512d0d064e5cb7a4af583f5f7200e2c19c29dc6b8966121680015e2c41f98a5bd4afd3a1aec6e7d5a7f01fdc0f7b629b70426105f4fed3b4feaddc2db350099a9ef269b8d4becd538cc2d0547c061cf222c49dc50bef03436d2ee93a792f694068a6945756a2a68db45ff29fc06c52f011a44057949c0a4033788114dfc106ffdf94fa865296032eaa524ea54f075a54799f3ab79c85609f8e6fdf36abadf4e9b0c5c1593f6bcf8d4df55956ca5a1e1bb8f244481a5fe90140e290441740b61843c76a9f0f978f477c09b7925f0466acafed93c3c838f6c380d875d697e7089b112360c222de309401514c81682cc3fb274a9736193329448eb92ade29da6d584e3e01aa8a2baa7ae12a4"), shakeOfLarge);
        Assert.Equal(shakeOfLarge, shakeOfLargeAgain);
    }

    [Fact(DisplayName = "Sha3 results should not change when a later hash is computed")]
    public void Sha3ResultsShouldNotChangeWhenALaterHashIsComputed()
    {
        // Given
        using HashAlgorithm algorithm = Sha3.CreateSha3Hash256();
        byte[] first = algorithm.ComputeHash(CreateReferenceInput(1000));
        byte[] firstCopy = [..first];

        // When
        algorithm.ComputeHash(CreateReferenceInput(137));

        // Then
        Assert.Equal(firstCopy, first);
    }

    /// <summary>
    /// Creates the deterministic input used to generate the hashlib reference vectors: byte i is (i * 31 + 7) mod 256.
    /// </summary>
    /// <param name="length">The length of the input.</param>
    /// <returns>Returns the reference input of the specified length.</returns>
    private static byte[] CreateReferenceInput(int length)
    {
        byte[] result = new byte[length];

        for (int index = 0; index < length; index++)
            result[index] = (byte)((index * 31 + 7) & 0xFF);

        return result;
    }
}
