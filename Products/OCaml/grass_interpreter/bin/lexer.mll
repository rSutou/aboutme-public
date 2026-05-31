{
open Parser
}

let digit = ['0'-'9']
let space = ' ' | '\t' | '\r' | '\n'
let lowercase = ['a'-'u'] | ['x' - 'z']
let uppercase = ['A'-'V'] | ['X' - 'Z']
let skip = digit | space | lowercase | uppercase | '_'



rule token = parse
| "w"       { SW }
| "W"       { LW }
| "v"       { SV }
| eof       { EOF }
| skip+    { token lexbuf }

{

}