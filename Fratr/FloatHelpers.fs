namespace Fratr

module FloatHelpers = 
    let ZeroThreshold = 0.0000000001

    let IsNearZero(f: float) =
        abs f <= ZeroThreshold

    let IsPositive(f: float) =
        f > ZeroThreshold

    let IsNegative(f: float) =
        f < -ZeroThreshold

    let (|NotNearZero|_|) (f: float) =
        if IsNearZero f then None
        else Some f

    let (|Negative|NearZero|Positive|) (f: float) =
        if f |> IsNegative then Negative
        elif f |> IsPositive then Positive
        else NearZero
