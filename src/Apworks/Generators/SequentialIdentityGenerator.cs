﻿// ==================================================================================================================                                                                                          
//        ,::i                                                           BBB                
//       BBBBBi                                                         EBBB                
//      MBBNBBU                                                         BBB,                
//     BBB. BBB     BBB,BBBBM   BBB   UBBB   MBB,  LBBBBBO,   :BBG,BBB :BBB  .BBBU  kBBBBBF 
//    BBB,  BBB    7BBBBS2BBBO  BBB  iBBBB  YBBJ :BBBMYNBBB:  FBBBBBB: OBB: 5BBB,  BBBi ,M, 
//   MBBY   BBB.   8BBB   :BBB  BBB .BBUBB  BB1  BBBi   kBBB  BBBM     BBBjBBBr    BBB1     
//  BBBBBBBBBBBu   BBB    FBBP  MBM BB. BB BBM  7BBB    MBBY .BBB     7BBGkBB1      JBBBBi  
// PBBBFE0GkBBBB  7BBX   uBBB   MBBMBu .BBOBB   rBBB   kBBB  ZBBq     BBB: BBBJ   .   iBBB  
//BBBB      iBBB  BBBBBBBBBE    EBBBB  ,BBBB     MBBBBBBBM   BBB,    iBBB  .BBB2 :BBBBBBB7  
//vr7        777  BBBu8O5:      .77r    Lr7       .7EZk;     L77     .Y7r   irLY  JNMMF:    
//               LBBj
//
// Apworks Application Development Framework
// Copyright (C) 2010-2015 by daxnet.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ==================================================================================================================

using System;
using System.Runtime.CompilerServices;

namespace Apworks.Generators
{
    /// <summary>
    /// Represents the sequential identity generator.
    /// </summary>
    public class SequentialIdentityGenerator : IIdentityGenerator, ISequenceGenerator
    {
        #region Private Constants
        //private const int Year = 2010;
        //private const int Month = 11;
        //private const int Day = 23;
        //private const int Hour = 0;
        //private const int Minute = 0;
        //private const int Second = 0;
        //private const int Millisecond = 0;
        #endregion

        #region Private Fields
        //private UInt16 counter = 0;
        //private readonly object syncObj = new object();
        //private readonly DateTime startingPoint = new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond).ToUniversalTime();
        #endregion

        #region Private Methods
        private static Guid NewSequentialGuid()
        {
            byte[] uid = Guid.NewGuid().ToByteArray();
            byte[] binDate = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            byte[] secuentialGuid = new byte[uid.Length];

            secuentialGuid[0] = uid[0];
            secuentialGuid[1] = uid[1];
            secuentialGuid[2] = uid[2];
            secuentialGuid[3] = uid[3];
            secuentialGuid[4] = uid[4];
            secuentialGuid[5] = uid[5];
            secuentialGuid[6] = uid[6];
            // set the first part of the 8th byte to '1100' so     
            // later we'll be able to validate it was generated by us   

            secuentialGuid[7] = (byte)(0xc0 | (0xf & uid[7]));

            // the last 8 bytes are sequential,    
            // it minimizes index fragmentation   
            // to a degree as long as there are not a large    
            // number of Secuential-Guids generated per millisecond  

            secuentialGuid[9] = binDate[0];
            secuentialGuid[8] = binDate[1];
            secuentialGuid[15] = binDate[2];
            secuentialGuid[14] = binDate[3];
            secuentialGuid[13] = binDate[4];
            secuentialGuid[12] = binDate[5];
            secuentialGuid[11] = binDate[6];
            secuentialGuid[10] = binDate[7];

            return new Guid(secuentialGuid);
        }
        #endregion

        #region IIdentityGenerator Members
        /// <summary>
        /// Generates the identity.
        /// </summary>
        /// <returns>The generated identity instance.</returns>
        public object Generate()
        {
            return NewSequentialGuid();
        }

        #endregion

        #region ISequence Members
        /// <summary>
        /// Gets the next value of the sequence.
        /// </summary>
        public object Next
        {
            get { return Generate(); }
        }

        #endregion
    }
}
