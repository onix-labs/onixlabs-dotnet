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

namespace OnixLabs.Security.UnitTests;

public sealed class SecurityTokenBuilderTests
{
    [Fact(DisplayName = "SecurityTokenBuilder should be constructable from pseudo-random number generator with length")]
    public void SecurityTokenBuilderShouldBeConstructableFromPseudoRandomNumberGeneratorWithLength()
    {
        // Given / When
        // ReSharper disable once JoinDeclarationAndInitializer
        SecurityTokenBuilder? builder;

        // When
        builder = SecurityTokenBuilder.CreatePseudoRandom(32);

        // Then
        Assert.NotNull(builder);
    }

    [Fact(DisplayName = "SecurityTokenBuilder should be constructable from pseudo-random number generator with length and random instance")]
    public void SecurityTokenBuilderShouldBeConstructableFromPseudoRandomNumberGeneratorWithLengthAndRandomInstance()
    {
        // Given / When
        // ReSharper disable once JoinDeclarationAndInitializer
        SecurityTokenBuilder? builder;

        // When
        builder = SecurityTokenBuilder.CreatePseudoRandom(32, Random.Shared);

        // Then
        Assert.NotNull(builder);
    }

    [Fact(DisplayName = "SecurityTokenBuilder should be constructable from secure-random number generator with length")]
    public void SecurityTokenBuilderShouldBeConstructableFromSecureRandomNumberGeneratorWithLength()
    {
        // Given / When
        // ReSharper disable once JoinDeclarationAndInitializer
        SecurityTokenBuilder? builder;

        // When
        builder = SecurityTokenBuilder.CreateSecureRandom(32);

        // Then
        Assert.NotNull(builder);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseCharacters should produce the expected result")]
    [InlineData(1, 0, "3")]
    [InlineData(1, 4, "!")]
    [InlineData(1, 7, "y")]
    [InlineData(1, 9, "z")]
    [InlineData(1, 123, "$")]
    [InlineData(1, 256, "z")]
    [InlineData(1, 721, "y")]
    [InlineData(1, 999, "2")]
    [InlineData(2, 0, "3!")]
    [InlineData(2, 4, "!$")]
    [InlineData(2, 7, "y@")]
    [InlineData(2, 9, "zz")]
    [InlineData(2, 123, "$@")]
    [InlineData(2, 256, "z$")]
    [InlineData(2, 721, "yy")]
    [InlineData(2, 999, "2$")]
    [InlineData(4, 0, "3!!1")]
    [InlineData(4, 4, "!$1y")]
    [InlineData(4, 7, "y@2A")]
    [InlineData(4, 9, "zzAz")]
    [InlineData(4, 123, "$@3!")]
    [InlineData(4, 256, "z$3C")]
    [InlineData(4, 721, "yy3z")]
    [InlineData(4, 999, "2$A!")]
    [InlineData(8, 0, "3!!1C1@z")]
    [InlineData(8, 4, "!$1yAA3z")]
    [InlineData(8, 7, "y@2Ay3A$")]
    [InlineData(8, 9, "zzAzxz$$")]
    [InlineData(8, 123, "$@3!3AAB")]
    [InlineData(8, 256, "z$3C!x2@")]
    [InlineData(8, 721, "yy3z32$B")]
    [InlineData(8, 999, "2$A!C112")]
    [InlineData(16, 0, "3!!1C1@z$xxz2z$A")]
    [InlineData(16, 4, "!$1yAA3zz3C2y1!1")]
    [InlineData(16, 7, "y@2Ay3A$@@z$zB@y")]
    [InlineData(16, 9, "zzAzxz$$B2$z!Bx2")]
    [InlineData(16, 123, "$@3!3AABC2@zCzA@")]
    [InlineData(16, 256, "z$3C!x2@23@x22C@")]
    [InlineData(16, 721, "yy3z32$BCBzyx12x")]
    [InlineData(16, 999, "2$A!C11212xyyCx$")]
    [InlineData(32, 0, "3!!1C1@z$xxz2z$A@$3x!@$A31$31ACz")]
    [InlineData(32, 4, "!$1yAA3zz3C2y1!11z!@y!3zBByB2x21")]
    [InlineData(32, 7, "y@2Ay3A$@@z$zB@y!y@A31CzyA$Az3!x")]
    [InlineData(32, 9, "zzAzxz$$B2$z!Bx221$!AA1C1x3xAx1!")]
    [InlineData(32, 123, "$@3!3AABC2@zCzA@1xA$BA!A!23@1B2@")]
    [InlineData(32, 256, "z$3C!x2@23@x22C@z32z!2xz!CAxxx3@")]
    [InlineData(32, 721, "yy3z32$BCBzyx12x$ACz23@@yx!z33yy")]
    [InlineData(32, 999, "2$A!C11212xyyCx$!y3B1$!A2A@3z2$2")]
    [InlineData(64, 0, "3!!1C1@z$xxz2z$A@$3x!@$A31$31ACzx$2!Ayy$13Bx@!yzBCy32zC@!3@32C@@")]
    [InlineData(64, 4, "!$1yAA3zz3C2y1!11z!@y!3zBByB2x21@BA$yBAC1!y3Ax3113@11x$$xBx@31!C")]
    [InlineData(64, 7, "y@2Ay3A$@@z$zB@y!y@A31CzyA$Az3!xBBBA!B1$@2BA2$31!BC@3@2!3C2y3AzB")]
    [InlineData(64, 9, "zzAzxz$$B2$z!Bx221$!AA1C1x3xAx1!@C!Bz1@AyCxx23y2Ayz!Bx$y$@@$!3$!")]
    [InlineData(64, 123, "$@3!3AABC2@zCzA@1xA$BA!A!23@1B2@!A2B$Cy!$@zC!BCyBz3BA1!$B3AByB2A")]
    [InlineData(64, 256, "z$3C!x2@23@x22C@z32z!2xz!CAxxx3@CyA@B@CxA@2C@CzzBABB32z!3Cy2Cx2y")]
    [InlineData(64, 721, "yy3z32$BCBzyx12x$ACz23@@yx!z33yyCAyAx3!yAA2@A32!21C32y121!Bz!1zz")]
    [InlineData(64, 999, "2$A!C11212xyyCx$!y3B1$!A2A@3z2$2AAzA1CzCzx3x21CB3xz$2AC3BACC3@B$")]
    [InlineData(128, 0, "3!!1C1@z$xxz2z$A@$3x!@$A31$31ACzx$2!Ayy$13Bx@!yzBCy32zC@!3@32C@@B@C2!C$z23!B33B@Cz1x3z@!!!$x12xx!CCA232$C$A@3BBB2$zB31C2Ay@Ax3$$")]
    [InlineData(128, 4, "!$1yAA3zz3C2y1!11z!@y!3zBByB2x21@BA$yBAC1!y3Ax3113@11x$$xBx@31!CB33x1@Czz1x1z$y!y@2@zB$2BA2C!B1AAC2x2yA31zA$Az1y311!@@3$!ACzC$2x")]
    [InlineData(128, 7, "y@2Ay3A$@@z$zB@y!y@A31CzyA$Az3!xBBBA!B1$@2BA2$31!BC@3@2!3C2y3AzBBB!xA!12!$BBzB@zxyCyz@2C1xy2zCC@C$$zBz1A11A!!1y2xCAy!B2C123!z$C2")]
    [InlineData(128, 9, "zzAzxz$$B2$z!Bx221$!AA1C1x3xAx1!@C!Bz1@AyCxx23y2Ayz!Bx$y$@@$!3$!AB12yBBB3@y!!C1@@AC2@322CyC22$x3x1C1B!C@CxBx$C23yAA3zx@yz$y$$11!")]
    [InlineData(128, 123, "$@3!3AABC2@zCzA@1xA$BA!A!23@1B2@!A2B$Cy!$@zC!BCyBz3BA1!$B3AByB2AAA3B@$$y@CAx12C2zx3Azz@CC2$CBxz!xzB3yA1C1yx!11z1zy2Ay222C1@CA!C1")]
    [InlineData(128, 256, "z$3C!x2@23@x22C@z32z!2xz!CAxxx3@CyA@B@CxA@2C@CzzBABB32z!3Cy2Cx2y$ABC2x!21xy$AA2131BCB2C2$CC@z1A3CCx@BC@@C11yCz322@@3Byxy!zxyB$C$")]
    [InlineData(128, 721, "yy3z32$BCBzyx12x$ACz23@@yx!z33yyCAyAx3!yAA2@A32!21C32y121!Bz!1zz!BBCC2ACC@z2B$@22A@C13!@13zz2y1z1CB$2y1$z$z!@$11$z@33x@Byx3zC21@")]
    [InlineData(128, 999, "2$A!C11212xyyCx$!y3B1$!A2A@3z2$2AAzA1CzCzx3x21CB3xz$2AC3BACC3@B$23yy3Cy2$13xC1yA@3$CBA$A!!$1ACz2B!2$C@y1@@ABx$z22BxA@@!23y1$x@23")]
    public void SecurityTokenBuilderUseCharactersShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseCharacters("ABCxyz123!@$");

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseLowerCase should produce the expected result")]
    [InlineData(1, 0, "s")]
    [InlineData(1, 4, "v")]
    [InlineData(1, 7, "j")]
    [InlineData(1, 9, "l")]
    [InlineData(1, 123, "z")]
    [InlineData(1, 256, "m")]
    [InlineData(1, 721, "k")]
    [InlineData(1, 999, "q")]
    [InlineData(2, 0, "sv")]
    [InlineData(2, 4, "vz")]
    [InlineData(2, 7, "jw")]
    [InlineData(2, 9, "ll")]
    [InlineData(2, 123, "zx")]
    [InlineData(2, 256, "my")]
    [InlineData(2, 721, "kj")]
    [InlineData(2, 999, "qy")]
    [InlineData(4, 0, "svto")]
    [InlineData(4, 4, "vzok")]
    [InlineData(4, 7, "jwrb")]
    [InlineData(4, 9, "llbm")]
    [InlineData(4, 123, "zxtv")]
    [InlineData(4, 256, "mysf")]
    [InlineData(4, 721, "kjtl")]
    [InlineData(4, 999, "qybu")]
    [InlineData(8, 0, "svtofoxl")]
    [InlineData(8, 4, "vzokabsl")]
    [InlineData(8, 7, "jwrbjrby")]
    [InlineData(8, 9, "llbmhlyy")]
    [InlineData(8, 123, "zxtvtbad")]
    [InlineData(8, 256, "mysfuhpw")]
    [InlineData(8, 721, "kjtlsqye")]
    [InlineData(8, 999, "qybugpnq")]
    [InlineData(16, 0, "svtofoxlzhhmqmza")]
    [InlineData(16, 4, "vzokabslmtgpjoun")]
    [InlineData(16, 7, "jwrbjrbyvwlylcwk")]
    [InlineData(16, 9, "llbmhlyycpxmvdhq")]
    [InlineData(16, 123, "zxtvtbadfqxmelcw")]
    [InlineData(16, 256, "mysfuhpwqrxiqrex")]
    [InlineData(16, 721, "kjtlsqyeedmjhori")]
    [InlineData(16, 999, "qybugpnqorhkkfgy")]
    [InlineData(32, 0, "svtofoxlzhhmqmzawzrivwzasnyrocel")]
    [InlineData(32, 4, "vzokabslmtgpjounomuwjvrlccjeqhrn")]
    [InlineData(32, 7, "jwrbjrbyvwlylcwkuiwatoemkayamsuh")]
    [InlineData(32, 9, "llbmhlyycpxmvdhqqoxuabnepirgainv")]
    [InlineData(32, 123, "zxtvtbadfqxmelcwogayeauatqrxndpw")]
    [InlineData(32, 256, "mysfuhpwqrxiqrexmsplvrgmueahggsx")]
    [InlineData(32, 721, "kjtlsqyeedmjhoriyafmqtvxjgvlstjj")]
    [InlineData(32, 999, "qybugpnqorhkkfgyujsdoyvapbvrmqzp")]
    [InlineData(64, 0, "svtofoxlzhhmqmzawzrivwzasnyrocelhzqtajiynsdhxuildfksqmfwvtwrqfxx")]
    [InlineData(64, 4, "vzokabslmtgpjounomuwjvrlccjeqhrnxcbyicafovksaisnntwnnhzzidixrnue")]
    [InlineData(64, 7, "jwrbjrbyvwlylcwkuiwatoemkayamsuhcecbvdoywrdbpysovdfwsxpvserisamd")]
    [InlineData(64, 9, "llbmhlyycpxmvdhqqoxuabnepirgainvxfudmnwbkfghqskpaklueizjywxytryt")]
    [InlineData(64, 123, "zxtvtbadfqxmelcwogayeauatqrxndpwtbpcxfkuyxmftcejemseaovydtbcjdqb")]
    [InlineData(64, 256, "mysfuhpwqrxiqrexmsplvrgmueahggsxgkawcveiavpgwglmebdcrqlvrgkpghqk")]
    [InlineData(64, 721, "kjtlsqyeedmjhoriyafmqtvxjgvlstjjfbkahtviaaqwbtqtpnerpjornvcmvnll")]
    [InlineData(64, 999, "qybugpnqorhkkfgyujsdoyvapbvrmqzpaamboflflgsiqofctgmzqafrcaeetvdy")]
    [InlineData(128, 0, "svtofoxlzhhmqmzawzrivwzasnyrocelhzqtajiynsdhxuildfksqmfwvtwrqfxxcveqvfzmqsvcssdxemoirmvvutyhnrihvfebqsqyfybxrcdcpymdrofqajwbhsyz")]
    [InlineData(128, 4, "vzokabslmtgpjounomuwjvrlccjeqhrnxcbyicafovksaisnntwnnhzzidixrnuectrhnvellohnkykukwqxldzpcbpfucobagqhqkarolbzamojsonvxxtytcflgzqi")]
    [InlineData(128, 7, "jwrbjrbyvwlylcwkuiwatoemkayamsuhcecbvdoywrdbpysovdfwsxpvserisamdcdvhavoquydclcwlikfimxpfogjqmffwezzldloaoocutnjphgaivdqeoqstlyep")]
    [InlineData(128, 9, "llbmhlyycpxmvdhqqoxuabnepirgainvxfudmnwbkfghqskpaklueizjywxytrytccppjdedrwkuufnwxbgqwtrpfkfpqyithnfodugxgichyfpsjabrlhxjlzkyxont")]
    [InlineData(128, 123, "zxtvtbadfqxmelcwogayeauatqrxndpwtbpcxfkuyxmftcejemseaovydtbcjdqbabtdwzyjwfahnpgqlhtbllvfgqzedhlvglesjbneoihunolomkqbjpppfnwgatfo")]
    [InlineData(128, 256, "mysfuhpwqrxiqrexmsplvrgmueahggsxgkawcveiavpgwglmebdcrqlvrgkpghqkzacfqivqnhkzbapotodfcpfpyfevlpbtffgwdfvvgnojgltrrwwrdkhjumgkdzfy")]
    [InlineData(128, 721, "kjtlsqyeedmjhoriyafmqtvxjgvlstjjfbkahtviaaqwbtqtpnerpjornvcmvnllucdfeqbffwmpczwqqbveortwntmlqjnmofcypkpymzmtxzooymvtrhwcjhslepow")]
    [InlineData(128, 999, "qybugpnqorhkkfgyujsdoyvapbvrmqzpaamboflflgsiqofctgmzqafrcaeetvdyrrjjtfjpynshgnkbwtyfeayavuynaglpdtryewjowwadiylppdiaxxuqtjoygwps")]
    public void SecurityTokenBuilderUseLowerCaseShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseLowerCase();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseUpperCase should produce the expected result")]
    [InlineData(1, 0, "S")]
    [InlineData(1, 4, "V")]
    [InlineData(1, 7, "J")]
    [InlineData(1, 9, "L")]
    [InlineData(1, 123, "Z")]
    [InlineData(1, 256, "M")]
    [InlineData(1, 721, "K")]
    [InlineData(1, 999, "Q")]
    [InlineData(2, 0, "SV")]
    [InlineData(2, 4, "VZ")]
    [InlineData(2, 7, "JW")]
    [InlineData(2, 9, "LL")]
    [InlineData(2, 123, "ZX")]
    [InlineData(2, 256, "MY")]
    [InlineData(2, 721, "KJ")]
    [InlineData(2, 999, "QY")]
    [InlineData(4, 0, "SVTO")]
    [InlineData(4, 4, "VZOK")]
    [InlineData(4, 7, "JWRB")]
    [InlineData(4, 9, "LLBM")]
    [InlineData(4, 123, "ZXTV")]
    [InlineData(4, 256, "MYSF")]
    [InlineData(4, 721, "KJTL")]
    [InlineData(4, 999, "QYBU")]
    [InlineData(8, 0, "SVTOFOXL")]
    [InlineData(8, 4, "VZOKABSL")]
    [InlineData(8, 7, "JWRBJRBY")]
    [InlineData(8, 9, "LLBMHLYY")]
    [InlineData(8, 123, "ZXTVTBAD")]
    [InlineData(8, 256, "MYSFUHPW")]
    [InlineData(8, 721, "KJTLSQYE")]
    [InlineData(8, 999, "QYBUGPNQ")]
    [InlineData(16, 0, "SVTOFOXLZHHMQMZA")]
    [InlineData(16, 4, "VZOKABSLMTGPJOUN")]
    [InlineData(16, 7, "JWRBJRBYVWLYLCWK")]
    [InlineData(16, 9, "LLBMHLYYCPXMVDHQ")]
    [InlineData(16, 123, "ZXTVTBADFQXMELCW")]
    [InlineData(16, 256, "MYSFUHPWQRXIQREX")]
    [InlineData(16, 721, "KJTLSQYEEDMJHORI")]
    [InlineData(16, 999, "QYBUGPNQORHKKFGY")]
    [InlineData(32, 0, "SVTOFOXLZHHMQMZAWZRIVWZASNYROCEL")]
    [InlineData(32, 4, "VZOKABSLMTGPJOUNOMUWJVRLCCJEQHRN")]
    [InlineData(32, 7, "JWRBJRBYVWLYLCWKUIWATOEMKAYAMSUH")]
    [InlineData(32, 9, "LLBMHLYYCPXMVDHQQOXUABNEPIRGAINV")]
    [InlineData(32, 123, "ZXTVTBADFQXMELCWOGAYEAUATQRXNDPW")]
    [InlineData(32, 256, "MYSFUHPWQRXIQREXMSPLVRGMUEAHGGSX")]
    [InlineData(32, 721, "KJTLSQYEEDMJHORIYAFMQTVXJGVLSTJJ")]
    [InlineData(32, 999, "QYBUGPNQORHKKFGYUJSDOYVAPBVRMQZP")]
    [InlineData(64, 0, "SVTOFOXLZHHMQMZAWZRIVWZASNYROCELHZQTAJIYNSDHXUILDFKSQMFWVTWRQFXX")]
    [InlineData(64, 4, "VZOKABSLMTGPJOUNOMUWJVRLCCJEQHRNXCBYICAFOVKSAISNNTWNNHZZIDIXRNUE")]
    [InlineData(64, 7, "JWRBJRBYVWLYLCWKUIWATOEMKAYAMSUHCECBVDOYWRDBPYSOVDFWSXPVSERISAMD")]
    [InlineData(64, 9, "LLBMHLYYCPXMVDHQQOXUABNEPIRGAINVXFUDMNWBKFGHQSKPAKLUEIZJYWXYTRYT")]
    [InlineData(64, 123, "ZXTVTBADFQXMELCWOGAYEAUATQRXNDPWTBPCXFKUYXMFTCEJEMSEAOVYDTBCJDQB")]
    [InlineData(64, 256, "MYSFUHPWQRXIQREXMSPLVRGMUEAHGGSXGKAWCVEIAVPGWGLMEBDCRQLVRGKPGHQK")]
    [InlineData(64, 721, "KJTLSQYEEDMJHORIYAFMQTVXJGVLSTJJFBKAHTVIAAQWBTQTPNERPJORNVCMVNLL")]
    [InlineData(64, 999, "QYBUGPNQORHKKFGYUJSDOYVAPBVRMQZPAAMBOFLFLGSIQOFCTGMZQAFRCAEETVDY")]
    [InlineData(128, 0, "SVTOFOXLZHHMQMZAWZRIVWZASNYROCELHZQTAJIYNSDHXUILDFKSQMFWVTWRQFXXCVEQVFZMQSVCSSDXEMOIRMVVUTYHNRIHVFEBQSQYFYBXRCDCPYMDROFQAJWBHSYZ")]
    [InlineData(128, 4, "VZOKABSLMTGPJOUNOMUWJVRLCCJEQHRNXCBYICAFOVKSAISNNTWNNHZZIDIXRNUECTRHNVELLOHNKYKUKWQXLDZPCBPFUCOBAGQHQKAROLBZAMOJSONVXXTYTCFLGZQI")]
    [InlineData(128, 7, "JWRBJRBYVWLYLCWKUIWATOEMKAYAMSUHCECBVDOYWRDBPYSOVDFWSXPVSERISAMDCDVHAVOQUYDCLCWLIKFIMXPFOGJQMFFWEZZLDLOAOOCUTNJPHGAIVDQEOQSTLYEP")]
    [InlineData(128, 9, "LLBMHLYYCPXMVDHQQOXUABNEPIRGAINVXFUDMNWBKFGHQSKPAKLUEIZJYWXYTRYTCCPPJDEDRWKUUFNWXBGQWTRPFKFPQYITHNFODUGXGICHYFPSJABRLHXJLZKYXONT")]
    [InlineData(128, 123, "ZXTVTBADFQXMELCWOGAYEAUATQRXNDPWTBPCXFKUYXMFTCEJEMSEAOVYDTBCJDQBABTDWZYJWFAHNPGQLHTBLLVFGQZEDHLVGLESJBNEOIHUNOLOMKQBJPPPFNWGATFO")]
    [InlineData(128, 256, "MYSFUHPWQRXIQREXMSPLVRGMUEAHGGSXGKAWCVEIAVPGWGLMEBDCRQLVRGKPGHQKZACFQIVQNHKZBAPOTODFCPFPYFEVLPBTFFGWDFVVGNOJGLTRRWWRDKHJUMGKDZFY")]
    [InlineData(128, 721, "KJTLSQYEEDMJHORIYAFMQTVXJGVLSTJJFBKAHTVIAAQWBTQTPNERPJORNVCMVNLLUCDFEQBFFWMPCZWQQBVEORTWNTMLQJNMOFCYPKPYMZMTXZOOYMVTRHWCJHSLEPOW")]
    [InlineData(128, 999, "QYBUGPNQORHKKFGYUJSDOYVAPBVRMQZPAAMBOFLFLGSIQOFCTGMZQAFRCAEETVDYRRJJTFJPYNSHGNKBWTYFEAYAVUYNAGLPDTRYEWJOWWADIYLPPDIAXXUQTJOYGWPS")]
    public void SecurityTokenBuilderUseUpperCaseShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseUpperCase();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseDigits should produce the expected result")]
    [InlineData(1, 0, "7")]
    [InlineData(1, 4, "8")]
    [InlineData(1, 7, "3")]
    [InlineData(1, 9, "4")]
    [InlineData(1, 123, "9")]
    [InlineData(1, 256, "4")]
    [InlineData(1, 721, "3")]
    [InlineData(1, 999, "6")]
    [InlineData(2, 0, "78")]
    [InlineData(2, 4, "89")]
    [InlineData(2, 7, "38")]
    [InlineData(2, 9, "44")]
    [InlineData(2, 123, "99")]
    [InlineData(2, 256, "49")]
    [InlineData(2, 721, "33")]
    [InlineData(2, 999, "69")]
    [InlineData(4, 0, "7875")]
    [InlineData(4, 4, "8954")]
    [InlineData(4, 7, "3860")]
    [InlineData(4, 9, "4404")]
    [InlineData(4, 123, "9978")]
    [InlineData(4, 256, "4971")]
    [InlineData(4, 721, "3374")]
    [InlineData(4, 999, "6907")]
    [InlineData(8, 0, "78752594")]
    [InlineData(8, 4, "89540064")]
    [InlineData(8, 7, "38603609")]
    [InlineData(8, 9, "44042499")]
    [InlineData(8, 123, "99787001")]
    [InlineData(8, 256, "49717268")]
    [InlineData(8, 721, "33747691")]
    [InlineData(8, 999, "69072556")]
    [InlineData(16, 0, "7875259492246490")]
    [InlineData(16, 4, "8954006447253575")]
    [InlineData(16, 7, "3860360988494184")]
    [InlineData(16, 9, "4404249905948126")]
    [InlineData(16, 123, "9978700116941408")]
    [InlineData(16, 256, "4971726866836619")]
    [InlineData(16, 721, "3374769111432563")]
    [InlineData(16, 999, "6907255656243229")]
    [InlineData(32, 0, "78752594922464908963889065965014")]
    [InlineData(32, 4, "89540064472535755478386400316365")]
    [InlineData(32, 7, "38603609884941848380751430904772")]
    [InlineData(32, 9, "44042499059481266597005153620358")]
    [InlineData(32, 123, "99787001169414085209107076685158")]
    [InlineData(32, 256, "49717268668366194764862471032268")]
    [InlineData(32, 721, "33747691114325639014678932846733")]
    [InlineData(32, 999, "69072556562432298371598060864695")]
    [InlineData(64, 0, "7875259492246490896388906596501429670339571297341247641887866288")]
    [InlineData(64, 4, "8954006447253575547838640031636590093102584703655785539931396571")]
    [InlineData(64, 7, "3860360988494184838075143090477211008159861059758128795861637041")]
    [InlineData(64, 9, "4404249905948126659700515362035892714580422267350447139398897697")]
    [InlineData(64, 123, "9978700116941408520910707668515870509247984171131471058917013160")]
    [InlineData(64, 256, "4971726866836619476486247103226824080813085282441010664862462363")]
    [InlineData(64, 721, "3374769111432563901467893284673320402783006807675516635658048544")]
    [InlineData(64, 999, "6907255656243229837159806086469500405141426365217249602610117819")]
    [InlineData(128, 0, "78752594922464908963889065965014296703395712973412476418878662880816829467817719145364887792563282106669290960116941652603802799")]
    [InlineData(128, 4, "89540064472535755478386400316365900931025847036557855399313965710762581445254947486941960062715002626406540904537558987970242963")]
    [InlineData(128, 7, "38603609884941848380751430904772110081598610597581287958616370410182085679104184331348515236422819941450550775353203816156774915")]
    [InlineData(128, 9, "44042499059481266597005153620358927145804222673504471393988976970056311168377258902687652426693725251728230392573006438349399557")]
    [InlineData(128, 123, "99787001169414085209107076685158705092479841711314710589170131600071899382025526437044822691134824163051533855454460356525820725")]
    [InlineData(128, 256, "49717268668366194764862471032268240808130852824410106648624623639002638652490055751215259218450721281288255324766886142374241919")]
    [InlineData(128, 721, "33747691114325639014678932846733204027830068076755166356580485447112160118450986608156785744635452096459494789559487628032741558")]
    [InlineData(128, 999, "69072556562432298371598060864695004051414263652172496026101178196633713695622540879210908795024517691835880139466130997673592857")]
    public void SecurityTokenBuilderUseDigitsShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseDigits();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseAlphaNumericCharacters should produce the expected result")]
    [InlineData(1, 0, "T")]
    [InlineData(1, 4, "Y")]
    [InlineData(1, 7, "x")]
    [InlineData(1, 9, "A")]
    [InlineData(1, 123, "9")]
    [InlineData(1, 256, "C")]
    [InlineData(1, 721, "y")]
    [InlineData(1, 999, "N")]
    [InlineData(2, 0, "TY")]
    [InlineData(2, 4, "Y9")]
    [InlineData(2, 7, "x2")]
    [InlineData(2, 9, "AC")]
    [InlineData(2, 123, "94")]
    [InlineData(2, 256, "C5")]
    [InlineData(2, 721, "yx")]
    [InlineData(2, 999, "N6")]
    [InlineData(4, 0, "TYVI")]
    [InlineData(4, 4, "Y9Iz")]
    [InlineData(4, 7, "x2Od")]
    [InlineData(4, 9, "ACdD")]
    [InlineData(4, 123, "94UY")]
    [InlineData(4, 256, "C5Rm")]
    [InlineData(4, 721, "yxTB")]
    [InlineData(4, 999, "N6dW")]
    [InlineData(8, 0, "TYVImI4B")]
    [InlineData(8, 4, "Y9IzadRB")]
    [InlineData(8, 7, "x2OdwPc7")]
    [InlineData(8, 9, "ACdDqA67")]
    [InlineData(8, 123, "94UYTcbj")]
    [InlineData(8, 256, "C5RmWrL2")]
    [InlineData(8, 721, "yxTBTN7j")]
    [InlineData(8, 999, "N6dWoKFM")]
    [InlineData(16, 0, "TYVImI4B8qsCND8b")]
    [InlineData(16, 4, "Y9IzadRBDUoKxIWG")]
    [InlineData(16, 7, "x2OdwPc701B5Bg2y")]
    [InlineData(16, 9, "ACdDqA67fK5EYjrO")]
    [InlineData(16, 123, "94UYTcbjmN4ElCe0")]
    [InlineData(16, 256, "C5RmWrL2OP3uMOk4")]
    [InlineData(16, 721, "yxTBTN7jkiDwrJPt")]
    [InlineData(16, 999, "N6dWoKFMJOryymp5")]
    [InlineData(32, 0, "TYVImI4B8qsCND8b19PtY09cRG5QHflC")]
    [InlineData(32, 4, "Y9IzadRBDUoKxIWGHDW1vZPAfgxkOsPF")]
    [InlineData(32, 7, "x2OdwPc701B5Bg2yXu1bUJkDxb7bDSVs")]
    [InlineData(32, 9, "ACdDqA67fK5EYjrOOJ5XadFkKtPqcuFZ")]
    [InlineData(32, 123, "94UYTcbjmN4ElCe0Jpa6jbWaUNQ3GiK0")]
    [InlineData(32, 256, "C5RmWrL2OP3uMOk4DSLBZOpCWlctppR2")]
    [InlineData(32, 721, "yxTBTN7jkiDwrJPt6amEMT04wqZARUww")]
    [InlineData(32, 999, "N6dWoKFMJOryymp5XxSiI6ZbLe0PDN9K")]
    [InlineData(64, 0, "TYVImI4B8qsCND8b19PtY09cRG5QHflCs9NVbxv7FShq4XuCjnzSMEm2ZT1QMn33")]
    [InlineData(64, 4, "Y9IzadRBDUoKxIWGHDW1vZPAfgxkOsPF4fc6vgboJZzTatRGHU2GGs99uit4QGXk")]
    [InlineData(64, 7, "x2OdwPc701B5Bg2yXu1bUJkDxb7bDSVsgkfeZjH52OheL7SJZjn2R4KYRlPuSbDh")]
    [InlineData(64, 9, "ACdDqA67fK5EYjrOOJ5XadFkKtPqcuFZ4nWjEF2eznqsOSyLbzCWjt9x6237UP5V")]
    [InlineData(64, 123, "94UYTcbjmN4ElCe0Jpa6jbWaUNQ3GiK0VeKf4mzX53ElUhlxkDRjaHZ7hTdgwhMd")]
    [InlineData(64, 256, "C5RmWrL2OP3uMOk4DSLBZOpCWlctppR2ozc1f0ltc0Kp2oACjdjgPNCYPpzLpsMy")]
    [InlineData(64, 721, "yxTBTN7jkiDwrJPt6amEMT04wqZARUwwodyaqTYvabN1cTNUKFkPLxIOGZfCYGCA")]
    [InlineData(64, 999, "N6dWoKFMJOryymp5XxSiI6ZbLe0PDN9KbcDdImAlAqQuMIngUqD8OanQhblkUZi5")]
    [InlineData(128, 0, "TYVImI4B8qsCND8b19PtY09cRG5QHflCs9NVbxv7FShq4XuCjnzSMEm2ZT1QMn33fZkNZn9DOSYgTSh4lDHuPDZYXV6sGPurYnleMQN5n5e3QfjgL7EjQJnMaw0drR79")]
    [InlineData(128, 4, "Y9IzadRBDUoKxIWGHDW1vZPAfgxkOsPF4fc6vgboJZzTatRGHU2GGs99uit4QGXkfTQsF0lBBIrGA6zWy0M3Ai8LfdMnWhHcboNrNzaQJBe8aDJxSHFZ43U6UfmCo8Ou")]
    [InlineData(128, 7, "x2OdwPc701B5Bg2yXu1bUJkDxb7bDSVsgkfeZjH52OheL7SJZjn2R4KYRlPuSbDhfjYrbYJOX5jfBg2AtxmuE3KmJqwOEmm1l98BiBHaJJeVVFxKsobuZiNlIMSUB5kK")]
    [InlineData(128, 9, "ACdDqA67fK5EYjrOOJ5XadFkKtPqcuFZ4nWjEF2eznqsOSyLbzCWjt9x6237UP5VfgJLwhkiQ0xXWnG14eoM2TPKnzmLM5tTsFmIiXo3puft6mKSwccQBs3vA8y75IFV")]
    [InlineData(128, 123, "94UYTcbjmN4ElCe0Jpa6jbWaUNQ3GiK0VeKf4mzX53ElUhlxkDRjaHZ7hTdgwhMdcdTh195w0maqGKpNCsTcBB0npN7kjsAYqBjRxeFlIvtXHIAHEzOdxKMKnG1obUnH")]
    [InlineData(128, 256, "C5RmWrL2OP3uMOk4DSLBZOpCWlctppR2ozc1f0ltc0Kp2oACjdjgPNCYPpzLpsMy8afnMtYNGsz9eaKIUJjmgLnL7nl0AJeTnlq1jn00oGJxoCUPO11PjzsxXEqyi8m7")]
    [InlineData(128, 721, "yxTBTN7jkiDwrJPt6amEMT04wqZARUwwodyaqTYvabN1cTNUKFkPLxIOGZfCYGCAWginkOcmm0DKg81MMe0lJQU0FTDBOvGEJmf6LzJ5D8DV38IH7C0UQr1fvrSAkLH0")]
    [InlineData(128, 999, "N6dWoKFMJOryymp5XxSiI6ZbLe0PDN9KbcDdImAlAqQuMIngUqD8OanQhblkUZi5OQxwTmxL6GRspFzd0T5nkb6aYW6GbpBKhVP5k1xH21bht5BLMhua44VNUxI6p0KR")]
    public void SecurityTokenBuilderUseAlphaNumericCharactersShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseAlphaNumericCharacters();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseBasicSpecialCharacters should produce the expected result")]
    [InlineData(1, 0, "^")]
    [InlineData(1, 4, "&")]
    [InlineData(1, 7, "$")]
    [InlineData(1, 9, "$")]
    [InlineData(1, 123, "*")]
    [InlineData(1, 256, "$")]
    [InlineData(1, 721, "$")]
    [InlineData(1, 999, "^")]
    [InlineData(2, 0, "^&")]
    [InlineData(2, 4, "&*")]
    [InlineData(2, 7, "$&")]
    [InlineData(2, 9, "$$")]
    [InlineData(2, 123, "**")]
    [InlineData(2, 256, "$*")]
    [InlineData(2, 721, "$#")]
    [InlineData(2, 999, "^*")]
    [InlineData(4, 0, "^&&%")]
    [InlineData(4, 4, "&*%$")]
    [InlineData(4, 7, "$&^!")]
    [InlineData(4, 9, "$$!$")]
    [InlineData(4, 123, "**^&")]
    [InlineData(4, 256, "$*^@")]
    [InlineData(4, 721, "$#^$")]
    [InlineData(4, 999, "^*!&")]
    [InlineData(8, 0, "^&&%@%*$")]
    [InlineData(8, 4, "&*%$!!^$")]
    [InlineData(8, 7, "$&^!#^!*")]
    [InlineData(8, 9, "$$!$#$**")]
    [InlineData(8, 123, "**^&^!!@")]
    [InlineData(8, 256, "$*^@&#%&")]
    [InlineData(8, 721, "$#^$^^*@")]
    [InlineData(8, 999, "^*!&@%%%")]
    [InlineData(16, 0, "^&&%@%*$*##$^$*!")]
    [InlineData(16, 4, "&*%$!!^$$^@%$%&%")]
    [InlineData(16, 7, "$&^!#^!*&&$*$!*$")]
    [InlineData(16, 9, "$$!$#$**!%*$&@#^")]
    [InlineData(16, 123, "**^&^!!@@^*$@$!&")]
    [InlineData(16, 256, "$*^@&#%&^^*#%^@*")]
    [InlineData(16, 721, "$#^$^^*@@@$##%^#")]
    [InlineData(16, 999, "^*!&@%%%%^#$$@#*")]
    [InlineData(32, 0, "^&&%@%*$*##$^$*!&*^#&&*!^%*^%!@$")]
    [InlineData(32, 4, "&*%$!!^$$^@%$%&%%$&&#&^$!!#@^#^%")]
    [InlineData(32, 7, "$&^!#^!*&&$*$!*$&#&!^%@$$!*!$^&#")]
    [InlineData(32, 9, "$$!$#$**!%*$&@#^^%*&!!%@%#^#!#%&")]
    [InlineData(32, 123, "**^&^!!@@^*$@$!&%#!*@!&!&^^*%@%&")]
    [InlineData(32, 256, "$*^@&#%&^^*#%^@*$^%$&^#$&@!###^*")]
    [InlineData(32, 721, "$#^$^^*@@@$##%^#*!@$%^&*##&$^^##")]
    [InlineData(32, 999, "^*!&@%%%%^#$$@#*&#^@%*&!%!&^$^*%")]
    [InlineData(64, 0, "^&&%@%*$*##$^$*!&*^#&&*!^%*^%!@$#*^&!$#*%^!#*&#$@@$^%$@*&^&^%@**")]
    [InlineData(64, 4, "&*%$!!^$$^@%$%&%%$&&#&^$!!#@^#^%*!!*#!!@%&$^!#^%%^&%%#**#@#*^%&@")]
    [InlineData(64, 7, "$&^!#^!*&&$*$!*$&#&!^%@$$!*!$^&#!@!!&@%**^!!%*^%&@@&^*%&^@^#^!$!")]
    [InlineData(64, 9, "$$!$#$**!%*$&@#^^%*&!!%@%#^#!#%&*@&@$%*!$@##^^$%!$$&@#*#****&^*&")]
    [InlineData(64, 123, "**^&^!!@@^*$@$!&%#!*@!&!&^^*%@%&&!%!*@$&**$@&!@$@$^@!%&*@^!!#!^!")]
    [InlineData(64, 256, "$*^@&#%&^^*#%^@*$^%$&^#$&@!###^*@$!&!&@#!&%@*@$$@!@!^^$&^@$%@#%$")]
    [InlineData(64, 721, "$#^$^^*@@@$##%^#*!@$%^&*##&$^^##@!$!#^&#!!^&!^^&%%@^%$%^%&!$&%$$")]
    [InlineData(64, 999, "^*!&@%%%%^#$$@#*&#^@%*&!%!&^$^*%!!$!%@$@$#^#%%@!^#$*^!@^!!@@^&@*")]
    [InlineData(128, 0, "^&&%@%*$*##$^$*!&*^#&&*!^%*^%!@$#*^&!$#*%^!#*&#$@@$^%$@*&^&^%@**!&@^&@*$^^&!^^@*@$%#^$&&&&*#%^##&@@!%^^*@*!*^!@!%*$@^%@%!#&!#^**")]
    [InlineData(128, 4, "&*%$!!^$$^@%$%&%%$&&#&^$!!#@^#^%*!!*#!!@%&$^!#^%%^&%%#**#@#*^%&@!^^#%&@$$%#%$*$&$&^*$@*%!!%@&!%!!@^#^$!^%$!*!$%$^%%&**^*&!@$@*^#")]
    [InlineData(128, 7, "$&^!#^!*&&$*$!*$&#&!^%@$$!*!$^&#!@!!&@%**^!!%*^%&@@&^*%&^@^#^!$!!@&#!&%^&*@!$!*$#$@#$*%@%##^$@@&@**$@$%!%%!&&%$%#@!#&@^@%%^&$*@%")]
    [InlineData(128, 9, "$$!$#$**!%*$&@#^^%*&!!%@%#^#!#%&*@&@$%*!$@##^^$%!$$&@#*#****&^*&!!%%#@@@^&$&&@%&*!@%&^^%@$@%%*#^#%@%@&@*@#!#*@%^#!!^$#*#$*$**%%&")]
    [InlineData(128, 123, "**^&^!!@@^*$@$!&%#!*@!&!&^^*%@%&&!%!*@$&**$@&!@$@$^@!%&*@^!!#!^!!!^!&**#&@!#%%@^$#^!$$&@@^*@@#$&#$@^$!%@%##&%%$%$$^!$%%%@%&@!&@%")]
    [InlineData(128, 256, "$*^@&#%&^^*#%^@*$^%$&^#$&@!###^*@$!&!&@#!&%@*@$$@!@!^^$&^@$%@#%$*!!@%#&^%#$*!!%%^%@@!%@%*@@&$%!^@@#&@@&&@%%$@$^^^&&^@$#$&$#$@*@*")]
    [InlineData(128, 721, "$#^$^^*@@@$##%^#*!@$%^&*##&$^^##@!$!#^&#!!^&!^^&%%@^%$%^%&!$&%$$&!@@@^!@@&$%!*&^%!&@%^&&%^$$^#%$%@!*%$%*$*$&**%%*$&^^#&!##^$@%%&")]
    [InlineData(128, 999, "^*!&@%%%%^#$$@#*&#^@%*&!%!&^$^*%!!$!%@$@$#^#%%@!^#$*^!@^!!@@^&@*^^$#^@$%*%^#@%$!&^*@@!*!&&*%!@$%!&^*@&$%*&!!#*$%%@#!**&^^#%*#&%^")]
    public void SecurityTokenBuilderUseBasicSpecialCharactersShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseBasicSpecialCharacters();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder.UseExtendedSpecialCharacters should produce the expected result")]
    [InlineData(1, 0, ">")]
    [InlineData(1, 4, "?")]
    [InlineData(1, 7, "-")]
    [InlineData(1, 9, "=")]
    [InlineData(1, 123, "\\")]
    [InlineData(1, 256, "{")]
    [InlineData(1, 721, "-")]
    [InlineData(1, 999, "\"")]
    [InlineData(2, 0, ">?")]
    [InlineData(2, 4, "?\\")]
    [InlineData(2, 7, "-/")]
    [InlineData(2, 9, "={")]
    [InlineData(2, 123, "\\`")]
    [InlineData(2, 256, "{`")]
    [InlineData(2, 721, "-+")]
    [InlineData(2, 999, "\"|")]
    [InlineData(4, 0, ">?,]")]
    [InlineData(4, 4, "?\\:=")]
    [InlineData(4, 7, "-/\'@")]
    [InlineData(4, 9, "={@}")]
    [InlineData(4, 123, "\\`>.")]
    [InlineData(4, 256, "{`<&")]
    [InlineData(4, 721, "-+>{")]
    [InlineData(4, 999, "\"|@.")]
    [InlineData(8, 0, ">?,]&]~{")]
    [InlineData(8, 4, "?\\:=!@<{")]
    [InlineData(8, 7, "-/\'@+\'@|")]
    [InlineData(8, 9, "={@}(=||")]
    [InlineData(8, 123, "\\`>.>@!%")]
    [InlineData(8, 256, "{`<&.(;/")]
    [InlineData(8, 721, "-+>{>\"|^")]
    [InlineData(8, 999, "\"|@.*:[;")]
    [InlineData(16, 0, ">?,]&]~{\\(){\"}\\!")]
    [InlineData(16, 4, "?\\:=!@<{}>*:-].[")]
    [InlineData(16, 7, "-/\'@+\'@|?/{`{$~-")]
    [InlineData(16, 9, "={@}(=||#:`}?%)\"")]
    [InlineData(16, 123, "\\`>.>@!%&\"`}&{#/")]
    [InlineData(16, 256, "{`<&.(;/\"\'~_;\"^`")]
    [InlineData(16, 721, "-+>{>\"|^^%}+):\')")]
    [InlineData(16, 999, "\"|@.*:[;:\")--&(`")]
    [InlineData(32, 0, ">?,]&]~{\\(){\"}\\!/\\\'_?/\\@<[`<]#^{")]
    [InlineData(32, 4, "?\\:=!@<{}>*:-].[]}./+?\'=#$+^\")\'[")]
    [InlineData(32, 7, "-/\'@+\'@|?/{`{$~-._/!>:^}-!|!}<,)")]
    [InlineData(32, 9, "={@}(=||#:`}?%)\"\":`.!@[^:_\'(@_[?")]
    [InlineData(32, 123, "\\`>.>@!%&\"`}&{#/:(!|^!,!,\"\'~[%;/")]
    [InlineData(32, 256, "{`<&.(;/\"\'~_;\"^`}<;{?\"({.^@)((<~")]
    [InlineData(32, 721, "-+>{>\"|^^%}+):\')|!&};>/`+(?=<>++")]
    [InlineData(32, 999, "\"|@.*:[;:\")--&(`.+>%]|?!;#?\'}\"\\;")]
    [InlineData(64, 0, ">?,]&]~{\\(){\"}\\!/\\\'_?/\\@<[`<]#^{)\\\",!-_|[<$(`._{%*=<;}&~?>/\';&~~")]
    [InlineData(64, 4, "?\\:=!@<{}>*:-].[]}./+?\'=#$+^\")\'[~#@`+$!*:?=>!_<[]>/[[)\\\\_%_`\'].^")]
    [InlineData(64, 7, "-/\'@+\'@|?/{`{$~-._/!>:^}-!|!}<,)$^##?%]`~\'$#;|<:?%*/<`;?<^\'_>!}$")]
    [InlineData(64, 9, "={@}(=||#:`}?%)\"\":`.!@[^:_\'(@_[?`&.%}[~#=&()\">-;!={,^_\\+|~~|,\'`,")]
    [InlineData(64, 123, "\\`>.>@!%&\"`}&{#/:(!|^!,!,\"\'~[%;/,#:#`&=.`~}&,$^-^}<%!]?|%>@$+$\"@")]
    [InlineData(64, 256, "{`<&.(;/\"\'~_;\"^`}<;{?\"({.^@)((<~*=@/#/^_@?:*~*={^@%$\'\"{?\'*=;*);-")]
    [InlineData(64, 721, "-+>{>\"|^^%}+):\')|!&};>/`+(?=<>++*#-!(>?_!!\"/@>\",:[^\';-]\'[?#{?[{=")]
    [InlineData(64, 999, "\"|@.*:[;:\")--&(`.+>%]|?!;#?\'}\"\\;!@}@]&=&=(<_;]&$>(}|\"!*<$!^^>?%`")]
    [InlineData(128, 0, ">?,]&]~{\\(){\"}\\!/\\\'_?/\\@<[`<]#^{)\\\",!-_|[<$(`._{%*=<;}&~?>/\';&~~#?^\"?*\\}\">.$><%~&}]_\'{?..,|)[\'_)?*^#;<\"`*`#~\'$%$;|}%\':*;!+/@)<|\\")]
    [InlineData(128, 4, "?\\:=!@<{}>*:-].[]}./+?\'=#$+^\")\'[~#@`+$!*:?=>!_<[]>/[[)\\\\_%_`\'].^#>\')[/^={])[=|=,-/\"~=%\\;#@;&.$]@!*\")\"-!<:{#\\!}:->][?~~>|,#&{*\\\"_")]
    [InlineData(128, 7, "-/\'@+\'@|?/{`{$~-._/!>:^}-!|!}<,)$^##?%]`~\'$#;|<:?%*/<`;?<^\'_>!}$#%?(!?:\".`%#{$~=)-&_}~:&:(+\"}&&/^\\\\{%{]!::#,,[-:)*!_?%\"^];<,{`^:")]
    [InlineData(128, 9, "={@}(=||#:`}?%)\"\":`.!@[^:_\'(@_[?`&.%}[~#=&()\">-;!={,^_\\+|~~|,\'`,#$:;+%^%</-..*[/`#*;/>\';&=&;;`_>)[&]%.*~*_#)`&:>+@@\'=)~+=\\-|`][,")]
    [InlineData(128, 123, "\\`>.>@!%&\"`}&{#/:(!|^!,!,\"\'~[%;/,#:#`&=.`~}&,$^-^}<%!]?|%>@$+$\"@@@>$/\\`+/&!(]:*\"{)>@{{?&*\"|^%)=?({^<-#[^]_).]]=]}=\"@-:;:&[/*!,&]")]
    [InlineData(128, 256, "{`<&.(;/\"\'~_;\"^`}<;{?\"({.^@)((<~*=@/#/^_@?:*~*={^@%$\'\"{?\'*=;*);-\\!#&;_?\"])-\\#!:]>:%&$;&;|*^/=:#>*&(/%*/?*[:-*{>\'\'//\'%=)-.}(-%\\&|")]
    [InlineData(128, 721, "-+>{>\"|^^%}+):\')|!&};>/`+(?=<>++*#-!(>?_!!\"/@>\",:[^\';-]\'[?#{?[{=,$%*^\"@&&/}:$\\/\";#?^:<,/[>}{\"+[}:&#|;=:`}\\},~\\]]|{?><(/#+(<=^;]/")]
    [InlineData(128, 999, "\"|@.*:[;:\")--&(`.+>%]|?!;#?\'}\"\\;!@}@]&=&=(<_;]&$>(}|\"!*<$!^^>?%`\'<-+>&-;|[<)*[=@/>`*^!|!..|[!*{;$,\'`^/-]~/!$)`{;;%_!~`,\">+]|(/:<")]
    public void SecurityTokenBuilderUseExtendedSpecialCharactersShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseExtendedSpecialCharacters();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SecurityTokenBuilder should produce the expected result")]
    [InlineData(1, 0, "&")]
    [InlineData(1, 4, "{")]
    [InlineData(1, 7, "K")]
    [InlineData(1, 9, "O")]
    [InlineData(1, 123, "|")]
    [InlineData(1, 256, "R")]
    [InlineData(1, 721, "L")]
    [InlineData(1, 999, "7")]
    [InlineData(2, 0, "&{")]
    [InlineData(2, 4, "{\\")]
    [InlineData(2, 7, "K;")]
    [InlineData(2, 9, "OR")]
    [InlineData(2, 123, "|>")]
    [InlineData(2, 256, "R.")]
    [InlineData(2, 721, "LI")]
    [InlineData(2, 999, "7?")]
    [InlineData(4, 0, "&{_0")]
    [InlineData(4, 4, "{\\1M")]
    [InlineData(4, 7, "K;!e")]
    [InlineData(4, 9, "ORfT")]
    [InlineData(4, 123, "|>*{")]
    [InlineData(4, 256, "R.%s")]
    [InlineData(4, 721, "LI*Q")]
    [InlineData(4, 999, "7?f-")]
    [InlineData(8, 0, "&{_0t0>P")]
    [InlineData(8, 4, "{\\1Mbf$Q")]
    [InlineData(8, 7, "K;!eI@d/")]
    [InlineData(8, 9, "ORfTzN?/")]
    [InlineData(8, 123, "|>*{*ebo")]
    [InlineData(8, 256, "R.%s+A5\"")]
    [InlineData(8, 721, "LI*Q&8/o")]
    [InlineData(8, 999, "7?f-w2W6")]
    [InlineData(16, 0, "&{_0t0>P`zBR7S|c")]
    [InlineData(16, 4, "{\\1Mbf$QS(w3JZ+X")]
    [InlineData(16, 7, "K;!eI@d/]:Q.Pk\"L")]
    [InlineData(16, 9, "ORfTzN?/i3,U}oA9")]
    [InlineData(16, 123, "|>*{*ebos7>UrRh]")]
    [InlineData(16, 256, "R.%s+A5\"9@<F69q>")]
    [InlineData(16, 721, "LI*Q&8/oqnSIA1!D")]
    [InlineData(16, 999, "7?f-w2W619ALLtx.")]
    [InlineData(32, 0, @"&{_0t0>P`zBR7S|c;\@D{]\d$X.#ZhrQ")]
    [InlineData(32, 4, "{\\1Mbf$QS(w3JZ+XZS+;G}!OijJp9C!V")]
    [InlineData(32, 7, "K;!eI@d/]:Q.Pk\"L=F;c(1qSKc/cT^_B")]
    [InlineData(32, 9, "ORfTzN?/i3,U}oA982,=bfWq2D@ydEV}")]
    [InlineData(32, 123, "|>*{*ebos7>UrRh]1xa?pc+a(7#<Wn4:")]
    [InlineData(32, 256, "R.%s+A5\"9@<F69q>S^5P[9yR-rdCyx$\'")]
    [InlineData(32, 721, "LI*Q&8/oqnSIA1!D?bsT5*]>Iy}N$(II")]
    [InlineData(32, 999, "7?f-w2W619ALLtx.=J^m0?}c5g[@T7\\4")]
    [InlineData(64, 0, "&{_0t0>P`zBR7S|c;\\@D{]\\d$X.#ZhrQB|8)cJG~V^lz>-FQnuM^6Ts\"}*:@6u<<")]
    [InlineData(64, 4, "{\\1Mbf$QS(w3JZ+XZS+;G}!OijJp9C!V>id?Gkcv2}M&bD$WY(\"XXC\\|EmE>#Y-p")]
    [InlineData(64, 7, "K;!eI@d/]:Q.Pk\"L=F;c(1qSKc/cT^_Bkpig}oY.\"!lg4~^1[nu\"%>4{$r!F&bSl")]
    [InlineData(64, 9, "ORfTzN?/i3,U}oA982,=bfWq2D@ydEV},u+oTW\'gMuzB9^L4cMQ_pE\\I?\"</(@,)")]
    [InlineData(64, 123, "|>*{*ebos7>UrRh]1xa?pc+a(7#<Wn4:)g3i,tM-.\'Us)krKpS$obZ}~m*fjIl7f")]
    [InlineData(64, 256, "R.%s+A5\"9@<F69q>S^5P[9yR-rdCyx$\'wMd:i]rDd[3w\"wORofoj!8Q{@wM4wC5L")]
    [InlineData(64, 721, "LI*Q&8/oqnSIA1!D?bsT5*]>Iy}N$(IIvgLaz*{Gbc7;e*7(2Wq@5J09X}iR{XRO")]
    [InlineData(64, 999, "7?f-w2W619ALLtx.=J^m0?}c5g[@T7\\4bdSfZsNsOy$E50uk*yS`9bv#kcqq*[n.")]
    [InlineData(128, 0, "&{_0t0>P`zBR7S|c;\\@D{]\\d$X.#ZhrQB|8)cJG~V^lz>-FQnuM^6Ts\"}*:@6u<<i[p8}u\\T9^{j&^l>rSYE@S[{-)?BX!FA}urg6$7.u.g<@iok5/Un@1v6aH]fA%~|")]
    [InlineData(128, 4, "{\\1Mbf$QS(w3JZ+XZS+;G}!OijJp9C!V>id?Gkcv2}M&bD$WY(\"XXC\\|EmE>#Y-ph&#BW]rPQ0BWN?M_L:7<On|4ie5t+kZecw8A7Ma$1Pg`bS1J^ZW}<<*?(hsRv`9F")]
    [InlineData(128, 7, "K;!eI@d/]:Q.Pk\"L=F;c(1qSKc/cT^_Bkpig}oY.\"!lg4~^1[nu\"%>4{$r!F&bSlho}Ac{19-.niPk\"ODKsFU<3s1zI8Ttt:r\\|PnPYb11h__VJ3CwcF}n7r06^(P.p3")]
    [InlineData(128, 9, "ORfTzN?/i3,U}oA982,=bfWq2D@ydEV},u+oTW\'gMuzB9^L4cMQ_pE\\I?\"</(@,)hj24Impm#:K--uX:,gw6\"*!3uMt46.D*BVt0n-v<xEhC?s2&Hdd@PC\'GO|K/,0W)")]
    [InlineData(128, 123, "|>*{*ebos7>UrRh]1xa?pc+a(7#<Wn4:)g3i,tM-.\'Us)krKpS$obZ}~m*fjIl7fdf*k;|.H]tazX3x8QC*ePP]tx7~qnCO{yPo$JgWqZFC=YZOYUN8fJ353uX;wb(uY")]
    [InlineData(128, 256, "R.%s+A5\"9@<F69q>S^5P[9yR-rdCyx$\'wMd:i]rDd[3w\"wORofoj!8Q{@wM4wC5L`aiu6D}7XBL\\gb3Z(1osj4u4/ur]O2g&usz;ou][wX1JwR(!9::@oNBJ-UyLm`s/")]
    [InlineData(128, 721, "LI*Q&8/oqnSIA1!D?bsT5*]>Iy}N$(IIvgLaz*{Gbc7;e*7(2Wq@5J09X}iR{XRO+jmuq9dss:S3j|;66g]r1#):V*TP9GXT1ti?4N2.S`S)<`0Z~R]*#A;iGA^Op4Z]")]
    [InlineData(128, 999, "7?f-w2W619ALLtx.=J^m0?}c5g[@T7\\4bdSfZsNsOy$E50uk*yS`9bv#kcqq*[n.!#JI&sJ4/X$BxVMf]&.vpc?a{+?XbxQ3l)!.q:JZ\';blD.P55lEb>,_7*JZ/x:2$")]
    public void SecurityTokenBuilderShouldProduceTheExpectedResult(int length, int seed, string expected)
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, seed)
            .UseAlphaNumericCharacters()
            .UseExtendedSpecialCharacters();

