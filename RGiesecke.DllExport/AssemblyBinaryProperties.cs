﻿//# Author of original code ([Decompiled] MIT-License): Copyright (c) 2009-2015  Robert Giesecke
//# Use Readme & LICENSE files for details.

//# Modifications: Copyright (c) 2016-2018  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
//$ Distributed under the MIT License (MIT)

using Mono.Cecil;

namespace RGiesecke.DllExport
{
    public sealed class AssemblyBinaryProperties
    {
        public static readonly AssemblyBinaryProperties EmptyNotScanned = new AssemblyBinaryProperties((ModuleAttributes)0, TargetArchitecture.I386, false, (string)null, (string)null, false);
        private readonly bool _BinaryWasScanned;
        private readonly bool _IsSigned;
        private readonly string _KeyContainer;
        private readonly string _KeyFileName;
        private readonly TargetArchitecture _MachineKind;
        private readonly ModuleAttributes _PeKind;

        public string KeyFileName
        {
            get {
                return this._KeyFileName;
            }
        }

        public string KeyContainer
        {
            get {
                return this._KeyContainer;
            }
        }

        public bool IsIlOnly
        {
            get {
                return this.PeKind.Contains(ModuleAttributes.ILOnly);
            }
        }

        public CpuPlatform CpuPlatform
        {
            get {
                if(this.PeKind.Contains(ModuleAttributes.ILOnly))
                {
                    return !this.MachineKind.Contains(TargetArchitecture.IA64) ? CpuPlatform.X64 : CpuPlatform.Itanium;
                }
                return !this.PeKind.Contains(ModuleAttributes.Required32Bit) ? CpuPlatform.AnyCpu : CpuPlatform.X86;
            }
        }

        internal ModuleAttributes PeKind
        {
            get {
                return this._PeKind;
            }
        }

        internal TargetArchitecture MachineKind
        {
            get {
                return this._MachineKind;
            }
        }

        public bool IsSigned
        {
            get {
                return this._IsSigned;
            }
        }

        public bool BinaryWasScanned
        {
            get {
                return this._BinaryWasScanned;
            }
        }

        internal AssemblyBinaryProperties(ModuleAttributes peKind, TargetArchitecture machineKind, bool isSigned, string keyFileName, string keyContainer, bool binaryWasScanned)
        {
            this._PeKind = peKind;
            this._MachineKind = machineKind;
            this._IsSigned = isSigned;
            this._KeyFileName = keyFileName;
            this._KeyContainer = keyContainer;
            this._BinaryWasScanned = binaryWasScanned;
        }

        internal AssemblyBinaryProperties(ModuleAttributes peKind, TargetArchitecture machineKind, bool isSigned, string keyFileName, string keyContainer)
        : this(peKind, machineKind, isSigned, keyFileName, keyContainer, true)
        {
        }

        public static AssemblyBinaryProperties GetEmpty()
        {
            return AssemblyBinaryProperties.EmptyNotScanned;
        }
    }
}
