# api-client-shared-dotnet

## Hvordan utvikle på dette prosjektet?
Bruk Rider, som er C#-varianten av Intellij IDEA.

Fjern `AssemblyOriginatorKeyFile` og `SignAssembly` for å deaktivere strong-named assemblies under utvikling. 

Har du tilgang til signingkey (digipost-utviklere) kan du evt dekryptere `signingkey.snk.enc` først. 
Man kan verifisere at DLL-en er strong-named ved å benytte `sn -v <path-to-dll>`.
 

## Hvordan deploye?
Releasing er gjort via tagging med [Semver](http://semver.org) versjons schema. For en beta-release, bruk `-beta` som versjon suffix i taggen.


## Hvordan legge til ny data-type

1. Copy-paste https://raw.githubusercontent.com/digipost/digipost-data-types/main/datatypes.xsd til `datatypes.xsd`
2. Legg til eksempel-xsd av nye datatyper i `datatypes-examples.xml` (kan også dupliseres fra repoet over)
3. Kjør GenerationUtil
    1. CLI:
        * $ cd Digipost.Api.Client.DataTypes.Utils/obj/Debug/netcoreapp3.1
        * $ dotnet run --project ../../../
4. Kjør tester
