using System;
using System.Collections.Generic;

// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable UseStringInterpolation

namespace PvMake.Lib
{
    public sealed class Dir2Model
    {
        private readonly IDictionary<string, string[]> _d2M;
        private readonly IDictionary<string, string> _m2D;

        public Dir2Model(
            IDictionary<string, string[]> d2M,
            IDictionary<string, string> m2D
        )
        {
            _d2M = d2M;
            _m2D = m2D;
        }

        public string FindDir(string term)
        {
            string found;
            if (_m2D.TryGetValue(term, out found))
            {
                return found;
            }
            throw new InvalidOperationException(string.Format("Unknown model '{0}'!", term));
        }
    }
}