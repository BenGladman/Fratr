namespace Fratr

open Light
open SceneObject
open ViewPort

module Scene =
    type Scene = {
        ViewPort: ViewPort
        ResX: int
        ResY: int
        Objs: SceneObject seq
        Lights: Light seq
        }