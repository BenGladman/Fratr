namespace Fratr

open Color

module Material = 
    type Material = {
        DiffuseColor: Color
        SpecularColor: Color
        TransparentColor: Color
        Shininess: int
        Reflectivity: float
        RefractiveIndex: float
        }

    let WhiteMatt: Material = {
        DiffuseColor = White
        SpecularColor = White
        TransparentColor = LightGrey
        Shininess = 0
        Reflectivity = 0.0
        RefractiveIndex = 0.0
        }

    let WhiteShiny = { WhiteMatt with Shininess = 20 }
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
    let Glass = { BlackShiny with RefractiveIndex = 1.5 }
    let RedGlass = { Glass with TransparentColor = Red }
    let GreenGlass = { Glass with TransparentColor = Green }
    let BlueGlass = { Glass with TransparentColor = Blue }