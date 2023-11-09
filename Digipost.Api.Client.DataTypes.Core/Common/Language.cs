using System;

namespace Digipost.Api.Client.DataTypes.Core.Common
{
    public enum Language
    {
        NB, NN, EN
    }

    internal static class LanguageToDto
    {
        internal static Internal.Language ToDto(this Language language)
        {
            switch (language)
            {
                case Language.NB: return Internal.Language.Nb;
                case Language.NN: return Internal.Language.Nn;
                case Language.EN: return Internal.Language.En;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

}
