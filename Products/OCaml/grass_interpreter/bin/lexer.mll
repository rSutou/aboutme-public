{
open Parser
}


rule token = parse
| "w"       { SW }
| "W"       { LW }
| "v"       { SV }
| eof       { EOF }

{

}