        // When
        string actual = builder.ToSecurityToken().ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "SecurityTokenBuilder.ToSecurityToken should throw InvalidOperationException when no character set is specified")]
    public void SecurityTokenBuilderToSecurityTokenShouldThrowWhenNoCharacterSetSpecified()
    {
        // Given
        SecurityTokenBuilder builder = SecurityTokenBuilder.CreatePseudoRandom(32, 0);

        // When / Then
        Assert.Throws<InvalidOperationException>(() => builder.ToSecurityToken());
    }

    [Fact(DisplayName = "SecurityTokenBuilder.CreatePseudoRandom should throw ArgumentOutOfRangeException when the length is negative")]
    public void SecurityTokenBuilderShouldThrowWhenLengthIsNegative()
    {
        // When / Then
        Assert.Throws<ArgumentOutOfRangeException>(() => SecurityTokenBuilder.CreatePseudoRandom(-1, 0));
    }

    [Fact(DisplayName = "SecurityTokenBuilder.ToSecurityToken should produce a token of the requested length when the length exceeds the stack-allocation threshold")]
    public void SecurityTokenBuilderToSecurityTokenShouldProduceExpectedLengthForLargeLength()
    {
        // Given
        const int length = 100_000;
        SecurityTokenBuilder builder = SecurityTokenBuilder
            .CreatePseudoRandom(length, 0)
            .UseAlphaNumericCharacters();

        // When
        SecurityToken token = builder.ToSecurityToken();

        // Then
        Assert.Equal(length, token.Length);
    }
}
