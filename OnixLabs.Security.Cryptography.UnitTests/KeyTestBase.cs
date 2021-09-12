// Copyright 2020-2021 ONIXLabs
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

namespace OnixLabs.Security.Cryptography.UnitTests
{
    public abstract class KeyTestBase
    {
        protected static byte[] GenerateRandomData(int length = 1024)
        {
            byte[] result = new byte[length];
            Random random = new(Guid.NewGuid().GetHashCode());

            random.NextBytes(result);

            return result;
        }
    }
}
