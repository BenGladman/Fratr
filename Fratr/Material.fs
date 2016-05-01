namespace Fratr

open Color

module Material = 
    type Material = {
        DiffuseColor: Color
        SpecularColor: Color
        Shininess: float
        Reflectivity: float
        }

    let WhiteMatt: Material = {
        DiffuseColor = White
        SpecularColor = White
        Shininess = 0.1
        Reflectivity = 0.0
        }

    let WhiteShiny = { WhiteMatt with Shininess = 0.9 }
    let WhiteReflective = { WhiteShiny with Reflectivity = 0.6 }
    let BlackMatt = { WhiteMatt with DiffuseColor = Black }
    let BlackShiny = { WhiteShiny with DiffuseColor = Black }
    let BlackReflective = { WhiteReflective with DiffuseColor = Black }
    let RedMatt = { WhiteMatt with DiffuseColor = Red }
    let RedShiny = { WhiteShiny with DiffuseColor = Red }
    let RedReflective = { WhiteReflective with DiffuseColor = Red }
    let GreenMatt = { WhiteMatt with DiffuseColor = Green }
    let GreenShiny = { WhiteShiny with DiffuseColor = Green }
    let GreenReflective = { WhiteReflective with DiffuseColor = Green }
    let BlueMatt = { WhiteMatt with DiffuseColor = Blue }
    let BlueShiny = { WhiteShiny with DiffuseColor = Blue }
    let BlueReflective = { WhiteReflective with DiffuseColor = Blue }