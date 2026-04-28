using System.Collections.Generic;

namespace PvMake.Models
{
    public record Dir2Model(
        IDictionary<string, string[]> D2M,
        IDictionary<string, string> M2D
    );
}