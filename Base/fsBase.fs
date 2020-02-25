namespace Fs.Base

type FsBase() = 
    member this.getName
        with
            get() : string = "something"
        and
            set(value : string) = ()

    member this.getAge = 20.5
    // member this.getInterests() = [ "F#"; "C#"; "Vb"; "Java"; "Scala"; "Python"; "Javascript" ]
    member this.getInterests() = [ "F#"; "C#"; "Vb"; "Java"; "Scala"; "Python"; "Javascript" ]
    member this.Add x y = x + y


