namespace Fratr

open Color

module Material = 
    type Material = {
        DiffuseColor: Color
        SpecularColor: Color
        Shininess: float
        }

    let WhiteMatt: Material = {
        DiffuseColor = White
        SpecularColor = White
        Shininess = 0.1
        }

    let WhiteShiny = { WhiteMatt with Shininess = 0.9 }
    let RedMatt = { WhiteMatt with DiffuseColor = Red }
    let RedShiny = { WhiteShiny with DiffuseColor = Red }
    let GreenMatt = { WhiteMatt with DiffuseColor = Green }
    let GreenShiny = { WhiteShiny with DiffuseColor = Green }
    let BlueMatt = { WhiteMatt with DiffuseColor = Blue }
    let BlueShiny = { WhiteShiny with DiffuseColor = Blue }